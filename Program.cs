using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;

namespace Omm2Tle
{
	internal static class Program
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool AttachConsole(uint dwProcessId);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool FreeConsole();

		private const uint ATTACH_PARENT_PROCESS = 0x0FFFFFFFF;

		[STAThread]
		static void Main(string[] args)
		{
			bool isSilent = args.Any(arg => arg.Equals("--silent", StringComparison.OrdinalIgnoreCase))
						 || args.Any(arg => arg.Equals("-s", StringComparison.OrdinalIgnoreCase));
			bool isHelp = args.Any(arg => arg.Equals("help", StringComparison.OrdinalIgnoreCase))
					   || args.Any(arg => arg.Equals("--help", StringComparison.OrdinalIgnoreCase))
					   || args.Any(arg => arg.Equals("-help", StringComparison.OrdinalIgnoreCase))
					   || args.Any(arg => arg.Equals("/help", StringComparison.OrdinalIgnoreCase));
			//TODO: implement selftest!
			bool isSelftest = args.Any(arg => arg.Equals("--test", StringComparison.OrdinalIgnoreCase))
				           || args.Any(arg => arg.Equals("-t", StringComparison.OrdinalIgnoreCase));

			if ((isSilent || isHelp) && !AttachConsole(ATTACH_PARENT_PROCESS))
			{
				AllocConsole();
			}

			if(isHelp)
			{
				Globals.ShowHelp();
				Globals.DetachConsole();
				return;
			}

			if (isSilent)
			{
				Globals.Process(args);
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
