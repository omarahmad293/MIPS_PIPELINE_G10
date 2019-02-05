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

		readonly static String[] R_Type_instructions = { "ADD", "AND", "OR", "JR", "SLL", "SLT" };
		readonly static String[] I_Type_instructions = { "LW", "SW", "BEQ", "ADDI", "ORI" };
		readonly static String[] J_Type_instructions = { "J", "JAL" };

		static void Main(string[] args)
		{
			String instruction = "J LOOP";
			//"SLL $S1, $S2, 15";
			//"LW $S2, 50($S1)";
			//"ADD $S1, $S2, $S3";

			String[] words = SplitInstruction(instruction);

			if (Array.Exists(R_Type_instructions, x => x.Equals(words[0])))
			{
				Console.WriteLine(instruction);
				Console.WriteLine("oooooossssstttttdddddhhhhhffffff");
				Console.WriteLine(R_Type(words));
				Console.WriteLine(R_Type(words).Length);
			}

			if (Array.Exists(I_Type_instructions, x => x.Equals(words[0])))
			{
				Console.WriteLine(instruction);
				Console.WriteLine("oooooossssstttttffffffffffffffff");
				Console.WriteLine(I_Type(words));
				Console.WriteLine(I_Type(words).Length);
			}

			if (Array.Exists(J_Type_instructions, x => x.Equals(words[0])))
			{
				Console.WriteLine(instruction);
				Console.WriteLine("ooooooaaaaaaaaaaaaaaaaaaaaaaaaaa");
				Console.WriteLine(J_Type(words));
				Console.WriteLine(J_Type(words).Length);
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
			String s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower())), 2).PadLeft(5, '0');
			String d, t;
			if (array.Length > 2)
			{
				d = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[2].ToLower())), 2).PadLeft(5, '0');
				t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[3].ToLower())), 2).PadLeft(5, '0');
			}
			else
			{
				d = "00000";
				t = "00000";
			}
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

				case "JR":
					function = "001000";
					t = "00000";
					d = "00000";
					break;
			}

			return opCode + s + t + d + shamt + function;
		}

		static String I_Type(String[] array)
		{
			String opCode = "";
			String s = "";
			String t = "";
			String immediate = "";

			if (array.Length == 3) //LW or SW
			{
				t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower())), 2).PadLeft(5, '0');
				String rs = array[2].Substring(array[2].IndexOf('$') + 1).Trim(')');
				s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(rs.ToLower())), 2).PadLeft(5, '0');
				immediate = Convert.ToString(Int16.Parse(array[2].Substring(0, array[2].IndexOf('('))), 2).PadLeft(16, '0');

				switch (array[0])
				{
					case "LW":
						opCode = "100011";
						break;

					case "SW":
						opCode = "101011";
						break;
				}
			}
			else
			{
				s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[2].ToLower())), 2).PadLeft(5, '0');
				t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower())), 2).PadLeft(5, '0');
				immediate = Convert.ToString(Int16.Parse(array[3]), 2).PadLeft(16, '0');
				switch (array[0])
				{
					case "ADDI":
						opCode = "001000";
						break;

					case "ORI":
						opCode = "001101";
						break;

					case "BEQ":
						opCode = "000100";
						s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower())), 2).PadLeft(5, '0');
						t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[2].ToLower())), 2).PadLeft(5, '0');
						break;
				}
			}
			return opCode + s + t + immediate;
		}

		static String J_Type(String[] array)
		{
			String opCode = "";
			String immediate = array[1]; //TODO: Calculate offset between next instruction and label and convert to binary

			switch (array[0])
			{
				case "J":
					opCode = "000010";
					break;

				case "JAL":
					opCode = "000011";
					break;
			}

			return opCode + immediate;
		}
	}
}
