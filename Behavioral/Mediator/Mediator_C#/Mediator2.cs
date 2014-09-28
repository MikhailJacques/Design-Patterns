// Mediator Design Pattern - Behavioral Category

// This real-world code demonstrates the Mediator pattern facilitating loosely coupled 
// communication between different Participants registering with a Chatroom. 

// In a chat application we can have several participants. It's not a good idea to connect 
// each participant to all the others because the number of connections would be really high, 
// there would be technical problems due to proxies and firewalls, etc.
// The most appropriate solution is to have a hub where all participants will connect.
// The Chatroom is the central hub through which all communication takes place.
// This central hub is just the Mediator class.
// At this point only one-to-one communication is implemented in the Chatroom, 
// but it is trivial to change it to one-to-many.

// Participants
// The classes and objects participating in this pattern are:

// Mediator (IChatroom)
// Defines an interface for communicating with Colleague objects

// ConcreteMediator (Chatroom)
// Knows the Colleague class and keeps a reference to the Colleague objects
// Implements cooperative behavior by coordinating communication between the Colleague objects

// Colleague classes (Participant)
// Each Colleague object keeps a reference to (knows about) its Mediator object
// Each Colleague object communicates with its Mediator object whenever it would have otherwise 
// communicated with another Colleague object

// http://www.dofactory.com/net/mediator-design-pattern

using System;
using System.Collections.Generic;

namespace Mediator2
{
    /// <summary>
    /// MainApp startup class for Real-World 
    /// Mediator Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create chatroom
            Chatroom chatroom = new Chatroom();

            // Create participants and register them
            Participant George = new Beatle("George");
            Participant Paul = new Beatle("Paul");
            Participant Ringo = new Beatle("Ringo");
            Participant John = new Beatle("John");
            Participant Yoko = new NonBeatle("Yoko");
            Participant Mike = new NonBeatle("Mike");

            chatroom.Register(George);
            chatroom.Register(Paul);
            chatroom.Register(Ringo);
            chatroom.Register(John);
            chatroom.Register(Yoko);
            chatroom.Register(Mike);

            // Chatting participants
            Mike.Send("John", "We miss you!");
            Yoko.Send("John", "Hi John!");
            Paul.Send("Ringo", "All you need is love");
            Ringo.Send("George", "My sweet Lord");
            Paul.Send("John", "Can't buy me love");
            John.Send("Yoko", "My sweet love");

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Mediator' abstract class
    /// </summary>
    abstract class AbstractChatroom
    {
        public abstract void Register(Participant participant);
        public abstract void Send(string from, string to, string message);
    }

    /// <summary>
    /// The 'ConcreteMediator' class
    /// </summary>
    class Chatroom : AbstractChatroom
    {
        private Dictionary<string, Participant> _participants =
          new Dictionary<string, Participant>();

        public override void Register(Participant participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.Name] = participant;
            }

            participant.Chatroom = this;
        }

        public override void Send(string from, string to, string message)
        {
            Participant participant = _participants[to];

            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }

    /// <summary>
    /// The 'AbstractColleague' class
    /// </summary>
    class Participant
    {
        private Chatroom _chatroom;
        private string _name;

        // Constructor
        public Participant(string name)
        {
            this._name = name;
        }

        // Gets participant name
        public string Name
        {
            get { return _name; }
        }

        // Gets chatroom
        public Chatroom Chatroom
        {
            set { _chatroom = value; }
            get { return _chatroom; }
        }

        // Sends message to given participant
        public void Send(string to, string message)
        {
            _chatroom.Send(_name, to, message);
        }

        // Receives message from given participant
        public virtual void Receive(string from, string message)
        {
            Console.WriteLine("{0} to {1}: '{2}'", from, Name, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    class Beatle : Participant
    {
        // Constructor
        public Beatle(string name) : base(name) { }

        public override void Receive(string from, string message)
        {
            Console.Write("To a Beatle: ");
            base.Receive(from, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    class NonBeatle : Participant
    {
        // Constructor
        public NonBeatle(string name) : base(name) { }

        public override void Receive(string from, string message)
        {
            Console.Write("To a non-Beatle: ");
            base.Receive(from, message);
        }
    }
}