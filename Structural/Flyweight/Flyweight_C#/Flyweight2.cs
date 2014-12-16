//// Flyweight Design Pattern - Structural Category

//// Definition
//// Use sharing to support large numbers of fine-grained objects efficiently.

//// Participants
////
//// Flyweight
//// - Declares an interface through which flyweights can receive and act on extrinsic state.
////
//// SharedConcreteFlyweight
//// - Implements the Flyweight interface and adds storage for intrinsic state, if any. 
////   A SharedConcreteFlyweight object must be sharable. Any state it stores must be intrinsic, 
////   that is, it must be independent of the SharedConcreteFlyweight object's context.
////
//// UnsharedSharedConcreteFlyweight
//// - Not all Flyweight subclasses need to be shared. The Flyweight interface enables sharing, but it doesn't enforce it. 
////   It is common for UnsharedSharedConcreteFlyweight objects to have SharedConcreteFlyweight objects as children at some 
////   level in the flyweight object structure (as the Row and Column classes have).
////
//// FlyweightFactory
//// - Creates and manages flyweight objects
//// - Ensures that flyweight are shared properly. When a client requests a flyweight, the FlyweightFactory objects assets 
////   an existing instance or creates one, if none exists.
////
//// Client
//// - Maintains a reference to flyweight(s).
//// - Computes or stores the extrinsic state of flyweight(s).

//// Structural code in C#
//// This structural code demonstrates the Flyweight pattern in which a
//// relatively small number of objects is shared many times by different clients.

//// http://www.dofactory.com/net/flyweight-design-pattern

//using System;
//using System.Collections;

//// The 'FlyweightFactory' class
//class FlyweightFactory
//{
//    private Hashtable flyweights = new Hashtable();

//    // Constructor
//    public FlyweightFactory()
//    {
//        flyweights.Add("X", new SharedConcreteFlyweight());
//        flyweights.Add("Y", new SharedConcreteFlyweight());
//        flyweights.Add("Z", new SharedConcreteFlyweight());
//    }

//    public Flyweight GetFlyweight(string key)
//    {
//        return ((Flyweight)flyweights[key]);
//    }
//}

//// The 'Flyweight' abstract class
//abstract class Flyweight
//{
//    public abstract void Operation(int extrinsic_state);
//}

//// The 'ConcreteFlyweight' class
//class SharedConcreteFlyweight : Flyweight
//{
//    public override void Operation(int extrinsic_state)
//    {
//        Console.WriteLine("SharedConcreteFlyweight: " + extrinsic_state);
//    }
//}

//// The 'ConcreteFlyweight' class
//class UnsharedConcreteFlyweight : Flyweight
//{
//    public override void Operation(int extrinsic_state)
//    {
//        Console.WriteLine("UnsharedConcreteFlyweight: " + extrinsic_state);
//    }
//}

//// MainApp startup class for Structural Flyweight Design Pattern.
//class MainApp
//{
//    private static Flyweight ch;
//    private static FlyweightFactory factory;

//    // Entry point into console application.
//    static void Main()
//    {
//        // Arbitrary extrinsic state
//        int extrinsic_state = 22;

//        factory = new FlyweightFactory();

//        // Work with different flyweight instances

//        ch = factory.GetFlyweight("X");
//        ch.Operation(--extrinsic_state);

//        ch = factory.GetFlyweight("Y");
//        ch.Operation(--extrinsic_state);

//        ch = factory.GetFlyweight("Z");
//        ch.Operation(--extrinsic_state);

//        ch = new UnsharedConcreteFlyweight();
//        ch.Operation(--extrinsic_state);

//        // Wait for user
//        Console.ReadKey();
//    }
//}
