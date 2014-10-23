// Observer Design Pattern - Behavioral Category

// Observer pattern is popularly known to be based on "The Hollywood Principle" which says:
// "Don’t call us, we will call you." 
// Pub-Sub (Publisher-Subscriber) is yet another popular nickname given to Observer pattern.

// "There are n numbers of observers (objects) which are interested in a special object (called the subject). 
// Explaining one step further - there are various objects (called observers) which are interested in things 
// going on with a special object (called the subject). So they register (or subscribe) themselves to subject 
// (also called publisher). The observers are interested in happening of an event (this event usually happens 
// in the boundary of subject object) whenever this event is raised (by the subject/publisher) the observers 
// are notified (they have subscribed for the happening of this event - Remember?)

// http://www.codeproject.com/Articles/769084/Observer-Pattern-Csharp

// Let's consider an online electronics store (Subject) which has a huge inventory and they keep on updating it. 
// The store wants to update all its customers (Observers) whenever any product arrives in the store. 
// Just reading the problem statement, we can fit in Observer pattern for the above problem.
// The online electronic store is going to be the Subject. Whenever the Subject has any addition 
// in its inventory, the Observers (customers/users) who have subscribed to store notifications are 
// notified through email.

using System;
using System.Collections.Generic;

interface ISubject
{
    void Subscribe(Observer observer);
    void Unsubscribe(Observer observer);
    void Notify();
}

// The Subject maintains a list of Observers. 
// The Subscribe and Unsubscribe methods are the ones through which the Observers 
// register and unregister themselves to the Subject. The Notify method is the one 
// which has the responsibility of notifying all the Observers.
public class Subject : ISubject
{
    private int _int;

    private List<IObserver> observers = new List<IObserver>();

    public int Inventory
    {
        get
        {
            return _int;
        }

        set
        {
            // Notify Observers only when there is an increase in inventory
            if (value > _int)
                Notify();

            _int = value;
        }
    }

    public void Subscribe(Observer observer)
    {
        observers.Add(observer);
    }

    public void Unsubscribe(Observer observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        // Notify all registered observers
        observers.ForEach(x => x.Update());
    }
}

interface IObserver
{
    void Update();
}

// The Observer class is pretty dumb as it has only one property, namely ObserverName,
// to distinguish one Observer from another and an Update method. This is the method 
// which almost every Observer class would have. It is a way for the Observer to get notified 
// that something has happened in the Subject and since the Observer is registered to receive 
// notifications, it is notified via the invocation of this method.
public class Observer : IObserver
{
    public string ObserverName { get; private set; }

    public Observer(string name)
    {
        this.ObserverName = name;
    }

    public void Update()
    {
        Console.WriteLine("{0}: A new product has arrived at the store", this.ObserverName);
    }
}

class MainApp
{
    static void Main(string[] args)
    {
        Subject subject = new Subject();

        // Create Observer1 and register it with the store
        Observer observer1 = new Observer("Observer 1");
        subject.Subscribe(observer1);

        // Create Observer2 and register it with the store
        subject.Subscribe(new Observer("Observer 2"));

        // Increase the inventory count
        subject.Inventory++;

        Console.WriteLine();

        // Unregister Observer1 from the store
        subject.Unsubscribe(observer1);

        // Register Observer3 with the store
        subject.Subscribe(new Observer("Observer 3"));

        // Increase the inventory count
        subject.Inventory++;

        Console.WriteLine();

        // Increse the inventory count
        subject.Inventory++;

        // Wait for user input
        Console.ReadKey();
    }
}