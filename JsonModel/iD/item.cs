using System;
using System.Collections.Generic;

namespace iDApi.JsonModel.iD
{
	public class item {
		public string id {
			get;
			set;
		}

		public bool visible {
			get;
			set;
		}

		public List<tag> tags {
			get;
			set;
		}

		public long v {
			get;
			set;
		}

		public List<string> nodes {
			get;
			set;
		}
	}
}

