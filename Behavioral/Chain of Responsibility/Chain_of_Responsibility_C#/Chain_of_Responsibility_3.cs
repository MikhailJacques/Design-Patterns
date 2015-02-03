// Chain of Responsibility Design Pattern - Behavioral Category

// Introduction:
// The Chain of Responsibility Pattern describes how we handle a single request by a chain of multiple handler objects. 
// The request has to be processed by only one handler object from this chain. 
// However, the determination of processing the request is decided by the current handler. 
// If the current handler object is able to process the request, then the request will be processed in the current handler object; 
// Otherwise, the current handler object needs to shirk responsibility and push the request to the next chain handler object. 
// And so on and so forth until the request is processed.

// http://www.codeproject.com/Articles/41786/Chain-of-Responsibility-Design-Pattern

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Request
{
    public ResponsibilityLevel Level { get; set; }
    public string Description { get; set; }
}

public enum ResponsibilityLevel
{
    Low,
    Medium,
    High
}

// Handler Objects

// The Staff class is the base abstract handler class. 
// It defines the common function (ProcessRequest) to allow the derived class to implement the details. 
// In this class, a Staff (Boss) object is declared to implement the Responsibility Chain.
public abstract class Staff
{
    public string Name { get; set; }
    public Staff Boss { get; set; }
    public abstract void ProcessRequest(Request request);
}

// In the Teacher class, the condition to decide what type of request the teacher can handle is coded.
// If the request level is anything but Low, the teacher pushes the request to his/her boss for review.
public class Teacher : Staff
{
    public override void ProcessRequest(Request request)
    {
        if (request.Level != ResponsibilityLevel.Low)
        {
            Console.WriteLine("This is {0}.\nI am a teacher in this daycare facility.\n" +
                "I am not able to process your request.\nMy boss {1} will review your request.\n", this.Name, Boss.Name);

            if (Boss != null)
                Boss.ProcessRequest(request);
            else
                throw new NullReferenceException("No boss assigned.");
        }
        else
        {
            Console.WriteLine("This is {0}.\nI am a teacher in this daycare facility.\nI have approved your request.\n", this.Name);
        }
    }
}

// In the Manager class, the condition to decide what type of request the manager can handle is coded.
// It has its own condition to check if a request can be processed or be pushed up the managerial hierarchy.
public class Manager : Staff
{
    public override void ProcessRequest(Request request)
    {
        if (request.Level != ResponsibilityLevel.Medium)
        {
            Console.WriteLine("This is {0}.\nI am a manager in this daycare facility.\n" +
                "Sorry, I am not able to process your request.\nMy boss {1} will review your request.\n", this.Name, Boss.Name);

            if (Boss != null)
                Boss.ProcessRequest(request);
            else
                throw new NullReferenceException("No boss assigned.");
        }
        else
        {
            Console.WriteLine("This is {0}.\nI am a manager in this daycare facility.\nI have approved your request.\n", this.Name);
        }
    }
}

// The Director class is the same as the Teacher and Manager classes. 
// It is used as the end of the chain of responsibility in this case study.
public class Director : Staff
{
    public override void ProcessRequest(Request request)
    {
        if (request.Level != ResponsibilityLevel.High)
        {
            if (Boss != null)
            {
                Console.WriteLine("This is {0}.\nI am a director of this daycare facility.\n" +
                    "I am not able to process your request.\nMy boss {1} will review your request.", this.Name, Boss.Name);

                Boss.ProcessRequest(request);
            }
            else
                throw new NullReferenceException("No boss assigned.");
        }
        else
        {
            Console.WriteLine("This is {0}.\nI am a director of this daycare facility.\nI have approved your request.", this.Name);
        }
    }
}

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a teacher
            Teacher teacher = new Teacher();
            teacher.Name = "Tom";

            // Create a manager
            Manager manager = new Manager();
            manager.Name = "Jerry";

            // Create a director
            Director director = new Director();
            director.Name = "Susan";

            // Create an Organizational Chart, i.e. the Responsibility Chain.
            teacher.Boss = manager;
            manager.Boss = director;


            // Create a request that can be handled by a teacher.
            Request firstRequest = new Request();
            firstRequest.Description = "The parent requests to have a copy of their kid's daily status.\n";
            firstRequest.Level = ResponsibilityLevel.Low;
            Console.WriteLine("Request Info: " + firstRequest.Description);

            // Send the request
            teacher.ProcessRequest(firstRequest);
            Console.WriteLine();


            // Create a request that can be handled by a manager.
            Request secondRequest = new Request();
            secondRequest.Description = "The parent requests to pay the tuition fees.\n";
            secondRequest.Level = ResponsibilityLevel.Medium;
            Console.WriteLine("Request Info: " + secondRequest.Description);

            // Send the request
            teacher.ProcessRequest(secondRequest);
            Console.WriteLine();


            // Create a request that can be handled by the director only.
            Request thirdRequest = new Request();
            thirdRequest.Description = "Mr. Jacques requests to schedule a visit for all his kids.\n";
            thirdRequest.Level = ResponsibilityLevel.High;
            Console.WriteLine("Request Info: " + thirdRequest.Description);

            // Send the request
            teacher.ProcessRequest(thirdRequest);

            Console.ReadKey();
        }
    }
}