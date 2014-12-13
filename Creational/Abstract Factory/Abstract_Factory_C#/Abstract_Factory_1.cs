// Abstract Factory - Creational Patterns Category

// Motivation
//
// Modularization is a big issue in today's programming. Programmers all over the world are trying to avoid the 
// idea of adding code to existing classes in order to make them support encapsulating more general information. 
// Take the case of an information manager that manages phone numbers. 
// Phone numbers have a particular rule on which they get generated depending on areas and countries. 
// If at some point the application should be changed in order to support adding numbers from a new country, 
// the code of the application would have to be changed and it would become more and more complicated.

// In order to prevent it, the Abstract Factory design pattern is used. 
// Using this pattern a framework is defined, which produces objects that follow a general pattern and at runtime 
// this factory is paired with any concrete factory to produce objects that follow the pattern of a certain country. 
// In other words, the Abstract Factory is a super-factory which creates other factories (Factory of factories).

// Intent
//
// Abstract Factory offers an interface for creating a family of related objects, without explicitly specifying their classes.

// Participants:
//
// AbstractFactory - Declares an interface for operations that create abstract products.
// ConcreteFactory - Implements operations to create concrete products.
// AbstractProduct - Declares an interface for a type of product objects.
// Product - Defines a product to be created by the corresponding ConcreteFactory; it implements the AbstractProduct interface.
// Client - Uses the interfaces declared by the AbstractFactory and AbstractProduct classes.

// The AbstractFactory class is the one that determines the actual type of the concrete object and creates it, 
// but it returns an abstract pointer to the concrete object just created. This determines the behavior of the 
// client that asks the factory to create an object of a certain abstract type and to return the abstract pointer to it, 
// keeping the client from knowing anything about the actual creation of the object.

// The fact that the factory returns an abstract pointer to the created object means that the client doesn't have knowledge 
// of the object's type. This implies that there is no need for including any class declarations relating to the concrete type, 
// the client dealing at all times with the abstract type. The objects of the concrete type, created by the factory, are accessed 
// by the client only through the abstract interface.

// The second implication of this way of creating objects is that when the adding new concrete types is needed, 
// all we have to do is modify the client code and make it use a different factory, which is far easier than instantiating 
// a new type, which requires changing the code wherever a new object is created.

// Definition:
//
// Provide an interface for creating families of related or dependent objects without specifying their concrete classes.

// Participants:
//
// AbstractFactory
// - Declares an interface for operations that create abstract products
//
// ConcreteFactory
// - Implements the operations to create concrete product objects
//
// AbstractProduct
// - Declares an interface for a type of product object
//
// Product
// - Defines a product object to be created by the corresponding concrete factory
// - Implements the AbstractProduct interface
//
// Client
// - Uses interfaces declared by AbstractFactory and AbstractProduct classes

// Structural code
// This structural code demonstrates the Abstract Factory pattern creating parallel hierarchies of objects. 
// Object creation has been abstracted and there is no need for hard-coded class names in the client code.

// http://www.oodesign.com/abstract-factory-pattern.html
// http://www.dofactory.com/net/abstract-factory-design-pattern

using System;

// The 'AbstractFactory' abstract class
// - Declares an interface for operations that create abstract products
abstract class AbstractFactory
{
    public abstract AbstractProductA CreateProductA();
    public abstract AbstractProductB CreateProductB();
}

// The 'ConcreteFactory1' class
// - Implements the operations to create concrete product objects
class ConcreteFactory1 : AbstractFactory
{
    public override AbstractProductA CreateProductA()
    {
        return new ProductA1();
    }
    public override AbstractProductB CreateProductB()
    {
        return new ProductB1();
    }
}

// The 'ConcreteFactory2' class
// - Implements the operations to create concrete product objects
class ConcreteFactory2 : AbstractFactory
{
    public override AbstractProductA CreateProductA()
    {
        return new ProductA2();
    }

    public override AbstractProductB CreateProductB()
    {
        return new ProductB2();
    }
}

// The 'AbstractProductA' abstract class
// - Declares an interface for a type of product object
abstract class AbstractProductA
{
    public abstract void Interact(AbstractProductB b);
}

// The 'AbstractProductB' abstract class
// - Declares an interface for a type of product object
abstract class AbstractProductB
{
    public abstract void Interact(AbstractProductA a);
}

// The 'ProductA1' class
// - Defines a product object to be created by the corresponding concrete factory
class ProductA1 : AbstractProductA
{
    public override void Interact(AbstractProductB b)
    {
        Console.WriteLine(this.GetType().Name + " interacts with " + b.GetType().Name);
    }
}

// The 'ProductB1' class
// - Defines a product object to be created by the corresponding concrete factory
class ProductB1 : AbstractProductB
{
    public override void Interact(AbstractProductA a)
    {
        Console.WriteLine(this.GetType().Name + " interacts with " + a.GetType().Name);
    }
}

// The 'ProductA2' class
// - Defines a product object to be created by the corresponding concrete factory
class ProductA2 : AbstractProductA
{
    public override void Interact(AbstractProductB b)
    {
        Console.WriteLine(this.GetType().Name + " interacts with " + b.GetType().Name);
    }
}

// The 'ProductB2' class
// - Defines a product object to be created by the corresponding concrete factory
class ProductB2 : AbstractProductB
{
    public override void Interact(AbstractProductA a)
    {
        Console.WriteLine(this.GetType().Name + " interacts with " + a.GetType().Name);
    }
}

// The 'Client' class. Interaction environment for the products.
class Client
{
    private AbstractProductA _abstractProductA;
    private AbstractProductB _abstractProductB;

    // Constructor
    public Client(AbstractFactory factory)
    {
        _abstractProductA = factory.CreateProductA();
        _abstractProductB = factory.CreateProductB();
    }

    public void Run()
    {
        _abstractProductA.Interact(_abstractProductB);
        _abstractProductB.Interact(_abstractProductA);
    }
}

// MainApp startup class for Structural Abstract Factory Design Pattern.
class MainApp
{
    // Entry point into console application.
    public static void Main()
    {
        // Abstract factory #1
        AbstractFactory factory1 = new ConcreteFactory1();
        Client client1 = new Client(factory1);
        client1.Run();

        // Abstract factory #2
        AbstractFactory factory2 = new ConcreteFactory2();
        Client client2 = new Client(factory2);
        client2.Run();

        // Wait for user input
        Console.ReadKey();
    }
}