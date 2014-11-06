// Singleton Design Pattern - Creational Category

// A Singleton stores common data in only one place. 
// A static class is also used to store single-instance data. 
// We save state between usages. We store caches to improve performance. 
// The object must be initialized only once and shared.

// Benchmark
// The version here is ten times faster than the version that does a null check. 
// For the version of Singleton I call naive, I add null checks and lazily instantiate the structure. 
// Here's a chart that compares 100 million accesses.
// Null check singleton: 435 ms
// Optimized singleton:   42 ms

// http://www.dotnetperls.com/singleton
// http://www.dotnetperls.com/singleton-static

using System;

// MainApp startup class for Singleton Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // Constructor is protected -- cannot use new
        SiteStructure s1 = SiteStructure.Instance;
        SiteStructure s2 = SiteStructure.Instance;

        // Test for same instance
        if (s1 == s2)
        {
            Console.WriteLine("Objects are the same instance");
        }

        // Wait for user
        Console.ReadKey();
    }
}

// Sample singleton object.
// Sealed allows the compiler to perform special optimizations during JIT compilation. 
public sealed class SiteStructure
{
    // This is an expensive resource.
    // We need to only store it in one place.
    object[] _data = new object[10];

    // Allocate ourselves.
    // We have a private constructor, so no one else can.
    // The readonly and static keywords are critical here. 
    // Readonly allows thread-safety, and that means it can only be allocated once.

    // This implementation is fast because the instance member is created directly in its declaration. 
    // FxCop warns when you initialize a static member in a static constructor. 
    // Static constructors are slower than most constructors.
    // Also they cause problems. They are lazily instantiated. 
    // Every access to the class must check that the static constructor has run.
    static readonly SiteStructure _instance = new SiteStructure();

    // This is a private constructor, meaning no outsiders have access.
    // A private constructor means that a class cannot be created anywhere but 
    // inside its own methods. So it must be accessed through the Singleton reference.
    private SiteStructure()
    {
        // Initialize members here.

        Console.WriteLine("Constructing first Singleton instance");
    }

    // Access SiteStructure.Instance to get the singleton object.
    // Then call methods on that instance.
    public static SiteStructure Instance
    {
        get { return _instance; }
    }
}