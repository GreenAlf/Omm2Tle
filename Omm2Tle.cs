using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omm2Tle
{
	internal class Omm2Tle
	{
		public static (string line1, string line2) ConvertRowToTle(dynamic row)
		{
			string noradId = ((string)row.NORAD_CAT_ID).PadLeft(5, '0');
			string classification = string.IsNullOrEmpty((string)row.CLASSIFICATION_TYPE) ? "U" : (string)row.CLASSIFICATION_TYPE;
			string objectId = (string)row.OBJECT_ID;
			string launchYear = objectId.Substring(2, 2);
			string launchNum = objectId.Substring(5, 3).PadLeft(3, '0');
			string launchPiece = objectId.Substring(8).PadRight(3, ' ');

			DateTime epochDate = DateTime.Parse((string)row.EPOCH, CultureInfo.InvariantCulture);
			string epochYear = epochDate.ToString("yy");
			double dayOfYear = epochDate.DayOfYear + epochDate.TimeOfDay.TotalDays;
			string epochDays = dayOfYear.ToString("000.00000000", CultureInfo.InvariantCulture);

			string meanMotionDot = FormatTleDecimal((double)row.MEAN_MOTION_DOT, 8, true); // .DDDDDDDD
			string meanMotionDdot = FormatExp((double)row.MEAN_MOTION_DDOT);              // DDddd-d
			string bstar = FormatExp((double)row.BSTAR);                                  // DDddd-d
			string elemSet = ((string)row.ELEMENT_SET_NO).PadLeft(4, ' ');

			StringBuilder l1 = new StringBuilder();
			l1.Append("1 ");
			l1.Append($"{noradId}{classification} ");
			l1.Append($"{launchYear}{launchNum}{launchPiece} ");
			l1.Append($"{epochYear}{epochDays} ");
			l1.Append($"{meanMotionDot} ");
			l1.Append($"{meanMotionDdot} ");
			l1.Append($"{bstar} 0 ");
			l1.Append($"{elemSet}");
			string line1 = AddChecksum(l1.ToString());

			string inc = ((double)row.INCLINATION).ToString("0.0000", CultureInfo.InvariantCulture).PadLeft(8, ' ');
			string raan = ((double)row.RA_OF_ASC_NODE).ToString(".0000", CultureInfo.InvariantCulture).PadLeft(8, ' ');
			//TODO: не round, а просто отрезают символы, нужно уточнить
			string ecc = ((double)row.ECCENTRICITY).ToString(".00000000", CultureInfo.InvariantCulture).Substring(1, 7); 
			string argPer = ((double)row.ARG_OF_PERICENTER).ToString(".0000", CultureInfo.InvariantCulture).PadLeft(8, ' ');
			string meanAnom = ((double)row.MEAN_ANOMALY).ToString(".0000", CultureInfo.InvariantCulture).PadLeft(8, ' ');
			string meanMotion = ((double)row.MEAN_MOTION).ToString(".00000000", CultureInfo.InvariantCulture).PadLeft(11, ' ');
			string revAtEpoch = ((string)row.REV_AT_EPOCH).PadLeft(5, ' ');

			StringBuilder l2 = new StringBuilder();
			l2.Append("2 ");
			l2.Append($"{noradId} ");
			l2.Append($"{inc} ");
			l2.Append($"{raan} ");
			l2.Append($"{ecc} ");
			l2.Append($"{argPer} ");
			l2.Append($"{meanAnom} ");
			l2.Append($"{meanMotion}");
			l2.Append($"{revAtEpoch}");
			string line2 = AddChecksum(l2.ToString());

			return (line1, line2);
		}

		private static string FormatTleDecimal(double value, int length, bool removeLeadingZero)
		{
			string sign = value < 0 ? "-" : " ";
			string num = Math.Abs(value).ToString($".00000000", CultureInfo.InvariantCulture);
			if (removeLeadingZero && num.StartsWith("0")) num = num.Substring(1);
			return (sign + num).Substring(0, length+2);
		}

		private static string FormatExp(double value)
		{
			if (value == 0) return " 00000+0";
			string sign = value < 0 ? "-" : " ";
			value = Math.Abs(value);

			int exponent = (int)Math.Floor(Math.Log10(value)) + 1;
			double mantissa = value / Math.Pow(10, exponent);

			string mantissaStr = mantissa.ToString(".00000", CultureInfo.InvariantCulture).Substring(1); // убираем "0."
			string expStr = exponent >= 0 ? $"+{exponent}" : $"{exponent}";

			return $"{sign}{mantissaStr}{expStr}";
		}

		private static string AddChecksum(string line)
		{
			int sum = 0;
			foreach (char c in line)
			{
				if (char.IsDigit(c)) sum += (c - '0');
				else if (c == '-') sum += 1;
			}
			return $"{line.PadRight(68, ' ')}{sum % 10}";
		}
	}
}
