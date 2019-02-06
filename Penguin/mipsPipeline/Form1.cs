using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mipsPipeline
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen data = new Pen(Brushes.Black);
			Pen control = new Pen(Brushes.Cyan);
			data.Width = 3.0f;
			control.Width = 3.0f;

			g.DrawRectangle(data   , 120, 315, 107, 100); //Instruction memory

			g.DrawRectangle(data   , 250, 185, 25 , 370); //IF/ID

			g.DrawRectangle(data   , 327, 315, 105, 100); //Register file
			g.DrawEllipse  (data   , 390, 427, 44 , 63 ); //Sign Extend
			g.DrawEllipse  (control, 535, 430, 44 , 63 ); //ALU Control
			g.DrawEllipse  (control, 400, 96 , 47 , 84 ); //Control

			g.DrawRectangle(control, 470, 150, 25, 35 ); //ID/EX
			g.DrawRectangle(control, 470, 115, 25, 35 ); //ID/EX
			g.DrawRectangle(control, 470, 80 , 25, 35 ); //ID/EX
			g.DrawRectangle(data   , 470, 185, 25, 370); //ID/EX

			g.DrawRectangle(control, 655, 150, 25, 35 ); //EX/MEM
			g.DrawRectangle(control, 655, 115, 25, 35 ); //EX/MEM
			g.DrawRectangle(data   , 655, 185, 25, 370); //EX/MEM

			g.DrawRectangle(data   , 700, 340, 107, 100); //Data memory

			g.DrawRectangle(control, 825, 150, 25, 35 ); //MEM/WB
			g.DrawRectangle(data   , 825, 185, 25, 370); //MEM/WB

		}
	}
}
