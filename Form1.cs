using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omm2Tle
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
		}

		private void LoadConsole()
		{
			var _textBoxWriter = new TextBoxWriter(t_output, 100);
			Console.SetOut(_textBoxWriter);
			Console.SetError(_textBoxWriter);
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			LoadConsole();
			t_output.Focus();
			Globals.ShowHelp();

			t_inputDir.Text = Globals.input;
			t_inMask.Text = Globals.in_mask;
			t_outputDir.Text = Globals.output;
			t_extension.Text = Globals.extension;
		}

		private void b_browseSrc_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
			{
				folderDialog.Description = "Select Source directory";
				folderDialog.ShowNewFolderButton = true;
				if (folderDialog.ShowDialog() == DialogResult.OK)
				{
					t_inputDir.Text = folderDialog.SelectedPath;
				}
			}
		}

		private void b_browseOut_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
			{
				folderDialog.Description = "Select Result directory";
				folderDialog.ShowNewFolderButton = true;
				if (folderDialog.ShowDialog() == DialogResult.OK)
				{
					t_outputDir.Text = folderDialog.SelectedPath;
				}
			}
		}

		private void b_convert_Click(object sender, EventArgs e)
		{
			Globals.Process(new string[] {
				t_inputDir.Text, 
				t_outputDir.Text,
				"--silent",							//NOTE: cuz I am too lazy to fix
				"--in_mask", t_inMask.Text,
				"--extension", t_extension.Text
			});
		}
	}
}
