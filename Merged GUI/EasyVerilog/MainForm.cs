using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyVerilog
{
    public partial class EasyVerilog : Form
    {
        //bool ApplyColoring = false;
        public EasyVerilog()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Welcome";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    string text = "";
                    FileHandler.OpenFileAbsolute(file, out text);

                    OnFileOpened(text);
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           /* var index = listBox1.SelectedIndex;
            OpenedFilesHandles.SaveText(index, richTextBox1.Text);
            toolStripStatusLabel1.Text = "Text changed, consider saving...";
            */
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            else
            {
                richTextBox1.Text = OpenedFilesHandles.GetText(index);
                toolStripStatusLabel1.Text = "Editing: " + OpenedFilesHandles.OpenedFilesNames[listBox1.SelectedIndex];
            }
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Running the compiler...";
            string errors = "";
            int exitCode;
            ExecutionHandler.CompileAll(out exitCode, out errors);

            OnCompiled(exitCode, errors);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void simulateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Running the simulator...";
            string errors = "";
            string output = "";
            int exitCode;
            ExecutionHandler.Simulate(out exitCode, out errors, out output);

            OnSimulated(exitCode, errors, output);

        }

        private void waveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Drawing Wave...";
            ExecutionHandler.DrawWave();
            toolStripStatusLabel1.Text = "Wave Drawing Ended";
        }

        private void gUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIDriver.ShowGUI();
        }


        public void OnFileOpened(string text)
        {
            listBox1.Items.Clear();
            foreach (string s in OpenedFilesHandles.OpenedFilesNames)
            {
                listBox1.Items.Add(s);
            }
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            toolStripStatusLabel1.Text = "Loaded file: " + OpenedFilesHandles.OpenedFilesNames[listBox1.SelectedIndex];

            richTextBox1.Text = text;
            ColorTextBox();
        }

        public void OnCompiled(int exitCode, string errors)
        {
            if (exitCode != 0)
                textBox2.Text = "ExitCode: " + exitCode + " Errors: " + errors;
            else
                textBox2.Text = "Compiled Succesfully.";
            toolStripStatusLabel1.Text = "Finished compiling";
        }

        public void OnSimulated(int exitCode, string errors, string output)
        {
            if (exitCode != 0 && exitCode != -1)
                textBox2.Text = "ExitCode: " + exitCode + " Errors: " + errors;
            else
                textBox2.Text = "Finished " + output;
            toolStripStatusLabel1.Text = "Finished simulation";
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            if (index < 0)
                return;
            //Remove file from program
            OpenedFilesHandles.CloseFile(index);
            //Remove file from list
            listBox1.Items.Clear();
            foreach (string s in OpenedFilesHandles.OpenedFilesNames)
            {
                listBox1.Items.Add(s);
            }
            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            else
            {
                richTextBox1.Text = "";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //richTextBox1.AppendText(allText, Color.White);
            //Saving


        }

        private void ColorTextBox()
        {
            //Coloring
            string allText = richTextBox1.Text;
            richTextBox1.Text = "";
            string word = "";
            foreach (char c in allText)
            {
                if (Char.IsLetterOrDigit(c))
                {
                    word += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        switch (word)
                        {
                            case "if":
                            case "else":
                                richTextBox1.AppendText(word, Color.Firebrick);
                                break;
                            case "input":
                            case "inout":
                            case "output":
                            case "reg":
                                richTextBox1.AppendText(word, Color.Yellow);
                                break;
                            case "switch":
                            case "case":
                            case "always":
                                richTextBox1.AppendText(word, Color.Fuchsia);
                                break;
                            case "begin":
                            case "end":
                            case "{":
                            case "}":
                                richTextBox1.AppendText(word, Color.HotPink);
                                break;
                            case "endmodule":
                            case "module":
                                richTextBox1.AppendText(word, Color.Orange);
                                break;
                            default:
                                richTextBox1.AppendText(word, Color.White);
                                break;
                        }
                        word = "";
                    }
                    richTextBox1.AppendText(c.ToString(), Color.White);
                }

            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            ColorTextBox();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            /*ApplyColoring = false;
            if(e.KeyCode == Keys.Space)
            {
                ApplyColoring = true;
            }
            */
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if(ApplyColoring)
            {
                ColorTextBox();
            }
            */
        }

        private void EasyVerilog_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.S)
            {
                SaveAll();
            }
        }

        private void SaveAll()
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                FileHandler.CreateFileAbsolute(OpenedFilesHandles.GetFullName(i), OpenedFilesHandles.GetText(i));
            }
            toolStripStatusLabel1.Text = "Saved all changes successfully";
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            OpenedFilesHandles.SaveText(index, richTextBox1.Text);
            toolStripStatusLabel1.Text = "Text changed, consider saving...";
        }
    }
}
