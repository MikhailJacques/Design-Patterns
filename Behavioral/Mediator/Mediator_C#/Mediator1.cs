// Mediator Design Pattern - Behavioral Category

// Defines an object that encapsulates how a set of objects interact. 
// Mediator promotes loose coupling by keeping objects from referring to each other explicitly, 
// and lets you vary their interaction independently.

// http://www.dofactory.com/net/mediator-design-pattern

using System;

// The 'Mediator' abstract class
// Defines an interface for communicating with Colleague objects
abstract class Mediator
{
    public abstract void Send(string message, Colleague colleague);
}

// The 'ConcreteMediator' class
// Knows the Colleague class and keeps a reference to the Colleague objects
// Implements cooperative behavior by coordinating communication between the Colleague objects
class ConcreteMediator : Mediator
{
    private ConcreteColleague1 colleague1;
    private ConcreteColleague2 colleague2;

    public ConcreteColleague1 Colleague1
    {
        set { colleague1 = value; }
    }

    public ConcreteColleague2 Colleague2
    {
        set { colleague2 = value; }
    }

    public override void Send(string message, Colleague colleague)
    {
        if (colleague1 == colleague)
        {
            colleague2.Notify(message);
        }
        else
        {
            colleague1.Notify(message);
        }
    }
}

// The 'Colleague' abstract class
// Each Colleague class knows its Mediator object
// Each colleague communicates with its mediator whenever 
// it would have otherwise communicated with another colleague
abstract class Colleague
{
    protected Mediator mediator;

    // Constructor
    public Colleague(Mediator mediator)
    {
        this.mediator = mediator;
    }
}

// A 'ConcreteColleague' class
class ConcreteColleague1 : Colleague
{
    // Constructor
    public ConcreteColleague1(Mediator mediator) : base(mediator) { }

    public void Send(string message)
    {
        mediator.Send(message, this);
    }

    public void Notify(string message)
    {
        Console.WriteLine("Colleague1 gets message: " + message);
    }
}

// A 'ConcreteColleague' class
class ConcreteColleague2 : Colleague
{
    // Constructor
    public ConcreteColleague2(Mediator mediator) : base(mediator) { }

    public void Send(string message)
    {
        mediator.Send(message, this);
    }

    public void Notify(string message)
    {
        Console.WriteLine("Colleague2 gets message: " + message);
    }
}

// MainApp start-up class for Structural Mediator Design Pattern
class MainApp
{
    private static ConcreteMediator mediator;
    private static ConcreteColleague1 c1;
    private static ConcreteColleague2 c2;

    // Entry point into console application
    static void Main()
    {
        mediator = new ConcreteMediator();

        c1 = new ConcreteColleague1(mediator);
        c2 = new ConcreteColleague2(mediator);

        mediator.Colleague1 = c1;
        mediator.Colleague2 = c2;

        c1.Send("How are you?");
        c2.Send("Fine, thanks");

        // Wait for user
        Console.ReadKey();
    }
}