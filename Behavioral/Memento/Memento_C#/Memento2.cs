// Memento Design Pattern - Behavioral Category

// The Memento Pattern can easily be thought of as the implementation 
// of an Undo / Redo functionality in our code. 
// We want to store the subsequent states of an object in our program, 
// but we don’t want to give the program direct access to the data. 
//
// The pattern consists of three distinct roles:
//
// - Memento stores a single object state.
// - Caretaker stores the Memento objects without knowing the details of the actual objects it stores.
// - Originator knows how to create a Memento. It also knows how to restore state from a given Memento.

// http://www.dofactory.com/net/memento-design-pattern

using System;

// MainApp startup class for Real-World Memento Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        SalesProspect s = new SalesProspect();
        s.Name = "Bob Northrop";
        s.Phone = "(705) 123-4567";
        s.Budget = 17000.0;

        // Store internal state
        ProspectMemory m = new ProspectMemory();
        m.Memento = s.SaveMemento();

        // Continue changing originator
        s.Name = "Eve Ugly";
        s.Phone = "(416) 456-1239";
        s.Budget = 700000.0;

        // Restore saved state
        s.RestoreMemento(m.Memento);

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Originator' class
class SalesProspect
{
    private string _name;
    private string _phone;
    private double _budget;

    // Gets or sets name
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            Console.WriteLine("Name:  " + _name);
        }
    }

    // Gets or sets phone
    public string Phone
    {
        get { return _phone; }
        set
        {
            _phone = value;
            Console.WriteLine("Phone: " + _phone);
        }
    }

    // Gets or sets budget
    public double Budget
    {
        get { return _budget; }
        set
        {
            _budget = value;
            Console.WriteLine("Budget: " + _budget);
        }
    }

    // Stores memento
    public Memento SaveMemento()
    {
        Console.WriteLine("\nSaving state --\n");
        return new Memento(_name, _phone, _budget);
    }

    // Restores memento
    public void RestoreMemento(Memento memento)
    {
        Console.WriteLine("\nRestoring state --\n");
        this.Name = memento.Name;
        this.Phone = memento.Phone;
        this.Budget = memento.Budget;
    }
}

// The 'Memento' class
class Memento
{
    private string _name;
    private string _phone;
    private double _budget;

    // Constructor
    public Memento(string name, string phone, double budget)
    {
        this._name = name;
        this._phone = phone;
        this._budget = budget;
    }

    // Gets or sets name
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    // Gets or set phone
    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }

    // Gets or sets budget
    public double Budget
    {
        get { return _budget; }
        set { _budget = value; }
    }
}

// The 'Caretaker' class
class ProspectMemory
{
    private Memento _memento;

    // Property
    public Memento Memento
    {
        set { _memento = value; }
        get { return _memento; }
    }
}