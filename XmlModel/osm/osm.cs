using System;
using System.Collections.Generic;

namespace iDApi.XmlModel.osm
{
	public class osm {
		public float version {
			get;
			set;
		}
		public string generator {
			get;
			set;
		}
		public string copyright {
			get;
			set;
		}
		public string attribution {
			get;
			set;
		}
		public string license {
			get;
			set;
		}

		public bounds bounds {
			get;
			set;
		}

		public List<node> nodes {
			get;
			set;
		}

		public List<way> ways {
			get;
			set;
		}
	}
}