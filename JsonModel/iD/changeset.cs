using System;
using System.Collections.Generic;

namespace iDApi.JsonModel.iD
{
	public class changeset
	{
		public List<item> created {
			get;
			set;
		}

		public List<item> modified {
			get;
			set;
		}

		public List<item> deleted {
			get;
			set;
		}
	}
}