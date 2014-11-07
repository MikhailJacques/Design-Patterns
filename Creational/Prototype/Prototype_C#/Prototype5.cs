// Prototype Design Pattern - Creational Category

// http://en.wikipedia.org/wiki/Prototype_pattern

// http://msdn.microsoft.com/en-us/library/orm-9780596527730-01-05.aspx

// Role
// The Prototype pattern creates new objects by cloning one of a few stored prototypes. 
// The Prototype pattern has two advantages: it speeds up the instantiation of very large, 
// dynamically loaded classes (when copying objects is faster), and it keeps a record of 
// identifiable parts of a large data structure that can be copied without knowing the 
// subclass from which they were created.

// Objects are usually instantiated from classes that are part of the program. 
// The Prototype pattern presents an alternative route by creating objects from existing prototypes.

// Given a key, the program creates an object of the required type, not by instantiation, 
// but by copying a clean instance of the class. This process of copying, or cloning, can be repeated 
// over and over again. The copies, or clones, are objects in their own right, and the intention of the 
// pattern is that their state can be altered at will without affecting the prototype. During the run of 
// the program new prototypes can be added, either from new classes or from variations on existing prototypes. 
// Although there are other designs, the most flexible is to keep a prototype manager that maintains an indexed 
// list of prototypes that can be cloned. The main players in the pattern are:

// IPrototype - Defines the interface that says prototypes must be cloneable
// Prototype - A class with cloning capabilities
// PrototypeManager - Maintains a list of clone types and their keys
// Client - Adds prototypes to the list and requests clones

// C# Features-Cloning and Serialization
// MemberwiseClone is a method that is available on all objects. It copies the values of all fields and any references, 
// and returns a reference to this copy. However, it does not copy what the references in the object point to. 
// That is, it performs what is known as a shallow copy. Many objects are simple, without references to other objects, 
// and therefore shallow copies are adequate. To preserve the complete value of the object, including all its subobjects 
// use a deep copy. It is not easy to write a general algorithm to follow every link in a structure and recreate the 
// arrangement elsewhere. However, algorithms do exist, and in the .NET Framework they are encapsulated in a process 
// called serialization. Objects are copied to a given destination and can be brought back again at will. The options 
// for serialization destinations are several, including disks and the Internet, but the easiest one for serializing 
// smallish objects is memory itself. Thus a deep copy consists of serializing and deserializing in one method.
// Marking a type as serializable is done with the [Serializable( )] attribute. Serialization is part of the .NET Framework, 
// not the C# language.
// Note that serializing an object structure is possible only if all referenced objects are serializable. 
// Avoid serializing an object that has a reference to a "resource," such as an open file handler or a database connection.

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


// Serialization is used for the deep copy option
// The type T must be marked with the attribute [Serializable()]
[Serializable()]
public abstract class IPrototype<T> 
{
    // Shallow copy
    public T ShalowCopy() 
    {
        return (T) this.MemberwiseClone();
    }
        
    // Deep Copy
    public T DeepCopy() 
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, this);
        stream.Seek(0, SeekOrigin.Begin);
        T copy = (T) formatter.Deserialize(stream);
        stream.Close();
        return copy;
    }
}


// Implementation
// The implementation of the Prototype pattern in C# is greatly assisted by two facilities in the .NET Framework: 
// cloning and serialization. Both of these are provided as interfaces in the System namespace.

// The following is a small example that sets up three prototypes, each consisting of a country, a capital and a language.
// The last of these, language, refers to another class called DeeperData. The purpose of this class is to create a reference 
// in the prototype. The example will show that for this deeper data item, there is a difference between shallow copying and 
// deep copying.

// Note that all classes involved in the prototype must add the Serializable() attribute, which is used for the deep copy option.

// Helper class used to create a second level data structure
[Serializable()]
class DeeperData
{
    public string Data {get; set;}

    public DeeperData(string s) 
    {
        Data = s;
    }

    public override string ToString() 
    {
        return Data;
    }
}

[Serializable()]
class Prototype : IPrototype<Prototype>  
{
    // Content members
    public string Country {get; set;}
    public string Capital {get; set;}
    public DeeperData Language {get; set;}
    
    public Prototype (string country, string capital, string language) 
    {
        Country = country;
        Capital = capital;
        Language = new DeeperData(language);
    }
    
    public override string ToString()
    {
        return Country + "\t" + Capital + "\t->" + Language;
    }
}

[Serializable()]
class PrototypeManager // : IPrototype<Prototype>  
{
    public Dictionary <string, Prototype> prototypes = new Dictionary <string, Prototype> 
    {
        {"Italy", new Prototype("Italy    ", "Rome    ", "Italian")}, 
        {"Germany", new Prototype("Germany  ", "Berlin  ", "German")}, 
        {"Australia", new Prototype("Australia", "Canberra", "English")}
    };
}

// The main program consists of a series of experiments demonstrating the effects of cloning and deep copying. 
// In the first group, Australia is shallow copied. The capital is Canberra in the prototype and Sydney in the clone. 
// However, changing the language to Chinese also changes the prototype's language. That is not what we wanted. 
// We got the error because we did a shallow copy and the language in the prototype and in the clone reference 
// the same DeeperData object.
// In the next experiment, we clone Germany using a deep copy. The output shows that altering the clone's shallow 
// state-its capital works correctly, as does altering the deep state-its language to Turkish. 
// The prototype after the changes is unaffected.

class PrototypeClient //: IPrototype<Prototype> 
{
    static void Report(string s, Prototype prototype, Prototype clone) 
    {
        Console.WriteLine("\n" + s);
        Console.WriteLine("Prototype\t" + prototype + "\nClone\t\t" + clone);
    }
    
    static void Main() 
    {
        Prototype c2, c3;
        
        PrototypeManager manager = new PrototypeManager();

        Console.WriteLine("List of available prototypes" 
            + "\n=========================================================");
        Console.WriteLine("Prototype\t" + manager.prototypes["Italy"]);
        Console.WriteLine("Prototype\t" + manager.prototypes["Germany"]);
        Console.WriteLine("Prototype\t" + manager.prototypes["Australia"]);

        // Make a copy of Australia's data
        c2  =  manager.prototypes["Australia"].ShalowCopy();
        Report("\nShallow cloning Australia\n" 
            + "=========================================================", manager.prototypes["Australia"], c2);

        // Change the capital of Australia to Sydney
        c2.Capital = "Sydney  ";
        Report("Altered Clone's shallow state, prototype unaffected", manager.prototypes["Australia"], c2);

        // Change the language of Australia (deep data)
        c2.Language.Data = "Chinese ";
        Report("Altering Clone's deep state, prototype affected", manager.prototypes["Australia"], c2);

        // Make a copy of Germany's data
        c3  =  manager.prototypes["Germany"].DeepCopy();
        Report("\nDeep cloning Germany\n" 
            + "========================================================", manager.prototypes["Germany"], c3);

        // Change the capital of Germany
        c3.Capital = "Munich  ";
        Report("Altering Clone's shallow state, prototype unaffected", manager.prototypes["Germany"], c3);

        // Change the language of Germany (deep data)
        c3.Language.Data = "Turkish ";
        Report("Altering Clone's deep state, prototype unaffected", manager.prototypes["Germany"], c3);

        Console.ReadKey();
    }
}

/* Output
 
List of available prototypes
=========================================================
Prototype       Italy           Rome            ->Italian
Prototype       Germany         Berlin          ->German
Prototype       Australia       Canberra        ->English


Shallow cloning Australia
=========================================================
Prototype       Australia       Canberra        ->English
Clone           Australia       Canberra        ->English

Altered Clone's shallow state, prototype unaffected
Prototype       Australia       Canberra        ->English
Clone           Australia       Sydney          ->English

Altering Clone's deep state, prototype affected
Prototype       Australia       Canberra        ->Chinese
Clone           Australia       Sydney          ->Chinese


Deep cloning Germany
========================================================
Prototype       Germany         Berlin          ->German
Clone           Germany         Berlin          ->German

Altering Clone's shallow state, prototype unaffected
Prototype       Germany         Berlin          ->German
Clone           Germany         Munich          ->German

Altering Clone's deep state, prototype unaffected
Prototype       Germany         Berlin          ->German
Clone           Germany         Munich          ->Turkish

*/