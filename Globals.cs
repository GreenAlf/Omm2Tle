using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omm2Tle
{
	//NOTE: global vars and funcs cuz I can ¯\_(ツ)_/¯
	internal static class Globals
	{
		public static int minArgsSilent = 3;
		public static string in_mask = "*.txt";
		public static string extension = "txt";
		public static string input = "test\\input";
		public static string output = "test\\output";

		public static void ShowHelp()
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("=== OMM to TLE Converter Utility v1.0 by Alexey Babenko ===");
			Console.ResetColor();
			Console.WriteLine("Converting satellite orbit parameters from CSV (OMM) format to classic TLE.\n");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("USAGE:");
			Console.ResetColor();
			Console.WriteLine("{0, -4}Omm2Tle.exe [OMM_dir] [TLE_dir] [options]\n", "");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("OPTIONS:");
			Console.ResetColor();

			//NOTE: {padding, colWidth} {description}
			string rowFormat = "  {0, -20} {1}";

			Console.WriteLine(rowFormat, "-h, --help", "Show this help information.");
			Console.WriteLine(rowFormat, "-s, --silent", "Run in console mode without opening the GUI window.");
			Console.WriteLine(rowFormat, "-i, --in_mask", "Input file mask (*.txt by default).");
			Console.WriteLine(rowFormat, "-e, --extension", "Output file extension (txt by default).");
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("EXAMPLES:");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("Standard launch with GUI: ");
			Console.ResetColor();
			Console.WriteLine("Omm2Tle.exe");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("Silent conversion: ");
			Console.ResetColor();
			Console.WriteLine(".\\bin\\Debug\\Omm2Tle.exe .\\test\\input\\ .\\test\\output\\ --silent --in_mask *.csv --extension txt");

			Console.WriteLine("\n-------------------------------------------------------------\n");
		}

		//TODO: mb add localization later?
		public static void ShowHelpRu()
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("=== OMM to TLE Converter Utility v1.0 by GreenAlf ===");
			Console.ResetColor();
			Console.WriteLine("Конвертация параметров орбиты спутников из формата CSV (OMM) в классический TLE.\n");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("ИСПОЛЬЗОВАНИЕ:");
			Console.ResetColor();
			Console.WriteLine("{0, -4}Omm2Tle.exe [папка_с_OMM] [папка_для_TLE] [опции]\n", "");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("ОПЦИИ:");
			Console.ResetColor();

			//NOTE: {padding, colWidth} {description}
			string rowFormat = "  {0, -20} {1}";

			Console.WriteLine(rowFormat, "-h, --help", "Показать эту справочную информацию.");
			Console.WriteLine(rowFormat, "-s, --silent", "Запуск в режиме консоли без открытия графического окна WinForms.");
			Console.WriteLine(rowFormat, "-i, --in_mask", "Маска входных файлов (*.txt по умолчанию).");
			Console.WriteLine(rowFormat, "-e, --extension", "Расширение выходных файлов (txt по умолчанию).");
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("ПРИМЕРЫ:");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("  Обычный запуск с формой:  ");
			Console.ResetColor();
			Console.WriteLine("Omm2Tle.exe");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("  Тихая конвертация:        ");
			Console.ResetColor();
			Console.WriteLine(".\\bin\\Debug\\Omm2Tle.exe .\\test\\input\\ .\\test\\output\\ --silent --in_mask *.csv --extension txt");

			Console.WriteLine("\n-------------------------------------------------------------\n");
		}

		public static bool Process(string[] args)
		{
			int minArgsSilent = Globals.minArgsSilent;
			string in_mask = Globals.in_mask;
			string extension = Globals.extension;
			List<OmmModel> data = new List<OmmModel>();

			if (args.Length < minArgsSilent)
			{
				ErrorLine("\nERROR: not enough arguments!");
				ErrorLine("Arguments count: " + args.Length);
				Globals.ShowHelp();
				DetachConsole();
				return false;
			}

			string input = args[0];
			string output = args[1];
			var filteredArgs = args.Skip(minArgsSilent).ToArray();

			for (int i = 0; i < filteredArgs.Length; i++)
			{
				var item = filteredArgs[i];
				switch (item)
				{
					case "help":
					case "--help":
					case "-help":
					case "/help":
						{
							break;
						}
					case "-s":
					case "--silent":
						{
							break;
						}
					case "-i":
					case "--in_mask":
						{
							if (++i < filteredArgs.Length)
							{
								in_mask = filteredArgs[i];
							}
							else
							{
								ErrorLine("ERROR: undefined in_mask! Abort.");
								return false;
							}
							break;
						}
					case "-e":
					case "--extension":
						{
							if (++i < filteredArgs.Length)
							{
								extension = filteredArgs[i];
							}
							else
							{
								ErrorLine("ERROR: undefined extension! Abort.");
								return false;
							}
							break;
						}
					case "-t":
					case "--test":
						{
							break;
						}
					default:
						{
							ErrorLine("ERROR: unrecognized argument: " + item);
							return false;
						}
				}
			}

			try
			{
				if (!Directory.Exists(input))
				{
					ErrorLine("ERROR: input directory doesn't exist! Abort.");
					return false;
				}
				if (!Directory.Exists(output))
				{
					WarningLine("Warning: output directory doesn't exist. Creating.");
					Directory.CreateDirectory(output);
				}

				string[] files = Directory.GetFiles(input, in_mask, SearchOption.TopDirectoryOnly);
				Console.WriteLine($"Found {files.Length} files...");
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
						using (StreamWriter sw = new StreamWriter(
							Path.Combine(output, Path.ChangeExtension(Path.GetFileName(file), extension))))
						{
							string line;

							while ((line = sr.ReadLine()) != null)
							{
								sw.WriteLine(line);
							}
						}
						continue;
					}

					using (StreamWriter sw = new StreamWriter(
							Path.Combine(output, Path.ChangeExtension(Path.GetFileName(file), extension))))
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
				Console.WriteLine("Conversion finished.");
			}
			catch (Exception ex)
			{
				ErrorLine("ERROR: " + ex.Message);
			}

			return true;
		}

		public static void ErrorLine(string line)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(line);
			Console.ResetColor();
		}

		public static void WarningLine(string line)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(line);
			Console.ResetColor();
		}

		public static void DetachConsole()
		{
			//TODO: fix this for proper terminal output
			//FreeConsole();
			//SendKeys.SendWait("{ENTER}");
			//FreeConsole();
		}
	}
}
