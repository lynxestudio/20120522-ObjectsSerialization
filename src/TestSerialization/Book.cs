using System;

namespace Samples
{
	[Serializable]
	public class Book
	{
		[NonSerialized]
		DateTime _created = DateTime.Now;
		[NonSerialized]
		double _version = 1.0;
		public string Title{set;get;}
		public int Numpages {set;get;}
		public DateTime PublicationDate {set;get;}
		public Author Author {set;get;}
		public Publisher Publisher {set;get;}
		
		public DateTime Created {get { return _created; }}
		public double Version{get{ return _version; }}
	}
}

