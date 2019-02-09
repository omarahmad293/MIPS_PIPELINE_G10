module alu(Read_data_1,Data_2,ALU_control,Result,Zero_flag); 

input [31:0] Read_data_1;
input [31:0] Data_2;
input [2:0] ALU_control;
output [31:0] Result;
output Zero_flag;

reg [31:0] Result_reg;
//assign Result = Result_reg;
assign Zero_flag = (Result==0)?1:0;

assign Result = (ALU_control == 0) ? (Read_data_1&Data_2) : (ALU_control == 1) ? (Read_data_1|Data_2) :
		(ALU_control == 2) ? (Read_data_1+Data_2) : (ALU_control == 6) ? (Read_data_1-Data_2) :
		(ALU_control == 7) ? ((Read_data_1>Data_2)?0:1) : 0;
/*
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
*/
endmodule 