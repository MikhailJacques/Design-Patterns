// Adapter Design Pattern - Structural Category

// An Adapter Design Pattern converts the interface of a class into another interface the clients expect. 
// Adapter lets classes work together that could not otherwise because of incompatible interfaces.

// An Adapter pattern comprises three components:
// Target: This is the interface with which the clients interact.
// Adaptee: This is the interface the client wants to interact with, but cannot without the help of the Adapter.
// Adapter: This is derived from Target and contains the object of Adaptee.

// http://www.codeproject.com/Tips/595716/Adapter-Design-Pattern-in-Cplusplus

#include <iostream>

using namespace std;

// Abstract Target: This is the interface the client interacts with.
class AbstractPlug 
{
	public:

		void virtual RoundPin() { }
		void virtual PinCount() { }
};

// Concrete Target
class Plug : public AbstractPlug 
{
	public:

		void RoundPin() { cout << "I am Round Pin" << endl; }
		void PinCount() { cout << "I have two pins" << endl; }
};


// Abstract Adaptee: This is the interface the client wants to interact with, 
// but cannot interact without the help of the Adapter.
class AbstractSwitchBoard 
{
	public:

		void virtual FlatPin() { }
		void virtual PinCount() { }
};

// Concrete Adaptee
class SwitchBoard : public AbstractSwitchBoard 
{
	public:

		void FlatPin() { cout << "I am Flat Pin" << endl; }
		void PinCount() { cout << "I have three pins" << endl; }
};

// Adapter: This is derived from Target and contains the object of Adaptee.
class Adapter : public AbstractPlug 
{
	private:

		AbstractSwitchBoard * _asb;

	public:

		Adapter(AbstractSwitchBoard * asb) { _asb = asb; }

		void RoundPin() { _asb->FlatPin(); }
		void PinCount() { _asb->PinCount(); }
};

// Client code
int main() 
{
	// Adaptee
	SwitchBoard *mySwitchBoard = new SwitchBoard; 
	
	// Target = Adapter(Adaptee)
	AbstractPlug * adapter = new Adapter(mySwitchBoard);

	adapter->RoundPin();
	adapter->PinCount();

	system("pause");

	return 0;
}