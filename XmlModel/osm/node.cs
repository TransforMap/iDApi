using System;

namespace iDApi.XmlModel.osm
{
	public class node
	{
		public long id {
			get;
			set;
		}
		public double lat {
			get;
			set;
		}
		public double lon {
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
		public long version {
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
	}
}