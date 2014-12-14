// Abstract Factory - Creational Patterns Category

// Trying to maintain portability across multiple "platforms" routinely requires lots of preprocessor "case" statements. 
// The Abstract Factory pattern suggests defining a creation services interface in a Factory base class, 
// and implementing each "platform" in a separate Factory derived class.

// The client creates a platform- specific "factory" object, while being careful to eschew the direct 
// use of the "new" operator to create products and delegates all creation requests to the factory.

// http://sourcemaking.com/design_patterns/abstract_factory/cpp/1

#include <iostream>
 
using namespace std;

#define WINDOWS

// Abstract product
class Widget 
{
	public:
	   virtual void draw() = 0;
};

// Concrete product
class MotifButton : public Widget 
{
	public:
	   void draw() { cout << "MotifButton\n"; }
};

// Concrete product
class MotifMenu : public Widget 
{
	public:
	   void draw() { cout << "MotifMenu\n"; }
};

// Concrete product
class WindowsButton : public Widget 
{
	public:
	   void draw() { cout << "WindowsButton\n"; }
};

// Concrete product
class WindowsMenu : public Widget 
{
	public:
	   void draw() { cout << "WindowsMenu\n"; }
};


// Abstract factory
class Factory 
{
	public:
	   virtual Widget * create_button() = 0;
	   virtual Widget * create_menu() = 0;
};

// Concrete factory
class MotifFactory : public Factory 
{
	public:
	   Widget * create_button() { return new MotifButton; }
	   Widget * create_menu() { return new MotifMenu; }
};

// Concrete factory
class WindowsFactory : public Factory 
{
	public:
	   Widget * create_button() { return new WindowsButton; }
	   Widget * create_menu() { return new WindowsMenu; }
};

void display_window_one(Factory * factory) 
{
   Widget * w[] = { factory->create_button(), factory->create_menu() };

   w[0]->draw();  
   w[1]->draw();
}

void display_window_two(Factory * factory) 
{
   Widget * w[] = { factory->create_menu(), factory->create_button() };

   w[0]->draw();  
   w[1]->draw();
}

int main() 
{
	Widget * w = NULL;
	Factory * factory;

	#ifdef MOTIF
	   factory = new MotifFactory;
	#else // WINDOWS
	   factory = new WindowsFactory;
	#endif

	w = factory->create_button();
	w->draw();

	w = factory->create_menu();
	w->draw();

	cout << endl;

	display_window_one(factory);

	cout << endl;

	display_window_two(factory);

	cin.get();

	return 0;
}