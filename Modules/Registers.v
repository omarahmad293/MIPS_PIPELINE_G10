module Registers(Read_register_1, Read_register_2, Write_register, Write_data, RegWrite, Read_data_1, Read_data_2, Clock);

//Inputs
input [4:0]Read_register_1;
input [4:0]Read_register_2;

input [4:0]Write_register;

input [31:0]Write_data;
input RegWrite, Clock;


//Outputs
output [31:0] Read_data_1;
output [31:0] Read_data_2;

//The Main Register
reg[31:0] Registers[0:31];

//Assign is used directly rather tha creating a dummy register due to the same output
assign Read_data_1 = Registers[Read_register_1];
assign Read_data_2 = Registers[Read_register_2];

integer i = 0;

initial
begin
	for (i = 0; i < 32; i = i+1)
	begin
		Registers[i] = 0;
	end
end


//To Write in a register
always@(posedge Clock)
	begin
		if(RegWrite == 1)
			begin
				Registers[Write_register] <= Write_data;
			end

	end

endmodule
