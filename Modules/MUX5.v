module MUX5(IN_1, IN_2, Sel, Out);
input[4:0] IN_1;
input[4:0] IN_2;
input Sel;
output[4:0] Out;

assign Out = (Sel == 1)?IN_2:IN_1;

endmodule
/*
module tb_MUX();
reg[31:0] IN_1,IN_2;
reg Sel;
wire[31:0] Out;

initial
begin
$monitor("IN_1 = %b , IN_2 = %b, Sel = %b,  Out = %b",IN_1,IN_2,Sel,Out);
#5
IN_1<=32'b0000_1111_0000_1111_0000_1111_0000_1111;
IN_2<=32'b0000_0000_0000_0000_0000_0000_0000_0000;
Sel<=0;
#5
Sel<=1;




end

MUX M1(IN_1, IN_2, Sel, Out);
endmodule
*/
