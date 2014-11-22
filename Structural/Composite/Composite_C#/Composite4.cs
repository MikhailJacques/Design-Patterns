// Composite Design Pattern - Structural Category

// The composite design pattern allows us to set up a tree structure and ask each 
// element in the tree structure to perform a task. A typical tree structure would 
// be a company organization chart, where a CEO is at the top and other employees 
// are at the bottom. After the tree structure is established, we can then ask each 
// element, or employee, to perform a common operation.

// The composite pattern classifies each element in the tree as a composite or a leaf. 
// A composite means that there can be other elements below it, whereas a leaf cannot 
// have any elements below it. Therefore the leaf must be at the very bottom of the tree. 

// Example
// In a company, we have supervisors and workers. The supervisors can manage other 
// supervisors or workers under them. The supervisors will be the composites. 
// The workers do not manage anyone and they will be the leaves.

// All the supervisors and workers are employees, and as an employee you can always 
// show your happiness level in the company (this is the common operation of the elements).


// The IEmployee interface defines the operation that all employees must be able to perform, which is the ShowHappiness method.
// The Worker class are the employees that do not manage anyone, and implements only the ShowHappiness method.
// The Supervisor class are the employees that can manage other employees and have the following variables and methods:
// - The private variable subordinate are the list of employees that the supervisor manages.
// - The AddSubordinate method adds an employee under the supervisor.
// - The ShowHappiness method shows the supervisor's happiness level.
// When we call a supervisor's ShowHappiness method, it will show both the supervisor’s happiness and all of its subordinate’s 
// happiness by calling each of the subordinate's ShowHappiness method.

// The key to the composite design pattern is that it allows us to set up a structure with a common operation 
// (such as the ShowHappiness method), and then we can have all the elements to perform the common operation. 
// This is done by keeping a list of child elements that implements the common interface in the composite class, 
// and then calling each child element's operations.

// http://www.codeproject.com/Articles/185797/Composite-Design-Pattern

using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Supervisor Jerry = new Supervisor("Jerry", 7);
        Supervisor Marry = new Supervisor("Mary", 6);
        Supervisor Bob = new Supervisor("Bob", 9);
        Worker Jimmy = new Worker("Jimmy", 8);
        Worker Tom = new Worker("Tom", 5);
        Worker Alice = new Worker("Alice", 6);

        // Set up the relationships
        Jerry.AddSubordinate(Marry);    // Mary works for Jerry
        Jerry.AddSubordinate(Bob);      // Bob works for Jerry
        Marry.AddSubordinate(Tom);      // Tom works for Marry
        Bob.AddSubordinate(Jimmy);      // Jimmy works for Bob
        Bob.AddSubordinate(Alice);      // Alice works for Bob

        // Jerry shows his happiness and asks everyone else to do the same
        if (Jerry is IEmployee)
            (Jerry as IEmployee).ShowHappiness();

        Console.ReadKey();
    }
}

public interface IEmployee
{
    string name { get; set; }
    int happiness { get; set; }

    void ShowHappiness();
}

public class Worker : IEmployee
{
    // private string name;
    // private int happiness;
    public string name { get; set; }
    public int happiness { get; set; }

    public Worker(string name, int happiness)
    {
        this.name = name;
        this.happiness = happiness;
    }

    void IEmployee.ShowHappiness()
    {
        Console.WriteLine("\tWorker " + name + " showed happiness level of " + happiness);
    }
}

public class Supervisor : IEmployee
{
    // private string name;
    // private int happiness;
    public string name { get; set; }
    public int happiness { get; set; }

    private List<IEmployee> subordinate = new List<IEmployee>();

    public Supervisor(string name, int happiness)
    {
        this.name = name;
        this.happiness = happiness;
    }

    void IEmployee.ShowHappiness()
    {
        Console.WriteLine("Supervisor " + name + " showed happiness level of " + happiness);

        // Show all the supervisor's subordinates happiness level
        foreach (IEmployee i in subordinate)
            i.ShowHappiness();
    }

    public void AddSubordinate(IEmployee employee)
    {
        subordinate.Add(employee);
    }
}

// Output:
/*
Supervisor Jerry showed happiness level of 7
Supervisor Mary showed happiness level of 6
        Worker Tom showed happiness level of 5
Supervisor Bob showed happiness level of 9
        Worker Jimmy showed happiness level of 8
        Worker Alice showed happiness level of 6
*/