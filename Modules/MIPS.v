module mips(CLK);
input wire CLK;

//Stage 1
reg [31:0] pc = 0;
integer four = 4;
reg [31:0] adder1Output;
//Adder adderOne(four, pc, adder1Output);
reg [133:0] EXMEM;
reg [97:0] MEMWB;

wire branchSel;
assign branchSel = EXMEM[131] & EXMEM[96];

wire [31:0] muxBranchOut;
MUX muxBranch(adder1Output, EXMEM[128:97], branchSel, muxBranchOut);

reg [63:0] IFID;
wire [31:0] inst;
prgmem program_memory(inst,pc);

//Stage 2
//Registers(Read_register_1, Read_register_2, Write_register, Write_data, RegWrite, Read_data_1, Read_data_2, Clock);
//Write_register -> control_unit
//Write_data -> mem to reg mux
//RegWrite -> dest reg mux / jal mux
//Read_data_1
wire [31:0] R1;
wire [31:0] R2;
wire [31:0] dataToWrite;

Registers register_file(IFID[25:21], IFID[20:16], MEMWB[31:0], dataToWrite, MEMWB[97], R1, R2, CLK);
//There is an omitted control

reg [147:0] IDIX;

wire [31:0] SignExtended;
Signextend extend(IFID[15:0],SignExtended);

wire RegDst, Jump, Branch, MemRead, MemtoReg, MemWrite, ALUSrc, RegWrite, JAL;
wire [1:0] ALUOp;

ControlUnit control_unit(CLK, IFID[31:26], RegDst, Jump, Branch, MemRead, MemtoReg, ALUOp, MemWrite, ALUSrc, RegWrite, JAL);

//Stage 3
wire [31:0] muxStage3Output;

MUX muxStage3(IDIX[73:42], IDIX[41:10], IDIX[138], muxStage3Output);
//Sel -> control_unit
//(ALU_control, control, funct, CLK);
//[2:0] ALU_control
wire [2:0] aluControl;
ALU_Control alu_control(aluControl, IDIX[140:139],IDIX[15:10], CLK);
//control -> control_unit
wire[31:0] alu_result;
wire zero_flag;
ALU alu(IDIX[105:74], muxStage3Output, aluControl, alu_result,zero_flag); 

wire [31:0] shiftedImmediate;
Shiftleft2 shifter(IDIX[41:10], shiftedImmediate);

wire [31:0] adder2Out;
Adder adder2(IDIX[137:106], shiftedImmediate, adder2Out);

//wire [31:0] muxStage3Out2; 

//MUX muxStage3No2(IDIX[137:106],alu_result, zero_flag /*& Branch*/, maxStage3Out2);
//Branch -> control_unit
wire [31:0] muxRegDstOut;
MUX muxRegDst(IDIX[9:5],IDIX[4:0], IDIX[141], muxRegDstOut);

integer regRa = 31;
wire [31:0] muxJALOut;
MUX muxJAL(muxRegDstOut, regRa, IDIX[142], muxJALOut);

//stage 4
wire[31:0] readData;
dataMem DataMemory(readData, EXMEM[63:32], EXMEM[95:64], EXMEM[129], EXMEM[130], CLK);
//memWrite -> control_unit
//memRead -> control_unit

//stage 5

MUX muxWB(MEMWB[63:32], MEMWB[95:64], MEMWB[96], dataToWrite);

always @ (posedge CLK)
begin
//Stage 5

//Stage 4
MEMWB <= {EXMEM[133], EXMEM[132], readData, EXMEM[95:64], EXMEM[31:0]};
//Stage 3
EXMEM <= {IDIX[147:143], adder2Out, zero_flag, alu_result, IDIX[73:42], muxJALOut};
//pc <= muxStage3Out2;
//Stage 2
IDIX <= {RegWrite, MemtoReg, Branch, MemRead, MemWrite, JAL, RegDst, ALUOp, ALUSrc, IFID[63:32], R1, R2, SignExtended, IFID[20:16], IFID[15:11]};
//Stage 1
adder1Output = pc + 4;
pc <= muxBranchOut;
IFID <= {adder1Output[31:0], inst[31:0]};
end

endmodule
