// Singleton Design Pattern - Creational Category

// Singleton pattern ensures a class has only one instance and 
// provides a global point of access to it.

// The classes and objects participating in this pattern are:

// Singleton
// - Defines an Instance operation that lets clients access its unique instance. 
//   Instance is a class operation.
// - Responsible for creating and maintaining its own unique instance.

// http://www.dofactory.com/net/singleton-design-pattern
// http://sourcemaking.com/design_patterns/singleton/c-sharp-dot-net

using System;

// MainApp startup class for Singleton Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // Constructor is protected -- cannot use new
        Singleton s1 = Singleton.Instance();
        Singleton s2 = Singleton.Instance();

        // Test for same instance
        if (s1 == s2)
        {
            Console.WriteLine("Objects are the same instance");
        }

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Singleton' class
class Singleton
{
    private static Singleton _instance;

    // Constructor is 'protected'
    protected Singleton()
    {
        Console.WriteLine("Constructing first Singleton instance");
    }

    public static Singleton Instance()
    {
        // Uses lazy initialization.
        // Note: this is not thread safe.
        if (_instance == null)
        {
            _instance = new Singleton();
        }

        return _instance;
    }
}