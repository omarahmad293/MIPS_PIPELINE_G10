using System;
using System.Collections.Generic;

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
		static String program = System.IO.File.ReadAllText(@"C:\Users\omara\Documents\mips.txt");
		static List<String> instructions = new List<string>(program.Split('\n'));
		static List<String> binary = new List<String>();

		static void Main(string[] args)
		{
			Assembler();
		}

		static void Assembler()
		{
			foreach (String instruction in instructions)
			{
				binary.Add(Decode(instruction));
			}

			System.IO.File.WriteAllLines(@"C:\Users\omara\Documents\mipsBinary.txt", binary);
		}

		static String Decode(String instruction)
		{
			List<String> words = new List<String>(SplitInstruction(instruction));

			if (words[0].IndexOf(':') != -1)
			{
				words.RemoveAt(0);
			}

			if (Array.Exists(R_Type_instructions, x => x.Equals(words[0])))
			{
				return R_Type(words.ToArray());
			}

			if (Array.Exists(I_Type_instructions, x => x.Equals(words[0])))
			{
				return I_Type(words.ToArray());
			}

			if (Array.Exists(J_Type_instructions, x => x.Equals(words[0])))
			{
				return J_Type(words.ToArray());
			}

			return "";
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
			String s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower().Trim())), 2).PadLeft(5, '0');
			String d, t;
			if (array.Length > 2)
			{
				d = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[2].ToLower().Trim())), 2).PadLeft(5, '0');
				t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[3].ToLower().Trim())), 2).PadLeft(5, '0');
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
				t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower().Trim())), 2).PadLeft(5, '0');
				String rs = array[2].Substring(array[2].IndexOf('$') + 1).Trim().Trim(')');
				s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(rs.ToLower().Trim())), 2).PadLeft(5, '0');
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
				s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[2].ToLower().Trim())), 2).PadLeft(5, '0');
				t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower().Trim())), 2).PadLeft(5, '0');
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
						s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower().Trim())), 2).PadLeft(5, '0');
						t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[2].ToLower().Trim())), 2).PadLeft(5, '0');
						break;
				}
			}
			return opCode + s + t + immediate;
		}

		static String J_Type(String[] array)
		{
			String instruction = array[0] + ' ' + array[1];
			String opCode = "";
			int offset = 0;

			for (int i = 0; i < instructions.Count; i++)
			{
				if (instructions[i].IndexOf(array[1]) == 0) //Found label
				{
					int x = 0;

					for (int j = 0; j < instructions.Count; j++)
					{
						if (instructions[j] == instruction)
						{
							x = j;
							break;
						}
					}
					offset = x + 1 - i;
				}
			}

			String immediate = Convert.ToString(offset, 2).PadLeft(26, '0');

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
