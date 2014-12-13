// Abstract Factory - Creational Patterns Category

// Background:
//
// Abstract factory pattern in useful when the client needs to create objects which are somehow related. 
// If we need to create a family of related or dependent objects, then we can use Abstract Factory Pattern.

// This pattern is particularly useful when the client doesn't know exactly what type to create. 
// As an example, let's say a Showroom exclusively selling cellphones gets a query for the smart phones vendorfactured by Samsung. 
// Here we don't know the exact type of object to be created (assuming all the information for a phone is wrapped 
// in the form of a concrete object). But we do know that we are looking for smart phones that are vendorfactured by Samsung. 
// This information can actually be utilized if our design has Abstract factory implementation.

// So with this idea of Abstract Factory pattern, we will now try to create a design that will facilitate the creation 
// of related objects. We will go ahead and write a rudimentary application for the scenario we just talked about.

// http://www.codeproject.com/Articles/328373/Understanding-and-Implementing-Abstract-Factory-Pa

using System;
using System.Collections.Generic;
using System.Text;

// Factories enum
enum VENDORS
{
    SAMSUNG,
    HTC,
    NOKIA,
    APPLE
}

// Abstract factory
interface IPhoneFactory
{
    // Note that each implemeted method should really return a list of all smart/dumb phones 
    // manufactured by a particular vendor, not just one phone as is currently done for the sake of the simplicity.
    ISmart GetSmart(); 
    IDumb GetDumb();
}

// Concrete factory
class SamsungFactory : IPhoneFactory
{
    public ISmart GetSmart()
    {
        return new GalaxyS5();
    }

    public IDumb GetDumb()
    {
        return new Primo();
    }
}

// Concrete factory
class HTCFactory : IPhoneFactory
{
    public ISmart GetSmart()
    {
        return new Titan();
    }

    public IDumb GetDumb()
    {
        return new Genie();
    }
}

// Concrete factory
class NokiaFactory : IPhoneFactory
{
    public ISmart GetSmart()
    {
        return new Lumia();
    }

    public IDumb GetDumb()
    {
        return new Asha();
    }
}

// Concrete factory
class AppleFactory : IPhoneFactory
{
    public ISmart GetSmart()
    {
        return new iPhone6();
    }

    public IDumb GetDumb()
    {
        return new iPhone();
    }
}

// Abstract product
interface ISmart
{
    string Name();
}

// Concrete product
class Lumia : ISmart
{
    public string Name()
    {
        return GetType().Name; // "Lumia"
    }
}

// Concrete product
class GalaxyS5 : ISmart
{
    public string Name()
    {
        return GetType().Name; // "GalaxyS5"
    }
}

// Concrete product
class Titan : ISmart
{
    public string Name()
    {
        return GetType().Name; // "Titan"
    }
}

// Concrete product
class iPhone6 : ISmart
{
    public string Name()
    {
        return GetType().Name; // "iPhone6"
    }
}

// Abstract product
interface IDumb
{
    string Name();
}

// Concrete product
class Asha : IDumb
{
    public string Name()
    {
        return GetType().Name; // "Asha"
    }
}

// Concrete product
class Primo : IDumb
{
    public string Name()
    {
        return GetType().Name; // "Guru"
    }
}

// Concrete product
class Genie : IDumb
{
    public string Name()
    {
        return GetType().Name; // "Genie"
    }
}

// Concrete product
class iPhone : IDumb
{
    public string Name()
    {
        return GetType().Name; // "iPhone"
    }
}

class PhoneVendor
{
    private ISmart sam;
    private IDumb htc;
    private IPhoneFactory factory;
    private VENDORS vendor;

    public PhoneVendor(VENDORS vendor)
    {
        this.vendor = vendor;
    }

    public void ListPhones()
    {
        switch (vendor)
        {
            case VENDORS.SAMSUNG:
                factory = new SamsungFactory();
                break;
            case VENDORS.HTC:
                factory = new HTCFactory();
                break;
            case VENDORS.NOKIA:
                factory = new NokiaFactory();
                break;
            case VENDORS.APPLE:
                factory = new AppleFactory();
                break;
        }

        Console.WriteLine(vendor.ToString() + ":" +
            "\nSmart Phone: " + factory.GetSmart().Name() +
            "\nDumb Phone: " + factory.GetDumb().Name() + "\n");
    }
}

class MainApp
{
    private static PhoneVendor vendor;

    static void Main(string[] args)
    {
        vendor = new PhoneVendor(VENDORS.SAMSUNG);
        vendor.ListPhones();

        vendor = new PhoneVendor(VENDORS.HTC);
        vendor.ListPhones();

        vendor = new PhoneVendor(VENDORS.NOKIA);
        vendor.ListPhones();

        vendor = new PhoneVendor(VENDORS.APPLE);
        vendor.ListPhones();

        Console.ReadKey();
    }
}