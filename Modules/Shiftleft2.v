module Shiftleft2_32(IN,Out);
input[31:0] IN;
output[31:0] Out;

assign Out = IN<<2;


endmodule

module Shiftleft2_28(IN,Out);
input[25:0] IN;
output[27:0] Out;

assign Out = IN<<2;


endmodule