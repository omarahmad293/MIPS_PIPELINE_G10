namespace EasyVerilog
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.DevA = new System.Windows.Forms.Button();
            this.TransQueue = new System.Windows.Forms.ListBox();
            this.AddTrans = new System.Windows.Forms.Button();
            this.TimetextBox = new System.Windows.Forms.TextBox();
            this.WordstextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Init = new System.Windows.Forms.RadioButton();
            this.Target = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.Run = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.easyVerilog = new System.Windows.Forms.Button();
            this.DevC = new System.Windows.Forms.Button();
            this.DevB = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DevA
            // 
            this.DevA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.DevA.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DevA.BackgroundImage")));
            this.DevA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DevA.FlatAppearance.BorderSize = 0;
            this.DevA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DevA.Location = new System.Drawing.Point(240, 0);
            this.DevA.Name = "DevA";
            this.DevA.Size = new System.Drawing.Size(161, 165);
            this.DevA.TabIndex = 0;
            this.DevA.Text = "Device A";
            this.DevA.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DevA.UseVisualStyleBackColor = false;
            this.DevA.Click += new System.EventHandler(this.DevA_Click);
            // 
            // TransQueue
            // 
            this.TransQueue.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransQueue.FormattingEnabled = true;
            this.TransQueue.ItemHeight = 14;
            this.TransQueue.Location = new System.Drawing.Point(11, 18);
            this.TransQueue.Name = "TransQueue";
            this.TransQueue.Size = new System.Drawing.Size(319, 172);
            this.TransQueue.TabIndex = 3;
            // 
            // AddTrans
            // 
            this.AddTrans.BackColor = System.Drawing.Color.Gold;
            this.AddTrans.FlatAppearance.BorderSize = 0;
            this.AddTrans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddTrans.Location = new System.Drawing.Point(336, 18);
            this.AddTrans.Name = "AddTrans";
            this.AddTrans.Size = new System.Drawing.Size(149, 103);
            this.AddTrans.TabIndex = 4;
            this.AddTrans.Text = "Add Transaction";
            this.AddTrans.UseVisualStyleBackColor = false;
            this.AddTrans.Click += new System.EventHandler(this.button4_Click);
            // 
            // TimetextBox
            // 
            this.TimetextBox.Location = new System.Drawing.Point(12, 94);
            this.TimetextBox.Name = "TimetextBox";
            this.TimetextBox.Size = new System.Drawing.Size(208, 27);
            this.TimetextBox.TabIndex = 8;
            // 
            // WordstextBox
            // 
            this.WordstextBox.Location = new System.Drawing.Point(16, 161);
            this.WordstextBox.Name = "WordstextBox";
            this.WordstextBox.Size = new System.Drawing.Size(204, 27);
            this.WordstextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 21);
            this.label4.TabIndex = 11;
            this.label4.Text = "Absolute Time";
            // 
            // Init
            // 
            this.Init.AutoSize = true;
            this.Init.Location = new System.Drawing.Point(12, 28);
            this.Init.Name = "Init";
            this.Init.Size = new System.Drawing.Size(89, 25);
            this.Init.TabIndex = 12;
            this.Init.TabStop = true;
            this.Init.Text = "Initiator";
            this.Init.UseVisualStyleBackColor = true;
            // 
            // Target
            // 
            this.Target.AutoSize = true;
            this.Target.Location = new System.Drawing.Point(119, 28);
            this.Target.Name = "Target";
            this.Target.Size = new System.Drawing.Size(80, 25);
            this.Target.TabIndex = 13;
            this.Target.TabStop = true;
            this.Target.Text = "Target";
            this.Target.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(208, 21);
            this.label5.TabIndex = 14;
            this.label5.Text = "No. Of Words/Transaction";
            // 
            // Run
            // 
            this.Run.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Run.FlatAppearance.BorderSize = 0;
            this.Run.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Run.Location = new System.Drawing.Point(336, 127);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(149, 63);
            this.Run.TabIndex = 15;
            this.Run.Text = "Run Simulation";
            this.Run.UseVisualStyleBackColor = false;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.easyVerilog);
            this.panel1.Controls.Add(this.DevC);
            this.panel1.Controls.Add(this.DevB);
            this.panel1.Controls.Add(this.DevA);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 166);
            this.panel1.TabIndex = 16;
            // 
            // easyVerilog
            // 
            this.easyVerilog.BackColor = System.Drawing.Color.DimGray;
            this.easyVerilog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.easyVerilog.FlatAppearance.BorderSize = 0;
            this.easyVerilog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.easyVerilog.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.easyVerilog.Location = new System.Drawing.Point(0, 0);
            this.easyVerilog.Name = "easyVerilog";
            this.easyVerilog.Size = new System.Drawing.Size(238, 165);
            this.easyVerilog.TabIndex = 3;
            this.easyVerilog.Text = "easyVerilog";
            this.easyVerilog.UseVisualStyleBackColor = false;
            this.easyVerilog.Click += new System.EventHandler(this.easyVerilog_Click);
            // 
            // DevC
            // 
            this.DevC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.DevC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DevC.BackgroundImage")));
            this.DevC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DevC.FlatAppearance.BorderSize = 0;
            this.DevC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DevC.Location = new System.Drawing.Point(574, 0);
            this.DevC.Name = "DevC";
            this.DevC.Size = new System.Drawing.Size(161, 165);
            this.DevC.TabIndex = 2;
            this.DevC.Text = "Device C";
            this.DevC.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DevC.UseVisualStyleBackColor = false;
            this.DevC.Click += new System.EventHandler(this.DevC_Click);
            // 
            // DevB
            // 
            this.DevB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.DevB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DevB.BackgroundImage")));
            this.DevB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DevB.FlatAppearance.BorderSize = 0;
            this.DevB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DevB.Location = new System.Drawing.Point(407, 0);
            this.DevB.Name = "DevB";
            this.DevB.Size = new System.Drawing.Size(161, 165);
            this.DevB.TabIndex = 1;
            this.DevB.Text = "Device B";
            this.DevB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DevB.UseVisualStyleBackColor = false;
            this.DevB.Click += new System.EventHandler(this.DevB_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TimetextBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Init);
            this.panel2.Controls.Add(this.Target);
            this.panel2.Controls.Add(this.WordstextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 166);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(238, 212);
            this.panel2.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TransQueue);
            this.panel3.Controls.Add(this.Run);
            this.panel3.Controls.Add(this.AddTrans);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(238, 166);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(497, 212);
            this.panel3.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(735, 378);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "EasyVerilog";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DevA;
        private System.Windows.Forms.ListBox TransQueue;
        private System.Windows.Forms.Button AddTrans;
        private System.Windows.Forms.TextBox TimetextBox;
        private System.Windows.Forms.TextBox WordstextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton Init;
        private System.Windows.Forms.RadioButton Target;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button easyVerilog;
        private System.Windows.Forms.Button DevC;
        private System.Windows.Forms.Button DevB;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}

