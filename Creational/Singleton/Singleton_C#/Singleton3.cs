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

// The .NET optimized code demonstrates the same code as in Sigleton2.cs, but uses more 
// modern, built-in .NET features. Here an elegant .NET specific solution is offered. 
// The Singleton pattern simply uses a private constructor and a static readonly instance 
// variable that is lazily initialized. Thread safety is guaranteed by the compiler.

// http://www.dofactory.com/net/singleton-design-pattern

using System;
using System.Collections.Generic;

// MainApp startup class for .NET optimized 
// Singleton Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        LoadBalancer b1 = LoadBalancer.GetLoadBalancer();
        LoadBalancer b2 = LoadBalancer.GetLoadBalancer();
        LoadBalancer b3 = LoadBalancer.GetLoadBalancer();
        LoadBalancer b4 = LoadBalancer.GetLoadBalancer();

        // Confirm these are the same instance
        if (b1 == b2 && b2 == b3 && b3 == b4)
        {
            Console.WriteLine("Same instance\n");
        }

        // Next, load balance 10 requests for a server
        LoadBalancer balancer = LoadBalancer.GetLoadBalancer();

        for (int i = 0; i < 10; i++)
        {
            string serverName = balancer.NextServer.Name;
            Console.WriteLine("Dispatch request to: " + serverName);
        }

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Singleton' class
sealed class LoadBalancer
{
    // Static members are 'eagerly initialized', that is, immediately when a class is
    // loaded for the first time. .NET guarantees thread safety for static initialization
    private static readonly LoadBalancer _instance = new LoadBalancer();

    // Type-safe generic list of servers
    private List<Server> _servers;
    private Random _random = new Random();

    // Note: constructor is 'private'
    private LoadBalancer()
    {
        // Load list of available servers
        _servers = new List<Server> 
        { 
            new Server{ Name = "Server1", IP = "120.14.220.18" },
            new Server{ Name = "Server2", IP = "120.14.220.19" },
            new Server{ Name = "Server3", IP = "120.14.220.20" },
            new Server{ Name = "Server4", IP = "120.14.220.21" },
            new Server{ Name = "Server5", IP = "120.14.220.22" },
        };
    }

    public static LoadBalancer GetLoadBalancer()
    {
        return _instance;
    }

    // Simple, but effective load balancer
    public Server NextServer
    {
        get
        {
            int r = _random.Next(_servers.Count);
            return _servers[r];
        }
    }
}

// Represents a server machine
class Server
{
    // Gets or sets server name
    public string Name { get; set; }

    // Gets or sets server IP address
    public string IP { get; set; }
}