using System;

namespace Samples
{
	[Serializable]
	public class Publisher
	{
		[NonSerialized]
		string _website;
		public string Name{set;get;}
		public string Country {set;get;}
		public string WebSite {
		  set{_website = value; }
		  get{ return _website; }
		}
	}
}

