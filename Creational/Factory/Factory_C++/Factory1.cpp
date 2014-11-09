// Factory Design Pattern - Creational Category

// The Factory Design Pattern is probably the most used design pattern in modern programming languages.
// The intent is to create objects without exposing the instantiation logic to the client and 
// to refer to the newly created object through a common interface.

// We use Factory Design Pattern when a class can't anticipate the type of the objects it is supposed to create.

// Basically a Factory consists of an interface class which is common to all of the implementation classes 
// that the factory will create. Then you have the factory class which is usually a singleton class that 
// spawns instances of these implementation classes.

// The implementation is really simple
// The client needs a product, but instead of creating it directly using the new operator, it asks 
// the factory object for a new product, providing the information about the type of object it needs.
// The factory instantiates a new concrete product and then returns to the client the newly created product 
// (casted to abstract product class).
// The client uses the products as abstract products without being aware about their concrete implementation.

// The advantage is obvious: New animals can be added without changing a single line of code in the framework 
// (the client code that uses the animals from the factory).

// http://www.codeproject.com/Articles/363338/Factory-Pattern-in-Cplusplus

#include <map>
#include <string>
#include <iostream>

using namespace std;

// Base interface
class IAnimal
{
	public:

		virtual int GetNumberOfLegs() const = 0;
		virtual void Speak() const = 0;
		virtual void Free() = 0;
};

// IAnimal implementations

class Cat : public IAnimal
{
	public:

		Cat() { cout << "Cat object is created\n"; }
		~Cat() { cout << "Cat object is destroyed\n"; }
		
		int GetNumberOfLegs() const { return 4; }
		void Speak() const { cout << "Meow" << endl; }
		void Free() { delete this; }

		// The __stdcall calling convention is used to call Win32 API functions. 
		// The callee cleans the stack, so the compiler makes vararg functions __cdecl. 
		// Functions that use this calling convention require a function prototype.
		static IAnimal * __stdcall Create() { return new Cat(); }
};

class Dog : public IAnimal
{
	public:

		Dog() { cout << "Dog object is created\n"; }
		~Dog() { cout << "Dog object is destroyed\n"; }

		int GetNumberOfLegs() const { return 4; }
		void Speak() const { cout << "Woof" << endl; }
		void Free() { delete this; }
		
		static IAnimal * __stdcall Create() { return new Dog(); }
};

// Spider is not really an animal
class Spider : public IAnimal 
{
	public:

		Spider() { cout << "Spider object is created\n"; }
		~Spider() { cout << "Spider object is destroyed\n"; }

		int GetNumberOfLegs() const { return 8; }
		void Speak() const { cout << "I do not talk. I just keep quite and crawl.\n"; }
		void Free() { delete this; }

		static IAnimal * __stdcall Create() { return new Spider(); }
};

class Horse : public IAnimal
{
	public:

		Horse() { cout << "Horse object is created\n"; }
		~Horse() { cout << "Horse object is destroyed\n"; }

		int GetNumberOfLegs() const { return 4; }
		void Speak() const { cout << "Neigh" << endl; }
		void Free() { delete this; }

		static IAnimal * __stdcall Create() { return new Horse(); }
};

class Dolphin : public IAnimal
{
	public:

		Dolphin() { cout << "Dolphin object is created\n"; }
		~Dolphin() { cout << "Dolphin object is destroyed\n"; }

		int GetNumberOfLegs() const { return 0; }
		void Speak() const { cout << "Whistle" << endl; }
		void Free() { delete this; }

		static IAnimal * __stdcall Create() { return new Dolphin(); }
};

class Pig : public IAnimal
{
	public:

		Pig() { cout << "Pig object is created\n"; }
		~Pig() { cout << "Pig object is destroyed\n"; }

		int GetNumberOfLegs() const { return 4; }
		void Speak() const { cout << "Oink" << endl; }
		void Free() { delete this; }

		static IAnimal * __stdcall Create() { return new Pig(); }
};

// Human is not really an animal
class Human : public IAnimal
{
	public:

		Human() { cout << "Human object is created\n"; }
		~Human() { cout << "Human object is destroyed\n"; }

		int GetNumberOfLegs() const { return 2; }
		void Speak() const { cout << "Blah blah" << endl; }
		void Free() { delete this; }

		static IAnimal * __stdcall Create() { return new Human(); }
};

// Create function pointer that takes no arguments and returns a pointer to IAnimal.
// __stdcall is the calling convention used for the function. 
// This tells the compiler the rules that apply for setting up the stack, 
// pushing arguments and getting a return value.
// There are a number of other calling conventions, namely:
// __cdecl, __thiscall, __fastcall and the wonderfully named __naked. 
// __stdcall is the standard calling convention for Win32 system calls.
typedef IAnimal * (__stdcall * CreateAnimalFn) (void);

// Factory for creating instances of IAnimal
// This is a singleton pattern implementation, meaning only one instance 
// of the factory can ever be instantiated, no more, no less.
class AnimalFactory
{
	private:

		AnimalFactory();
		AnimalFactory(const AnimalFactory &) { }
		AnimalFactory & operator=(const AnimalFactory &) { return *this; }

		// Use typedef to declare a map that maps the animal name to
		// the function that creates that particular type of animal.
		typedef map<string, CreateAnimalFn>	FactoryMap;
		FactoryMap m_FactoryMap;
	
	public:

		~AnimalFactory() { m_FactoryMap.clear(); }

		static AnimalFactory * Get() 
		{
			// Mechanism for initializing a Factory class
			// This causes the Factory default constructor to be called, 
			// which registers the Factory functions - - the types of animals.
			static AnimalFactory instance;

			return &instance; 
		}
	
		void Register(const string & animalName, CreateAnimalFn pfnCreate);

		IAnimal * CreateAnimal(const string & animalName);
};


// Animal factory constructor. 
// The constructor is where we register our Factory functions - the types of animals.
// It is private and is called by the singleton accessor on first call.
// Though the registration does not have to be done here. It is done here for the purpose of this example. 
// We could for instance register our Factory types with the Factory class from somewhere else in the code.
AnimalFactory::AnimalFactory()
{
	Register("Cat", &Cat::Create);
	Register("Dog", &Dog::Create);
	Register("Spider", &Spider::Create);
	Register("Horse", &Horse::Create);
	Register("Dolphin", &Dolphin::Create);
	Register("Pig", &Pig::Create);
	Register("Human", &Human::Create);
}

// The implementation of the Register function is pretty straightforward since we use an
// std::map to hold the mapping between the animal type (string) and the create function.
void AnimalFactory::Register(const string & animalName, CreateAnimalFn pfnCreate)
{
	m_FactoryMap[animalName] = pfnCreate;
}

// The CreateAnimal function accepts a string parameter which corresponds to the string 
// registered in the AnimalFactory constructor. When this function receives "Horse" for example, 
// it will return an instance of the Horse class, which implements the IAnimal interface.
IAnimal * AnimalFactory::CreateAnimal(const string & animalName)
{
	FactoryMap::iterator it = m_FactoryMap.find(animalName);

	if( it != m_FactoryMap.end() )
		return it->second();

	return NULL;
}


int main( int argc, char **argv )
{
	string animalName;
	IAnimal * pAnimal = NULL;

	while( pAnimal == NULL )
	{
		cout << "Type the name of an animal or 'q' to quit: ";
		cin >> animalName;

		if( animalName == "q" )
			break;

		// The client needs a product, but instead of creating it directly using the new operator, 
		// it asks the factory object for a new product, providing the information about the type
		// of object it needs, in our case it is the animal's name.
		// The factory instantiates a new concrete product and then returns to the client the newly 
		// created product (casted to abstract product class).
		IAnimal * pAnimal = AnimalFactory::Get()->CreateAnimal(animalName);

		if( pAnimal )
		{
			cout << "Your animal has " << pAnimal->GetNumberOfLegs() << " legs." << endl;
			cout << "Your animal says: ";
			pAnimal->Speak();
		}
		else
		{
			cout << "This animal does not exist in the farm! Please choose another." << endl;
		}

		if( pAnimal )
			pAnimal->Free();

		pAnimal = NULL;
		animalName.clear();
	}

	return 0;
}