// Composite Design Pattern - Structural Category

// In software engineering, the composite pattern is a partitioning design pattern. 
// The composite pattern describes that a group of objects is to be treated in the same way as a single instance of an object. 
// The intent of a composite is to "compose" objects into tree structures to represent part-whole hierarchies. 
// Implementing the composite pattern lets clients treat individual objects and compositions uniformly.

// Motivation
// When dealing with Tree-structured data, programmers often have to discriminate between a leaf-node and a branch. 
// This makes code more complex, and therefore, error prone. The solution is an interface that allows treating complex 
// and primitive objects uniformly. In object-oriented programming, a composite is an object designed as a composition of
// one-or-more similar objects, all exhibiting similar functionality. This is known as a "has-a" relationship between objects. 
// The key concept is that you can manipulate a single instance of the object just as you would manipulate a group of them. 
// The operations you can perform on all the composite objects often have a least common denominator relationship. 
// For example, if defining a system to portray grouped shapes on a screen, it would be useful to define resizing 
// a group of shapes to have the same effect (in some sense) as resizing a single shape.

// When to use
// Composite should be used when clients ignore the difference between compositions of objects and individual objects.
// If programmers find that they are using multiple objects in the same way, and often have nearly identical code to handle each
// of them, then composite is a good choice. It is less complex in this situation to treat primitives and composites as homogeneous.

// Component
// - Is the abstraction for all components, including composite ones.
// - Declares the interface for objects in the composition.
// - Defines an interface (optional) for accessing a component's parent in the recursive structure, 
//   and implements it if that's appropriate.
// Leaf
// - Represents leaf objects in the composition.
// - Implements all Component methods.
// Composite
// - Represents a composite Component (component having children).
// - Implements methods to manipulate children.
// - Implements all Component methods, generally by delegating them to its children.

// http://en.wikipedia.org/wiki/Composite_pattern

using System;
using System.Collections.Generic;
using System.Linq;

// Client
class MainApp
{
    static void Main(string[] args)
    {
        // Initialize variables
        var compositeGraphic1 = new CompositeGraphic();
        var compositeGraphic2 = new CompositeGraphic();
        var compositeGraphic3 = new CompositeGraphic();

        // Add 1 Graphic to compositeGraphic2
        compositeGraphic2.Add(new Ellipse());

        // Add 2 Graphic to compositeGraphic3
        compositeGraphic3.AddRange(new Ellipse(), new Circle());

        // Add 1 Graphic, compositeGraphic2, and  compositeGraphic3 to compositeGraphic1
        compositeGraphic1.AddRange(new Circle(), compositeGraphic2, compositeGraphic3);

        // Print the complete graphic
        compositeGraphic1.Print();

        Console.ReadKey();
    }
}

// Component
public interface IGraphic
{
    void Print();
}

// Leaf
public class Circle : IGraphic
{
    // Prints the graphic
    public void Print()
    {
        Console.WriteLine("Circle");
    }
}

// Leaf
public class Ellipse : IGraphic
{
    // Prints the graphic
    public void Print()
    {
        Console.WriteLine("Ellipse");
    }
}

// Composite
public class CompositeGraphic : IGraphic
{
    // Collection of Graphics
    private readonly List<IGraphic> graphics;

    // Constructor 
    public CompositeGraphic()
    {
        // Initialize generic Collection (Composition)
        graphics = new List<IGraphic>();
    }

    // Add the graphic to the composition
    public void Add(IGraphic graphic)
    {
        graphics.Add(graphic);
    }

    // Add multiple graphics to the composition
    public void AddRange(params IGraphic[] graphic)
    {
        graphics.AddRange(graphic);
    }

    // Remove the graphic from the composition
    public void Delete(IGraphic graphic)
    {
        graphics.Remove(graphic);
    }

    // Print the graphic
    public void Print()
    {
        foreach (var childGraphic in graphics)
        {
            childGraphic.Print();
        }
    }
}

// Output:
/*
Circle
Ellipse
Ellipse
Circle
*/