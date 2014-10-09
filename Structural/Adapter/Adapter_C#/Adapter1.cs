// Adapter Design Patter - Structural Category

// Convert the interface of a class into another interface clients expect. 
// Adapter lets classes work together that could not otherwise because of incompatible interfaces.

// This structural code demonstrates the Adapter design pattern which maps 
// the interface of one class onto another so that they can work together. 
// These incompatible classes may come from different libraries or frameworks.

// http://www.dofactory.com/net/adapter-design-pattern

using System;

namespace Adapter
{
    /// <summary>
    /// MainApp startup class for Structural
    /// Adapter Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create adapter and place a request
            Target target = new Adapter();
            target.Request();

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Target' class
    /// </summary>
    class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("Called Target Request()");
        }
    }

    /// <summary>
    /// The 'Adapter' class
    /// </summary>
    class Adapter : Target
    {
        private Adaptee _adaptee = new Adaptee();

        public override void Request()
        {
            // Possibly do some other work and then call SpecificRequest
            _adaptee.SpecificRequest();
        }
    }

    /// <summary>
    /// The 'Adaptee' class
    /// </summary>
    class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Called SpecificRequest()");
        }
    }
}