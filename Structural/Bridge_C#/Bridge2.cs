// Bridge Design Pattern - Structural Category

// Decouples an abstraction from its implementation so that the two can vary independently.

// Motivation

// Sometimes an abstraction should have different implementations.
// Consider an object that handles persistence of objects over different platforms 
// using either relational databases or file system structures (files and folders). 
// A simple implementation might choose to extend the object itself to implement the 
// functionality for both file system and RDBMS. 
// However this implementation would create a problem.
// Inheritance binds an implementation to the abstraction and thus it would be difficult 
// to modify, extend, and reuse abstraction and implementation independently.


// Participants
// The classes and objects participating in this pattern are:

// Abstraction (BusinessObject)
// Defines the abstraction's interface.
// Maintains a reference to an object of type Implementor.

// RefinedAbstraction (CustomersBusinessObject)
// Extends the interface defined by Abstraction.

// Implementor (Data)
// Defines the interface for implementation classes. 
// This interface does not have to correspond exactly to Abstraction's interface.
// In fact the two interfaces can be quite different. 
// Typically the Implementation interface provides only primitive operations, 
// and Abstraction defines higher-level operations based on these primitives.

// ConcreteImplementor (CustomersDataObject)
// Implements the Implementor interface and defines its concrete implementation.

// This real-world code demonstrates the Bridge pattern in which a BusinessObject 
// abstraction is decoupled from the implementation in Data. 
// The Data implementations can evolve dynamically without changing any clients.

// http://www.dofactory.com/net/bridge-design-pattern

using System;
using System.Collections.Generic;

namespace Bridge
{
    /// <summary>
    /// MainApp startup class for Real-World 
    /// Bridge Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create RefinedAbstraction
            Customers customers = new Customers("Chicago");

            // Set ConcreteImplementor
            customers.Data = new CustomersData();

            // Exercise the bridge
            customers.Show();
            customers.Next();
            customers.Show();
            customers.Next();
            customers.Show();

            customers.Add("Henry Velasquez");
            customers.Add("Mikhail Jacques");

            customers.Next();
            customers.Show();

            customers.Add("Tom Jerry");

            customers.ShowAll();

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Abstraction' class (BusinessObject)
    // Defines the abstraction's interface.
    // Maintains a reference to an object of type Implementor.
    /// </summary>
    class CustomersBase
    {
        protected string group;
        private Data _dataObject;   // Implementer

        public CustomersBase(string group)
        {
            this.group = group;
        }

        // Property
        public Data Data
        {
            set { _dataObject = value; }
            get { return _dataObject; }
        }

        public virtual void Next()
        {
            _dataObject.NextRecord();
        }

        public virtual void Prior()
        {
            _dataObject.PriorRecord();
        }

        public virtual void Add(string customer)
        {
            _dataObject.AddRecord(customer);
        }

        public virtual void Delete(string customer)
        {
            _dataObject.DeleteRecord(customer);
        }

        public virtual void Show()
        {
            _dataObject.ShowRecord();
        }

        public virtual void ShowAll()
        {
            Console.WriteLine("Customer Group: " + group);
            _dataObject.ShowAllRecords();
        }
    }

    /// <summary>
    /// The 'RefinedAbstraction' class (CustomersBusinessObject)
    // Extends the interface defined by Abstraction.
    // Provides an implementation in terms of operations provided by Implementor interface.
    /// </summary>
    class Customers : CustomersBase
    {
        // Constructor
        public Customers(string group) : base(group) { }

        public override void ShowAll()
        {
            // Add separator lines
            Console.WriteLine();
            Console.WriteLine("------------------------");
            base.ShowAll();
            Console.WriteLine("------------------------");
        }
    }

    /// <summary>
    /// The 'Implementor' abstract class (Data)
    // Defines the interface for implementation classes. 
    // This interface does not have to correspond exactly to Abstraction's interface.
    // In fact the two interfaces can be quite different. 
    // Typically the Implementation interface provides only primitive operations, 
    // and Abstraction defines higher-level operations based on these primitives.
    /// </summary>
    abstract class Data
    {
        public abstract void NextRecord();
        public abstract void PriorRecord();
        public abstract void AddRecord(string name);
        public abstract void DeleteRecord(string name);
        public abstract void ShowRecord();
        public abstract void ShowAllRecords();
    }

    /// <summary>
    /// The 'ConcreteImplementor' class (CustomersDataObject)
    // Implements the Implementor interface and defines its concrete implementation.
    /// </summary>
    class CustomersData : Data
    {
        private int _current = 0;
        private List<string> _customers = new List<string>();

        public CustomersData()
        {
            // Load (simulate loading) data from a database 
            _customers.Add("Jim Jones");
            _customers.Add("Samual Jackson");
            _customers.Add("Allen Good");
            _customers.Add("Ann Stills");
            _customers.Add("Lisa Giolani");
        }

        public override void NextRecord()
        {
            if (_current <= _customers.Count - 1)
            {
                _current++;
            }
        }

        public override void PriorRecord()
        {
            if (_current > 0)
            {
                _current--;
            }
        }

        public override void AddRecord(string customer)
        {
            _customers.Add(customer);
        }

        public override void DeleteRecord(string customer)
        {
            _customers.Remove(customer);
        }

        public override void ShowRecord()
        {
            Console.WriteLine(_customers[_current]);
        }

        public override void ShowAllRecords()
        {
            foreach (string customer in _customers)
            {
                Console.WriteLine(" " + customer);
            }
        }
    }
}