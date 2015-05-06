using System;

namespace iDApi.XmlModel.osm
{
	public class relation
	{
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

		public relation ()
		{
		}
	}
}