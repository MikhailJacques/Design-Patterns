// Flyweight Design Pattern - Structural Category

// Definition
// Use sharing to support large numbers of fine-grained objects efficiently.

// Participants
//
// Flyweight (Character)
// - Declares an interface through which flyweights can receive and act on extrinsic state.
//
// SharedConcreteFlyweight (CharacterA, CharacterB, ..., CharacterZ)
// - Implements the Flyweight interface and adds storage for intrinsic state, if any. 
//   A SharedConcreteFlyweight object must be sharable. Any state it stores must be intrinsic, 
//   that is, it must be independent of the SharedConcreteFlyweight object's context.
//
// UnsharedSharedConcreteFlyweight (not used)
// - Not all Flyweight subclasses need to be shared. The Flyweight interface enables sharing, but it doesn't enforce it. 
//   It is common for UnsharedSharedConcreteFlyweight objects to have SharedConcreteFlyweight objects as children at some 
//   level in the flyweight object structure (as the Row and Column classes have).
//
// FlyweightFactory (CharacterFactory)
// - Creates and manages flyweight objects
// - Ensures that flyweight are shared properly. When a client requests a flyweight, the FlyweightFactory objects assets 
//   an existing instance or creates one, if none exists.
//
// Client (FlyweightApp)
// - Maintains a reference to flyweight(s).
// - Computes or stores the extrinsic state of flyweight(s).

// Real-world code in C#
// This real-world code demonstrates the Flyweight pattern in which a relatively small number 
// of Character objects is shared many times by a document that has potentially many characters.

// http://www.dofactory.com/net/flyweight-design-pattern

using System;
using System.Collections.Generic;

// The 'FlyweightFactory' class
class CharacterFactory
{
    private Dictionary<char, Character> characters = new Dictionary<char, Character>();

    public Character GetCharacter(char key)
    {
        // Uses "lazy initialization"
        Character character = null;

        if (characters.ContainsKey(key))
        {
            character = characters[key];
        }
        else
        {
            switch (key)
            {
                case 'A': character = new CharacterA(); break;
                case 'B': character = new CharacterB(); break;

                // ...

                case 'Z': character = new CharacterZ(); break;
                default: character = null; break;
            }

            characters.Add(key, character);
        }

        return character;
    }
}

// The 'Flyweight' abstract class
abstract class Character
{
    protected char symbol;
    protected int width;
    protected int height;
    protected int ascent;
    protected int descent;
    protected int point_size;

    public abstract void Display(int point_size);
}

// A 'ConcreteFlyweight' class
class CharacterA : Character
{
    // Constructor
    public CharacterA()
    {
        this.symbol = 'A';
        this.height = 100;
        this.width = 120;
        this.ascent = 70;
        this.descent = 0;
    }

    public override void Display(int point_size)
    {
        this.point_size = point_size;
        Console.WriteLine(this.symbol + " (point size " + this.point_size + ")");
    }
}

// A 'ConcreteFlyweight' class
class CharacterB : Character
{
    // Constructor
    public CharacterB()
    {
        this.symbol = 'B';
        this.height = 100;
        this.width = 140;
        this.ascent = 72;
        this.descent = 0;
    }

    public override void Display(int point_size)
    {
        this.point_size = point_size;
        Console.WriteLine(this.symbol + " (point size " + this.point_size + ")");
    }

}

// ... C, D, E, etc.

// A 'ConcreteFlyweight' class
class CharacterZ : Character
{
    // Constructor
    public CharacterZ()
    {
        this.symbol = 'Z';
        this.height = 100;
        this.width = 100;
        this.ascent = 68;
        this.descent = 0;
    }

    public override void Display(int point_size)
    {
        this.point_size = point_size;
        Console.WriteLine(this.symbol + " (point size " + this.point_size + ")");
    }
}

// MainApp startup class for Real-World Flyweight Design Pattern.
class MainApp
{
    private static Character character;
    private static CharacterFactory factory;

    // Entry point into console application.
    static void Main()
    {
        // Build a document with text
        string document = "ADAZZBBZBR";
        char[] chars = document.ToCharArray();

        factory = new CharacterFactory();

        // Extrinsic state
        int point_size = 10;

        // For each character use a flyweight object
        foreach (char c in chars)
        {
            character = factory.GetCharacter(c);

            if (character != null)
                character.Display(++point_size);
        }

        // Wait for user
        Console.ReadKey();
    }
}