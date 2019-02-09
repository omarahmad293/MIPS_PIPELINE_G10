using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;


namespace Assembler
{
    public partial class Form1 : Form
    {
      

        public static String PATH = Path.GetTempFileName(); // Modifiable
        //Path.GetTempFileName
        //--------------------------------Binary Converting--------------------------------------------
        readonly static String[] registers = {
            "zero", "at", "v0", "v1", "a0", "a1", "a2", "a3" ,
            "t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7" ,
            "s0", "s1", "s2", "s3", "s4", "s5", "s6", "s7" ,
            "t8", "t9", "k0", "k1", "gp", "sp", "fp", "ra"};

        readonly static String[] R_Type_instructions = { "add", "and", "or", "jr", "sll", "slt" };
        readonly static String[] I_Type_instructions = { "lw", "sw", "beq", "addi", "ori" };
        readonly static String[] J_Type_instructions = { "j", "jal" };
        static String program = System.IO.File.ReadAllText(PATH);
        static List<String> instructions = new List<string>(program.Split('\n'));
        static List<String> binary = new List<String>();

        //-----------------------------------------------------------------------------------



        //----------------Decoding Fuctions------------------------------------------------
        static void Assembler()
        {
            foreach (String instruction in instructions)
            {
                binary.Add(Decode(instruction));
            }

            Spacing();

            System.IO.File.WriteAllLines(@"c:\ProgramMem.txt", binary);
        }

        static void Spacing()
        {
            int i;

            for (i=0;i<binary.Count;i++) 
            {
                binary[i] = binary[i].Substring(0, 8) + " " + binary[i].Substring(8, 8) + " " + binary[i].Substring(16, 8) + " " + binary[i].Substring(24, 8);
            }

        }

        static String Decode(String instruction)
        {
            List<String> words = new List<String>(SplitInstruction(instruction));

            if (words[0].IndexOf(':') != -1)
            {
                words.RemoveAt(0);
            }

            if (Array.Exists(R_Type_instructions, x => x.Equals(words[0].ToLower())))
            {
                return R_Type(words.ToArray());
            }

            if (Array.Exists(I_Type_instructions, x => x.Equals(words[0].ToLower())))
            {
                return I_Type(words.ToArray());
            }

            if (Array.Exists(J_Type_instructions, x => x.Equals(words[0].ToLower())))
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

            switch (array[0].ToLower())
            {
                case "add":
                    function = "100000";
                    break;

                case "sll":
                    function = "000000";
                    shamt = Convert.ToString(Int32.Parse(array[3]), 2).PadLeft(5, '0');
                    t = "00000";
                    break;

                case "and":
                    function = "100100";
                    break;

                case "or":
                    function = "100101";
                    break;

                case "slt":
                    function = "101010";
                    break;

                case "jr":
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

                switch (array[0].ToLower())
                {
                    case "lw":
                        opCode = "100011";
                        break;

                    case "sw":
                        opCode = "101011";
                        break;
                }
            }
            else
            {
                s = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[2].ToLower().Trim())), 2).PadLeft(5, '0');
                t = Convert.ToString(Array.FindIndex(registers, x => x.Equals(array[1].ToLower().Trim())), 2).PadLeft(5, '0');
                immediate = Convert.ToString(Int16.Parse(array[3]), 2).PadLeft(16, '0');
                switch (array[0].ToLower())
                {
                    case "addi":
                        opCode = "001000";
                        break;

                    case "ori":
                        opCode = "001101";
                        break;

                    case "beq":
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

            switch (array[0].ToLower())
            {
                case "j":
                    opCode = "000010";
                    break;

                case "jal":
                    opCode = "000011";
                    break;
            }

            return opCode + immediate;
        }

        //------------------------------------------------------------------------------------------
        public Form1()
        {
            
            InitializeComponent();


        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            File.Delete(PATH);
            File.Delete(@"c:\ProgramMem.txt");
            this.Close();
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + startIndex), word.Length);
                    this.richTextBox1.SelectionColor = color;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.Black;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.CheckKeyword("add", Color.DarkRed, 0);
            this.CheckKeyword("sw", Color.DarkRed, 0);
            this.CheckKeyword("lw", Color.DarkRed, 0);
            this.CheckKeyword("sll", Color.DarkRed, 0);
            this.CheckKeyword("and", Color.DarkRed, 0);
            this.CheckKeyword("or", Color.DarkRed, 0);
            this.CheckKeyword("beq", Color.DarkRed, 0);
            this.CheckKeyword("j", Color.DarkRed, 0);
            this.CheckKeyword("jal", Color.DarkRed, 0);
            this.CheckKeyword("jr", Color.DarkRed, 0);
            this.CheckKeyword("addi", Color.DarkRed, 0);
            this.CheckKeyword("ori", Color.DarkRed, 0);
            this.CheckKeyword("slt", Color.DarkRed, 0);

            this.CheckKeyword("$s0", Color.DarkBlue, 0);
            this.CheckKeyword("$s1", Color.DarkBlue, 0);
            this.CheckKeyword("$s2", Color.DarkBlue, 0);
            this.CheckKeyword("$s3", Color.DarkBlue, 0); 
            this.CheckKeyword("$s4", Color.DarkBlue, 0);
            this.CheckKeyword("$s5", Color.DarkBlue, 0);
            this.CheckKeyword("$s6", Color.DarkBlue, 0);
            this.CheckKeyword("$s7", Color.DarkBlue, 0);

            this.CheckKeyword("$t8", Color.DarkBlue, 0);
            this.CheckKeyword("$t9", Color.DarkBlue, 0);

            this.CheckKeyword("$t0", Color.DarkBlue, 0);
            this.CheckKeyword("$t1", Color.DarkBlue, 0);
            this.CheckKeyword("$t2", Color.DarkBlue, 0);
            this.CheckKeyword("$t3", Color.DarkBlue, 0);
            this.CheckKeyword("$t4", Color.DarkBlue, 0);
            this.CheckKeyword("$t5", Color.DarkBlue, 0);
            this.CheckKeyword("$t6", Color.DarkBlue, 0);
            this.CheckKeyword("$t7", Color.DarkBlue, 0);

            this.CheckKeyword("$k0", Color.DarkBlue, 0);
            this.CheckKeyword("$k1", Color.DarkBlue, 0);
            this.CheckKeyword("$gp", Color.DarkBlue, 0);
            this.CheckKeyword("$sp", Color.DarkBlue, 0);
            this.CheckKeyword("$fp", Color.DarkBlue, 0);
            this.CheckKeyword("$ra", Color.DarkBlue, 0);

            this.CheckKeyword("$zero", Color.DarkBlue, 0);
            this.CheckKeyword("$at", Color.DarkBlue, 0);

            this.CheckKeyword("$v0", Color.DarkBlue, 0);
            this.CheckKeyword("$v1", Color.DarkBlue, 0);

            this.CheckKeyword("$a0", Color.DarkBlue, 0);
            this.CheckKeyword("$a1", Color.DarkBlue, 0);
            this.CheckKeyword("$a2", Color.DarkBlue, 0);
            this.CheckKeyword("$a3", Color.DarkBlue, 0);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opentext = new OpenFileDialog();
            if (opentext.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(opentext.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //richTextBox1.SaveFile(PATH, RichTextBoxStreamType.RichText);
            System.IO.File.WriteAllText(PATH, richTextBox1.Text.Replace("\n", Environment.NewLine));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            program = System.IO.File.ReadAllText(PATH);
            instructions = new List<string>(program.Split('\n'));
            Assembler();
        }
    }
}
