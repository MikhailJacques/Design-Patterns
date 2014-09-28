// Mediator Design Pattern - Behavioral Category

// The Mediator Design Pattern is used to takes the role of a hub or router and facilitates 
// the communication between many classes. The Mediator transforms a hard-to-implement many-to-many relation 
// to many-to-one and one-to-many relations, where the communication is handled by the mediator class.

// Mediator defines an object that encapsulates how a set of objects interact. 
// It promotes loose coupling by keeping objects from referring to each other explicitly 
// and it lets us vary their interaction independently.


// http://www.oodesign.com/mediator-pattern.html
// http://en.wikibooks.org/wiki/C%2B%2B_Programming/Code/Design_Patterns/Behavioral_Patterns#Mediator
// http://www.codeproject.com/Articles/667702/Mediator-Pattern
// http://www.dofactory.com/net/mediator-design-pattern

#include <list>
#include <string>
#include <iostream>

using namespace std;

class MediatorInterface;
 
class ColleagueInterface 
{
	private:

		string name;
	
	public:
		
		ColleagueInterface(const string & newName) : name(newName) {}
		string getName() const { return name; }
		virtual void sendMessage(const MediatorInterface &, const string &) const = 0;
		virtual void receiveMessage(const ColleagueInterface *, const string &) const = 0;
};


class Colleague : public ColleagueInterface 
{
	public:

		using ColleagueInterface::ColleagueInterface;

		Colleague(const string & newName) : ColleagueInterface (newName) {}

		virtual void sendMessage(const MediatorInterface &, const string &) const override;

	private:

		virtual void receiveMessage(const ColleagueInterface *, const string &) const override;
};


class MediatorInterface 
{
	private:

		list<ColleagueInterface *> colleagueList;

	public:

		const list<ColleagueInterface *>& getColleagueList() const { return colleagueList; }
		virtual void distributeMessage(const ColleagueInterface *, const string &) const = 0;
		virtual void registerColleague(ColleagueInterface* colleague) { colleagueList.emplace_back(colleague); }
};


class Mediator : public MediatorInterface 
{
	virtual void distributeMessage(const ColleagueInterface *, const string &) const override;
};


void Colleague::sendMessage(const MediatorInterface & mediator, const string & message) const 
{
	mediator.distributeMessage(this, message);
}


void Colleague::receiveMessage(const ColleagueInterface* sender, const string & message) const 
{
	cout << getName() << " received the message from " << sender->getName() << ": " << message << endl;			
}


void Mediator::distributeMessage(const ColleagueInterface* sender, const string& message) const 
{
	for ( const ColleagueInterface * x : getColleagueList() )
		if ( x != sender )  // Make sure not to send the message back to the sender
			x->receiveMessage (sender, message);
}


int main() 
{
	// (const Colleague &)
	Colleague * bob = new Colleague("Bob"), *sam = new Colleague("Sam"), 
		*frank = new Colleague("Frank"), *tom = new Colleague("Tom");

	Colleague * staff[] = {bob, sam, frank, tom};

	Mediator mediatorStaff, mediatorSamsBuddies;

	for (Colleague * x : staff)
		mediatorStaff.registerColleague(x);

	bob->sendMessage(mediatorStaff, "I'm quitting this job!");

	mediatorSamsBuddies.registerColleague(frank);  mediatorSamsBuddies.registerColleague (tom);  // Sam's buddies only

	sam->sendMessage(mediatorSamsBuddies, "Hooray!  He's gone!  Let's go for a drink, guys!");	
	
	system("pause");

	return 0;
}