// Prototype Design Pattern - Creational Category

// The formal definition of the prototype pattern is the following: 
// Specify the kinds of objects to create using a prototypical instance and create new objects by copying this prototype.

// What this basically says is instead of using ‘new’ to create a new object we’re going to use a prototype, 
// an existing object to specify the new objects we’re going to create. Then we create new objects by copying from this prototype. 
// So the prototype is a master, a blueprint and the other objects we create will be copies of that object. 
// Another word that can be used instead of ‘copy’ is ‘clone’. So this pattern is very much about cloning objects. 
// A real life example could be a photocopy machine that can get you exact copies of the original document instead of asking the 
// original source to send you a brand new one. Making a copy in this case is a cheaper and a lot more efficient way of getting 
// a copy of the object, i.e. the document.
// The implementation of the pattern is very easy and almost confusingly easy.

// http://dotnetcodr.com/2013/08/05/design-patterns-and-practices-in-net-the-prototype-pattern/

using System;
using System.Net.Http;
using System.Threading.Tasks;

// We send in a URI to the constructor which downloads the string content of that URI. 
// The Analyse method then fakes a true string content analysis.
// PrintPageData simply prints these findings in the console.

// In a true implementation of the document reader we would probably parse the HTML document and try to find the real title, 
// the body contents, the headers and lot more properties. However, even a true implementation of the Analyse method would run 
// a lot faster than the actual download in the httpClient.GetStringAsync(uri) call. You’ll see that there’s a delay before we 
// see the printout. The delay is not very significant as the HttpClient object coupled with the Task library is very efficient. 
// However, we don’t want to cause the same delay if we need a copy of the page data.

// The first solution is of course to create a new copy of the document reader, pass in bbc.co.uk and let it get the page data 
// again. In other words we need to make the web request twice which is probably not very clever if we need a copy of the data 
// that’s already been constructed. This is where the prototype pattern comes into the picture: we can make a copy of the document 
// reader without having to perform the HTTP web request.

// As it turns out the prototype pattern can be implemented using an interface available in .NET, the IClonable interface. 
// The interface itself represents the abstract prototype; by the implementing object will itself be of type IClonable, 
// i.e. a concrete prototype. The prototype will need to define a method which makes a copy of the object. 
// The IClonable interface has a Clone() method which has this very purpose. The concrete prototype will have the ability to 
// copy itself in the Clone() method where you can choose between creating a deep copy or a shallow copy, more on this later.

// The interface has one member to be implemented which is the Clone method. Every object in .NET has built-in method called 
// MemberwiseClone which suits our purposes just fine. It is going to copy all the data that exist in the original object, 
// i.e. the prototype. It returns an object with the same data inside. However, be careful with this method as it cannot copy 
// complex objects. Say that the DocumentReader had another object, like WebPage which in turn has its own private members, 
// then MemberwiseClone will not copy those. In other words it creates a shallow copy as opposed to a deep copy. It copies the 
// reference of complex objects instead of the objects themselves. However, it may be enough depending on what you want to achieve. 
// Probably reading data from the same reference is OK, but not making changes to that reference. If you want perform a deep copy 
// then you’ll have to manually make a memberwise clone of the entire object graph.

public class DocumentReader : ICloneable
{
    private string _pageTitle;
    private int _headerCount;
    private string _bodyContent;

    public DocumentReader(Uri uri)
    {
        HttpClient httpClient = new HttpClient();
        Task<string> contents = httpClient.GetStringAsync(uri);
        string stringContents = contents.Result;
        Analyse(stringContents);
    }

    private void Analyse(string stringContents)
    {
        _pageTitle = "Homepage";
        _headerCount = 2;
        _bodyContent = "Welcome to my homepage";
    }

    public void PrintPageData()
    {
        Console.WriteLine("Page title: {0}, Header count: {1}, Body: {2}", _pageTitle, _headerCount, _bodyContent);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}

// Whne you run this and you’ll see that there’s no delay at all before the second printout appears in the console window.
// This is the easiest implementation of the prototype pattern in .NET. It doesn’t make any sense to go through the object 
// construction again and make the second web request.

// Another similar scenario would have been making the same database calls. Often this is not necessary if all you need is 
// the same set of data.

// Yet another example is when you need a copy of an object with the same state. Imagine an object which has several private 
// fields and those fields can be manipulated with public objects. A good solution is to implement the IClonable interface 
// and clone the original object. You’ll then have access to the same state as in the prototype.

class MainApp
{
    static void Main(string[] args)
    {
        DocumentReader reader = new DocumentReader(new Uri("http://bbc.co.uk"));
        reader.PrintPageData();

        DocumentReader readerClone = reader.Clone() as DocumentReader;
        readerClone.PrintPageData();

        Console.ReadKey();
    }
}