// Mediator Design Pattern - Behavioral Category

// The Mediator Design Pattern is used to takes the role of a hub or router and facilitates the communication between many classes. 
// It essentially encapsulates interaction and communication protocol between a set of loosely-coupled objects.
// The Mediator transforms a hard-to-implement many-to-many relation to many-to-one and one-to-many relations, 
// where the communication is handled by the mediator class thus keeping the objects from referring to each other directly.

// Mediator defines an object that encapsulates how a set of objects interact. 
// It promotes loose coupling by keeping objects from referring to each other explicitly and 
// it lets us vary their interaction independently.
// Though the Mediator pattern helps to avoid tight coupled frameworks it has a negative impact on the performance 
// since every communication must pass through the mediator on the way to the destined object and that makes the mediator 
// a bottle neck in the system.

// We use the Mediator pattern in the following cases:
//
// - Behaviour that is distributed between some objects can be grouped or customized.
// - Object reuse is difficult because it communicates with other objects.
// - Objects in the system communicate in well-defined but complex ways.

// Participants:
//
// MediatorInterface
// - Defines an interface for communicating with Colleague objects.
// - Knows the Colleague class and keeps a list of pointers to the Colleague objects.

// Mediator
// - Knows the Colleague class and keeps a list of pointers to the Colleague objects.
// - Implements cooperative behavior by coordinating communication between the Colleague objects.

// Colleague classes (Participant)
// - Each Colleague object keeps a reference to (knows about) its Mediator object NOT HERE.
// - Each Colleague object communicates with its Mediator object whenever it would have otherwise.
//   communicated with another Colleague object.

// http://www.oodesign.com/mediator-pattern.html
// http://en.wikibooks.org/wiki/C%2B%2B_Programming/Code/Design_Patterns/Behavioral_Patterns#Mediator

#include <list>
#include <vector>
#include <string>
#include <iostream>

using namespace std;

class MediatorInterface;
 
class ColleagueInterface 
{
	private:

		string name;
	
	public:
		
		ColleagueInterface(const string & new_name) : name(new_name) { }
		string getName() const { return name; }
		virtual void sendMessage(const MediatorInterface &, const string &) const = 0;
		virtual void receiveMessage(const ColleagueInterface *, const string &) const = 0;
};

// Colleague classes (Participant)
// - Each Colleague object registers with a Mediator object and communicates only with it 
//   whenever it would have otherwise communicated with another Colleague object directly.
class Colleague : public ColleagueInterface 
{
	public:

		// using ColleagueInterface::ColleagueInterface;

		Colleague(const string & new_name) : ColleagueInterface (new_name) { }

		virtual void sendMessage(const MediatorInterface &, const string &) const override;

	private:

		virtual void receiveMessage(const ColleagueInterface *, const string &) const override;
};

// Defines an interface for communicating with Colleague objects.
// Knows the Colleague class and keeps a list of pointers to the Colleague objects.
class MediatorInterface 
{
	private:

		list<ColleagueInterface *> colleagueList;

	public:

		const list<ColleagueInterface *> & getColleagueList() const { return colleagueList; }

		virtual void registerColleague(ColleagueInterface * colleague) { colleagueList.emplace_back(colleague); }

		virtual void distributeMessage(const ColleagueInterface *, const string &) const = 0;
};


// Implements cooperative behavior by coordinating communication between the Colleague objects
class Mediator : public MediatorInterface 
{
	virtual void distributeMessage(const ColleagueInterface *, const string &) const override;
};



// Implementation section

void Colleague::sendMessage(const MediatorInterface & mediator, const string & message) const 
{
	mediator.distributeMessage(this, message);
}

void Colleague::receiveMessage(const ColleagueInterface * sender, const string & message) const 
{
	cout << getName() << " received message from " << sender->getName() << ": " << message << endl;			
}

// Re-sends the received message to the registered Colleagues
void Mediator::distributeMessage(const ColleagueInterface * sender, const string & message) const 
{
	// Step through the list of the registered colleagues and transfer the message
	for ( const ColleagueInterface * receiver : getColleagueList() )
		if ( receiver != sender )  // Make sure not to re-send the message back to the sender
			receiver->receiveMessage(sender, message);
}


int main() 
{
	Colleague * bob = new Colleague("Bob"), *sam = new Colleague("Sam"),  *frank = new Colleague("Frank"), 
		*dilan = new Colleague("Dilan"), *tom = new Colleague("Tom"), *jerry = new Colleague("Jerry");

	Colleague * staff[] = {bob, sam, frank, dilan, tom, jerry};

	// vector<Colleague *> v(begin(staff), end(staff));

	Mediator everybody, friends_of_Sam, friends_of_Bob;

	// Register all the staff members
	for (Colleague * x : staff) // (Colleague * x : v)
		everybody.registerColleague(x);

	bob->sendMessage(everybody, "I'm quitting this job!");

	// Another way of implementing the above line might be as follows:
	// everybody.distributeMessage(bob, "ky");

	// Register Sam's friends only
	friends_of_Sam.registerColleague(frank);
	friends_of_Sam.registerColleague(dilan);

	sam->sendMessage(friends_of_Sam, "Hooray! Bob has left the building!");

	// Register Bob's friends only
	friends_of_Bob.registerColleague(tom);
	friends_of_Bob.registerColleague(jerry);

	bob->sendMessage(friends_of_Bob, "It is a raw deal mates.");

	cin.get(); // system("pause");

	return 0;
}