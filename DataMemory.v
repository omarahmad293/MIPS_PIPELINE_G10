module dataMem(readData, writeData, address, memWrite, memRead, CLK);
/*
Data memory module
------------------
Description: It's a MIPS's data memory (main memory) which allow the CPU to perform 
normal read/write operations.
Note: assuming address bus is 5-bit then memory is 32*32 bits
*/
output reg [31:0] readData;
input [31:0] writeData;
input [4:0] address;
input memWrite,memRead, CLK;

reg [31:0] mem [0:31];

always @(posedge CLK)
begin
if(memRead)
begin
readData <= mem[address];
end

else if(memWrite)
begin
mem[address] <= writeData;
end

end

endmodule

//-----------------------------------------------------------------
/*
Data memory test bench
----------------------
inputs : writeData, address, memRead, memWrite, CLK
outputs: readdata
*/
module tb();

reg [31:0] writeData;
reg [4:0] address;
reg memRead, memWrite, CLK;
wire [31:0] readData;

initial
begin
$monitor("%d %d %d %d %d",$time, memRead, memWrite, readData, writeData, address);
CLK = 0;
memRead =0;
memWrite=1;
address=0;
writeData=12;
#50
CLK=1;
#50
CLK=0;
#50
memWrite=0;
#50
CLK=1;
#50
address=0;
memRead=1;
#50
CLK=0;
#50
CLK=1;
#50
CLK=0;
end

dataMem data1(readData, writeData, address, memWrite, memRead, CLK);
endmodule