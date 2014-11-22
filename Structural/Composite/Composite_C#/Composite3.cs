// Composite Design Pattern - Structural Category

// Composite Pattern is used to arrange structured hierarchies.

// Composite pattern is used to separate an abstraction from its implementation so that both can be modified independently.
// Composite pattern is used when we need to treat a group of objects and a single object in the same way. 
// Composite pattern composes objects in term of a tree structure to represent part as well as whole hierarchies.
// This pattern creates a class contains group of its own objects. This class provides ways to modify its group of same objects.

// Component
//    This is an abstract class containing members that will be implemented by all objects in the hierarchy. 
//    It acts as the base class for all the objects within the hierarchy.
// Composite
//    This is a class which includes Add, Remove, Find and Get methods to do operations on child components.
// Leaf
//    This is a class which is used to define Leaf components within the tree structure.
//    Leaf components cannot have children.

// When to use it?
// Hierarchical representations of objects are required.
// A single object and a group of objects should be treated in the same way.
// The Composite pattern is used when data is organized in a tree structure (for example directories in a computer).

// http://www.dotnet-tricks.com/Tutorial/designpatterns/XSN6130713-Composite-Design-Pattern---C

using System;
using System.Collections;
using System.Collections.Generic;

// The 'Component' Treenode
public interface IEmployed
{
    int EmpID { get; set; }
    string Name { get; set; }
}

// The 'Composite' class
public class Employee : IEmployed, IEnumerable<IEmployed>
{
    private List<IEmployed> subordinates = new List<IEmployed>();

    public int EmpID { get; set; }
    public string Name { get; set; }

    public void AddSubordinate(IEmployed subordinate)
    {
        subordinates.Add(subordinate);
    }

    public void RemoveSubordinate(IEmployed subordinate)
    {
        subordinates.Remove(subordinate);
    }

    public IEmployed GetSubordinate(int index)
    {
        return subordinates[index];
    }

    public IEnumerator<IEmployed> GetEnumerator()
    {
        foreach (IEmployed subordinate in subordinates)
        {
            // We use the yield keyword in a statement to indicate that the method, 
            // operator, or get accessor in which it appears is an iterator.
            // We use a yield return statement to return each element one at a time.
            yield return subordinate;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// The 'Leaf' class
public class Contractor : IEmployed, IEnumerable<IEmployed>
{
    public int EmpID { get; set; }
    public string Name { get; set; }

    public IEnumerator<IEmployed> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class MainApp
{
    static void Main(string[] args)
    {
        Employee Michael = new Employee { EmpID = 1, Name = "Michael" };

        Employee Tom = new Employee { EmpID = 2, Name = "Tom" };
        Employee Jerry = new Employee { EmpID = 3, Name = "Jerry" };

        Michael.AddSubordinate(Tom);
        Michael.AddSubordinate(Jerry);

        Employee Bob = new Employee { EmpID = 4, Name = "Bob" };
        Employee Alice = new Employee { EmpID = 5, Name = "Alice" };

        Tom.AddSubordinate(Bob);
        Tom.AddSubordinate(Alice);

        Employee Carol = new Employee { EmpID = 6, Name = "Carol" };
        Employee Frank = new Employee { EmpID = 7, Name = "Frank" };

        Jerry.AddSubordinate(Carol);
        Jerry.AddSubordinate(Frank);

        Contractor Sam = new Contractor { EmpID = 8, Name = "Sam" };
        Contractor Tim = new Contractor { EmpID = 9, Name = "Tim" };
        Contractor Kaka = new Contractor { EmpID = 10, Name = "Kaka" };

        Carol.AddSubordinate(Sam);
        Carol.AddSubordinate(Tim);
        Frank.AddSubordinate(Kaka);

        Console.WriteLine("EmpID={0}, Name={1}", Michael.EmpID, Michael.Name);

        foreach (Employee manager in Michael)
        {
            Console.WriteLine("\n\tEmpID={0}, Name={1}", manager.EmpID, manager.Name);

            foreach (var employee in manager)
            {
                Console.WriteLine("\n\t\tEmpID={0}, Name={1}", employee.EmpID, employee.Name);

                // TODO: Currently does not work. Use virtual method for printing instead.
                //foreach (var contractor in employee)
                //{
                //    Console.WriteLine("\n\t\tEmpID={0}, Name={1}", contractor.EmpID, contractor.Name);
                //}
            }
        }

        Console.ReadKey();
    }
}

// Output:
/*
EmpID=1, Name=Michael

        EmpID=2, Name=Tom

                EmpID=4, Name=Bob

                EmpID=5, Name=Alice

        EmpID=3, Name=Jerry

                EmpID=6, Name=Carol

                EmpID=7, Name=Frank
*/