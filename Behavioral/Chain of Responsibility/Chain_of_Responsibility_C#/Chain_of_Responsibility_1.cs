//// Chain of Responsibility Design Pattern - Behavioral Category

//// Definition:
////
//// Avoid coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. 
//// Chain the receiving objects and pass the request along the chain until an object handles it.

//// Participants:
////
//// Handler
//// - Defines an interface for handling the requests
//// - Implements the successor link (optional)
////
//// ConcreteHandler
//// - Handles requests it is responsible for
//// - Can access its successor
//// - If the ConcreteHandler can handle the request, it does so; otherwise it forwards the request to its successor
////
//// Client
//// - Initiates the request to a ConcreteHandler object on the chain

//// This structural code demonstrates the Chain of Responsibility pattern in which several linked objects 
//// (the Chain) are offered the opportunity to respond to a request or hand it off to the object next in line.

//// http://www.dofactory.com/net/chain-of-responsibility-design-pattern

//using System;

//// The 'Handler' abstract class
//abstract class Handler
//{
//    protected Handler successor;

//    public Handler() { }

//    public Handler(Handler successor)
//    {
//        this.successor = successor;
//    }

//    public void SetSuccessor(Handler successor)
//    {
//        this.successor = successor;
//    }

//    public abstract void HandleRequest(int request);
//}


//// The 'ConcreteHandler1' class
//class ConcreteHandler1 : Handler
//{
//    public override void HandleRequest(int request)
//    {
//        if (request >= 0 && request < 10)
//        {
//            Console.WriteLine("{0} handled request {1}", this.GetType().Name, request);
//        }
//        else if (successor != null)
//        {
//            successor.HandleRequest(request);
//        }
//    }
//}


//// The 'ConcreteHandler2' class
//class ConcreteHandler2 : Handler
//{
//    public override void HandleRequest(int request)
//    {
//        if (request >= 10 && request < 20)
//        {
//            Console.WriteLine("{0} handled request {1}", this.GetType().Name, request);
//        }
//        else if (successor != null)
//        {
//            successor.HandleRequest(request);
//        }
//    }
//}


//// The 'ConcreteHandler3' class
//class ConcreteHandler3 : Handler
//{
//    public ConcreteHandler3() { }

//    public ConcreteHandler3(Handler successor) : base(successor) { }

//    public override void HandleRequest(int request)
//    {
//        if (request >= 20 && request < 30)
//        {
//            Console.WriteLine("{0} handled request {1}", this.GetType().Name, request);
//        }
//        else if (successor != null)
//        {
//            successor.HandleRequest(request);
//        }
//    }
//}


//// The 'ConcreteHandler4' class
//class ConcreteHandler4 : Handler
//{
//    public override void HandleRequest(int request)
//    {
//        if (request >= 30 && request < 40)
//        {
//            Console.WriteLine("{0} handled request {1}", this.GetType().Name, request);
//        }
//        else if (successor != null)
//        {
//            successor.HandleRequest(request);
//        }
//    }
//}

//// MainApp startup class for Structural Chain of Responsibility Design Pattern.
//class MainApp
//{
//    // Entry point into console application.
//    static void Main()
//    {
//        // Setup Chain of Responsibility (top-down)
//        Handler h4 = new ConcreteHandler4();
//        Handler h3 = new ConcreteHandler3(h4);
//        Handler h2 = new ConcreteHandler2();
//        Handler h1 = new ConcreteHandler1();

//        h1.SetSuccessor(h2);
//        h2.SetSuccessor(h3);

//        // Generate requests
//        int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20, 3, 35, 50, 39, 42, 32 };

//        // Process requests
//        foreach (int request in requests)
//            h1.HandleRequest(request);

//        // Wait for user
//        Console.ReadKey();
//    }
//}