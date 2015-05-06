using System;
using System.Collections.Generic;

namespace iDApi.XmlModel.osm
{
	public class way {
		public long id {
			get;
			set;
		}

		public string user {
			get;
			set;
		}

		public long uid {
			get;
			set;
		}

		public bool visible {
			get;
			set;
		} 

		public int version {
			get;
			set;
		}

		public long changeset {
			get;
			set;
		}

		public DateTime timestamp {
			get;
			set;
		}

		public List<nd> nodeRefs {
			get;
			set;
		}
	}
}