// Iterator Design Pattern - Behavioral Category

// Iterator pattern provides a way to traverse (iterate) sequentially over a collection of items 
// without detailing (exposing) the underlying structure of the collection.

// http://sourcemaking.com/design_patterns/iterator/c-sharp-dot-net

using System;
using System.Collections;


// "Iterator" 
abstract class Iterator
{
    public abstract object First();
    public abstract object Next();
    public abstract object Current();
    public abstract bool IsDone();
}


// "ConcreteIterator" 
class ConcreteIterator : Iterator
{
    private int current = 0;
    private ConcreteAggregate aggregate;

    // Constructor 
    public ConcreteIterator(ConcreteAggregate aggregate)
    {
        this.aggregate = aggregate;
    }

    public override object First()
    {
        return aggregate[0];
    }

    public override object Next()
    {
        object ret = null;
        if (current < aggregate.Count - 1)
        {
            ret = aggregate[++current];
        }

        return ret;
    }

    public override object Current()
    {
        return aggregate[current];
    }

    public override bool IsDone()
    {
        return current >= aggregate.Count ? true : false;
    }
}


// "Aggregate" 
abstract class Aggregate
{
    public abstract Iterator CreateIterator();
}


// "ConcreteAggregate" 
class ConcreteAggregate : Aggregate
{
    private ArrayList items = new ArrayList();

    public override Iterator CreateIterator()
    {
        return new ConcreteIterator(this);
    }

    // Property 
    public int Count
    {
        get { return items.Count; }
    }

    // Indexer 
    public object this[int index]
    {
        get { return items[index]; }
        set { items.Insert(index, value); }
    }
}


class MainApp
{
    static void Main()
    {
        ConcreteAggregate aggregate = new ConcreteAggregate();

        aggregate[0] = "Item A";
        aggregate[1] = "Item B";
        aggregate[2] = "Item C";
        aggregate[3] = "Item D";
        aggregate[4] = "Bob";

        // Create Iterator and provide aggregate 
        ConcreteIterator it = new ConcreteIterator(aggregate);

        Console.WriteLine("Iterating over collection:");

        object item = it.First();
        while (item != null)
        {
            Console.WriteLine(item);
            item = it.Next();
        }

        // Wait for user 
        Console.ReadKey();
    }
}