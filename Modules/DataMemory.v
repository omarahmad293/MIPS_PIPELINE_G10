module dataMem(readData, writeData, address, memWrite, memRead, CLK);
/*
Data memory module
------------------
Description: It's a MIPS's data memory (main memory) which allow the CPU to perform 
normal read/write operations.
Note: assuming address bus is 5-bit then memory is 32*32 bits
*/
output [31:0] readData;
input [31:0] writeData;
input [31:0] address;
input memWrite,memRead, CLK;

reg [31:0] mem [0:31];

integer i = 0;

initial
begin
	for (i = 0; i < 32; i = i+1)
	begin
		mem[i] = 32'hFFFF_FFFF;
	end
end

assign readData = (memRead) ? mem[address] : 32'dx;

always @(posedge CLK)
begin

if(memWrite)
begin
mem[address] <= writeData;
end

end

endmodule
