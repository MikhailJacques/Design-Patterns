// Chain of Responsibility Design Pattern - Behavioral Category

// Broken Chain
// 
// Sometimes we could forget to include in the implementation of the handleRequest method the call to the successor, 
// causing a break in the chain. The request isn’t sent forward from the broken link and so it ends up unhandled. 
// A variation of the pattern can be made to send the request to all the handlers by removing the condition from the handler 
// and always calling the successor.

// The following implementation eliminates the Broken Chain problem. The implementation moves the code to 
// traverse the chain into the base class keeping the request handling in a different method in the subclasses. 
// The handleRequest method is declared as final in the base class and is responsible to traverse the chain. 
// Each Handler have to implement the handleRequestImpl method, declared as abstract in the super class.

// http://www.oodesign.com/chain-of-responsibility-pattern.html

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Request
{
    private int m_value;
    private String m_description;

    public Request(String description, int value)
    {
        m_description = description;
        m_value = value;
    }

    public int getValue()
    {
        return m_value;
    }

    public String getDescription()
    {
        return m_description;
    }
}


public abstract class Handler
{
    protected Handler m_successor;

	public void setSuccessor(Handler successor)
	{
		m_successor = successor;
	}

    public void handleRequest(Request request)
    {
        bool handledByThisNode = this.handleRequestImpl(request);

        if (m_successor != null && !handledByThisNode)
        {
            m_successor.handleRequest(request);
        }
    }

    protected abstract bool handleRequestImpl(Request request);
}


public class ConcreteHandlerOne : Handler
{
    protected override bool handleRequestImpl(Request request)
    {
        if (request.getValue() < 0)
        {	    
            // If request is eligible handle it
	        Console.WriteLine("Negative values are handled by ConcreteHandlerOne: ");
            Console.WriteLine("\tConcreteHandlerOne.HandleRequest : " + request.getDescription() + request.getValue());
	        return true;
        }
        else
        {
	        return false;
        }
    }
}


public class ConcreteHandlerThree : Handler
{
    protected override bool handleRequestImpl(Request request)
    {
        if (request.getValue() == 0)
        {
            // If request is eligible handle it
            Console.WriteLine("Zero values are handled by ConcreteHandlerThree: ");
            Console.WriteLine("\tConcreteHandlerThree.HandleRequest : " + request.getDescription() + request.getValue());
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class ConcreteHandlerTwo : Handler
{
    protected override bool handleRequestImpl(Request request)
    {
        if (request.getValue() > 0)
        {
            // If request is eligible handle it
            Console.WriteLine("Positive values are handled by ConcreteHandlerTwo: ");
            Console.WriteLine("\tConcreteHandlerTwo.HandleRequest : " + request.getDescription() + request.getValue());
            return true;
        }
        else
        {
            return false;
        }
    }
}


public class MainApp
{
    static void Main(string[] args)
    {
        // Setup Chain of Responsibility
        Handler h1 = new ConcreteHandlerOne();
        Handler h2 = new ConcreteHandlerTwo();
        Handler h3 = new ConcreteHandlerThree();

        h1.setSuccessor(h2);
        h2.setSuccessor(h3);

        // Send requests to the chain
        h1.handleRequest(new Request("Negative Value ", -1));
        h1.handleRequest(new Request("Zero Value ", 0));
        h1.handleRequest(new Request("Positive Value ", 1));
        h1.handleRequest(new Request("Positive Value ", 2));
        h1.handleRequest(new Request("Negative Value ", -5));

        Console.ReadKey();
    }
}