using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Omm2Tle
{
	internal class TextBoxWriter : StringWriter
	{
		private TextBox _textBox;
		private int _maxLines;

		public override Encoding Encoding => Encoding.UTF8;

		public TextBoxWriter(TextBox textBox, int maxLines = 100)
		{
			_textBox = textBox;
			_maxLines = maxLines;
		}

		public override void WriteLine(string value)
		{
			if (string.IsNullOrEmpty(value))
				return;

			if (_textBox.InvokeRequired)
			{
				_textBox.Invoke(new Action<string>(WriteLine), value);
				return;
			}
			Appender(value);
		}

		public override void Write(string value)
		{
			if (string.IsNullOrEmpty(value))
				return;

			if (_textBox.InvokeRequired)
			{
				_textBox.Invoke(new Action<string>(Write), value);
				return;
			}
			Appender(value);
		}

		private void Appender(string value)
		{
			var lines = _textBox.Lines.ToList();
			lines.Add(value);
			if (lines.Count > _maxLines)
			{
				lines.RemoveAt(0);
			}
			_textBox.Focus();
			_textBox.Lines = lines.ToArray();
			_textBox.SelectionStart = _textBox.Text.Length;
			_textBox.ScrollToCaret();
		}

		public override void Flush()
		{
			base.Flush();
		}
	}
}
