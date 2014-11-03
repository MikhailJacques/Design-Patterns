// Strategy Design Pattern - Behavioral Category

// Strategy pattern defines a family of algorithms, 
// encapsulates each one and makes them interchangeable. 
// Strategy lets the algorithm vary independently from clients that use it.

//  The classes and objects participating in this pattern are:

// Strategy
// - Declares an interface common to all supported algorithms. 
// - Context uses this interface to call the algorithm defined by a ConcreteStrategy.
//
// ConcreteStrategy
// - Implements the algorithm using the Strategy interface.
//
// Context
// - Is configured with a ConcreteStrategy object
// - Maintains a reference to a Strategy object
// - May define an interface that lets Strategy access its data.

// This following structural code demonstrates the Strategy pattern 
// which encapsulates functionality in the form of an object. 
// This allows clients to dynamically change algorithmic strategies.

// http://www.dofactory.com/net/strategy-design-pattern
// http://sourcemaking.com/design_patterns/strategy/c-sharp-dot-net

using System;

// MainApp startup class for Structural
// Strategy Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        Context context;

        // Three contexts following different strategies
        context = new Context(new ConcreteStrategyA());
        context.ContextInterface();

        context = new Context(new ConcreteStrategyB());
        context.ContextInterface();

        context = new Context(new ConcreteStrategyC());
        context.ContextInterface();

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Strategy' abstract class
abstract class Strategy
{
    public abstract void AlgorithmInterface();
}

// A 'ConcreteStrategy' class
class ConcreteStrategyA : Strategy
{
    public override void AlgorithmInterface()
    {
        Console.WriteLine("Called ConcreteStrategyA.AlgorithmInterface()");
    }
}

// A 'ConcreteStrategy' class
class ConcreteStrategyB : Strategy
{
    public override void AlgorithmInterface()
    {
        Console.WriteLine("Called ConcreteStrategyB.AlgorithmInterface()");
    }
}

// A 'ConcreteStrategy' class
class ConcreteStrategyC : Strategy
{
    public override void AlgorithmInterface()
    {
        Console.WriteLine("Called ConcreteStrategyC.AlgorithmInterface()");
    }
}

// The 'Context' class
class Context
{
    private Strategy _strategy;

    // Constructor
    public Context(Strategy strategy)
    {
        this._strategy = strategy;
    }

    public void ContextInterface()
    {
        _strategy.AlgorithmInterface();
    }
}