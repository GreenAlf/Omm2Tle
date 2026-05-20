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
			string filemask = "*.txt";
			List<OmmModel> data = new List<OmmModel>();

			bool isSilent = args.Any(arg => arg.Equals("--silent", StringComparison.OrdinalIgnoreCase))
						|| args.Any(arg => arg.Equals("-s", StringComparison.OrdinalIgnoreCase));

			bool isHelp = args.Any(arg => arg.Equals("help", StringComparison.OrdinalIgnoreCase))
						|| args.Any(arg => arg.Equals("--help", StringComparison.OrdinalIgnoreCase))
						|| args.Any(arg => arg.Equals("-help", StringComparison.OrdinalIgnoreCase))
						|| args.Any(arg => arg.Equals("//help", StringComparison.OrdinalIgnoreCase));

			if ((isSilent || isHelp) && !AttachConsole(ATTACH_PARENT_PROCESS))
			{
				AllocConsole();
			}

			if(isHelp)
			{
				ShowHelp();
				DetachConsole();
				return;
			}

			if (isSilent)
			{
				if(args.Length < 3)
				{
					ErrorLine("\nERROR: not enough arguments!");
					ErrorLine("Arguments count: " + args.Length);
					ShowHelp();
					DetachConsole();
					return;
				}

				string input = args[0];
				string output = args[1];
				var filteredArgs = args.Skip(2).ToArray();
				//TODO: processing additional args here, mask and Searchoption mb

				try
				{
					if(!Directory.Exists(input))
					{
						ErrorLine("ERROR: input directory doesn't exist! Abort.");
						return;
					}
					if(!Directory.Exists(output))
					{
						Console.WriteLine("Warning: output directory doesn't exist. Creating.");
						Directory.CreateDirectory(output);
					}

					string[] files = Directory.GetFiles(input, filemask, SearchOption.TopDirectoryOnly);
					foreach (string file in files)
					{
						data.Clear();
						Console.WriteLine("Processing file: " + file);
						try
						{
							data = CsvParser.ParseCsvFile(file);
						}
						catch (Exception ex)
						{
							//TODO: add FORWARD mode
							ErrorLine("ERROR: " + ex.ToString());
							using (StreamReader sr = new StreamReader(file))
							using (StreamWriter sw = new StreamWriter(Path.Combine(output, Path.GetFileName(file))))
							{
								string line;

								while ((line = sr.ReadLine()) != null)
								{
									//if (string.IsNullOrWhiteSpace(line)) continue;
									sw.WriteLine(line);
								}
							}
							continue;
						}
						
						using (StreamWriter sw = new StreamWriter(Path.Combine(output, Path.GetFileName(file))))
						{
							foreach (var sat in data)
							{
								Console.WriteLine("Processing sat: " + sat.OBJECT_NAME);
								var (line1, line2) = Omm2Tle.ConvertRowToTle(sat);
								sw.WriteLine(sat.OBJECT_NAME);
								sw.WriteLine(line1);
								sw.WriteLine(line2);
							}
						}
					}
				}
				catch (Exception ex)
				{
					ErrorLine("ERROR: " + ex.Message);
				}
				finally
				{

				}
				return;
			}


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		private static void ShowHelp()
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("=== OMM to TLE Converter Utility v1.0 ===");
			Console.ResetColor();
			Console.WriteLine("Конвертация параметров орбиты спутников из формата CSV (OMM) в классический TLE.\n");
			
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("ИСПОЛЬЗОВАНИЕ:");
			Console.ResetColor();
			Console.WriteLine("{0, -4}Omm2Tle.exe [папка_с_OMM] [папка_для_TLE] [опции]\n", "");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("ОПЦИИ:");
			Console.ResetColor();

			//NOTE: {отступ, ширина_колонки_флагов} {описание}
			string rowFormat = "  {0, -20} {1}";

			Console.WriteLine(rowFormat, "-h, --help", "Показать эту справочную информацию.");
			Console.WriteLine(rowFormat, "-s, --silent", "Запуск в режиме консоли без открытия графического окна WinForms.");
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("ПРИМЕРЫ:");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("  Обычный запуск с формой:  ");
			Console.ResetColor();
			Console.WriteLine("Omm2Tle.exe");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("  Тихая конвертация файла:  ");
			Console.ResetColor();
			Console.WriteLine("Omm2Tle.exe \"C:\\data\\sat.csv\" --silent");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("  Конвертация с сохранением: ");
			Console.ResetColor();
			Console.WriteLine("Omm2Tle.exe data.csv -s -o output.txt");
			Console.WriteLine("\n-------------------------------------------------------------\n");
		}

		private static void DetachConsole()
		{
			//TODO: not working!
			//FreeConsole();
			SendKeys.SendWait("{ENTER}");
			FreeConsole();
		}

		private static void ErrorLine(string line)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(line);
			Console.ResetColor();
		}
	}
}
