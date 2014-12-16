// Flyweight Design Pattern - Structural Category

// Some programs require a large number of objects that have some shared state among them. 
// Consider for example a game of war, were there is a large number of soldier objects.
// A soldier object maintains the graphical representation of a soldier, soldier's behavior such as motion, firing weapons,
// soldier's health and location on the war terrain. 
// Creating a large number of soldier objects is a necessity, however, it would incur a huge memory cost. 
// Note that although the representation and behavior of a soldier is the same, their health and location can vary greatly.

// Intent:
//
// The intent of this pattern is to use sharing to support a large number of objects that have part of their internal state 
// in common where the other part of state can vary.

// Participants:
//
// Flyweight - Declares an interface through which flyweights can receive and act on extrinsic state.
// ConcreteFlyweight - Implements the Flyweight interface and stores intrinsic state. 
// A Concrete flyweight object must be sharable.
// A Concrete flyweight object must maintain state that it is intrinsic to it, and must be able to manipulate state that is extrinsic. 
// In the war game example graphical representation is an intrinsic state, where location and health states are extrinsic. 
// Soldier moves, the motion behavior manipulates the external state (location) to create a new location.
// FlyweightFactory - The factory creates and manages flyweight objects. In addition the factory ensures sharing of the flyweight 
// objects. The factory maintains a pool of different flyweight objects and returns an object from the pool if it is already created,
// adds one to the pool and returns it in case it is new.
// In the war example a Soldier Flyweight factory can create two types of flyweights: 
// a Soldier flyweight, as well as a Colonel Flyweight. When the Client asks the Factory for a soldier, 
// the factory checks to see if there is a soldier in the pool, if there is, it is returned to the client, if there is no soldier 
// in pool, a soldier is created, added to pool, and returned to the client, the next time a client asks for a soldier, 
// the soldier created previously is returned, no new soldier is created.
// Client - A client maintains references to flyweights in addition to computing and maintaining extrinsic state.

// When a client needs a flyweight object it calls the factory to get the flyweight object. 
// The factory checks a pool of flyweights to determine if a flyweight object of the requested type is in the pool. 
// If there is, the reference to that object is returned. If there is no object of the required type, the factory creates 
// a flyweight of the requested type, adds it to the pool, and returns a reference to the flyweight. 
// The flyweight maintains intrinsic state (state that is shared among the large number of objects that we have created 
// the flyweight for) and provides methods to manipulate external state (state that varies from object to object and is 
// not common among the objects we have created the flyweight for).

// Applicability & Examples:
// The flyweight pattern applies to a program using a huge number of objects that have part of their internal state in common 
// where the other part of state can vary. The pattern is used when the larger part of the object's state can be made extrinsic 
// (external to that object).

// Related Patterns
// Factory and Singleton patterns - Flyweights are usually created using a factory and the singleton is applied to that factory 
// so that for each type or category of flyweights a single instance is returned.
// State and Strategy Patterns - State and Strategy objects are usually implemented as Flyweights.

// http://www.oodesign.com/flyweight-pattern.html

// Immutability and equality
// To enable safe sharing, between clients and threads, Flyweight objects must be immutable. 
// Flyweight objects are by definition value objects. The identity of the object instance is of no consequence 
// therefore two Flyweight instances of the same value are considered equal.
// Example in C# (note Equals and GetHashCode overrides as well as == and != operator overloads):

// Concurrency
// Special consideration must be made in scenarios where Flyweight objects are created on multiple threads. 
// If the list of values is finite and known in advance the Flyweights can be instantiated ahead of time and retrieved from a 
// container on multiple threads with no contention. If Flyweights are instantiated on multiple threads there are two options:
//
// - Make Flyweight instantiation single threaded thus introducing contention and ensuring one instance per value.
// - Allow concurrent threads to create multiple Flyweight instances thus eliminating contention and allowing multiple 
//   instances per value. This option is only viable if the equality criterion is met.

// http://en.wikipedia.org/wiki/Flyweight_pattern

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

public class CoffeeFlavour
{
    private readonly string _flavour;

    public CoffeeFlavour(string flavour)
    {
        _flavour = flavour;
    }

    public string Flavour
    {
        get { return _flavour; }
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is CoffeeFlavour && Equals((CoffeeFlavour)obj);
    }

    public bool Equals(CoffeeFlavour other)
    {
        return string.Equals(_flavour, other._flavour);
    }

    public override int GetHashCode()
    {
        return (_flavour != null ? _flavour.GetHashCode() : 0);
    }

    public static bool operator ==(CoffeeFlavour a, CoffeeFlavour b)
    {
        return Equals(a, b);
    }

    public static bool operator !=(CoffeeFlavour a, CoffeeFlavour b)
    {
        return !Equals(a, b);
    }
}

public interface ICoffeeFlavourFactory
{
    CoffeeFlavour GetFlavour(string flavour);
}

public class ReducedMemoryFootprint : ICoffeeFlavourFactory
{
    private readonly object _cacheLock = new object();
    private readonly IDictionary<string, CoffeeFlavour> _cache = new Dictionary<string, CoffeeFlavour>();

    public CoffeeFlavour GetFlavour(string flavour)
    {
        if (_cache.ContainsKey(flavour)) return _cache[flavour];
        var coffeeFlavour = new CoffeeFlavour(flavour);
        ThreadPool.QueueUserWorkItem(AddFlavourToCache, coffeeFlavour);
        return coffeeFlavour;
    }

    private void AddFlavourToCache(object state)
    {
        var coffeeFlavour = (CoffeeFlavour)state;
        if (!_cache.ContainsKey(coffeeFlavour.Flavour))
        {
            lock (_cacheLock)
            {
                if (!_cache.ContainsKey(coffeeFlavour.Flavour)) _cache.Add(coffeeFlavour.Flavour, coffeeFlavour);
            }
        }
    }
}

public class MinimumMemoryFootprint : ICoffeeFlavourFactory
{
    private readonly ConcurrentDictionary<string, CoffeeFlavour> _cache = new ConcurrentDictionary<string, CoffeeFlavour>();

    public CoffeeFlavour GetFlavour(string flavour)
    {
        return _cache.GetOrAdd(flavour, flv => new CoffeeFlavour(flv));
    }
}