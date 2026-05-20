using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omm2Tle
{
	internal class CsvParser
	{
		public static List<OmmModel> ParseCsvText(string rawCsvText)
		{
			var config = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				PrepareHeaderForMatch = args => args.Header.Trim(),
				IgnoreBlankLines = true
			};

			using (var reader = new StringReader(rawCsvText))
			using (var csv = new CsvReader(reader, config))
			{
				return csv.GetRecords<OmmModel>().ToList();
			}
		}

		public static List<OmmModel> ParseCsvFile(string filePath)
		{
			var config = new CsvConfiguration(CultureInfo.InvariantCulture);

			using (var reader = new StreamReader(filePath))
			using (var csv = new CsvReader(reader, config))
			{
				return csv.GetRecords<OmmModel>().ToList();
			}
		}
	}
}
