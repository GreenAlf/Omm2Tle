namespace Omm2Tle
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.t_inputDir = new System.Windows.Forms.TextBox();
			this.t_inMask = new System.Windows.Forms.TextBox();
			this.b_browseSrc = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.t_outputDir = new System.Windows.Forms.TextBox();
			this.b_browseOut = new System.Windows.Forms.Button();
			this.t_extension = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.b_convert = new System.Windows.Forms.Button();
			this.t_output = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.b_browseSrc);
			this.groupBox1.Controls.Add(this.t_inMask);
			this.groupBox1.Controls.Add(this.t_inputDir);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(594, 82);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Source";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "OMM folder:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "File mask:";
			// 
			// t_inputDir
			// 
			this.t_inputDir.Location = new System.Drawing.Point(83, 24);
			this.t_inputDir.Name = "t_inputDir";
			this.t_inputDir.Size = new System.Drawing.Size(420, 20);
			this.t_inputDir.TabIndex = 1;
			// 
			// t_inMask
			// 
			this.t_inMask.Location = new System.Drawing.Point(83, 50);
			this.t_inMask.Name = "t_inMask";
			this.t_inMask.Size = new System.Drawing.Size(100, 20);
			this.t_inMask.TabIndex = 3;
			// 
			// b_browseSrc
			// 
			this.b_browseSrc.Location = new System.Drawing.Point(509, 22);
			this.b_browseSrc.Name = "b_browseSrc";
			this.b_browseSrc.Size = new System.Drawing.Size(75, 23);
			this.b_browseSrc.TabIndex = 2;
			this.b_browseSrc.Text = "Browse";
			this.b_browseSrc.UseVisualStyleBackColor = true;
			this.b_browseSrc.Click += new System.EventHandler(this.b_browseSrc_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.t_extension);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.b_browseOut);
			this.groupBox2.Controls.Add(this.t_outputDir);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Location = new System.Drawing.Point(12, 100);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(594, 82);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Result";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Output folder:";
			// 
			// t_outputDir
			// 
			this.t_outputDir.Location = new System.Drawing.Point(83, 24);
			this.t_outputDir.Name = "t_outputDir";
			this.t_outputDir.Size = new System.Drawing.Size(420, 20);
			this.t_outputDir.TabIndex = 4;
			// 
			// b_browseOut
			// 
			this.b_browseOut.Location = new System.Drawing.Point(509, 22);
			this.b_browseOut.Name = "b_browseOut";
			this.b_browseOut.Size = new System.Drawing.Size(75, 23);
			this.b_browseOut.TabIndex = 5;
			this.b_browseOut.Text = "Browse";
			this.b_browseOut.UseVisualStyleBackColor = true;
			this.b_browseOut.Click += new System.EventHandler(this.b_browseOut_Click);
			// 
			// t_extension
			// 
			this.t_extension.Location = new System.Drawing.Point(83, 50);
			this.t_extension.Name = "t_extension";
			this.t_extension.Size = new System.Drawing.Size(100, 20);
			this.t_extension.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Extension:";
			// 
			// b_convert
			// 
			this.b_convert.Location = new System.Drawing.Point(521, 188);
			this.b_convert.Name = "b_convert";
			this.b_convert.Size = new System.Drawing.Size(75, 23);
			this.b_convert.TabIndex = 7;
			this.b_convert.Text = "Convert";
			this.b_convert.UseVisualStyleBackColor = true;
			this.b_convert.Click += new System.EventHandler(this.b_convert_Click);
			// 
			// t_output
			// 
			this.t_output.BackColor = System.Drawing.SystemColors.Window;
			this.t_output.HideSelection = false;
			this.t_output.Location = new System.Drawing.Point(12, 217);
			this.t_output.Multiline = true;
			this.t_output.Name = "t_output";
			this.t_output.ReadOnly = true;
			this.t_output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.t_output.Size = new System.Drawing.Size(594, 221);
			this.t_output.TabIndex = 8;
			this.t_output.WordWrap = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(618, 450);
			this.Controls.Add(this.t_output);
			this.Controls.Add(this.b_convert);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Omm2Tle";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button b_browseSrc;
		private System.Windows.Forms.TextBox t_inMask;
		private System.Windows.Forms.TextBox t_inputDir;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox t_extension;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button b_browseOut;
		private System.Windows.Forms.TextBox t_outputDir;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button b_convert;
		private System.Windows.Forms.TextBox t_output;
	}
}

