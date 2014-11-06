// Singleton Design Pattern - Creational Category

// The singleton pattern is one of the best-known patterns in software engineering. 
// Essentially, a singleton is a class which only allows a single instance of itself 
// to be created, and usually gives simple access to that instance. Most commonly, 
// singletons don't allow any parameters to be specified when creating the instance - 
// as otherwise a second request for an instance but with a different parameter could 
// be problematic! (If the same instance should be accessed for all requests with the 
// same parameter, the factory pattern is more appropriate.) This article deals only 
// with the situation where no parameters are required. Typically a requirement of 
// singletons is that they are created lazily - i.e. that the instance isn't created 
// until it is first needed.

// There are various different ways of implementing the singleton pattern in C#. 
// I shall present them here in reverse order of elegance, starting with the most 
// commonly seen, which is not thread-safe, and working up to a fully lazily-loaded, 
// thread-safe, simple and highly performant version.

// All these implementations share four common characteristics, however:

// - A single constructor, which is private and parameterless. This prevents other 
//   classes from instantiating it (which would be a violation of the pattern). 
//   Note that it also prevents subclassing - if a singleton can be subclassed once, 
//   it can be subclassed twice, and if each of those subclasses can create an instance, 
//   the pattern is violated. The factory pattern can be used if you need a single 
//   instance of a base type, but the exact type isn't known until runtime.
// - The class is sealed. This is unnecessary, strictly speaking, due to the above point, 
//   but may help the JIT to optimise things more.
// - A static variable which holds a reference to the single created instance, if any.
// - A public static means of getting the reference to the single created instance, 
// creating one if necessary.
//
// Note that all of these implementations also use a public static property Instance 
// as the means of accessing the instance. In all cases, the property could easily be 
// converted to a method, with no impact on thread-safety or performance.

// http://msdn.microsoft.com/en-us/library/ff650316.aspx
// http://csharpindepth.com/articles/general/singleton.aspx

using System;

// Bad code! Do not use!
//
// First version - not thread-safe
//
// Two different threads could both have evaluated the test 'if (instance==null)' 
// and found it to be true, then both create instances, which violates the singleton 
// pattern. Note that in fact the instance may already have been created before the 
// expression is evaluated, but the memory model doesn't guarantee that the new value of 
// instance will be seen by other threads unless suitable memory barriers have been passed.
public sealed class Singleton_Thread_Unsafe
{
    private static Singleton_Thread_Unsafe instance = null;

    private Singleton_Thread_Unsafe() { }

    public static Singleton_Thread_Unsafe Instance
    {
        get
        {
            if (instance == null)
                instance = new Singleton_Thread_Unsafe();

            return instance;
        }
    }
}


// Second version - simple thread-safety
//
// This implementation is thread-safe. The thread takes out a lock on a shared object, 
// and then checks whether or not the instance has been created before creating the instance. 
// This takes care of the memory barrier issue (as locking makes sure that all reads occur 
// logically after the lock acquire, and unlocking makes sure that all writes occur logically 
// before the lock release) and ensures that only one thread will create an instance (as only 
// one thread can be in that part of the code at a time - by the time the second thread enters 
// it, the first thread will have created the instance, so the expression will evaluate to false). 
// Unfortunately, performance suffers as a lock is acquired every time the instance is requested.

// Note that instead of locking on typeof(Singleton) as some versions of this implementation do, 
// I lock on the value of a static variable which is private to the class. Locking on objects 
// which other classes can access and lock on (such as the type) risks performance issues and 
// even deadlocks. This is a general style preference of mine - wherever possible, only lock on 
// objects specifically created for the purpose of locking, or which document that they are to be 
// locked on for specific purposes (e.g. for waiting/pulsing a queue). Usually such objects should 
// be private to the class they are used in. This helps to make writing thread-safe applications 
// significantly easier.
public sealed class Singleton_Thread_Safe_1
{
    private static Singleton_Thread_Safe_1 instance = null;
    private static readonly object padlock = new object();

    Singleton_Thread_Safe_1() { }

    public static Singleton_Thread_Safe_1 Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new Singleton_Thread_Safe_1();

                return instance;
            }
        }
    }
}

// Bad code! Do not use!
//
// Third version - attempted thread-safety using double-check locking
//
// This implementation attempts to be thread-safe without the necessity of taking out 
// a lock every time. Unfortunately, there are four downsides to the pattern:
// 
// - It doesn't work in Java. This may seem an odd thing to comment on, but it's worth knowing if
//   you ever need the singleton pattern in Java, and C# programmers may well also be Java programmers. 
//   The Java memory model doesn't ensure that the constructor completes before the reference to the new 
//   object is assigned to instance. The Java memory model underwent a reworking for version 1.5, 
//   but double-check locking is still broken after this without a volatile variable (as in C#).
// - Without any memory barriers, it's broken in the ECMA CLI specification too. It's possible that under 
//   the .NET 2.0 memory model (which is stronger than the ECMA spec) it's safe, but I'd rather not rely 
//   on those stronger semantics, especially if there's any doubt as to the safety. Making the instance 
//   variable volatile can make it work, as would explicit memory barrier calls, although in the latter 
//   case even experts can't agree exactly which barriers are required. I tend to try to avoid situations 
//   where experts don't agree what's right and what's wrong!
// - It's easy to get wrong. The pattern needs to be pretty much exactly as below - any significant changes 
//   are likely to impact either performance or correctness.
// - It still doesn't perform as well as the later implementations.
public sealed class Singleton_Thread_Safe_2
{
    private static Singleton_Thread_Safe_2 instance = null;
    private static readonly object padlock = new object();

    Singleton_Thread_Safe_2() { }

    public static Singleton_Thread_Safe_2 Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton_Thread_Safe_2();
                    }
                }
            }

            return instance;
        }
    }
}

// Fourth version - not quite as lazy, but thread-safe without using locks
//
// As you can see, this really is extremely simple - but why is it thread-safe and how lazy is it? 
// Well, static constructors in C# are specified to execute only when an instance of the 
// class is created or a static member is referenced, and to execute only once per AppDomain. 
// Given that this check for the type being newly constructed needs to be executed whatever 
// else happens, it will be faster than adding extra checking as in the previous examples. 
// There are a couple of wrinkles, however:
// - It's not as lazy as the other implementations. In particular, if you have static members 
//   other than Instance, the first reference to those members will involve creating the instance. 
//   This is corrected in the next implementation.
// - Blah-blah-blah ...
public sealed class Singleton_Thread_Safe_3
{
    private static readonly Singleton_Thread_Safe_3 instance = new Singleton_Thread_Safe_3();

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static Singleton_Thread_Safe_3() { }

    private Singleton_Thread_Safe_3() { }

    public static Singleton_Thread_Safe_3 Instance
    {
        get { return instance; }
    }
}

// Fifth version - fully lazy instantiation
// Here, instantiation is triggered by the first reference to the static member of the nested class, 
// which only occurs in Instance. This means the implementation is fully lazy, but has all the 
// performance benefits of the previous ones. Note that although nested classes have access to the 
// enclosing class's private members, the reverse is not true, hence the need for instance to be 
// internal here. That doesn't raise any other problems, though, as the class itself is private. 
// The code is a bit more complicated in order to make the instantiation lazy, however.
public sealed class Singleton_Lazy_Instantiation
{
    private Singleton_Lazy_Instantiation() { }

    public static Singleton_Lazy_Instantiation Instance 
    { 
        get { return Nested.instance; } 
    }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested() { }

        internal static readonly Singleton_Lazy_Instantiation instance 
            = new Singleton_Lazy_Instantiation();
    }
}

// Sixth version - using .NET 4's Lazy<T> type
//
// If you're using .NET 4 (or higher), you can use the System.Lazy<T> type to make 
// the laziness really simple. All you need to do is pass a delegate to the constructor 
// which calls the Singleton constructor - which is done most easily with a lambda expression.

// You typically should use the Lazy<T> type when you want to instantiate something the first 
// time its actually used. This delays the cost of creating it untill if/when it is needed 
// instead of always incurring the cost. Usually this is preferable when the object may or may 
// not be used and the cost of constructing it is non-trivial.
public sealed class Singleton
{
    // Because Singleton's constructor is private, we must explicitly
    // give the Lazy<Singleton> a delegate for creating the Singleton.
    private static readonly Lazy<Singleton> lazy =
        new Lazy<Singleton>(() => new Singleton());

    private Singleton() { }

    public static Singleton Instance { get { return lazy.Value; } }
}