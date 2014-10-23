//// Observer Design Pattern - Behavioral Category

//// Observer Design Pattern defines a one-to-many dependency between objects so that when 
//// one object changes state, all its dependents are notified and updated automatically.

//// Participants
////
//// Subject  (Stock)
//// - Knows its Observers. Any number of Observer objects may observe a Subject
//// - Provides an interface for attaching and detaching Observer objects.
////
//// ConcreteSubject (IBM)
//// - Stores state of interest to ConcreteObserver
//// - Sends a notification to its Observers when its state changes
////
//// Observer (IInvestor)
//// - Defines an updating interface for objects that should be notified of changes in a Subject.
////
//// ConcreteObserver (Investor)
//// - Maintains a reference to a ConcreteSubject object
//// - Stores state that should stay consistent with the Subject's
//// - Implements the Observer updating interface to keep its state consistent with the Subject's

//// http://www.dofactory.com/net/observer-design-pattern

//using System;
//using System.Collections.Generic;

//// MainApp startup class for Observer Design Pattern.
//class MainApp
//{
//    // Entry point into console application.
//    static void Main()
//    {
//        // Configure Observer pattern
//        ConcreteSubject s = new ConcreteSubject();

//        s.Attach(new ConcreteObserver(s, "X"));
//        s.Attach(new ConcreteObserver(s, "Y"));

//        // Change subject and notify observers
//        s.SubjectState = "A";
//        s.Notify();

//        Console.WriteLine();

//        s.Attach(new ConcreteObserver(s, "Z"));

//        s.SubjectState = "B";
//        s.Notify();

//        // Wait for user input
//        Console.ReadKey();
//    }
//}

//// The 'Subject' abstract class
//abstract class Subject
//{
//    private List<Observer> _observers = new List<Observer>();

//    public void Attach(Observer observer)
//    {
//        _observers.Add(observer);
//    }

//    public void Detach(Observer observer)
//    {
//        _observers.Remove(observer);
//    }

//    public void Notify()
//    {
//        foreach (Observer ob in _observers)
//        {
//            ob.Update();
//        }
//    }
//}

//// The 'ConcreteSubject' class
//class ConcreteSubject : Subject
//{
//    private string _subjectState;

//    // Gets or sets subject state
//    public string SubjectState
//    {
//        get { return _subjectState; }
//        set { _subjectState = value; }
//    }
//}

//// The 'Observer' abstract class
//abstract class Observer
//{
//    public abstract void Update();
//}

//// The 'ConcreteObserver' class
//class ConcreteObserver : Observer
//{
//    private string _name;
//    private string _observerState;
//    private ConcreteSubject _subject;

//    // Constructor
//    public ConcreteObserver(ConcreteSubject subject, string name)
//    {
//        this._subject = subject;
//        this._name = name;
//    }

//    public override void Update()
//    {
//        // "Pull" the state from the Subject
//        _observerState = _subject.SubjectState;
//        Console.WriteLine("Observer {0}'s new state is {1}", _name, _observerState);
//    }

//    // Gets or sets subject
//    public ConcreteSubject Subject
//    {
//        get { return _subject; }
//        set { _subject = value; }
//    }
//}