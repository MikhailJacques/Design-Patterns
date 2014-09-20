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

#include <iostream>

using namespace std;

class Number;

class Memento
{
	public:
		
		Memento(int val) { _state = val; }
	
	private:
		
		friend class Number;
		int _state;
};

class Number
{
	public:

		Number(int value) { _value = value; }
		void doubleValue() { _value = 2 * _value; }
		void half() { _value = _value / 2; }
		int getValue() { return _value; }
		Memento *createMemento() { return new Memento(_value); }
		void reinstateMemento(Memento *mem) { _value = mem->_state; }

	private:

		int _value;
};

class Command
{
	public:

		typedef void(Number:: *Action)();

		Command(Number *receiver, Action action)
		{
			_receiver = receiver;
			_action = action;
		}

		virtual void execute()
		{
			_mementoList[_numCommands] = _receiver->createMemento();
			_commandList[_numCommands] = this;

			if (_numCommands > _highWater)
				_highWater = _numCommands;

			_numCommands++;
			(_receiver->*_action)();
		}

		static void undo()
		{
			if (_numCommands == 0)
			{
				cout << "*** Attempt to run off the end!! ***" << endl;
				return ;
			}

			_commandList[_numCommands - 1]->_receiver->reinstateMemento(_mementoList[_numCommands - 1]);

			_numCommands--;
		}

		void static redo()
		{
			if (_numCommands > _highWater)
			{
				cout << "*** Attempt to run off the end!! ***" << endl;
				return ;
			}
			(_commandList[_numCommands]->_receiver->*(_commandList[_numCommands]->_action))();

			_numCommands++;
		}

	protected:

		Number *_receiver;
		Action _action;
		static Command *_commandList[20];
		static Memento *_mementoList[20];
		static int _numCommands;
		static int _highWater;
};

Command *Command::_commandList[];
Memento *Command::_mementoList[];
int Command::_numCommands = 0;
int Command::_highWater = 0;

int main()
{
	int i;

	cout << "Integer: ";
	cin >> i;

	Number *object = new Number(i);

	Command *commands[3];
	commands[1] = new Command(object, &Number::doubleValue);
	commands[2] = new Command(object, &Number::half);

	cout << "Exit[0], Double[1], Half[2], Undo[3], Redo[4]: ";
	cin >> i;

	while (i)
	{
		if (i == 3)
			Command::undo();
		else if (i == 4)
			Command::redo();
		else
			commands[i]->execute();

		cout << "   " << object->getValue() << endl;
		cout << "Exit[0], Double[1], Half[2], Undo[3], Redo[4]: ";
		cin >> i;
	}
}