# Serialización de Objetos en .NET

Serialización se denomina al proceso de transferir un objeto que se ejecuta en la memoria de un programa hacia un medio de persistencia para conservar su estado y sus datos en ese momento, para deserializarlo o bien volver a recuperarlo cuando se considere conveniente, en algunos casos el mecanismo de serialización/deserialización es más práctico que utilizar las clases readers/writers para persistir.

En .NET existen tres tipos de serialización: Binaria, XML y Soap los cuales nos permiten cierta independencia del medio de transporte. La serialización binaria es la más sencilla, óptima y ofrece un mejor rendimeinto que los otros tipos de serialización, aunque no es recomendada si los objetos serializados requieren ser transportados hacia otros sistemas operativos, lenguajes de programación o arquitecturas que no sean .NET, por lo que para el tema de interoperabilidad las mejores opciones son la serialización de objetos en XML ó SOAP.

La serialización en .NET consiste básicamente en dos responsabilidades: una es la responsabilidad del objeto serializable el cuál indica los miembros a serializar y la otra es la del serializador/deserializador la cuál se encarga de transferir el objeto hacia o desde un medio de persistencia. Supongamos una aplicación encargada de administrar una colección de libros, esta aplicación tiene una clase Author la cuál queremos serializar, primeramente marcamos la clase con el atributo:

[Serializable]
Hay que tener presente que no todas variable de instancia puede ser serializables, por ejemplo los metodos o variables estáticas no lo son, al contrario de los tipos primitivos, los arreglos y los objetos string los cuales son serializables de forma predeterminada. Siempre hay que asegurarse que los objetos y su clase base sean serializables antes de asumir que cualquier tipo de objeto puede ser serializado. Si encontramos o necesitamos que una propiedad del objeto no sea serializable la marcamos con el atributo:
[NonSerialized]
A continuación mostramos el código fuente de una aplicación que muestra la serialización y deserialización binaria de una relación de objetos mediante la utilización de la clase BinaryFormatter. Los archivos que componen la aplicación son: Author, Book , Publisher y la clase principal TestSerialization la cuál implementa todo el código de forma concreta.
La clase Author


La clase Book


La clase Publisher


La clase TestSerialization
<pre>
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
</pre>
Este ejemplo básicamente crea un grafo de una relación de clases y lo serializa (lo pasa de la memoria hacia un medio de almacenamiento) en un archivo binario e inmediatamente lo deserializa, (del medio de almacenamiento lo trae a la memoria).En todo este proceso (excepto en donde se indique que no) se convierte el objeto Book,sus clases relacionadas y sus miembros en un flujo de bytes hacia el disco duro para posteriormente recuperarlo con el mismo proceso en orden inverso. Todo esto se logra en esencia con las siguientes líneas.Para serializar y deserializar respectivamente:

using(stream = new FileStream("Book.bin",FileMode.OpenOrCreate,FileAccess.Write)){
   binaryformatter.Serialize(stream,b);
 }
using(stream = new FileStream("Book.bin",FileMode.Open,FileAccess.Read)){
   b = (Book)binaryformatter.Deserialize(stream);
}
Al compilar y ejecutar el programa, mediante los siguientes comandos:
$ mcs /t:library Author.cs Book.cs Publisher.cs /out:Samples.dll
$ mcs -r:Samples.dll TestSerialization.cs
$ mono TestSerialization.cs



Mostramos la serialización y deserialización de un grafo de objetos relacionados.


