// Adapter Design Pattern - Structural Category
 
// Specify the new desired interface
// Design a "wrapper" class that can map or "impedance match" the old to the new
// The client uses (is coupled to) the new interface
// The adapter/wrapper "maps" to the legacy implementation

// An Adapter pattern comprises three components:
// Target: This is the interface with which the client interacts.
// Adaptee: This is the interface the client wants to interact with, but cannot without the help of the Adapter.
// Adapter: This is derived from Target and contains the object of Adaptee.

// https://gist.github.com/pazdera/1145857

#include <string>
#include <iostream>

using namespace std;

// Define new type - wire with electrons
typedef int wire; 

enum wire_type { EARTH = 0, LIVE, NEUTRAL };
 
// Adaptee: This is the interface the client wants to interact with, but cannot without the help of the Adapter.
class EuropeanSocketInterface
{
	public:

		virtual int voltage() = 0;
		virtual wire live() = 0;
		virtual wire neutral() = 0; 
		virtual wire earth() = 0;
};
 
// Concrete Adaptee
class EuropeanSocket : public EuropeanSocketInterface
{
	public:

		int voltage() { return 230; }
		wire live() { return LIVE; }
		wire neutral() { return NEUTRAL; }
		wire earth() { return EARTH; }
};
 
// Target: This is the interface with which the client interacts.
class USASocketInterface
{
	public:

		virtual int voltage() = 0;
		virtual wire live() = 0;
		virtual wire neutral() = 0;
};
 
// Adapter: This is derived from Target and contains the object of Adaptee.
class USAToEuropeanSocketAdapter : public USASocketInterface
{
	// Adaptee: This is the interface the client wants to interact with, but cannot without the help of the Adapter.
	EuropeanSocketInterface * socket;
 
	public:

		void plugIn(EuropeanSocketInterface * outlet)
		{
			socket = outlet;
		}
 
		int voltage() { return 110; }
		wire live() { return socket->live(); }
		wire neutral() { return socket->neutral(); }
};
 
// Client
class ElectricKettle
{
	USASocketInterface * power;
 
	public:

		void plugIn(USASocketInterface * supply)
		{
			power = supply;
		}
 
		void boil()
		{
			if (power->voltage() > 110)
			{
				cout << "Kettle is on fire!" << endl;
				return;
			}
 
			if (power->live() == LIVE && power->neutral() == NEUTRAL)
			{
				cout << "Coffee time!" << endl;
			}
		}
};
 
 
int main()
{
	EuropeanSocket * socket = new EuropeanSocket;								// Adaptee
	USAToEuropeanSocketAdapter * adapter = new USAToEuropeanSocketAdapter;		// Adapter
	ElectricKettle * kettle = new ElectricKettle;								// Client
 
	// Pluging in
	adapter->plugIn(socket);
	kettle->plugIn(adapter);
 
	// Having coffee
	kettle->boil();
 
	system("pause");

	return 0;
}