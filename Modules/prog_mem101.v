module prgmem(inst,prgcntr);

input [31:0]prgcntr;
output reg [31:0]inst;
reg [7:0]mem[0:63];


initial
begin

$readmemb("C:/Users/lamee/Desktop/MIPSProgramMem.txt",mem);

end

always@(prgcntr)
begin
inst<={mem[prgcntr],mem[prgcntr+1],mem[prgcntr+2],mem[prgcntr+3]};
end
    
	
endmodule
