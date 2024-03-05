using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Samples;

namespace Samples{
  public class TestSerialization{
    static FileStream stream = null;
    static BinaryFormatter binaryformatter = new BinaryFormatter();
    static Book b;
    public static int Main(string[] args){
      Console.WriteLine("Enter the Author ");
      Console.Write("Name: ");
      string na = Console.ReadLine();
      Console.Write("Last Name: ");
      string la = Console.ReadLine();
      Console.Write("Organization: ");
      string or = Console.ReadLine();
      Console.Write("Country: ");
      string ac = Console.ReadLine();
      Console.WriteLine("Enter the book");
      Console.Write("Title: ");
      string bt = Console.ReadLine();
      Console.Write("Publication's date: ");
      string pd = Console.ReadLine();
      Console.Write("Number of pages: ");
      string np = Console.ReadLine();
      Console.WriteLine("Enter the publisher");
      Console.Write("Name: ");
      string pn = Console.ReadLine();
      Console.Write("Country: ");
      string pc = Console.ReadLine();
      b = new Book{
	Title = bt,
	Numpages = Convert.ToInt32(np),
	PublicationDate = Convert.ToDateTime(pd),
	Author = new Author{
	  Name = na,
	  LastName = la,
	  Organization = or,
	  Country = ac
	},
	Publisher = new Publisher{
	  Name = pn,
	  Country = pc
	}
      };
      try{
	Console.WriteLine("Serializing ...");
	using(stream = new FileStream("Book.bin",FileMode.OpenOrCreate,FileAccess.Write)){
	  binaryformatter.Serialize(stream,b);
	}
	Console.WriteLine("Done.");
	Console.WriteLine("Deserializing ...");
	using(stream = new FileStream("Book.bin",FileMode.Open,FileAccess.Read)){
	  b = (Book)binaryformatter.Deserialize(stream);
	  Console.WriteLine("======== Get the Author ========");
	  Console.WriteLine("Name [{0}]",b.Author.Name);
	  Console.WriteLine("Last Name [{0}]",b.Author.LastName);
	  Console.WriteLine("Organization [{0}]",b.Author.Organization);
	  Console.WriteLine("Country [{0}]",b.Author.Country);
	  Console.WriteLine("======== Get the Book ========");
	  Console.WriteLine("Title [{0}]",b.Title);
	  Console.WriteLine("Numpages [{0}]",b.Numpages);
	  Console.WriteLine("Publication's Date [{0}]",b.PublicationDate);
	  Console.WriteLine("======== Get the Publisher ========");
	  Console.WriteLine("Name [{0}]",b.Publisher.Name);
	  Console.WriteLine("Country [{0}]",b.Publisher.Country);
	}
      }catch(SerializationException ex){
	Console.WriteLine(ex.Message);
      }
      return 0;
    }
  }
}
