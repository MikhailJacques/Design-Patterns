// Observer Design Pattern - Behavioral Category

// Background
//
// Many times, we need one part of our application updated with the status of some other part of the application. 
// One way to do this is to have the receiver part repeatedly check the sender for updates, but this approach has 
// two main problems. First, it takes up a lot of CPU time to check the new status and second, depending on the interval 
// we are checking for change we might not get the updates "immediately".

// Using the code
//
// Abstract Subject: This class keeps a track of all the Observers and provides the facility to add or remove the Observers. 
// It is also the class that is responsible for updating the Observers when any change occurs. 
// 
// Product: This class is the Concrete Subject class that implements the Abstract Subject class. 
// This class is the entity whose change will affect other objects. 
//
// IObserver: This represents an interface that defines the method that should be called whenever there is change. 
//
// Shop: This is the Concrete Observer class which needs to keep itself updated with the change. 
// This class just needs to implement the IObserver and register itself with the Shop (Concrete Subject) 
// and it is all set to receive the updates.

// In .NET, we have delegates which are actually a very good example of Observer pattern. 
// So actually we don't need to implement the pattern completely in C# as we can use the delegates 
// for the same functionality. Hence, here we have done both - implemented pattern completely in C# in order 
// to understand the pattern and implemented the delegate's way of having an observer pattern working too.

// http://www.codeproject.com/Articles/328361/Understanding-and-Implementing-Observer-Pattern-in

using System;
using System.Collections;
using System.Collections.Generic;

// Abstract Subject
abstract class ASubject
{
    // This is the one way we can implement observer, lets call it WAY_1
    ArrayList list = new ArrayList();

    // This is another way we can implement observer, lets call it WAY_2
    public delegate void StatusUpdate(float price);
    public event StatusUpdate OnStatusUpdate = null;

    public void Attach(Shop product)
    {
        // WAY_1 attach Observers to Subject
        list.Add(product);
    }

    public void Detach(Shop product)
    {
        // WAY_1 detach Observers from Subjects
        list.Remove(product);
    }

    public void Attach2(Shop product)
    {
        // WAY_2 attach Observers to Subjects
        OnStatusUpdate += new StatusUpdate(product.Update);
    }

    public void Detach2(Shop product)
    {
        // WAY_2 detach Observers from Subjects
        OnStatusUpdate -= new StatusUpdate(product.Update);
    }

    // Notify the Observers about the change in price
    public void Notify(float price)
    {
        // WAY_1 
        foreach (Shop p in list)
        {
            p.Update(price);
        }

        // WAY_2
        if (OnStatusUpdate != null)
        {
            OnStatusUpdate(price);
        }
    }
}

// Subject
class Product : ASubject
{
    public void ChangePrice(float price)
    {
        Notify(price);
    }
}

// Observer Interface
interface IObserver
{
    void Update(float price);
}

// Concrete Observer
class Shop : IObserver
{
    // Product name
    string name;

    // Product price
    float price = 0.0f;

    public Shop(string name)
    {
        this.name = name;
    }

    #region IObserver Members

    public void Update(float price)
    {
        this.price = price;

        // Print out to test the functionality
        Console.WriteLine(@"Price at {0} is now {1}", name, price);
    }

    #endregion
}

class MainApp
{
    static void Main(string[] args)
    {
        Product product = new Product();

        // We have four shops wanting to keep updated price set by the product owner
        Shop shop1 = new Shop("Shop 1");
        Shop shop2 = new Shop("Shop 2");
        Shop shop3 = new Shop("Shop 3");
        Shop shop4 = new Shop("Shop 4");

        // Lets use WAY_1 for the first two shops
        product.Attach(shop1);
        product.Attach(shop2);

        // Lets use WAY_2 for the other two shops
        product.Attach2(shop3);
        product.Attach2(shop4);

        // Now lets try changing the product's price, this should update the shops automatically
        product.ChangePrice(10.0f);

        Console.WriteLine();

        // Now shop2 and shop4 are not interested in new prices so they unsubscribe
        product.Detach(shop2);
        product.Detach2(shop4);

        // Now lets try changing the product's price again
        product.ChangePrice(16.0f);

        Console.WriteLine();

        Shop shop5 = new Shop("Shop 5");
        Shop shop6 = new Shop("Shop 6");

        product.Attach(shop5);
        product.Attach2(shop6);

        // Now lets try changing the product's price again
        product.ChangePrice(25.0f);

        Console.ReadKey();
    }
}