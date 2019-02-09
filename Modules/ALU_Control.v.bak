module ALU_Control(ALU_control, control, funct, CLK);

input [1:0] control; //2 bits coming from the control unit
input [5:0] funct; //6 function bits coming from the instruction
input CLK;

output reg [2:0] ALU_control; //3 bits going to the alu

//add - sw - lw - sll - and - or - beq - J - JAL - JR - addi - ori - slt

parameter ADD = 3'b010;
parameter SUB = 3'b110;
parameter AND = 3'b000;
parameter OR  = 3'b001;
parameter SLT = 3'b111;

always @(posedge CLK)
begin

case (control)
	2'b00: ALU_control <= ADD; //Load or Store
	2'b01: ALU_control <= SUB; //Branch
	2'b10: begin               //Arithmetic -> Check function
		case (funct)
			6'b100000: ALU_control <= ADD;
			6'b100010: ALU_control <= SUB;
			6'b100100: ALU_control <= AND;
			6'b100101: ALU_control <=  OR;
			6'b101010: ALU_control <= SLT;
		endcase
		end
endcase
end

endmodule


module ALU_Control_tb();


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
