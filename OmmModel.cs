using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omm2Tle
{
	public class OmmModel
	{
		public string OBJECT_NAME { get; set; }
		public string OBJECT_ID { get; set; }
		public string EPOCH { get; set; }
		public double MEAN_MOTION { get; set; }
		public double ECCENTRICITY { get; set; }
		public double INCLINATION { get; set; }
		public double RA_OF_ASC_NODE { get; set; }
		public double ARG_OF_PERICENTER { get; set; }
		public double MEAN_ANOMALY { get; set; }
		public int EPHEMERIS_TYPE { get; set; }
		public string CLASSIFICATION_TYPE { get; set; }
		public string NORAD_CAT_ID { get; set; }
		public string ELEMENT_SET_NO { get; set; }
		public string REV_AT_EPOCH { get; set; }
		public double BSTAR { get; set; }
		public double MEAN_MOTION_DOT { get; set; }
		public double MEAN_MOTION_DDOT { get; set; }
	}
}
