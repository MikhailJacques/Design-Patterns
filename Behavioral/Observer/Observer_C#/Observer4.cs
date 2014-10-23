// Observer Design Pattern - Behavioral Category

// Observer Design Pattern defines a one-to-many dependency between objects so that when 
// one object changes state, all its dependents are notified and updated automatically.

// Participants
//
// Subject  (Stock)
// - Knows its Observers. Any number of Observer objects may observe a Subject
// - Provides an interface for attaching and detaching Observer objects.
//
// ConcreteSubject (IBM)
// - Stores state of interest to ConcreteObserver
// - Sends a notification to its Observers when its state changes
//
// Observer (IInvestor)
// - Defines an updating interface for objects that should be notified of changes in a Subject.
//
// ConcreteObserver (Investor)
// - Maintains a reference to a ConcreteSubject object
// - Stores state that should stay consistent with the Subject's
// - Implements the Observer updating interface to keep its state consistent with the Subject's

// http://www.dofactory.com/net/observer-design-pattern

using System;
using System.Collections.Generic;

// MainApp startup class for Real-World Observer Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // Create IBM stock and attach investors
        IBM ibm = new IBM("IBM", 120.00);
        ibm.Attach(new Investor("Sorros"));
        ibm.Attach(new Investor("Berkshire"));

        // Fluctuating prices will notify investors
        ibm.Price = 120.10;
        ibm.Price = 121.00;
        ibm.Price = 120.50;
        ibm.Price = 120.75;

        // Wait for user
        Console.ReadKey();
    }
}

/// The 'Subject' abstract class
abstract class Stock
{
    private string _symbol;
    private double _price;
    private List<IInvestor> _investors = new List<IInvestor>();

    // Constructor
    public Stock(string symbol, double price)
    {
        this._symbol = symbol;
        this._price = price;
    }

    public void Attach(IInvestor investor)
    {
        _investors.Add(investor);
    }

    public void Detach(IInvestor investor)
    {
        _investors.Remove(investor);
    }

    public void Notify()
    {
        foreach (IInvestor investor in _investors)
        {
            investor.Update(this);
        }

        Console.WriteLine("");
    }

    // Gets or sets the price
    public double Price
    {
        get { return _price; }
        set
        {
            if (_price != value)
            {
                _price = value;
                Notify();
            }
        }
    }

    // Gets the symbol
    public string Symbol
    {
        get { return _symbol; }
    }
}

// The 'ConcreteSubject' class
class IBM : Stock
{
    // Constructor
    public IBM(string symbol, double price)
        : base(symbol, price)
    {
    }
}

// The 'Observer' interface
interface IInvestor
{
    void Update(Stock stock);
}

// The 'ConcreteObserver' class
class Investor : IInvestor
{
    private string _name;
    private Stock _stock;

    // Constructor
    public Investor(string name)
    {
        this._name = name;
    }

    public void Update(Stock stock)
    {
        Console.WriteLine("Notified {0} of {1}'s " +
            "change to {2:C}", _name, stock.Symbol, stock.Price);
    }

    // Gets or sets the stock
    public Stock Stock
    {
        get { return _stock; }
        set { _stock = value; }
    }
}
