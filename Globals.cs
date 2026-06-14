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
			Console.WriteLine("=== OMM to TLE Converter Utility v1.1 by Alexey Babenko ===");
			Console.ResetColor();
			Console.WriteLine("Converting satellite orbit parameters from CSV (OMM) format to classic TLE.\n");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("USAGE:");
			Console.ResetColor();
			Console.WriteLine("{0, -4}Omm2Tle.exe [OMM_dir or File] [TLE_dir or File] [options]\n", "");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("OPTIONS:");
			Console.ResetColor();

			//NOTE: {padding, colWidth} {description}
			string rowFormat = "  {0, -20} {1}";

			Console.WriteLine(rowFormat, "-h, --help", "Show this help information.");
			Console.WriteLine(rowFormat, "-s, --silent", "Run in console mode without opening the GUI window.");
			Console.WriteLine(rowFormat, "-i, --in_mask", "Input file mask (*.txt by default).");
			Console.WriteLine(rowFormat, "-e, --extension", "Output file extension (txt by default).");
			Console.WriteLine(rowFormat, "-a, --add", "Process selected satellites (can be used multiple times).");
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\nEXAMPLES:");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("\tStandard launch with GUI: ");
			Console.ResetColor();
			Console.WriteLine("\tOmm2Tle.exe");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("\n\tSilent conversion: ");
			Console.ResetColor();
			Console.WriteLine("\t.\\bin\\Debug\\Omm2Tle.exe .\\test\\input\\ .\\test\\output\\ --silent --in_mask *.csv --extension txt");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("\n\tConversion for selected satellite: ");
			Console.ResetColor();
			Console.WriteLine("\t.\\bin\\Debug\\Omm2Tle.exe weather.csv selected.txt --silent --in_mask *.csv -a \"FENGYUN 3F\" -a \"NOAA 20 (JPSS-1)\"");

			Console.WriteLine("\n-------------------------------------------------------------\n");
		}

		public static bool Process(string[] args)
		{
			int minArgsSilent = Globals.minArgsSilent;
			string in_mask = Globals.in_mask;
			string extension = Globals.extension;
			List<OmmModel> data = new List<OmmModel>();
			List<string> selected_sats = new List<string>();

			Console.WriteLine("\n");
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

			//foreach (string arg in filteredArgs)
			//{
			//	WarningLine(arg);
			//}

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
					case "-a":
					case "--add":
						{
							if (++i < filteredArgs.Length)
							{
								selected_sats.Add(filteredArgs[i]);
							}
							else
							{
								ErrorLine("ERROR: sat name was not provided! Abort.");
								return false;
							}
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
				if (!Directory.Exists(Path.GetDirectoryName(input)))
				{
					ErrorLine("ERROR: input directory doesn't exist! Abort.");
					return false;
				}
				if (!Directory.Exists(Path.GetDirectoryName(output)))
				{
					WarningLine("Warning: output directory doesn't exist. Creating.");
					if (IsDirectoryPath(output))
					{
						Directory.CreateDirectory(output);
					}
				}

				//NOTE: проверяем файл или директория на входе
				string[] files = null;
				if (IsDirectoryPath(input))
				{
					files = Directory.GetFiles(input, in_mask, SearchOption.TopDirectoryOnly);
				}
				if(IsFilePath(input))
				{
					files = new[] { input };
				}
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
					string output_path = null;
					if(IsDirectoryPath(output))
					{
						output_path = Path.Combine(output, Path.ChangeExtension(Path.GetFileName(file), extension));
					}
					if(IsFilePath(output))
					{
						output_path = output;
					}
					using (StreamWriter sw = new StreamWriter(output_path))
					{
						foreach (var sat in data)
						{
							//NOTE: basic mode
							if (selected_sats.Count == 0)
							{
								ProcessSat(sw, sat);
							}
							else
							{
								if (selected_sats.Contains(sat.OBJECT_NAME))
								{
									ProcessSat(sw, sat);
								}
							}
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

		private static void ProcessSat(StreamWriter sw, OmmModel sat)
		{
			Console.WriteLine("Processing sat: " + sat.OBJECT_NAME);
			var (line1, line2) = Omm2Tle.ConvertRowToTle(sat);
			sw.WriteLine(sat.OBJECT_NAME);
			sw.WriteLine(line1);
			sw.WriteLine(line2);
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

		public static bool IsFilePath(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return false;

			//NOTE: Если путь заканчивается на разделитель - это директория
			if (path.EndsWith(Path.DirectorySeparatorChar.ToString()) ||
				path.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
				return false;

			return Path.HasExtension(path);
		}

		public static bool IsDirectoryPath(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return false;

			//NOTE: Если путь заканчивается на разделитель - это директория
			if (path.EndsWith(Path.DirectorySeparatorChar.ToString()) ||
				path.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
				return true;

			return !Path.HasExtension(path);
		}
	}
}
