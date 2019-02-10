module MUX5(IN_1, IN_2, Sel, Out);
input[4:0] IN_1;
input[4:0] IN_2;
input Sel;
output[4:0] Out;

assign Out = (Sel === 1)?IN_2:IN_1;

endmodule
