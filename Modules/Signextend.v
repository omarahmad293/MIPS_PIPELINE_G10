module Signextend(IN,Out);
input[15:0] IN;
output[31:0] Out;

assign Out =(IN[15] == 0)?{16'b0000_0000_0000_0000,IN}:{16'b1111_1111_1111_1111,IN} ;
endmodule
