// Chain of Responsibility Design Pattern - Behavioural Category

// Definition:
//
// Avoid coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. 
// Chain the receiving objects and pass the request along the chain until an object handles it.

// Participants:
//
// Handler (Approver)
// - Defines an interface for handling the requests
// - Implements the successor link (optional)
//
// ConcreteHandler (Director, VicePresident, President)
// - Handles requests it is responsible for
// - Can access its successor
// - If the ConcreteHandler can handle the request, it does so; otherwise it forwards the request to its successor
//
// Client (ChainApp)
// - Initiates the request to a ConcreteHandler object on the chain

// This real-world code demonstrates the Chain of Responsibility pattern in which several 
// linked managers and executives can respond to a purchase request or hand it off to a superior. 
// Each position can have its own set of rules which orders they can approve.

// http://www.dofactory.com/net/chain-of-responsibility-design-pattern

using System;

// Class holding request details
class Purchase
{
    private int _number;
    private double _amount;
    private string _purpose;

    // Constructor
    public Purchase(int number, double amount, string purpose)
    {
        this._number = number;
        this._amount = amount;
        this._purpose = purpose;
    }

    // Gets or sets purchase number
    public int Number
    {
        get { return _number; }
        set { _number = value; }
    }

    // Gets or sets purchase amount
    public double Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    // Gets or sets purchase purpose
    public string Purpose
    {
        get { return _purpose; }
        set { _purpose = value; }
    }
}


// The 'Handler' abstract class
abstract class Approver
{
    protected Approver successor;

    public void SetSuccessor(Approver successor)
    {
        this.successor = successor;
    }

    public abstract void ProcessRequest(Purchase purchase);
}


// The 'ConcreteHandler' class
class Director : Approver
{
    public override void ProcessRequest(Purchase purchase)
    {
        if (purchase.Amount < 10000.0)
        {
            Console.WriteLine("{0} approved request# {1} for {2}", this.GetType().Name, purchase.Number, purchase.Purpose);
        }
        else if (successor != null)
        {
            successor.ProcessRequest(purchase);
        }
    }
}

// The 'ConcreteHandler' class
class VicePresident : Approver
{
    public override void ProcessRequest(Purchase purchase)
    {
        if (purchase.Amount < 25000.0)
        {
            Console.WriteLine("{0} approved request# {1} for {2}", this.GetType().Name, purchase.Number, purchase.Purpose);
        }
        else if (successor != null)
        {
            successor.ProcessRequest(purchase);
        }
    }
}

// The 'ConcreteHandler' class
class President : Approver
{
    public override void ProcessRequest(Purchase purchase)
    {
        if (purchase.Amount < 100000.0)
        {
            Console.WriteLine("{0} approved request# {1} for {2}", this.GetType().Name, purchase.Number, purchase.Purpose);
        }
        else
        {
            Console.WriteLine("Request# {0} requires an executive meeting!", purchase.Number);
        }
    }
}


// MainApp start-up class for Real-World Chain of Responsibility Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // Setup Chain of Responsibility (bottom-up)
        Approver larry = new Director();
        Approver sam = new VicePresident();
        Approver tammy = new President();

        larry.SetSuccessor(sam);
        sam.SetSuccessor(tammy);

        // Generate and process purchase requests
        Purchase p = new Purchase(2034, 350.00, "Assets");
        larry.ProcessRequest(p);

        p = new Purchase(2035, 32590.10, "Project X");
        larry.ProcessRequest(p);

        p = new Purchase(2036, 92100.00, "Project Y");
        larry.ProcessRequest(p);

        p = new Purchase(2037, 122100.00, "Project Y");
        larry.ProcessRequest(p);

        // Wait for user
        Console.ReadKey();
    }
}
