// Adapter Design Patter - Structural Category

// “Adapter” as the name suggests is the object which lets  two mutually incompatible interfaces communicate with each other.
// That being said, we use Adapters when incompatible interfaces are involved.
// Our client object wants to call a method but it is not able to because the interface which our client object can use, 
// is not available with the code which our client object wants to call.

// The Adapter design pattern is easy to implement and ensures calling the existing code which was otherwise 
// difficult because their interfaces being incompatible. It is quiet common when legacy code has to be called.


// Participants
// The classes and objects participating in this pattern are:

// Client: This is the class which wants to achieve some functionality by using the adaptee’s code.

// Adaptee: This is the functionality which the client desires but its interface is not compatible with the client.

// ITarget: This is the interface which is used by the client to achieve functionality.

// Adapter: This is the class which would implement ITarget and would call the Adaptee code which the client wants to call.

// http://www.codeproject.com/Articles/774259/Adapter-Design-Pattern-Csharp

using System;
using System.Collections.Generic;

namespace Adapter_CS
{
    class Adapter3
    {
        // ITarget: Method which the online shopping portal calls to get the list of products. 
        // Here getting the list of products is the functionality which this portal wants to achieve 
        // and this request has been encapsulated in this interface. 
        // In short - functionality to achieve is exposed through this interface.
        interface ITarget
        {
            List<string> GetProducts();
        }

        // Adapter: The wrapper which implements ITarget and calls third party vendor’s code.
        // This VendorAdapter is called Object Adapter because it uses the object composition 
        // (creates an instance of VendorAdaptee) to call the adaptee code. 
        class VendorAdapter : ITarget
        {
            public List<string> GetProducts()
            {
                VendorAdaptee adaptee = new VendorAdaptee();
                return adaptee.GetListOfProducts();
            }
        }

        // Adaptee: The third party vendor’s code which gives us the list of products.
        // For the sake of the example a List is also used to store the products, 
        // but any other storage structure could have been used instead.
        public class VendorAdaptee
        {
            public List<string> GetListOfProducts()
            {
                List<string> products = new List<string>();
                products.Add("Books");
                products.Add("Gadgets");
                products.Add("Widgets");
                products.Add("Television");
                products.Add("Gaming Consoles");
                products.Add("Musical Instruments");
                products.Add("Tools");

                return products;
            }
        }

        // Client: The online shopping portal code which gets the list of products and then displays them.
        class ShoppingPortalClient
        {
            static void Main(string[] args)
            {
                ITarget adapter = new VendorAdapter();

                foreach (string product in adapter.GetProducts())
                {
                    Console.WriteLine(product);
                }

                Console.ReadLine();
            }
        }
    }
}