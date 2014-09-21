// Memento pattern is one the GOF design pattern that comes as a rescue to store the state of an object. 
// In Memento pattern, we do not create a copy of an object. We create Mementos that hold the state of 
// an object and it might include full object or elements of an object that needs to be stored.

// Wikipedia definition of Memento Pattern: The memento pattern is a software design pattern that provides 
// the ability to restore an object to its previous state (undo via rollback).

// Common usage: Undo and restore operations in most software.

// Ingredients of a Memento Pattern

// Originator: It is the one whose state needs to be saved and creates the Memento object.
// Memento: It holds the internal state of an Originator.
// Caretaker: It is responsible for keeping the memento.

// Discussion of the code below. 
// A memento is an object that stores a snapshot of the internal state of another object. 
// It can be leveraged to support multi-level undo of the Command pattern. 
// In this example, before a command is run against the Number object, 
// Number's current state is saved in Command's static memento history list, 
// and the command itself is saved in the static command history list. 
// Undo() simply "pops" the memento history list and reinstates Number's state from the memento. 
// Redo() "pops" the command history list. Note that Number's encapsulation is preserved, and Memento is wide open to Number.


#include <string>
#include <vector>
#include <sstream>
#include <iostream>

using namespace std;
 
const string NAME = "Number";
 
template <typename T>
string toString (const T& t) 
{
	stringstream ss;
	ss << t;
	return ss.str();
}
 
class Memento;
 
class Number 
{
	private:

		int value;
		string name;
		double decimal;  // and suppose there are loads of other data members

	public:

		Number (int newValue) : value (newValue), name (NAME + toString (value)), decimal ((float)value / 100) { }
		void doubleValue() { value = value * 2; name = NAME + toString (value); decimal = (float)value / 100; }
		void halfValue() { value = value / 2; name = NAME + toString (value); decimal = (float)value / 100; }
		void increaseByOne() { value++; name = NAME + toString (value); decimal = (float)value / 100; }
		int getValue() const { return value; }
		string getName() const { return name; }
		double getDecimal() const { return decimal; }
		Memento* createMemento() const;
		void reinstateMemento (Memento* mem);
};

 
class Memento {
	
	private:

		Number number;

	public:

		Memento (const Number & num) : number (num) { }

		// We want a snapshot of the entire Number because of its potentially numerous data members
		Number snapshot() const { return number; }  
};
 
Memento* Number::createMemento() const 
{
	return new Memento (*this);
}
 
void Number::reinstateMemento(Memento* mem) 
{
	*this = mem->snapshot();
}
 
class Command 
{
	private:

		typedef void (Number::*Action)();
		Number* receiver;
		Action action;
		static unsigned int numCommands;
		static unsigned int maxCommands;
		static vector<Command*> commandList;
		static vector<Memento*> mementoList;
	
	public:

		enum Operation {Exit, Double, Half, IncreasebyOne, Undo, Redo};

		Command (Number *newReceiver, Action newAction): receiver (newReceiver), action (newAction) { }
		
		virtual void execute() 
		{
			if (mementoList.size() < numCommands + 1)
				mementoList.resize (numCommands + 1);
			
			// Save the last value
			mementoList[numCommands] = receiver->createMemento();  
			
			if (commandList.size() < numCommands + 1)
				commandList.resize(numCommands + 1);
			
			 // Save the last command
			commandList[numCommands] = this; 
			
			if (numCommands > maxCommands)
				maxCommands = numCommands;

			numCommands++;

			(receiver->*action)();
		}

		static void undo() 
		{
			if (numCommands == 0)
			{
				cout << "There is nothing to undo at this point." << endl;
				return;
			}

			commandList[numCommands - 1]->receiver->reinstateMemento(mementoList[numCommands - 1]);

			numCommands--;
		}
		
		void static redo() 
		{
			if (numCommands > maxCommands)
			{
				cout << "There is nothing to redo at this point." << endl;
				return;
			}

			Command* commandRedo = commandList[numCommands];

			(commandRedo->receiver->*(commandRedo->action))();

			numCommands++;
		}
};

unsigned int Command::numCommands = 0;
unsigned int Command::maxCommands = 0;
vector<Command*> Command::commandList;
vector<Memento*> Command::mementoList;


int main()
{
	int i;
	cout << "Please enter an integer: ";
	cin >> i;

	Number *object = new Number(i);
 
	Command *commands[4];
	commands[1] = new Command(object, &Number::doubleValue);
	commands[2] = new Command(object, &Number::halfValue);
	commands[3] = new Command(object, &Number::increaseByOne);
 
	cout << "[0]Exit, [1]Double, [2]Half, [3]Increase by One, [4]Undo, [5]Redo: ";
	cin >> i;
 
	while (i != Command::Operation::Exit)
	{
		if (i == Command::Operation::Undo)
			Command::undo();
		else if (i == Command::Operation::Redo)
			Command::redo();
		else if (i > Command::Operation::Exit && i <= Command::Operation::IncreasebyOne)
			commands[i]->execute();
		else
		{
			cout << "Enter a proper choice: ";
			cin >> i;
			continue;
		}
		cout << "   " << object->getValue() << "  " << object->getName() << "  " << object->getDecimal() << endl;
		cout << "[0]Exit, [1]Double, [2]Half, [3]Increase by One, [4]Undo, [5]Redo: ";
		cin >> i;
	}
}