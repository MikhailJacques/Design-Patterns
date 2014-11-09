// Factory Design Pattern - Creational Category

// Factory Definition
// A utility class that creates an instance of a class from a family of derived classes.

// Abstract Factory Definition
// A utility class that creates an instance of several families of classes. 
// It can also return a factory for a certain group.

// The Factory Design Pattern is useful in a situation that requires the creation 
// of many different types of objects, all derived from a common base type. 
// The Factory Method defines a method for creating the objects, which subclasses 
// can then override to specify the derived type that will be created. 
// Thus, at run time, the Factory Method can be passed a description of a desired 
// object (e.g., a string read from user input) and return a base class pointer 
// to a new instance of that object. 
// The pattern works best when a well-designed interface is used for the base class, 
// so there is no need to cast the returned object.

// Problem 
// We want to decide at run time what object is to be created based on some 
// configuration or application parameter. When we write the code, we do not know 
// what class should be instantiated.

// Solution 
// Define an interface for creating an object, but let subclasses decide which 
// class to instantiate. Factory Method lets a class defer instantiation to subclasses.

// In the following example, a factory method is used to create laptop or desktop computer objects at run time.

// http://en.wikibooks.org/wiki/C%2B%2B_Programming/Code/Design_Patterns#Factory

#include <map>
#include <string>
#include <iostream>

using namespace std;

class Computer
{
	public:

		// Without this destructor, you do not call Laptop or Desktop destructor in this example!
		virtual ~Computer() {};

		virtual void Run() = 0;
		virtual void Stop() = 0;
		virtual void Name() = 0;
	};

	class Laptop : public Computer
	{
		public:

			// Because we have virtual functions, we need virtual destructor
			virtual ~Laptop() {};

			virtual void Run() { mHibernating = false; }; 
			virtual void Stop() { mHibernating = true; };
			virtual void Name() { cout << "I am a Laptop\n"; };

		private:

			// Whether or not the machine is hibernating
			bool mHibernating; 
	};

	class Desktop : public Computer
	{
		public:
		
			virtual ~Desktop() { };

			virtual void Run() {mOn = true;}; 
			virtual void Stop() {mOn = false;};
			virtual void Name() { cout << "I am a Desktop\n"; };
			

		private:

			bool mOn; // Whether or not the machine has been turned on
 };

// The actual ComputerFactory class returns a Computer, given a real world description of the object.
class ComputerFactory
{
	public:
	
		static Computer * NewComputer(const string & description)
		{
			if(description == "laptop")
				return new Laptop;

			if(description == "desktop")
				return new Desktop;

			return NULL;
		}
};

int main( int argc, char **argv )
{
	Computer * computer = NULL;

	computer = ComputerFactory::NewComputer("laptop");
	computer->Name();

	computer = ComputerFactory::NewComputer("desktop");
	computer->Name();

	system("pause");

	return 0;
}