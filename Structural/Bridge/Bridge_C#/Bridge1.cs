// Bridge Design Pattern - Structural Category

// Decouples an abstraction from its implementation so that the two can vary independently.

// Motivation

// Sometimes an abstraction should have different implementations.
// Consider an object that handles persistence of objects over different platforms 
// using either relational databases or file system structures (files and folders). 
// A simple implementation might choose to extend the object itself to implement the 
// functionality for both file system and RDBMS. 
// However this implementation would create a problem.
// Inheritance binds an implementation to the abstraction and thus it would be difficult 
// to modify, extend, and reuse abstraction and implementation independently.


// Participants
// The classes and objects participating in this pattern are:

// Abstraction (BusinessObject)
// Defines the abstraction's interface.
// Maintains a reference to an object of type Implementor.

// RefinedAbstraction (CustomersBusinessObject)
// Extends the interface defined by Abstraction.

// Implementor (Data)
// Defines the interface for implementation classes. 
// This interface does not have to correspond exactly to Abstraction's interface.
// In fact the two interfaces can be quite different. 
// Typically the Implementation interface provides only primitive operations, 
// and Abstraction defines higher-level operations based on these primitives.

// ConcreteImplementor (CustomersDataObject)
// Implements the Implementor interface and defines its concrete implementation.


// Benefits of using Bridge Pattern

// 1. Decoupling abstraction from implementation
// Inheritance tightly couples an abstraction with an implementation at compile time. 
// Bridge pattern can be used to avoid the binding between abstraction and implementation 
// and to select the implementation at run time.

// 2. Reduction in the number of sub classes
// Sometimes, using pure inheritance will increase the number of sub classes. 

// 3. Cleaner code and Reduction in executable size

// 4. Interface and implementation can be varied independently
// Maintaining two different class hierarchies for interface and implementation 
// entitles to vary one independent of the other.

// 5. Improved Extensibility
// Abstraction and implementation can be extended independently.

// 6. Loosely coupled client code
// Abstraction separates the client code from the implementation. 
// So, the implementation can be changed without affecting the client code 
// and the client code need not be compiled when the implementation changes.


// Drawbacks of using Bridge Pattern

// 1. Double indirection
// This will have a slight impact on performance.


// Bridge and Strategy
// Often, the Strategy Pattern is confused with the Bridge Pattern. 
// Even though, these two patterns are similar in structure, 
// they are trying to solve two different design problems. 
// Strategy is mainly concerned in encapsulating algorithms, 
// whereas Bridge decouples the abstraction from the implementation, 
// to provide different implementation for the same abstraction.

// Bridge and Adapter
// The structure of the Adapter Pattern (object adapter) may look similar to the
// Bridge Pattern. However, the adapter is meant to change the interface of an 
// existing object and is mainly intended to make unrelated classes work together.

// Summary
// Pure inheritance hardwires the abstraction and the implementation. 
// Bridge Pattern can be used when an abstraction can have different 
// implementations and when both of them can vary independently.

// http://www.dofactory.com/net/bridge-design-pattern
// http://www.codeproject.com/Articles/890/Bridge-Pattern-Bridging-the-gap-between-Interface#Listing 2

using System;

namespace Bridge
{
    /// <summary>
    /// MainApp startup class for Structural
    /// Bridge Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            Abstraction ab = new RefinedAbstraction();

            // Set implementation and call
            ab.Implementor = new ConcreteImplementorA();
            ab.Operation();

            // Change implemention and call
            ab.Implementor = new ConcreteImplementorB();
            ab.Operation();

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Abstraction' class (BusinessObject)
    /// Defines the abstraction's interface.
    /// Maintains a reference to an object of type Implementor.
    /// </summary>
    class Abstraction
    {
        protected Implementor implementor;

        // Property
        public Implementor Implementor
        {
            set { implementor = value; }
        }

        public virtual void Operation()
        {
            implementor.Operation();
        }
    }

    /// <summary>
    /// The 'Implementor' abstract class (DataObject)
    /// Defines the interface for implementation classes. 
    /// This interface does not have to correspond exactly to Abstraction's interface.
    /// In fact the two interfaces can be quite different. 
    /// Typically the Implementation interface provides only primitive operations, 
    /// and Abstraction defines higher-level operations based on these primitives.
    /// </summary>
    abstract class Implementor
    {
        public abstract void Operation();
    }

    /// <summary>
    /// The 'RefinedAbstraction' class (CustomersBusinessObject)
    /// Extends the interface defined by Abstraction.
    /// </summary>
    class RefinedAbstraction : Abstraction
    {
        public override void Operation()
        {
            implementor.Operation();
        }
    }

    /// <summary>
    /// The 'ConcreteImplementorA' class
    /// </summary>
    class ConcreteImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorA Operation");
        }
    }

    /// <summary>
    /// The 'ConcreteImplementorB' class
    /// </summary>
    class ConcreteImplementorB : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorB Operation");
        }
    }
}
