// Creational Pattern: Singleton

// We use the Singleton pattern when we want to create only one instance of a class in a truly object oriented fashion 
// by adhering to the basic principles of object oriented programming. The Singleton design pattern comes under the 
// classification of creational patterns, which deal with the best ways to create objects. 
// The Singleton design pattern is used, where only one instance of an object is needed throughout the lifetime of an application. 
// The Singleton class is instantiated at the time of first access and the same instance is used thereafter till the application quits.

// The Singletons are often used to control access to resources such as database connections or sockets. 
// Suppose we have a license for only one connection for our database. 
// A Singleton connection object makes sure that only one connection can be made at any time.

// It is pretty easy to implement the Singleton Pattern in any object oriented programming languages like C++, JAVA or C#. 
// There are lots of different ways to implement the Singleton Pattern. But by using a private constructor and a static method 
// to create and return an instance of the class is a popular way for implementing Singleton Pattern. 

#include <iostream>

using namespace std;

class Singleton
{
	public:

		static Singleton* getInstance();
		~Singleton();
		void name();
		inline int getValue() { return m_value; }
		inline void setValue( int v ) { m_value = v; }
		void printValue();

	private:

		static Singleton *single;
		int m_value;

		// Declare these three private. This ensures they are unaccessable 
		// so that we do not accidently get copies of our singleton appearing
		Singleton() { }
		Singleton( Singleton const & ) { }	
		Singleton & operator=( Singleton const & ) { }
};

// Global static pointer used to ensure a single instance of the class.
Singleton* Singleton::single = NULL;


// This function is called to create an instance of the class.
Singleton* Singleton::getInstance()
{
	if ( !single )
		single = new Singleton();

	return single;
}

Singleton::~Singleton()
{
	if ( single )
		delete single;
}

void Singleton::name()
{
	cout << "My name is Singleton" << endl;
	cout << "My address is: " << this << endl;
}

void Singleton::printValue() 
{ 
	cout << "My value is: " << m_value << endl; 
}

int main()
{
	Singleton *sc1, *sc2;

	sc1 = Singleton::getInstance();
	sc1->name();
	sc1->setValue(5);
	sc1->printValue();

	cout << endl;

	sc2 = Singleton::getInstance();
	sc2->name();
	sc1->setValue(7);
	sc2->printValue();

	cout << endl;

	system("pause");

	return 0;
}