module alu(Read_data_1,Data_2,ALU_control,Result,Zero_flag); 

input [31:0] Read_data_1;
input [31:0] Data_2;
input [2:0] ALU_control;
output [31:0] Result;
output Zero_flag;

reg [31:0] Result_reg;
assign Result = Result_reg;
assign Zero_flag = (Result_reg==0)?1:0;

always
begin
case (ALU_control)
	0: Result_reg <= Read_data_1&Data_2;
	1: Result_reg <= Read_data_1|Data_2;
	2: Result_reg <= Read_data_1+Data_2;
	6: Result_reg <= Read_data_1-Data_2;
	7: Result_reg <= (Read_data_1>Data_2)?0:1; 
endcase
end 

endmodule 


module tb_ALU();
//Inputs
 reg[31:0] Read_data_1,Data_2;
 reg[2:0] ALU_control;

//Outputs
 wire[31:0] Result;
 wire Zero_flag;

ALU ALU1(Read_data_1,Data_2,ALU_control,Result,Zero_flag);

initial
begin 

$display("Time   .   Data1   .   Data2   .  ALUControl   .   Result   .   Zero_flag");
$monitor("%d   .   %d   .   %d   .    %d   .	%d",$time,Read_data_1,Data_2,ALU_control,Result,Zero_flag);

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