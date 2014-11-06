// Singleton Design Pattern - Creational Category

// We use the Singleton pattern when we want to create only one instance of a class in a truly 
// object oriented fashion by adhering to the basic principles of object oriented programming. 
// The Singleton design pattern comes under the classification of creational patterns, which 
// deal with the best ways to create objects. The Singleton design pattern is used, where only 
// one instance of an object is needed throughout the lifetime of an application. 
// The Singleton class is instantiated at the time of first access and the same instance is used 
// thereafter till the application quits.

// The Singletons are often used to control access to resources such as database connections or sockets. 
// Suppose we have a license for only one connection for our database. 
// A Singleton connection object makes sure that only one connection can be made at any time.

// It is pretty easy to implement the Singleton pattern in any object oriented programming languages 
// like C++, JAVA or C#. There are lots of different ways to implement the Singleton Pattern. 
// But by using a private constructor and a static method to create and return an instance of the class 
// is a popular way for implementing Singleton pattern. 

#include <iostream>

using namespace std;

class Singleton
{
	public:

		static Singleton * getInstance();
		//~Singleton();
		void name();
		inline int getValue() { return m_value; }
		inline void setValue( int v ) { m_value = v; }
		void printValue();
		void release();

	private:

		int m_value;
		static Singleton * m_singleton;

		// Declare these three private. This ensures they are unaccessable 
		// so that we do not accidently get copies of our singleton appearing
		Singleton() { }
		Singleton( Singleton const & ) { }	
		Singleton & operator=( Singleton const & ) { }
};

// Global static pointer used to ensure a single instance of the class.
Singleton* Singleton::m_singleton = NULL;


// This function is called to create an instance of the class.
Singleton* Singleton::getInstance()
{
	if ( !m_singleton )
		m_singleton = new Singleton();

	return m_singleton;

	// In single-threaded applications a locally static instance can be used 
	// instead of a dynamically allocated instance.
	// static Singleton m_singleton;
    // return &m_singleton;
}

void Singleton::name()
{
	cout << "My name is Singleton\nMy address is: " << this << endl;
}

void Singleton::printValue() 
{ 
	cout << "My value is: " << m_value << endl; 
}

void Singleton::release()
{
	cout << "\nSingleton has been destroyed\n";
}

// Do not be tempted to use the destructor to destroy the Singleton object.
//Singleton::~Singleton()
//{
//	// calling 'delete m_singleton' will invoke the destructor, 
//	// which in turn will try to delete _instance once more, infinitely. 
//	if ( m_singleton )
//		delete m_singleton;
//}


// This implementation is known as Scott Mayers' Singleton
// This approach is founded on C++'s guarantee that local static objects are intiialized 
// when the object's definition is first encountered during a call to that function.
// As a bonus, if you never call a function emulating a non-local static object, 
// you never incur the cost of constructing and destructing the object.

// When we call Singleton& s = Singleton::Instance() first time the object is created 
// and every next call to Singleton::Instance() results with the same object being returned. 
// Main issue: subject to 'Destruction Order Fiasco'
class Singleton2
{
	public:

		static Singleton2 & getInstance() 
		{
			static Singleton2 m_singleton;
			return m_singleton;
		}

		void name()
		{
			cout << "My name is Singleton 2\nMy address is: " << this << endl;
		}

		int getValue() { return m_value; }
		void setValue( int v ) { m_value = v; }
		void printValue() { cout << "My value is: " << m_value << endl; }

	private:

		int m_value;

		Singleton2() { };
		~Singleton2() { };
		Singleton2( Singleton2 const & );
		Singleton2 & operator=( Singleton2 const & );
};



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

	sc1->release();

	cout << endl;

	Singleton2 & sc3 = Singleton2::getInstance();
	sc3.name();
	sc3.setValue(3);
	sc3.printValue();

	cout << endl;

	Singleton2 & sc4 = Singleton2::getInstance();
	sc4.name();
	sc3.setValue(5);
	sc4.printValue();

	cout << endl;

	system("pause");

	return 0;
}