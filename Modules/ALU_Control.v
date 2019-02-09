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
	2'b11: ALU_control <= OR;
endcase
end

endmodule
