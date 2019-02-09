module MUX(IN_1, IN_2, Sel, Out);
input[31:0] IN_1;
input[31:0] IN_2;
input Sel;
output[31:0] Out;

assign Out = (Sel == 1)?IN_2:IN_1;

endmodule
