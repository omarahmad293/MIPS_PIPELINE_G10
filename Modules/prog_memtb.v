module prgmemtb();
wire [31:0]instd;
reg [3:0]prgcntrd;

initial
begin
$monitor("%b ,   %d",instd,prgcntrd);
prgcntrd=0;
#5
prgcntrd=4;
#5
prgcntrd=8;
#5
prgcntrd=12;

end

prgmem dummy(instd,prgcntrd);
endmodule 