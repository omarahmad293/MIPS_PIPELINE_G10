module testmips();

reg CLK ;
initial
begin CLK=0;
$dumpvars(0, testmips);
end

always
begin
	#5
	CLK = ~CLK;
end


MIPS M1(CLK);

endmodule

