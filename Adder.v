module Adder(IN_1, IN_2, Out);
input[31:0] IN_1;
input[31:0] IN_2;

output[31:0] Out;

assign Out = IN_1 + IN_2;

endmodule
