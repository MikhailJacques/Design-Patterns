// Iterator Design Pattern - Behavioral Category

// GoF defines the Iterator pattern as:
// "Provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation".

// Background:
// Having a collection of objects is a very common thing in software development. 
// If we have a collection of objects then we might also find ourselves in need to traverse this collection. 
// Most languages provide traversal techniques over basic collection types. 
// C# also contains some special container types capable of holding a collection of values (example: Lists, and ArrayLists in C#). 
// These specialized containers also come with the possibility of getting iterated. 
// C# container classes are the best examples of how the iterator pattern is implemented.

// If we want the underlying working mechanism of these iterator objects then we will perhaps need to understand 
// the Iterator pattern first. The idea behind the Iterator pattern is that we decouple the actual collection object 
// from the traversal logic. This will make the collection object lighter as it does not have to deal with all the iteration 
// related functionalities and from the user's point of view, there is a clear separation between the collection and how the 
// collection is being iterated. Also, the user will not have to worry about keeping track of the number of items traversed, 
// remaining, and whether to check for boundary conditions as all this is already being done in the iterator object 
// (as these things will depend on the underlying structure and implementation of the collection object).

// Using the code:
// IIterator:   This is an interface that defines the methods for accessing and traversing elements.
// MyIterator:  This is ConcreteIterator, this implements the Iterator interface and keeps track 
//              of the current position in the traversal of the aggregate object.
// IAggregate:  This is an interface that defines methods for creating an Iterator object.
// MyAggregate: This is the ConcreteAggregate object, i.e., the real collection lies inside this.
//              This class implements the IAggregate creation interface.

// Points of Interest:
// The .NET Framework and C# language has the Iterator pattern embedded deep in its code. 
// The IEnumerable interface is in fact the facilitator of the Iterator pattern. 
// Generics and Collection classes in C# can be iterated through an enumerator which is in fact 
// an Iterator pattern implementation.  

// http://www.codeproject.com/Articles/362986/Understanding-and-Implementing-the-Iterator-Patter

using System;
using System.Collections.Generic;
using System.Text;

interface IIterator
{
    string FirstItem { get; }
    string NextItem { get; }
    string CurrentItem { get; }
    bool IsDone { get; }
}

interface IAggregate
{
    IIterator GetIterator();
    string this[int itemIndex] { set; get; }
    int Count { get; }

}

class MyAggregate : IAggregate
{
    List<string> values = null;

    public MyAggregate()
    {
        values = new List<string>();
    }

    #region IAggregate Members

    public IIterator GetIterator()
    {
        return new MyIterator(this);
    }

    #endregion

    public string this[int itemIndex]
    {
        get
        {
            if (itemIndex < values.Count)
            {
                return values[itemIndex];
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            values.Add(value);
        }
    }

    public int Count
    {
        get
        {
            return values.Count;
        }
    }
}


class MyIterator : IIterator
{
    int current_index = 0;
    IAggregate aggregate = null;

    public MyIterator(IAggregate aggregate)
    {
        this.aggregate = aggregate;
    }

    #region IIterator Members

    public string FirstItem
    {
        get
        {
            current_index = 0;
            return aggregate[current_index];
        }
    }

    public string NextItem
    {
        get
        {
            current_index += 1;

            if (IsDone == false)
            {
                return aggregate[current_index];
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public string CurrentItem
    {
        get
        {
            return aggregate[current_index];
        }
    }

    public bool IsDone
    {
        get
        {
            if (current_index < aggregate.Count)
            {
                return false;
            }

            return true;
        }
    }

    #endregion
}


class Program
{
    static void Main(string[] args)
    {
        MyAggregate aggr = new MyAggregate();

        aggr[0] = "1";
        aggr[1] = "2";
        aggr[2] = "3";
        aggr[3] = "4";
        aggr[4] = "5";
        aggr[5] = "6";
        aggr[6] = "7";
        aggr[7] = "8";
        aggr[8] = "9";
        aggr[9] = "Bob";

        IIterator iter = aggr.GetIterator();

        for (string s = iter.FirstItem; iter.IsDone == false; s = iter.NextItem)
        {
            Console.WriteLine(s);
        }

        Console.Read();
    }
}