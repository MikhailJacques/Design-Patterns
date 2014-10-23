// Observer Design Pattern - Behavioral Category

// SensorSystem is the "Subject". 
// Lighting, Gates and Surveillance are the "Views". 
// The Subject is only coupled to the "abstraction" of AlarmListener.

// An object's class defines how the object is implemented. 
// In contrast, an object's type only refers to its interface. 
// Class inheritance defines an object's implementation in terms of another object's implementation. 
// Type inheritance describes when an object can be used in place of another.

// http://sourcemaking.com/design_patterns/observer/cpp/2

#include <vector>
#include <iostream>

using namespace std;

class AlarmListener
{
	public:

		virtual void alarm() = 0;
};

class SensorSystem
{
	vector <AlarmListener *> listeners;
	
	public:

		void attach(AlarmListener * alarm)
		{
			listeners.push_back(alarm);
		}

		void soundTheAlarm()
		{
			for (unsigned int i = 0; i < listeners.size(); i++)
				listeners[i]->alarm();
		}
};

class Lighting : public AlarmListener
{
	public:

		void alarm() { cout << "Turn the lights on" << '\n'; }	// virtual
};

class Gates : public AlarmListener
{
	public:

		void alarm() { cout << "Close the gates" << '\n'; } // virtual
};

class CheckList
{
	virtual void localize() { cout << "  - Establish a perimeter" << '\n'; }
	virtual void isolate()  { cout << "  - Isolate the grid" << '\n'; }
	virtual void identify() { cout << "  - Identify the source" << '\n'; }

	public:

		void byTheNumbers()
		{
			// Template Method design pattern
			localize();
			isolate();
			identify();
		}
};

// class inheritance and type inheritance
class Surveillance : public CheckList, public AlarmListener
{
	void isolate() { cout << "  - Train the cameras" << '\n'; } // virtual
	
	public:

		void alarm() // virtual
		{
			cout << "Surveillance - by the numbers:" << '\n';
			byTheNumbers();
		}
};

int main()
{
	SensorSystem ss;

	ss.attach(&Gates());
	ss.attach(&Lighting());
	ss.attach(&Surveillance());

	ss.soundTheAlarm();

	cin.get();
}