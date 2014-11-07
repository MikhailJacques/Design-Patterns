// Prototype Design Pattern - Creational Category

// Definition
// Specify the kind of objects to create using a prototypical 
// instance and create new objects by copying this prototype.

// Participants
//
// The classes and objects participating in this pattern are:
//
// Prototype - Declares an interface for cloning itself
// ConcretePrototype - Implements an operation for cloning itself
// Client - Creates a new object by asking a prototype to clone itself

// The following structural code demonstrates the Prototype pattern in which new 
// objects are created by copying pre-existing objects (prototypes) of the same class.

// http://www.dofactory.com/net/prototype-design-pattern
// http://sourcemaking.com/design_patterns/prototype/c-sharp-dot-net

using System;

// MainApp startup class for Prototype Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // Create two instances and clone each

        ConcretePrototype1 p1 = new ConcretePrototype1("I");
        ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
        Console.WriteLine("Cloned: {0}", c1.Id);

        ConcretePrototype2 p2 = new ConcretePrototype2("II");
        ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
        Console.WriteLine("Cloned: {0}", c2.Id);

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Prototype' abstract class
abstract class Prototype
{
    private string _id;

    // Constructor
    public Prototype(string id)
    {
        this._id = id;
    }

    // Gets id
    public string Id
    {
        get { return _id; }
    }

    public abstract Prototype Clone();
}

// A 'ConcretePrototype' class 
class ConcretePrototype1 : Prototype
{
    // Constructor
    public ConcretePrototype1(string id) : base(id) { }

    // Returns a shallow copy
    public override Prototype Clone()
    {
        return (Prototype)this.MemberwiseClone();
    }
}

// A 'ConcretePrototype' class 
class ConcretePrototype2 : Prototype
{
    // Constructor
    public ConcretePrototype2(string id) : base(id) { }

    // Returns a shallow copy
    public override Prototype Clone()
    {
        return (Prototype)this.MemberwiseClone();
    }
}