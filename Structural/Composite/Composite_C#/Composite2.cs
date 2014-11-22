// Composite Design Pattern - Structural Category

// Definition
// Compose objects into tree structures to represent part-whole hierarchies. 
// Composite lets clients treat individual objects and compositions of objects uniformly.

// Participants
// The classes and objects participating in this pattern are:
//
// Component (DrawingElement)
// - Declares the interface for objects in the composition.
// - Implements default behavior for the interface common to all classes, as appropriate.
// - Declares an interface for accessing and managing its child components.
// - Defines an interface (optional) for accessing a component's parent in the recursive 
//   structure, and implements it if that's appropriate.
// Leaf (PrimitiveElement)
// - Represents leaf objects in the composition. A leaf has no children.
// - Defines behavior for primitive objects in the composition.
// Composite (CompositeElement)
// - Defines behavior for components having children.
// - Stores child components.
// - Implements child-related operations in the Component interface.
// Client (CompositeApp)
// - Manipulates objects in the composition through the Component interface

// MainApp startup class for Structural Composite Design Pattern.

// http://www.dofactory.com/net/composite-design-pattern

using System;
using System.Collections.Generic;

// MainApp startup class for Real-World Composite Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // Create a tree structure 
        CompositeElement root = new CompositeElement("Picture");
        root.Add(new PrimitiveElement("Red Line"));
        root.Add(new PrimitiveElement("Blue Circle"));
        root.Add(new PrimitiveElement("Green Box"));

        // Create a branch
        CompositeElement comp1 = new CompositeElement("Two Circles");
        comp1.Add(new PrimitiveElement("Black Circle"));
        comp1.Add(new PrimitiveElement("White Circle"));
        root.Add(comp1);

        // Create a branch
        CompositeElement comp2 = new CompositeElement("Two Squares");
        comp2.Add(new PrimitiveElement("Red Square"));
        comp2.Add(new PrimitiveElement("Blue Square"));
        root.Add(comp2);

        // Add and remove a PrimitiveElement
        PrimitiveElement pe1 = new PrimitiveElement("Green Line");
        root.Add(pe1);

        // Add and remove a PrimitiveElement
        PrimitiveElement pe2 = new PrimitiveElement("Yellow Line");
        root.Add(pe2);
        root.Remove(pe2);

        // Recursively display nodes
        root.Display(1);

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Component' Treenode
abstract class DrawingElement
{
    protected string name;

    // Constructor
    public DrawingElement(string name)
    {
        this.name = name;
    }

    public abstract void Add(DrawingElement d);
    public abstract void Remove(DrawingElement d);
    public abstract void Display(int indent);
}

// The 'Leaf' class
class PrimitiveElement : DrawingElement
{
    // Constructor
    public PrimitiveElement(string name) : base(name) { }

    public override void Add(DrawingElement c)
    {
        Console.WriteLine("Cannot add to a PrimitiveElement");
    }

    public override void Remove(DrawingElement c)
    {
        Console.WriteLine("Cannot remove from a PrimitiveElement");
    }

    public override void Display(int indent)
    {
        Console.WriteLine(new String('-', indent) + " " + name);
    }
}

// The 'Composite' class
class CompositeElement : DrawingElement
{
    private List<DrawingElement> elements = new List<DrawingElement>();

    // Constructor
    public CompositeElement(string name) : base(name) { }

    public override void Add(DrawingElement d)
    {
        elements.Add(d);
    }

    public override void Remove(DrawingElement d)
    {
        elements.Remove(d);
    }

    public override void Display(int indent)
    {
        Console.WriteLine(new String('-', indent) + "+ " + name);

        // Display each child element on this node
        foreach (DrawingElement d in elements)
        {
            d.Display(indent + 2);
        }
    }
}

// Output:
/*
-+ Picture
--- Red Line
--- Blue Circle
--- Green Box
---+ Two Circles
----- Black Circle
----- White Circle
---+ Two Squares
----- Red Square
----- Blue Square
--- Green Line
*/