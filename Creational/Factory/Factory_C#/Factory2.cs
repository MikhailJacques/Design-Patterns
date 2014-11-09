// Factory Design Pattern - Creational Category

// Definition
// Define an interface for creating an object, but let subclasses decide which class
// to instantiate. Factory Method lets a class defer instantiation to subclasses.

// Participants
// The classes and objects participating in this pattern are:
//
// Product (Page)
// - Defines the interface of objects the factory method creates
//
// ConcreteProduct (SkillsPage, EducationPage, ExperiencePage)
// - Implements the Product interface
//
// Creator (Document)
// - Declares the factory method, which returns an object of type Product. 
// - Creator may also define a default implementation of the factory method 
//   that returns a default ConcreteProduct object. 
// - May call the factory method to create a Product object.
//
// ConcreteCreator  (Report, Resume)
// - Overrides the factory method to return an instance of a ConcreteProduct.

// The following real-world code demonstrates the Factory method offering 
// flexibility in creating different documents. The derived Document classes 
// Report and Resume instantiate extended versions of the Document class. 
// Here, the Factory Method is called in the constructor of the Document base class.

// http://www.dofactory.com/net/factory-method-design-pattern

using System;
using System.Collections.Generic;

// MainApp startup class for Real-World Factory Method Design Pattern.
class MainApp
{
    // Entry point into console application.
    static void Main()
    {
        // Note: constructors call Factory Method
        Document[] documents = new Document[2];

        documents[0] = new Resume();
        documents[1] = new Report();

        // Display document pages
        foreach (Document document in documents)
        {
            Console.WriteLine("\n" + document.GetType().Name + "--");
            foreach (Page page in document.Pages)
            {
                Console.WriteLine(" " + page.GetType().Name);
            }
        }

        // Wait for user
        Console.ReadKey();
    }
}

// The 'Product' abstract class
abstract class Page { }

// A 'ConcreteProduct' class
class SkillsPage : Page { }

// A 'ConcreteProduct' class
class EducationPage : Page { }

// A 'ConcreteProduct' class
class ExperiencePage : Page { }

// A 'ConcreteProduct' class
class IntroductionPage : Page { }

// A 'ConcreteProduct' class
class ResultsPage : Page { }

// A 'ConcreteProduct' class
class ConclusionPage : Page { }

// A 'ConcreteProduct' class
class SummaryPage : Page { }

// A 'ConcreteProduct' class
class BibliographyPage : Page { }

// The 'Creator' abstract class
abstract class Document
{
    private List<Page> _pages = new List<Page>();

    // Constructor calls abstract Factory method
    public Document()
    {
        this.CreatePages();
    }

    public List<Page> Pages
    {
        get { return _pages; }
    }

    // Factory Method
    public abstract void CreatePages();
}

// A 'ConcreteCreator' class
class Resume : Document
{
    // Factory Method implementation
    public override void CreatePages()
    {
        Pages.Add(new SkillsPage());
        Pages.Add(new EducationPage());
        Pages.Add(new ExperiencePage());
    }
}

// A 'ConcreteCreator' class
class Report : Document
{
    // Factory Method implementation
    public override void CreatePages()
    {
        Pages.Add(new IntroductionPage());
        Pages.Add(new ResultsPage());
        Pages.Add(new ConclusionPage());
        Pages.Add(new SummaryPage());
        Pages.Add(new BibliographyPage());
    }
}