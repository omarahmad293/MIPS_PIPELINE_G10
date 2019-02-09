module ControlUnit(OpCode, RegDst, Jump, Branch, MemRead, MemtoReg, ALUOp, MemWrite, ALUSrc, RegWrite, JAL);

input [5:0] OpCode;
output reg RegDst, Jump, Branch, MemRead, MemtoReg, MemWrite, ALUSrc, RegWrite, JAL;
output reg [1:0] ALUOp;

always@(OpCode)
begin

case (OpCode)
	6'b000000:
		begin
			 RegDst<=1; ALUSrc<=0; MemtoReg<=0; RegWrite<=1; MemRead<=0; MemWrite<=0; Branch<=0; ALUOp<=2'b10; Jump<=0; JAL<=0; //r-type
		end
	6'b 100011: 
		begin
			RegDst<=0; ALUSrc<=1; MemtoReg<=1; RegWrite<=1; MemRead<=1; MemWrite<=0; Branch<=0; ALUOp<=2'b00; Jump<=0; JAL<=0; //lw
		end
	6'b 101011: 
		begin
			ALUSrc<=1; RegWrite<=0; MemRead<=0; MemWrite<=1; Branch<=0; ALUOp<=2'b00; Jump<=0; //sw
		end
	6'b 000100: 
		begin	
			ALUSrc<=0; RegWrite<=0; MemRead<=0; MemWrite<=0; Branch<=1; ALUOp<=2'b01; Jump<=0; //beq
		end
	6'b 000010: 
		begin
			RegWrite<=0; MemRead<=0; MemWrite<=0; Branch<=0; Jump<=1; //j
		end
	6'b 000011: 
		begin	
			RegWrite<=0; MemRead<=0; MemWrite<=0; Branch<=0; Jump<=1; JAL<=1; //jal
		end	
	6'b 001000: //addi
		begin
			RegDst<=0; ALUSrc<=1; MemtoReg<=0; RegWrite<=1; MemRead<=0; MemWrite<=0; Branch<=0; ALUOp<=2'b00; Jump<=0; JAL<=0; 
		end
	6'b 001101: //ori
		begin
			RegDst<=0; ALUSrc<=1; MemtoReg<=0; RegWrite<=1; MemRead<=0; MemWrite<=0; Branch<=0; ALUOp<=2'b11; Jump<=0; JAL<=0; 
		end
endcase 
end 

endmodule
