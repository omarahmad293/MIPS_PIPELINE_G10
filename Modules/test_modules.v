module testmips();

reg CLK ;
initial
begin CLK=0;
$dumpvars(0, testmips);
end

always
begin
	#5
	CLK = ~CLK;
end


MIPS M1(CLK);

endmodule


module tb_ALU();
//Inputs
 reg[31:0] Read_data_1,Data_2;
 reg[2:0] ALU_control;

//Outputs
 wire[31:0] Result;
 wire Zero_flag;

alu ALU1(Read_data_1,Data_2,ALU_control,Result,Zero_flag);

initial
begin 

$display("Data1   .   Data2   .  ALUControl   .   Result   .   Zero_flag");
$monitor("%d   .   %d   .   %d   .    %d   .	%d",Read_data_1,Data_2,ALU_control,Result,Zero_flag);

#2
//000 AND 001
Read_data_1 = 0;
Data_2 = 1;
ALU_control = 0;

#30
//000 OR 101
Read_data_1 = 3'b000;
Data_2 = 3'b101;
ALU_control = 1;

#30
//011 + 100
Read_data_1 = 3'b011;
Data_2 = 3'b100;
ALU_control = 2;

#30
//111 - 100
Read_data_1 = 3'b111;
Data_2 = 3'b100;
ALU_control = 6;

#30
//100 < 111
Read_data_1 = 3'b100;
Data_2 = 3'b111;
ALU_control = 7;

end 
endmodule 

module tb_ALU_Control();


reg [1:0]control;
reg [5:0]funct;
reg CLK;

wire [2:0] ALU_control;

ALU_Control a1(ALU_control, control, funct, CLK);

initial
begin
$display("                  Time  | Control | Function | Output");
$monitor($time,"    |   %b    |   %b |   %b", control, funct, ALU_control);

CLK = 0;
#2
control <= 2'b00;
funct <= 6'b110100;

#10
control <= 2'b01;
funct <= 6'b100110;

#10
control <= 2'b10;
funct <= 6'b100100;

end


always
begin
#5
CLK = ~CLK;
end

endmodule

module tb_ControlUnit();

//Inputs
reg [5:0] OpCode;
reg CLK;
//Outputs
wire RegDst, Jump, Branch, MemRead, MemtoReg, MemWrite, ALUSrc, RegWrite, JAL;
wire [1:0] ALUOp;

always
begin
	#5
	CLK = ~CLK;
end

initial
begin

CLK = 0;

$display("Time   .  OpCode   .   RegDst   .  ALUSrc   .   MemtoReg   .   RegWrite   .   MemRead   .   MemWrite   .   Branch   .   ALUOp   .   Jump   .   JAL   ");
$monitor("%d   .   %d   .   %d   .    %d   .	%d   .	%d   .	%d   .	%d   .	%b   .	%d   .	%d", $time, OpCode, RegDst, ALUSrc, MemtoReg, RegWrite, MemRead, MemWrite, Branch, ALUOp, Jump, JAL);

#4
OpCode = 6'b000000;

#10
OpCode = 6'b100011;

#10
OpCode = 6'b101011;

#10
OpCode = 6'b101011;

#10
OpCode = 6'b000100;

#10
OpCode = 6'b000010;

#10
OpCode = 6'b000011;
end

ControlUnit test(CLK, OpCode, RegDst, Jump, Branch, MemRead, MemtoReg, ALUOp, MemWrite, ALUSrc, RegWrite, JAL);

endmodule 



module tb_DataMemory();

reg [31:0] writeData;
reg [31:0] address;
reg memRead, memWrite, CLK;
wire [31:0] readData;

initial
begin
$monitor("%d %d %d %d %d",$time, memRead, memWrite, readData, writeData, address);
CLK = 0;
memRead =0;
memWrite=1;
address=0;
writeData=12;
#50
CLK=1;
#50
CLK=0;
#50
memWrite=0;
#50
CLK=1;
#50
address=0;
memRead=1;
#50
CLK=0;
#50
CLK=1;
#50
CLK=0;
end

dataMem data1(readData, writeData, address, memWrite, memRead, CLK);
endmodule

module tb_MUX();
reg[31:0] IN_1,IN_2;
reg Sel;
wire[31:0] Out;

initial
begin
$monitor("IN_1 = %b , IN_2 = %b, Sel = %b,  Out = %b",IN_1,IN_2,Sel,Out);
#5
IN_1<=32'b0000_1111_0000_1111_0000_1111_0000_1111;
IN_2<=32'b0000_0000_0000_0000_0000_0000_0000_0000;
Sel<=0;
#5
Sel<=1;




end

MUX M1(IN_1, IN_2, Sel, Out);
endmodule

module tb_ProgMemory();
wire [31:0]instd;
reg [3:0]prgcntrd;

initial
begin
$monitor("%b ,   %d",instd,prgcntrd);
prgcntrd=0;
#5
prgcntrd=4;
#5
prgcntrd=8;
#5
prgcntrd=12;

end

prgmem dummy(instd,prgcntrd);
endmodule 

module tb_ShiftLeft2();
reg[31:0] IN;
wire[31:0] Out;

initial
begin
$monitor("IN = %b,  Out = %b",IN,Out);
#5
IN<=1;
#5
IN<=7;

end

Shiftleft2 SL2(IN,Out);
endmodule


module tb_SignExtend();
reg[15:0] IN;
wire[31:0] Out;

initial 
begin
$monitor("IN = %b  Out = %b",IN,Out);
#5
IN<=1;
#5
IN<=-5;
#5
IN<=4;
#5
IN<=12;

end


Signextend S1(IN,Out);
endmodule

module tb_REG();

//Inputs
reg [4:0]Read_register_1;
reg [4:0]Read_register_2;

reg [4:0]Write_register;

reg [31:0]Write_data;
reg RegWrite, Clock;


//Outputs
wire [31:0] Read_data_1;
wire [31:0] Read_data_2;

initial
	begin
		$display("                  Time  | Register Location 1 | Register Location 2 | Register Address 1 | Register Address 2 | RegWrite  | Register Write Address | Register Write Data");

		$monitor($time,"    |   %d        |    %d       |        %d       |         %d         |        %d      |        %d     |   %d"     , Read_data_1, Read_data_2, Read_register_1, Read_register_2, RegWrite, Write_register, Write_data);

		Clock = 0;
		#2
		RegWrite <=1;
		Write_data <= 100;
		Write_register <= 1;

		#10
		RegWrite <=1;
		Write_data <= 200;
		Write_register <= 2;


		#10
		RegWrite <=1;
		Write_data <= 300;
		Write_register <= 3;

		#10
		RegWrite <=1;
		Write_data <= 400;
		Write_register <= 4;

		#10
		RegWrite <=0;
		Read_register_1 <= 1;
		Read_register_2 <= 4;
		
		#10
		RegWrite <=0;
		Read_register_1 <= 2;
		Read_register_2 <= 3;

		#10
		RegWrite <=0;
		Read_register_1 <= 5;
		Read_register_2 <= 6;







	end




always
	begin
		#5
		Clock = ~Clock; 
	end
Registers RF(Read_register_1, Read_register_2, Write_register, Write_data, RegWrite, Read_data_1, Read_data_2, Clock);
endmodule

