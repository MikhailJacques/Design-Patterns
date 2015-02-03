// Observer Design Pattern - Behavioral Category

// The Observer Design Pattern enables a Subscriber to register with and receive notifications from a Provider. 
// It is suitable for any scenario that requires push-based notification. 
// The pattern defines a Provider (also known as a Subject or an Observable) and zero, one, or more Subscribers 
// (also known as Observers). Subscribers register with the Provider, and whenever a predefined condition, event, 
// or state change occurs, the Provider automatically notifies all Subscribers by calling one of their methods. 
// In this method call, the Provider (Subject) can also provide current state information to Subscribers (Observers). 
// In the .NET Framework, the Observer design pattern is applied by implementing the generic System.IObservable<T> and 
// System.IObserver<T> interfaces. The generic type parameter represents the type that provides notification information.

// Applying the Pattern
//
// The Observer Design Pattern is suitable for distributed push-based notifications, because it supports a clean 
// separation between two different components or application layers, such as a data source (business logic) layer 
// and a user interface (display) layer. The pattern can be implemented whenever a Provider uses callbacks to supply 
// its clients with current information.

// Implementing the pattern requires the following:
//
// A Provider (Subject), which is the object that sends notifications to Subscribers (Observers). 
// A Provider is a class or structure that implements the IObservable<T> interface. 
// The Provider must implement a single method, IObservable<T>.Subscribe, which is called by Subscribers
// that wish to receive notifications from the Provider.

// - A Subscriber (Observer) is an object that receives notifications from a Provider. 
// A Subscriber is a class or structure that implements the IObserver<T> interface. 
// The Subscriber must implement three methods, all of which are called by the Provider:
//
// -- IObserver<T>.OnNext - supplies the Subscriber with new or current information.
// -- IObserver<T>.OnError - informs the Subscriber that an error has occurred.
// -- IObserver<T>.OnCompleted - indicates that the Provider has finished sending notifications.
//
// A mechanism that allows the Provider to keep track of Subscribers. 
// Typically, the Provider uses a container object, such as a System.Collections.Generic.List<T> object, 
// to hold references to the IObserver<T> implementations that have subscribed to notifications. 
// Using a storage container for this purpose enables the Provider to handle zero to an unlimited number of Subscribers. 
// The order in which Subscribers receive notifications is not defined; the Provider is free to use any method 
// to determine the order.

// An IDisposable implementation that enables the Provider to remove Subscribers when notification is complete. 
// Subscribers receive a reference to the IDisposable implementation from the subscribe method, so they can also 
// call the IDisposable.Dispose method to unsubscribe before the Provider has finished sending notifications.

// An object that contains the data that the Provider sends to its Subscribers. The type of this object corresponds 
// to the generic type parameter of the IObservable<T> and IObserver<T> interfaces. Although this object can be the 
// same as the IObservable<T> implementation, most commonly it is a separate type.

// The following example uses the Observer Design Pattern to implement an airport baggage claim information system. 
// A BaggageInfo class provides information about arriving flights and the carousels where baggage from each flight 
// is available for pickup.

// http://msdn.microsoft.com/en-us/library/ee850490(v=vs.110).aspx

using System;
using System.Collections.Generic;

// Provides information about arriving flights and the carousels 
// where baggage from each flight is available for pickup.
public class BaggageInfo
{
    private int flightNo;
    private string from;
    private int carousel;

    internal BaggageInfo(int flight, string from, int carousel)
    {
        this.flightNo = flight;
        this.from = from;
        this.carousel = carousel;
    }

    public int FlightNumber
    {
        get { return this.flightNo; }
    }

    public string From
    {
        get { return this.from; }
    }

    public int Carousel
    {
        get { return this.carousel; }
    }
}

// A Provider (Subject) is the object that sends notifications to Subscribers (Observers). 
// A Provider is a class or structure that implements the IObservable<T> interface. 
// The Provider must implement a single method, IObservable<T>.Subscribe, 
// which is called by Subscribers that wish to receive notifications from the Provider.

// A BaggageHandler class is responsible for receiving information about arriving flights and baggage claim carousels. 
// Internally, it maintains two collections:
// - flights - A collection of flights and their assigned carousels.
// - observers - A collection of clients that will receive updated information.
// Both collections are represented by generic List<T> objects 
// that are instantiated in the BaggageHandler class constructor. 

// Subject
public class BaggageHandler : IObservable<BaggageInfo>
{
    private List<BaggageInfo> flights;
    private List<IObserver<BaggageInfo>> observers;

    public BaggageHandler()
    {
        flights = new List<BaggageInfo>();
        observers = new List<IObserver<BaggageInfo>>();
    }

    // Called by Subscribers that wish to receive notifications from the Provider.
    public IDisposable Subscribe(IObserver<BaggageInfo> observer)
    {
        // Check whether the Subscriber is already registered. If not, register it.
        if (!observers.Contains(observer))
        {
            observers.Add(observer);

            // Provide the newly registered Subscriber with existing data. 
            foreach (var item in flights)
                observer.OnNext(item);
        }

        return new Unsubscriber<BaggageInfo>(observers, observer);
    }

    // The overloaded BaggageHandler.BaggageStatus method can be called to indicate 
    // that baggage from a flight is either being unloaded or is no longer being unloaded.

    // In the first case, the method is passed a flight number, the airport from which 
    // the flight originated and the carousel where the baggage is being unloaded. 
    // In the second case, the method is passed only a flight number. 

    // Called to indicate that baggage from a flight is being unloaded.
    public void BaggageStatus(int flightNo, string from, int carousel)
    {
        var info = new BaggageInfo(flightNo, from, carousel);

        // Carousel is assigned, so add new info object to list. 
        if (carousel > 0 && !flights.Contains(info))
        {
            flights.Add(info);

            foreach (var observer in observers)
                observer.OnNext(info);
        }
        else if (carousel == 0)
        {
            // Baggage claim for flight is done 
            var flightsToRemove = new List<BaggageInfo>();

            foreach (var flight in flights)
            {
                if (info.FlightNumber == flight.FlightNumber)
                {
                    flightsToRemove.Add(flight);

                    foreach (var observer in observers)
                        observer.OnNext(info);
                }
            }

            foreach (var flightToRemove in flightsToRemove)
                flights.Remove(flightToRemove);

            flightsToRemove.Clear();
        }
    }

    // Called to indicate all baggage has been unloaded. 
    public void BaggageStatus(int flightNo)
    {
        BaggageStatus(flightNo, String.Empty, 0);
    }

    public void LastBaggageClaimed()
    {
        foreach (var observer in observers)
            observer.OnCompleted();

        observers.Clear();
    }
}

// Clients that wish to receive updated information call the BaggageInfo.Subscribe method. 
// If the client has not previously subscribed to notifications, a reference to the client's 
// IObserver<T> implementation is added to the Observers collection.

// The overloaded BaggageHandler.BaggageStatus method can be called to indicate that baggage 
// from a flight is either being unloaded or is no longer being unloaded. In the first case, 
// the method is passed a flight number, the airport from which the flight originated and the 
// carousel where baggage is being unloaded. 
// In the second case, the method is passed only a flight number. 
// For baggage that is being unloaded, the method checks whether the BaggageInfo information 
// passed to the method exists in the flights collection. If it does not, the method adds the 
// information and calls each Observer's OnNext method. 
// For flights whose baggage is no longer being unloaded, the method checks whether information 
// on that flight is stored in the flights collection. If it is, the method calls each observer's 
// OnNext method and removes the BaggageInfo object from the flights collection.

// When the last flight of the day has landed and its baggage has been processed, the 
// BaggageHandler.LastBaggageClaimed method is called. This method calls each Observer's OnCompleted 
// method to indicate that all notifications have completed, and then clears the observers collection.

// The Provider's Subscribe method returns an IDisposable implementation that enables Observers 
// to stop receiving notifications before the OnCompleted method is called. The source code for 
// this Unsubscriber (of BaggageInfo) class is shown in the following example. When the class is 
// instantiated in the BaggageHandler.Subscribe method, it is passed a reference to the Observers 
// collection and a reference to the Observer that is added to the collection. These references 
// are assigned to local variables. When the Object's Dispose method is called, it checks whether 
// the Observer still exists in the Observers collection, and, if it does, removes the Observer.

internal class Unsubscriber<BaggageInfo> : IDisposable
{
    private List<IObserver<BaggageInfo>> _observers;
    private IObserver<BaggageInfo> _observer;

    internal Unsubscriber(List<IObserver<BaggageInfo>> observers, IObserver<BaggageInfo> observer)
    {
        this._observers = observers;
        this._observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
            _observers.Remove(_observer);
    }
}

// The following example provides an IObserver<T> implementation named ArrivalsMonitor, 
// which is a base class that displays baggage claim information. 
// The information is displayed alphabetically, by the name of the originating city. 
// The methods of ArrivalsMonitor are marked as virtual in C#, so they can be overridden in a derived class.

// The ArrivalsMonitor class includes the Subscribe and Unsubscribe methods. 
// The Subscribe method enables the class to save the IDisposable implementation 
// returned by the call to Subscribe to a private variable. 
// The Unsubscribe method enables the class to unsubscribe from notifications by calling the Provider's Dispose implementation. 
// ArrivalsMonitor also provides implementations of the OnNext, OnError and OnCompleted methods. 
// Only the OnNext implementation contains a significant amount of code. The method works with a private, sorted, 
// generic List<T> object that maintains information about the airports of origin for arriving flights and 
// the carousels on which their baggage is available. If the BaggageHandler class reports a new flight arrival, 
// the OnNext method implementation adds information about that flight to the list. If the BaggageHandler class 
// reports that the flight's baggage has been unloaded, the OnNext method removes that flight from the list. 
// Whenever a change is made, the list is sorted and displayed to the console.

// Observer
public class ArrivalsMonitor : IObserver<BaggageInfo>
{
    private string name;
    private List<string> flightInfos = new List<string>();
    private IDisposable cancellation;
    private string fmt = "{0,-20} {1,5}  {2, 3}";

    public ArrivalsMonitor(string name)
    {
        if (String.IsNullOrEmpty(name))
            throw new ArgumentNullException("The Observer must be assigned a name.");

        this.name = name;
    }

    public virtual void Subscribe(BaggageHandler provider)
    {
        cancellation = provider.Subscribe(this);
    }

    public virtual void Unsubscribe()
    {
        cancellation.Dispose();
        flightInfos.Clear();
    }

    public virtual void OnCompleted()
    {
        flightInfos.Clear();
    }

    // No implementation needed: Method is not called by the BaggageHandler class. 
    public virtual void OnError(Exception e)
    {
        // No implementation.
    }

    // Update information. 
    public virtual void OnNext(BaggageInfo info)
    {
        bool updated = false;

        // Flight has unloaded its baggage; remove from the monitor. 
        if (info.Carousel == 0)
        {
            var flightsToRemove = new List<string>();
            string flightNo = String.Format("{0,5}", info.FlightNumber);

            foreach (var flightInfo in flightInfos)
            {
                if (flightInfo.Substring(21, 5).Equals(flightNo))
                {
                    flightsToRemove.Add(flightInfo);
                    updated = true;
                }
            }

            foreach (var flightToRemove in flightsToRemove)
                flightInfos.Remove(flightToRemove);

            flightsToRemove.Clear();
        }
        else
        {
            // Add flight if it does not exist in the collection. 
            string flightInfo = String.Format(fmt, info.From, info.FlightNumber, info.Carousel);

            if (!flightInfos.Contains(flightInfo))
            {
                flightInfos.Add(flightInfo);
                updated = true;
            }
        }

        if (updated)
        {
            flightInfos.Sort();

            Console.WriteLine("Arrivals information from {0}", this.name);

            foreach (var flightInfo in flightInfos)
                Console.WriteLine(flightInfo);

            Console.WriteLine();
        }
    }
}

// The following example contains the application entry point that instantiates the BaggageHandler class as well as two 
// instances of the ArrivalsMonitor class, and uses the BaggageHandler.BaggageStatus method to add and remove information 
// about arriving flights. In each case, the Observers receive updates and correctly display baggage claim information.

public class MainApp
{
    public static void Main()
    {
        BaggageHandler provider = new BaggageHandler();

        ArrivalsMonitor observer1 = new ArrivalsMonitor("BaggageClaimMonitor1");
        ArrivalsMonitor observer2 = new ArrivalsMonitor("SecurityExit");

        provider.BaggageStatus(712, "Detroit", 3);
        observer1.Subscribe(provider);

        provider.BaggageStatus(712, "Kalamazoo", 3);
        provider.BaggageStatus(400, "New York-Kennedy", 1);
        provider.BaggageStatus(712, "Detroit", 3);

        observer2.Subscribe(provider);

        provider.BaggageStatus(511, "San Francisco", 2);
        provider.BaggageStatus(712);

        observer2.Unsubscribe();

        provider.BaggageStatus(400);
        provider.LastBaggageClaimed();

        // Wait for user
        Console.ReadKey();
    }
}