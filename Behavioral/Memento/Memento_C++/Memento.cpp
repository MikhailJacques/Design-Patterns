// Memento Design Pattern - Behavioral Category

// Memento pattern is one the GoF design patterns that comes as a rescue to store the state of an object. 
// In Memento pattern, we do not create a copy of an object. We create Mementos that hold the state of 
// an object and it might include full object or elements of an object that needs to be stored.

// Wikipedia definition of Memento Pattern: The Memento pattern is a software design pattern 
// that provides the ability to restore an object to its previous state (undo via rollback).

// Common usage: Undo and restore operations in most software.

// Ingredients of a Memento Pattern:
//
// Originator: The one whose state needs to be saved and the one that creates a Memento object.
// Memento: It holds the internal state of an Originator.
// Caretaker: It is responsible for keeping the Memento.

// Discussion of the code below. 
// A Memento is an object that stores a snapshot of the internal state of another object. 
// It can be leveraged to support multi-level undo of the Command pattern. 
// In this example, before a command is run against the Number object, the 
// Number's current state is saved in the Command's static Memento history list, 
// and the Command itself is saved in the Command's static Command history list. 
// Undo() simply "pops" the memento history list and reinstates Number's state from the Memento. 
// Redo() "pops" the command history list. 
// Note that Number's encapsulation is preserved and Memento is wide open to Number.

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
 
// Originator: One whose state needs to be saved and the one that creates a Memento object.
class Number 
{
	private:

		int value;
		string name;
		double decimal;  
		// and suppose there are loads of other data members

	public:

		Number (int newValue) : value(newValue), name(NAME + toString(value)), decimal((float)value / 100) { }

		void doubleValue() { value = value * 2; name = NAME + toString(value); decimal = (float)value / 100; }
		void halfValue() { value = value / 2; name = NAME + toString(value); decimal = (float)value / 100; }
		void increaseByOne() { value++; name = NAME + toString(value); decimal = (float)value / 100; }
		void decreaseByOne() { value--; name = NAME + toString(value); decimal = (float)value / 100; }

		int getValue() const { return value; }
		string getName() const { return name; }
		double getDecimal() const { return decimal; }

		Memento * createMemento() const;
		void reinstateMemento(Memento * mem);
};

// Memento: Holds the internal state of an Originator.
class Memento {
	
	private:

		Number number;

	public:

		Memento (const Number & num) : number(num) { }

		// We want a snapshot of the entire Number object
		// because of its potentially numerous data members
		Number snapshot() const { return number; }  
};
 
// Creates a new Memento object and returns a pointer to it.
// Passes to the Memento's constructor a reference to itself so that the newly 
// created Memento saves the state of the Number object that invoked this method.
Memento * Number::createMemento() const 
{
	return new Memento(*this);
}
 
// Reinstates the state saved in the Memento object passed as an argument.
void Number::reinstateMemento(Memento * mem) 
{
	*this = mem->snapshot();
}
 
// Caretaker: Responsible for keeping the Memento objects
class Command 
{
	private:

		// Define a new type, which is a pointer to a function 
		// declared in the Number class that takes no arguments.
		typedef void (Number::*Action)();

		Action action;
		Number * number;
		static unsigned int numCommands;
		static unsigned int maxCommands;
		static vector<Command *> commandHistory;	// Command history list (saves commands)
		static vector<Memento *> mementoHistory;	// Memento history list (saves Mementos with Number state)

	public:

		enum Operation {Exit, Double, Half, IncreasebyOne, DecreasebyOne, Undo, Redo};

		Command(Number * newNumber, Action newAction) : number(newNumber), action(newAction) { }
		
		// Before a command is run against the Number object, 
		// the Number's current state is saved in the Command's static Memento history list, 
		// and the Command itself is saved in the Command's static Command history list. 
		virtual void execute() 
		{
			// Ensure there is sufficient room in the list for adding a new element.
			if (mementoHistory.size() < numCommands + 1)
				mementoHistory.resize(numCommands + 1);
			
			// Save the Number's last value in the Command's static Memento history list.
			mementoHistory[numCommands] = number->createMemento();  
			
			// Ensure there is sufficient room in the list for adding a new element.
			if (commandHistory.size() < numCommands + 1)
				commandHistory.resize(numCommands + 1);
			
			// Save the last command in the Command's static Command history list.
			commandHistory[numCommands] = this; 
			
			if (numCommands > maxCommands)
				maxCommands = numCommands;

			numCommands++;

			// Run a command against the Number object
			(number->*action)();
		}

		// Pop the Memento history list and reinstate the Number's state from the Memento.
		static void undo() 
		{
			if (numCommands == 0)
			{
				cout << "There is nothing to undo at this point." << endl;
				return;
			}

			commandHistory[numCommands - 1]->number->reinstateMemento(mementoHistory[numCommands - 1]);

			numCommands--;
		}
		
		// Pop the Command history list 
		void static redo() 
		{
			if (numCommands > maxCommands)
			{
				cout << "There is nothing to redo at this point." << endl;
				return;
			}

			Command * commandRedo = commandHistory[numCommands];

			(commandRedo->number->*(commandRedo->action))();

			numCommands++;
		}
};

unsigned int Command::numCommands = 0;
unsigned int Command::maxCommands = 0;
vector<Command *> Command::commandHistory;
vector<Memento *> Command::mementoHistory;


int main()
{
	int i;
	cout << "Please enter an integer: ";
	cin >> i;

	Number * object = new Number(i);
 
	Command * commands[5];
	commands[1] = new Command(object, &Number::doubleValue);
	commands[2] = new Command(object, &Number::halfValue);
	commands[3] = new Command(object, &Number::increaseByOne);
	commands[4] = new Command(object, &Number::decreaseByOne);
 
	cout << "[0]Exit, [1]Double, [2]Half, [3]Increase, [4]Decrease, [5]Undo, [6]Redo: ";
	cin >> i;
 
	while (i != Command::Operation::Exit)
	{
		if (i == Command::Operation::Undo)
		{
			Command::undo();
		}
		else if (i == Command::Operation::Redo)
		{
			Command::redo();
		}
		else if (i > Command::Operation::Exit && i < Command::Operation::Undo)
		{
			commands[i]->execute();
		}
		else
		{
			cout << "Enter a proper choice: ";
			cin >> i;
			continue;
		}

		cout << "   " << object->getValue() << "  " << object->getName() << "  " << object->getDecimal() << endl;
		cout << "[0]Exit, [1]Double, [2]Half, [3]Increase, [4]Decrease, [5]Undo, [6]Redo: ";
		cin >> i;
	}
}