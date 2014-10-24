// Memento Design Pattern - Behavioral Category

// The Memento Design Pattern allows saving historical states of an 
// object and restoring the object back from the historical states. 
// As the application is progressing, we may want to save checkpoints 
// in our application and restore back to those checkpoints later. 
// An example are the checkpoints saved in a video game where the user 
// is allowed to go back to the stages that they have already conquered. 
// Another example are the undo operations in a word processing application.

// The Originator class is the object that will be saved and restored later:
// - The state variable contains information that represents the state of the 
//   Originator object. This is the variable that we save and restore.
// - The createMemento method is used to save the state of the Originator. 
//   It creates a Memento object by saving the state variable into the Memento 
//   object and return it. This is for recording the state of the Originator.
// - The setMemento method restores the Originator by accepting a Memento 
//   object, unpackage it, and sets its state variable using the state variable 
//   from the Memento. This is for restoring the state of the Originator using 
//   the information that was previously saved in the Memento.
// 
// The Memento class stores the historical information of the Originator. 
// The information is stored in its state variable.
//
// The Caretaker class manages the list of Mementos. 
// This is the class for the client code to access.

// The key to the Memento Design Pattern is that the client code will never 
// access the Memento object, all of the interactions are done through the 
// Caretaker class. The client code does not need to be concerned about how 
// the states are stored and retrieved.

// http://www.codeproject.com/Articles/186184/Memento-Design-Pattern

using System;
using System.Collections.Generic;
using System.Linq;

class MainApp
{
    static void Main(string[] args)
    {
        Originator<string> orig = new Originator<string>();

        // Set and save the state of the Originator
        orig.setState("State 0");
        Caretaker<string>.saveState(orig);
        orig.showState();

        // Set and save the state of the Originator
        orig.setState("State 1");
        Caretaker<string>.saveState(orig);
        orig.showState();

        // Set and save the state of the Originator
        orig.setState("State 2");
        Caretaker<string>.saveState(orig);
        orig.showState();

        // Restore the state of the Originator
        Caretaker<string>.restoreState(orig, 0);
        orig.showState();  // Shows State 0

        Caretaker<string>.restoreState(orig, 1);
        orig.showState();  // Shows State 1

        // Wait for user
        Console.ReadKey();
    }
}

// Memento is an object that stores the historical state of the Originator
public class Memento<T>
{
    private T state;

    public T getState()
    {
        return state;
    }

    public void setState(T state)
    {
        this.state = state;
    }
}

// Originator is an object that we want to save and restore, 
// such as a check point in an application
public class Originator<T>
{
    private T state;

    // For saving the current state
    public Memento<T> createMemento()
    {
        Memento<T> m = new Memento<T>();
        m.setState(state);
        return m;
    }

    // For restoring the state from Memento
    public void setMemento(Memento<T> m)
    {
        state = m.getState();
    }

    // Change the state of the Originator
    public void setState(T state)
    {
        this.state = state;
    }

    // Show the current state of the Originator
    public void showState()
    {
        Console.WriteLine(state.ToString() + "\n");
    }
}

// Caretaker is an object for the client to access
public static class Caretaker<T>
{
    // List of the saved states
    private static List<Memento<T>> mementoList = new List<Memento<T>>();

    // Save state of the Originator
    public static void saveState(Originator<T> orig)
    {
        Console.WriteLine("Saving Originator state...");
        mementoList.Add(orig.createMemento());
        Console.WriteLine("Originator state has been saved.");
    }

    // Restore state of the Originator
    public static void restoreState(Originator<T> orig, int state_num)
    {
        Console.WriteLine("Restoring Originator state...");
        orig.setMemento(mementoList[state_num]);
        Console.WriteLine("Originator state has been restored.");
    }
}