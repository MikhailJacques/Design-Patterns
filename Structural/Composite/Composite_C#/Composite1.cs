// Composite Design Pattern - Structural Category

using System;
using System.Collections.Generic;

// http://www.dofactory.com/net/composite-design-pattern
// http://sourcemaking.com/design_patterns/composite/c-sharp-dot-net

// Definition
// Compose objects into tree structures to represent part-whole hierarchies. 
// Composite lets clients treat individual objects and compositions of objects uniformly.

// Participants
// The classes and objects participating in this pattern are:
//
// Component
// - Declares the interface for objects in the composition.
// - Implements default behavior for the interface common to all classes, as appropriate.
// - Declares an interface for accessing and managing its child components.
// - Defines an interface (optional) for accessing a component's parent in the recursive 
//   structure, and implements it if that's appropriate.
// Leaf
// - Represents leaf objects in the composition. A leaf has no children.
// - Defines behavior for primitive objects in the composition.
// Composite
// - Defines behavior for components having children.
// - Stores child components.
// - Implements child-related operations in the Component interface.
// Client
// - Manipulates objects in the composition through the Component interface

// MainApp startup class for Structural Composite Design Pattern.
class MainApp
{
    /// Entry point into console application.
    static void Main()
    {
        // Create a tree structure
        Composite root = new Composite("root");
        root.Add(new Leaf("Leaf A"));
        root.Add(new Leaf("Leaf B"));

        Composite comp1 = new Composite("Composite X");
        comp1.Add(new Leaf("Leaf XA"));
        comp1.Add(new Leaf("Leaf XB"));
        root.Add(comp1);

        Composite comp2 = new Composite("Composite Y");
        comp2.Add(new Leaf("Leaf YA"));
        comp2.Add(new Leaf("Leaf YB"));
        root.Add(comp2);

        Composite comp3 = new Composite("Composite Z");
        comp3.Add(new Leaf("Leaf ZA"));
        comp3.Add(new Leaf("Leaf ZB"));
        comp2.Add(comp3);

        root.Add(new Leaf("Leaf C"));

        // Add and remove a leaf
        Leaf leaf = new Leaf("Leaf D");
        root.Add(leaf);
        root.Remove(leaf);

        // Recursively display tree
        root.Display(1);

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Component' abstract class
abstract class Component
{
    protected string name;

    // Constructor
    public Component(string name)
    {
        this.name = name;
    }

    public abstract void Add(Component c);
    public abstract void Remove(Component c);
    public abstract void Display(int depth);
}

// The 'Composite' class
class Composite : Component
{
    private List<Component> children = new List<Component>();

    // Constructor
    public Composite(string name) : base(name) { }

    public override void Add(Component component)
    {
        children.Add(component);
    }

    public override void Remove(Component component)
    {
        children.Remove(component);
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + name);

        // Recursively display child nodes
        foreach (Component component in children)
        {
            component.Display(depth + 2);
        }
    }
}

// The 'Leaf' class
class Leaf : Component
{
    // Constructor
    public Leaf(string name) : base(name) { }

    public override void Add(Component c)
    {
        Console.WriteLine("Cannot add to a leaf");
    }

    public override void Remove(Component c)
    {
        Console.WriteLine("Cannot remove from a leaf");
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + name);
    }
}

// Output:
/*
-root
---Leaf A
---Leaf B
---Composite X
-----Leaf XA
-----Leaf XB
---Composite Y
-----Leaf YA
-----Leaf YB
-----Composite Z
-------Leaf ZA
-------Leaf ZB
---Leaf C
*/