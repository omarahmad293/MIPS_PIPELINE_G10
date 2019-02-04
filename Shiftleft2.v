module Shiftleft2(IN,Out);
input[31:0] IN;
output[31:0] Out;

assign Out = IN<<2;


endmodule

module tb();
reg[31:0] IN;
wire[31:0] Out;

initial
begin
$monitor("IN = %b,  Out = %b",IN,Out);
#5
IN<=1;
#5
IN<=7;

end

Shiftleft2 SL2(IN,Out);
endmodule

