// Singleton Design Pattern - Creational Category

// Singleton pattern ensures a class has only one instance and 
// provides a global point of access to it.

// The classes and objects participating in this pattern are:

// Singleton (LoadBalancer)
// - Defines an Instance operation that lets clients access its unique instance. 
//   Instance is a class operation.
// - Responsible for creating and maintaining its own unique instance.

// This real-world code demonstrates the Singleton pattern as a LoadBalancing object. 
// Only a single instance (the singleton) of the class can be created because servers 
// may dynamically come on-line or off-line and every request must go throught the one 
// object that has knowledge about the state of the (web) farm.

// http://www.dofactory.com/net/singleton-design-pattern

using System;
using System.Collections.Generic;
using System.Threading;

// MainApp startup class for Real-World Singleton Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        LoadBalancer b1 = LoadBalancer.GetLoadBalancer();
        LoadBalancer b2 = LoadBalancer.GetLoadBalancer();
        LoadBalancer b3 = LoadBalancer.GetLoadBalancer();
        LoadBalancer b4 = LoadBalancer.GetLoadBalancer();

        // Same instance?
        if (b1 == b2 && b2 == b3 && b3 == b4)
        {
            Console.WriteLine("Same instance\n");
        }

        // Load balance 10 server requests
        LoadBalancer balancer = LoadBalancer.GetLoadBalancer();

        for (int i = 0; i < 10; i++)
        {
            string server = balancer.Server;
            Console.WriteLine("Dispatch Request to: " + server);
        }

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Singleton' class
class LoadBalancer
{
    private static LoadBalancer _instance;
    private List<string> _servers = new List<string>();
    private Random _random = new Random();

    // Lock synchronization object
    private static object syncLock = new object();

    // Constructor (protected)
    protected LoadBalancer()
    {
        // List of available servers
        _servers.Add("Server1");
        _servers.Add("Server2");
        _servers.Add("Server3");
        _servers.Add("Server4");
        _servers.Add("Server5");
    }

    public static LoadBalancer GetLoadBalancer()
    {
        // Support multithreaded applications through 'Double checked locking' pattern 
        // which (once the instance exists) avoids locking each time the method is invoked
        if (_instance == null)
        {
            lock (syncLock)
            {
                if (_instance == null)
                {
                    _instance = new LoadBalancer();
                }
            }
        }

        return _instance;
    }

    // Simple, but effective random load balancer
    public string Server
    {
        get
        {
            int r = _random.Next(_servers.Count);
            return _servers[r].ToString();
        }
    }
}