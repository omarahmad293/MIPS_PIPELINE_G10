module Signextend(IN,Out);
input[15:0] IN;
output[31:0] Out;

assign Out =(IN[15] == 0)?{16'b0000_0000_0000_0000,IN}:{16'b1111_1111_1111_1111,IN} ;
endmodule

module tb();
reg[15:0] IN;
wire[31:0] Out;

initial 
begin
$monitor("IN = %b  Out = %b",IN,Out);
#5
IN<=1;
#5
IN<=-5;
#5
IN<=4;
#5
IN<=12;

end


Signextend S1(IN,Out);
endmodule
