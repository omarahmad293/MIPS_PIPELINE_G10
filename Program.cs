using System;

namespace testMIPS
{
	class Program
	{
		readonly static String[] registers = {
			"zero", "at", "v0", "v1", "a0", "a1", "a2", "a3" ,
			"t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7" ,
			"s0", "s1", "s2", "s3", "s4", "s5", "s6", "s7" ,
			"t8", "t9", "k0", "k1", "gp", "sp", "fp", "ra"};

		static void Main(string[] args)
		{
			String instruction = "SLL $S1, $S2, 15";
			//"LW $S2, 50($S1)";
			//"ADD $R1, $R2, $R3";

			String[] words = SplitInstruction(instruction);

			if (instruction.IndexOf('(') == -1) // no brackets
			{
				if (words.Length == 4) //R-Type
				{
					Console.WriteLine(instruction);
					Console.WriteLine("oooooossssstttttdddddhhhhhffffff");
					Console.WriteLine(R_Type(words));
					Console.WriteLine(R_Type(words).Length);
				}
			}
		}

		static String[] SplitInstruction(String instruction)
		{
			String[] words = instruction.Split(' ');
			for (int i = 0; i < words.Length; i++)
			{
				words[i] = words[i].Trim(',', '$');
			}
			return words;
		}

		static String R_Type(String[] array)
		{
			String opCode = "000000";
			String d = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower())), 2).PadLeft(5, '0');
			String s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[2].ToLower())), 2).PadLeft(5, '0');
			String t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[3].ToLower())), 2).PadLeft(5, '0');
			String shamt = "00000";
			String function = "";

			switch (array[0])
			{
				case "ADD":
					function = "100000";
					break;

				case "SLL":
					function = "000000";
					shamt = Convert.ToString(Int32.Parse(array[3]), 2).PadLeft(5, '0');
					t = "00000";
					break;

				case "AND":
					function = "100100";
					break;

				case "OR":
					function = "100101";
					break;

				case "SLT":
					function = "101010";
					break;
			}

			return opCode + s + t + d + shamt + function;
		}

	}
}
