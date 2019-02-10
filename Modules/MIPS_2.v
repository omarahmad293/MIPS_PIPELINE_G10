module MIPS(CLK);
input CLK;

//-------------------------------------------Stage 1--------------------------------------
reg[31:0] PC=0;
reg[31:0] IFID_INSTR;
reg[31:0] IFID_PC;

wire[31:0] IFID_PC_WIRE;
wire[31:0] IFID_INSTR_WIRE;
wire [31:0] PC_WIRE1, PC_WIRE2;

//-------------------------------------------Stage 2--------------------------------------
reg IDEX_RegDst, IDEX_Jump, IDEX_Branch, IDEX_MemRead, IDEX_MemtoReg, IDEX_MemWrite, IDEX_ALUSrc, IDEX_RegWrite, IDEX_JAL;
reg[1:0] IDEX_ALUOp;
reg [31:0] IDEX_READ_DATA_1;
reg [31:0] IDEX_READ_DATA_2;
reg [31:0] IDEX_IMMEDIATE;
reg [31:0] IDEX_PC;
reg [4:0] IDEX_Rt;
reg [4:0] IDEX_Rd;
reg [31:0] IDEX_JUMP_ADDRESS;

wire [31:0] READ_DATA_1_WIRE;
wire [31:0] READ_DATA_2_WIRE;
wire [31:0] IMMEDIATE_WIRE;

wire [27:0] JUMP_ADDRESS_28;

wire IDEX_RegDst_WIRE, IDEX_Jump_WIRE, IDEX_Branch_WIRE, IDEX_MemRead_WIRE, IDEX_MemtoReg_WIRE, IDEX_MemWrite_WIRE, IDEX_ALUSrc_WIRE, IDEX_RegWrite_WIRE, IDEX_JAL_WIRE;
wire[1:0]  IDEX_ALUOp_WIRE;

//-------------------------------------------Stage 3--------------------------------------
wire [2:0] ALU_WIRE;
wire [31:0] MUX_ALU_WIRE;
wire ZERO_WIRE;
wire [31:0] ALU_RESULT_WIRE;
wire [31:0] SL_WIRE;
wire [31:0] BRANCH_WIRE;
wire [4:0] WRITE_REG_WIRE;

wire PC_SRC;

reg EXMEM_Branch, EXMEM_MemRead, EXMEM_MemtoReg, EXMEM_MemWrite, EXMEM_RegWrite, EXMEM_JAL;
reg EXMEM_ZERO;
reg [31:0] ALU_RESULT, WRITE_DATA, BRANCH;
reg [4:0] WRITE_REG;
reg [31:0]EXMEM_PC;
reg [31:0] EXMEM_JUMP_ADDRESS;

//-------------------------------------------Stage 4--------------------------------------
wire [31:0] MEM_OUT_WIRE;

reg MEMWB_RegWrite, MEMWB_MemtoReg, MEMWB_JAL;

reg [31:0] MEMWB_ALU_RESULT, MEM_OUT;
reg [4:0] MEMWB_WRITE_REG;
reg [31:0] MEMWB_PC;

//-------------------------------------------Stage 5--------------------------------------

wire [31:0] WB_DATA_WIRE;
wire [4:0] JAL_ADDRESS;
wire [31:0] JAL_DATA;


//-------------------------------------------Stage 1--------------------------------------

MUX BRANCH_MUX(IFID_PC_WIRE, BRANCH, PC_SRC, PC_WIRE1);
MUX JUMP_MUX(PC_WIRE1, IDEX_JUMP_ADDRESS, IDEX_Jump, PC_WIRE2);
Adder NEXT_INSTR(PC, 4, IFID_PC_WIRE);
prgmem PROGRAM_MEMORY(IFID_INSTR_WIRE,PC);

//-------------------------------------------Stage 2--------------------------------------
Registers REGISTER(IFID_INSTR[25:21], IFID_INSTR[20:16], JAL_ADDRESS, JAL_DATA, MEMWB_RegWrite, READ_DATA_1_WIRE, READ_DATA_2_WIRE, CLK);
ControlUnit CONTROL_UNIT(IFID_INSTR[31:26], IDEX_RegDst_WIRE, IDEX_Jump_WIRE, IDEX_Branch_WIRE, IDEX_MemRead_WIRE, IDEX_MemtoReg_WIRE, IDEX_ALUOp_WIRE, IDEX_MemWrite_WIRE, IDEX_ALUSrc_WIRE, IDEX_RegWrite_WIRE, IDEX_JAL_WIRE);
Signextend SIGN_EXTEND(IFID_INSTR[15:0], IMMEDIATE_WIRE);

Shiftleft2_28 SHIFT28(IFID_INSTR[25:0], JUMP_ADDRESS_28);

//-------------------------------------------Stage 3--------------------------------------
MUX  M_ALU(IDEX_READ_DATA_2, IDEX_IMMEDIATE, IDEX_ALUSrc, MUX_ALU_WIRE);
MUX5 M_IMM(IDEX_Rt, IDEX_Rd, IDEX_RegDst, WRITE_REG_WIRE);
alu ALU(IDEX_READ_DATA_1, MUX_ALU_WIRE, ALU_WIRE,ALU_RESULT_WIRE, ZERO_WIRE);
ALU_Control ALU_CONTROL(ALU_WIRE, IDEX_ALUOp, IDEX_IMMEDIATE[5:0]);
Shiftleft2_32 SL2(IDEX_IMMEDIATE, SL_WIRE);
Adder OFFSET(IDEX_PC, SL_WIRE, BRANCH_WIRE);

//-------------------------------------------Stage 4--------------------------------------
dataMem DATA_MEMORY(MEM_OUT_WIRE, WRITE_DATA, ALU_RESULT, EXMEM_MemWrite, EXMEM_MemRead, CLK);
and BRANCH_AND(PC_SRC, EXMEM_Branch, EXMEM_ZERO);

//-------------------------------------------Stage 5--------------------------------------
MUX REG_RES(MEMWB_ALU_RESULT, MEM_OUT, MEMWB_MemtoReg, WB_DATA_WIRE);
MUX5 JAL_ADDRESS_MUX(MEMWB_WRITE_REG, 5'b11111, MEMWB_JAL, JAL_ADDRESS);
MUX JAL_DATA_MUX (WB_DATA_WIRE, MEMWB_PC, MEMWB_JAL, JAL_DATA);


always @(posedge CLK)
begin
//STAGE 1
	IFID_PC <= IFID_PC_WIRE;
	//PC <= IFID_PC_WIRE;
	PC <= PC_WIRE2;
	IFID_INSTR <= IFID_INSTR_WIRE;

//STAGE 2
	IDEX_RegDst<=IDEX_RegDst_WIRE; 
	IDEX_Jump<=IDEX_Jump_WIRE;
	IDEX_Branch<=IDEX_Branch_WIRE;
	IDEX_MemRead<=IDEX_MemRead_WIRE; 
	IDEX_MemtoReg <=IDEX_MemtoReg_WIRE;
	IDEX_MemWrite<=IDEX_MemWrite_WIRE;
	IDEX_ALUSrc<=IDEX_ALUSrc_WIRE;
	IDEX_RegWrite<=IDEX_RegWrite_WIRE;
	IDEX_JAL<=IDEX_JAL_WIRE;
	IDEX_ALUOp<=IDEX_ALUOp_WIRE;

	IDEX_READ_DATA_1<=READ_DATA_1_WIRE;
	IDEX_READ_DATA_2<=READ_DATA_2_WIRE;
	IDEX_IMMEDIATE<=IMMEDIATE_WIRE;
	IDEX_PC<=IFID_PC;
	IDEX_Rt<=IFID_INSTR[20:16];
	IDEX_Rd<=IFID_INSTR[15:11];

	IDEX_JUMP_ADDRESS <= {IFID_PC_WIRE[31:28], JUMP_ADDRESS_28};

//STAGE 3
	//EXMEM_Jump <= IDEX_Jump;
	EXMEM_Branch <= IDEX_Branch;
	EXMEM_MemRead <= IDEX_MemRead;
	EXMEM_MemtoReg <= IDEX_MemtoReg; 
	EXMEM_MemWrite <= IDEX_MemWrite;
	EXMEM_RegWrite <= IDEX_RegWrite;
	EXMEM_JAL <= IDEX_JAL;

	EXMEM_ZERO <= ZERO_WIRE;
	ALU_RESULT <= ALU_RESULT_WIRE;
	WRITE_DATA <= IDEX_READ_DATA_2;
	BRANCH <= BRANCH_WIRE;
	WRITE_REG <= WRITE_REG_WIRE;

	EXMEM_PC <= IDEX_PC;

	//EXMEM_JUMP_ADDRESS <= IDEX_JUMP_ADDRESS;

//STAGE 4
	MEMWB_RegWrite <=EXMEM_RegWrite;
	MEMWB_MemtoReg <=EXMEM_MemtoReg;

	MEMWB_ALU_RESULT <=ALU_RESULT;
	MEM_OUT <=MEM_OUT_WIRE;
	MEMWB_WRITE_REG <=WRITE_REG;

	MEMWB_JAL <= EXMEM_JAL;
	MEMWB_PC <= EXMEM_PC;
end


endmodule

