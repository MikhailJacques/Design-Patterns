// Adapter Design Pattern - Structural Category
 
// Specify the new desired interface
// Design a "wrapper" class that can map or "impedance match" the old to the new
// The client uses (is coupled to) the new interface
// The adapter/wrapper "maps" to the legacy implementation


// https://gist.github.com/pazdera/1145857

#include <string>
#include <iostream>

using namespace std;

// Define new type - wire with electrons
typedef int Cable; 
 
// Adaptee (source) interface
class EuropeanSocketInterface
{
	public:

		virtual int voltage() = 0;
		virtual Cable live() = 0;
		virtual Cable neutral() = 0; 
		virtual Cable earth() = 0;
};
 
// Concrete Adaptee
class Socket : public EuropeanSocketInterface
{
	public:

		int voltage() { return 230; }
		Cable live() { return 1; }
		Cable neutral() { return -1; }
		Cable earth() { return 0; }
};
 
// Target interface
class USASocketInterface
{
	public:

		virtual int voltage() = 0;
		virtual Cable live() = 0;
		virtual Cable neutral() = 0;
};
 
// Adapter
class Adapter : public USASocketInterface
{
	EuropeanSocketInterface * socket;
 
	public:

		void plugIn(EuropeanSocketInterface* outlet)
		{
			socket = outlet;
		}
 
		int voltage() { return 110; }
		Cable live() { return socket->live(); }
		Cable neutral() { return socket->neutral(); }
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
				cout << "Kettle is on fire!" << std::endl;
				return;
			}
 
			if (power->live() == 1 && power->neutral() == -1)
			{
				cout << "Coffee time!" << std::endl;
			}
		}
};
 
 
int main()
{
	Socket * socket = new Socket;
	Adapter * adapter = new Adapter;
	ElectricKettle* kettle = new ElectricKettle;
 
	// Pluging in
	adapter->plugIn(socket);
	kettle->plugIn(adapter);
 
	// Having coffee
	kettle->boil();
 
	system("pause");

	return 0;
}