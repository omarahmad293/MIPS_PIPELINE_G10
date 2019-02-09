using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyVerilog
{
    
    public partial class Form2 : Form
    {
        
        
        public static class Globals
        {

            public static string Initiator_var= null; // Modifiable
            public static string Target_var = null;
            public static string Time_var = null;
            public static string Words_var = null;

            public static string LST_Init_var = null; // Modifiable
            public static string LST_Target_var = null;
        }


        public Form2()
        {
            InitializeComponent();

            string[] HeaderText = { "module tsst310();","//counters" , "integer i;", "// Inputs", "reg forceRequestA;", "reg forceRequestB;", "reg forceRequestC;", "reg[31:0] addressToContact;", "reg CLK;", "reg RESET;", " reg[1:0] phaseWire;", "// Instantiate the Unit Under Test (UUT)", " PCI uut(", "	.forceRequestA(forceRequestA),", ".forceRequestB(forceRequestB),", ".forceRequestC(forceRequestC),", ".addressToContact(addressToContact),", ".CLK(CLK),", ".RESET(RESET),", ".phaseWire(phaseWire)", "            );", "initial", "fork", "$dumpvars(0, tsst310);", "for(i = 0; i < 10; i = i + 1)", "begin", "$dumpvars(0, uut.A.mem[i]);", "$dumpvars(0, uut.B.mem[i]);", " $dumpvars(0, uut.C.mem[i]);", "end", "addressToContact = 0;", "CLK = 1;", "RESET = 0;", "#2", "   RESET = 1;" };
            StreamWriter sw = new StreamWriter(Path.Combine(Application.StartupPath, "Testbench.v"));
            foreach (string line in HeaderText)
            {
                sw.WriteLine(line);
            }

            sw.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Getting the Textboxes Values
            Globals.Words_var = WordstextBox.Text;
            Globals.Time_var = TimetextBox.Text;

            if (Globals.Initiator_var == null)
            {
                MessageBox.Show("Please Choose an Initiator");
                return;
            }

            if (Globals.Target_var == null)
            {
                MessageBox.Show("Please Choose a Target");
                return;
            }

            if (Globals.Time_var == null)
            {
                MessageBox.Show("Please Choose enter a valid time");
                return;
            }

            if (Globals.Words_var == null)
            {
                MessageBox.Show("Please Choose enter a valid number of Words");
                return;
            }


            

            string Action = "-" + Globals.Initiator_var + " is sending " + Globals.Words_var + " words to " + Globals.Target_var + " at time " + Globals.Time_var ;
    
            TransQueue.Items.Add(Action);




            //Code Generation
            string addrs =null;

            if(Globals.Target_var == "A")
            {
                addrs = "0";
            }
            else if (Globals.Target_var == "B")
            {
                addrs = "1";
            }
            else if (Globals.Target_var == "C")
            {
                addrs = "2";
            }
            int time_int = Int32.Parse(Globals.Time_var) + 2;
            string time_str = time_int.ToString();
            StreamWriter sw = new StreamWriter(Path.Combine(Application.StartupPath, "Testbench.v"), true);
          
            sw.WriteLine("#" + Globals.Time_var);
            sw.WriteLine("forceRequest"+ Globals.Initiator_var +" = 0;");
            sw.WriteLine("#" + Globals.Time_var);
            sw.WriteLine("addressToContact = " + addrs + ";");
            sw.WriteLine("#" + Globals.Time_var);
            sw.WriteLine("phaseWire = " + Globals.Words_var + ";");

            sw.WriteLine("#" + time_str);
            sw.WriteLine("forceRequest" + Globals.Initiator_var + " = 1;");

            sw.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void DevA_Click(object sender, EventArgs e)
        {
            if (Init.Checked == true)
            {
                if (Globals.LST_Init_var == "Device A")
                {
                    DevA.Text = "Device A";
                }
                else if (Globals.LST_Init_var == "Device B")
                {
                    DevB.Text = "Device B";
                }
                else if (Globals.LST_Init_var == "Device C")
                {
                    DevC.Text = "Device C";
                }

                Globals.Initiator_var = "A";
                DevA.Text = "Device A(Initiator)";
                Globals.LST_Init_var = "Device A";
            }

            else if (Target.Checked == true)
            {
                if (Globals.LST_Target_var == "Device A")
                {
                    DevA.Text = "Device A";
                }
                else if (Globals.LST_Target_var == "Device B")
                {
                    DevB.Text = "Device B";
                }
                else if (Globals.LST_Target_var == "Device C")
                {
                    DevC.Text = "Device C";
                }

                Globals.Target_var = "A";
                DevA.Text = "Device A(Target)";
                Globals.LST_Target_var = "Device A";
            }

            else
            {
                MessageBox.Show("Please Specify the Device Type");
            }
        }

        private void DevB_Click(object sender, EventArgs e)
        {
            if (Init.Checked == true)
            {
                if (Globals.LST_Init_var == "Device A")
                {
                    DevA.Text = "Device A";
                }
                else if (Globals.LST_Init_var == "Device B")
                {
                    DevB.Text = "Device B";
                }
                else if (Globals.LST_Init_var == "Device C")
                {
                    DevC.Text = "Device C";
                }

                Globals.Initiator_var = "B";
                DevB.Text = "Device B(Initiator)";
                Globals.LST_Init_var = "Device B";
            }

            else if (Target.Checked == true)
            {
                if (Globals.LST_Target_var == "Device A")
                {
                    DevA.Text = "Device A";
                }
                else if (Globals.LST_Target_var == "Device B")
                {
                    DevB.Text = "Device B";
                }
                else if (Globals.LST_Target_var == "Device C")
                {
                    DevC.Text = "Device C";
                }

                Globals.Target_var = "B";
                DevB.Text = "Device B(Target)";
                Globals.LST_Target_var = "Device B";
            }

            else
            {
                MessageBox.Show("Please Specify the Device Type");
            }

        }

        private void DevC_Click(object sender, EventArgs e)
        {
            if (Init.Checked == true)
            {
                if (Globals.LST_Init_var == "Device A")
                {
                    DevA.Text = "Device A";
                }
                else if (Globals.LST_Init_var == "Device B")
                {
                    DevB.Text = "Device B";
                }
                else if (Globals.LST_Init_var == "Device C")
                {
                    DevC.Text = "Device C";
                }

                Globals.Initiator_var = "C";
                DevC.Text = "Device C(Initiator)";
                Globals.LST_Init_var = "Device C";
            }

            else if (Target.Checked == true)
            {

                if (Globals.LST_Target_var == "Device A")
                {
                    DevA.Text = "Device A";
                }
                else if (Globals.LST_Target_var == "Device B")
                {
                    DevB.Text = "Device B";
                }
                else if (Globals.LST_Target_var == "Device C")
                {
                    DevC.Text = "Device C";
                }



                Globals.Target_var = "C";
                DevC.Text = "Device C(Target)";
                Globals.LST_Target_var = "Device C";
            }

            else
            {
                MessageBox.Show("Please Specify the Device Type");
            }
        }

        private void easyVerilog_Click(object sender, EventArgs e)
        {
            GUIDriver.ShowIDE();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "Testbench.v"));
            string all = sr.ReadToEnd();
            sr.Close();
            if (!all.Contains("endmodule"))
            {
                string[] FooterText = { "join", "always", "begin", "#1", " CLK = ~CLK;", "end", "endmodule" };
                StreamWriter sw = new StreamWriter(Path.Combine(Application.StartupPath, "Testbench.v"), true);
                foreach (string line in FooterText)
                {
                    sw.WriteLine(line);
                }

                sw.Close();
            }

            //Start Icarus and Waveform

            //Opening the file
            string text = " ";
            try
            {
                FileHandler.OpenFileAbsolute(Path.Combine(Application.StartupPath, "Testbench.v"), out text);
            }
            catch(Exception ex)
            {
                GUIDriver.ShowIDE();
                return;
            }
            
            GUIDriver.mainForm.OnFileOpened(text);

            int exitCode;
            string errors;
            string output;
            ExecutionHandler.CompileAll(out exitCode, out errors);

            if(exitCode != 0)
            {
                GUIDriver.mainForm.OnCompiled(exitCode, errors);
                GUIDriver.ShowIDE();
                return;
            }


            ExecutionHandler.Simulate(out exitCode, out errors, out output);

            if(exitCode != 0 && exitCode != -1)
            {
                GUIDriver.mainForm.OnSimulated(exitCode, errors, output);
                GUIDriver.ShowIDE();
                return;
            }

            ExecutionHandler.DrawWave();
        }
    }
}