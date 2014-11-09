// Factory Design Pattern - Creational Category

// Definition
// Define an interface for creating an object, but let subclasses decide which class
// to instantiate. Factory Method lets a class defer instantiation to subclasses.

// Participants
// The classes and objects participating in this pattern are:
//
// Product
// - Defines the interface of objects the factory method creates
//
// ConcreteProduct
// - Implements the Product interface
//
// Creator
// - Declares the factory method, which returns an object of type Product. 
// - Creator may also define a default implementation of the factory method 
//   that returns a default ConcreteProduct object. 
// - May call the factory method to create a Product object.
//
// ConcreteCreator  (Report, Resume)
// - Overrides the factory method to return an instance of a ConcreteProduct.

// http://www.dofactory.com/net/factory-method-design-pattern

using System;

// MainApp startup class for Factory Method Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // An array of creators
        Creator[] creators = new Creator[2];

        creators[0] = new ConcreteCreatorA();
        creators[1] = new ConcreteCreatorB();

        // Iterate over creators and create products
        foreach (Creator creator in creators)
        {
            Product product = creator.FactoryMethod();
            Console.WriteLine("Created {0}", product.GetType().Name);
        }

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Product' abstract class
abstract class Product { }

// A 'ConcreteProduct' class
class ConcreteProductA : Product { }

// A 'ConcreteProduct' class
class ConcreteProductB : Product { }

// The 'Creator' abstract class
abstract class Creator
{
    public abstract Product FactoryMethod();
}

// A 'ConcreteCreator' class
class ConcreteCreatorA : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductA();
    }
}

// A 'ConcreteCreator' class
class ConcreteCreatorB : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductB();
    }
}