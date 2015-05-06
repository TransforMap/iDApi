using System;

namespace iDApi.XmlModel.osm
{
	public class member
	{
		public long _ref {
			get;
			set;
		}
		public string role {
			get;
			set;
		}

		/// <summary>
		/// Can be either "node", "way" or "relation".
		/// </summary>
		public string type {
			get;
			set;
		}

		public member ()
		{
		}
	}
}