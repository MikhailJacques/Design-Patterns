// Memento Design Pattern - Behavioral Category

// Definition
// Without violating encapsulation, capture and externalize an object's 
// internal state allowing the object to be restored to this state later.

// Participants
//
// The classes and objects participating in this pattern are:

// Memento (Memento)
// - Stores internal state of the Originator object. 
//   The Memento may store as much or as little of the Originator's 
//   internal state as necessary at its Originator's discretion.
// - Protect against access by objects other than the Originator. 
//   Mementos have effectively two interfaces. 
//   Caretaker sees a narrow interface to the Memento - 
//   it can only pass the Memento to the other objects.
//   Originator, in contrast, sees a wide interface, one that lets it 
//   access all the data necessary to restore itself to its previous state. 
//   Ideally, only the Originator that produces the Memento would be 
//   permitted to access the Memento's internal state.
// Originator (SalesProspect)
// - Creates a Memento containing a snapshot of its current internal state.
// - Uses the Memento to restore its internal state
// Caretaker (Caretaker)
// - Is responsible for the Memento's safekeeping
// - Never operates on or examines the contents of a Memento.

// http://www.dofactory.com/net/memento-design-pattern

using System;

// MainApp startup class for Structural 
// 
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        Originator o = new Originator("On");
        // o.State = "On";

        // Store Originator's internal state within Memento inside Caretaker
        Caretaker c = new Caretaker(o.CreateMemento());
        // c.Memento = o.CreateMemento();

        // Change Originator's state
        o.State = "Off";

        // Restore Originator's previously saved state
        o.SetMemento(c.Memento);

        // Change Originator's state again
        o.State = "Off";

        // Store Originator's internal state within Memento inside Caretaker
        c = new Caretaker(o.CreateMemento());

        // Change Originator's state
        o.State = "On";

        // Restore Originator's previously saved state
        o.SetMemento(c.Memento);

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Originator' class
class Originator
{
    private string _state;

    // Constructor
    public Originator(string state)
    {
        this._state = state;
        Console.WriteLine("Originator current state: " + _state);
    }

    // Property
    public string State
    {
        get { return _state; }
        set
        {
            _state = value;
            Console.WriteLine("Originator state has been changed to: " + _state);
        }
    }

    // Creates Memento 
    public Memento CreateMemento()
    {
        return (new Memento(_state));
    }

    // Restores original state
    public void SetMemento(Memento memento)
    {
        Console.WriteLine("\nRestoring Originator state...");
        State = memento.State;
        Console.WriteLine("Originator state has been restored\n");
    }
}


// The 'Memento' class
class Memento
{
    private string _state;

    // Constructor
    public Memento(string state)
    {
        Console.WriteLine("\nSaving Originator state...");
        this._state = state;
        Console.WriteLine("Originator state has been saved\n");
    }

    // Gets or sets state
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
}


// The 'Caretaker' class
class Caretaker
{
    private Memento _memento;

    // Constructor
    public Caretaker(Memento memento)
    {
        this._memento = memento;
    }

    // Gets or sets memento
    public Memento Memento
    {
        get { return _memento; }
        set { _memento = value; }
    }
}