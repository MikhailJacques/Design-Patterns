// Prototype Design Pattern - Creational Category

// What is the Prototype Pattern?
// The intent behind the usage of a Prototype pattern is for creation of an object clone.
// In other words it allows us to create a new instance by copying existing instances.

// The cloning falls under two categories: shallow and deep.
// A shallow copy copies all reference types or value types, but it does not copy the objects 
// that the references refer to. The references in the new object point to the same objects 
// that the references in the original object points to. Only Parent Object is cloned here.

// In contrast, a deep copy of an object copies the elements and everything directly or 
// indirectly referenced by the elements. Parent Object is cloned along with the containing objects.

// In .Net, cloning can be achieved by making use of the ICloneable interface. 
// Using the clone method, we can create clones or copies of instances with the same value as the existing instance. 


// Prototype Design Pattern falls under Creational Pattern of Gang of Four (GOF) Design Patterns in .Net. 
// It provides an interface for creating parts of a product and is used when creation of an object is costly or 
// complex. It is used to create a duplicate object or clone of the current object to enhance performance. 
// For example, an object is to be created after a costly database operation. We can cache the object, return its 
// clone on next request and update the database as and when needed thus reducing database calls.

// http://www.dotnet-tricks.com/Tutorial/designpatterns/R9R4060613-Prototype-Design-Pattern---C

using System;

// The 'Prototype' interface
public interface IEmployee
{
    IEmployee Clone();
    string GetDetails();
}

// A 'ConcretePrototype' class
public class Developer : IEmployee
{
    public string Name { get; set; }
    public string Role { get; set; }
    public int WordsPerMinute { get; set; }
    public string PreferredLanguage { get; set; }

    public IEmployee Clone()
    {
        // Shallow Copy: only top-level objects are duplicated
        return (IEmployee)MemberwiseClone();

        // Deep Copy: all objects are duplicated
        // return (IEmployee) this.Clone();
    }

    public string GetDetails()
    {
        return string.Format("Name: {0}\nRole: {1}\nLanguage: {2}\n", Name, Role, PreferredLanguage);
    }
}

// A 'ConcretePrototype' class
public class Typist : IEmployee
{
    public string Name { get; set; }
    public string Role { get; set; }
    public int WordsPerMinute { get; set; }

    public IEmployee Clone()
    {
        // Shallow Copy: only top-level objects are duplicated
        return (IEmployee)MemberwiseClone();

        // Deep Copy: all objects are duplicated
        // return (IEmployee) this.Clone();
    }

    public string GetDetails()
    {
        return string.Format("Name: {0}\nRole: {1}\nWords per minute: {2}\n", Name, Role, WordsPerMinute);
    }
}

// Prototype Pattern Demo
class MainApp
{
    static void Main(string[] args)
    {
        Developer dev = new Developer();
        dev.Name = "Michael";
        dev.Role = "Software Engineer";
        dev.PreferredLanguage = "C#";

        Developer devCopy = (Developer)dev.Clone();
        devCopy.Name = "Bob";
        // Role and PreferredLanguage will be copies of the above

        Console.WriteLine(dev.GetDetails());
        Console.WriteLine(devCopy.GetDetails());

        Typist typist = new Typist();
        typist.Name = "Tom";
        typist.Role = "Typist";
        typist.WordsPerMinute = 150;

        Typist typistCopy = (Typist)typist.Clone();
        typistCopy.Name = "Jerry";
        typistCopy.WordsPerMinute = 110;
        // Role and WordsPerMinute will be copies of the above

        Console.WriteLine(typist.GetDetails());
        Console.WriteLine(typistCopy.GetDetails());

        Console.ReadKey();
    }
}