// Strategy Design Pattern - Behavioral Category

// Strategy pattern defines a family of algorithms, 
// encapsulates each one and makes them interchangeable. 
// Strategy lets the algorithm vary independently from clients that use it.

//  The classes and objects participating in this pattern are:

// Strategy (SortStrategy)
// - Declares an interface common to all supported algorithms. 
// - Context uses this interface to call the algorithm defined by a ConcreteStrategy.
//
// ConcreteStrategy (QuickSort, ShellSort, MergeSort)
// - Implements the algorithm using the Strategy interface.
//
// Context (SortedList)
// - Is configured with a ConcreteStrategy object.
// - Maintains a reference to a Strategy object.
// - May define an interface that lets Strategy access its data.

// This following structural code demonstrates the Strategy pattern 
// which encapsulates functionality in the form of an object. 
// This allows clients to dynamically change algorithmic strategies.

// Points of Interest
// Having learned strategy pattern think of applying this at places where you feel 
// your objects needs to perform a similar action but that action has difference in 
// the way it is being performed. Think of applying this pattern for a discounting 
// system, which calculates a discount for different customers. So this system would 
// decide at run time which discounting method to call based on type of customer.

// http://www.dofactory.com/net/strategy-design-pattern
// http://www.codeproject.com/Articles/776819/Strategy-Pattern-Csharp


using System;
using System.Collections.Generic;

// MainApp startup class for Real-World 
// Strategy Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // Two contexts following different strategies
        SortedList studentRecords = new SortedList();

        studentRecords.Add("Samual");
        studentRecords.Add("Jimmy");
        studentRecords.Add("Sandra");
        studentRecords.Add("Vivek");
        studentRecords.Add("Anna");
        studentRecords.Add("Mike");

        studentRecords.Print();

        studentRecords.SetSortStrategy(new QuickSort());
        studentRecords.Sort();
        studentRecords.Print();

        studentRecords.SetSortStrategy(new ShellSort());
        studentRecords.Sort();
        studentRecords.Print();

        studentRecords.SetSortStrategy(new MergeSort());
        studentRecords.Sort();
        studentRecords.Print();

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Strategy' abstract class
// - Declares an interface common to all supported algorithms. 
// - The 'Context' class uses this interface to call the algorithm 
// defined by a 'ConcreteStrategy' class.
abstract class SortStrategy
{
    public abstract void Sort(List<string> list);
}

// A 'ConcreteStrategy' class
class QuickSort : SortStrategy
{
    public override void Sort(List<string> list)
    {
        list.Sort(); // Default is Quicksort
        Console.WriteLine("QuickSorted list ");
    }
}

// A 'ConcreteStrategy' class
class ShellSort : SortStrategy
{
    public override void Sort(List<string> list)
    {
        // list.ShellSort(); not-implemented
        Console.WriteLine("ShellSorted list ");
    }
}

// A 'ConcreteStrategy' class
class MergeSort : SortStrategy
{
    public override void Sort(List<string> list)
    {
        // list.MergeSort(); not-implemented        
        Console.WriteLine("MergeSorted list ");
    }
}

// The 'Context' class
class SortedList
{
    private List<string> _list = new List<string>();
    private SortStrategy _sortstrategy;

    public void Add(string name)
    {
        _list.Add(name);
    }

    public void SetSortStrategy(SortStrategy sortstrategy)
    {
        _sortstrategy = sortstrategy;
    }

    public void Sort()
    {
        _sortstrategy.Sort(_list);
    }

    public void Print()
    {
        // Iterate over list and display results
        foreach (string name in _list)
        {
            Console.WriteLine(" " + name);
        }

        Console.WriteLine();
    }
}