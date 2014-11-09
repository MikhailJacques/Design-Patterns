// Prototype Design Pattern - Creational Category

// Note the clone() method in the Stooge hierarchy. 
// Each derived class implements that method by returning an instance of itself. 
// A Factory class maintains a suite of "breeder" objects (aka prototypes), 
// and knows how to delegate to the correct prototype.

// Dictionary
//
// Slapstick 
// - Comedy based on deliberately clumsy actions and humorously embarrassing events.
// - A device consisting of two flexible pieces of wood joined together at one end, 
//   used by clowns and in pantomime to produce a loud slapping noise.
// Stooge
// - A performer whose act involves being the butt of a comedian's jokes.
// - A person who serves merely to support or assist others, particularly in doing unpleasant work.

#include <map>
#include <vector>
#include <string>
#include <iostream>

using namespace std;

class Stooge 
{
	public:

		virtual Stooge * clone() = 0;
		virtual void slapStick() = 0;
};

class Larry : public Stooge 
{
	public:

		Stooge * clone() { return new Larry; }
		void slapStick() { cout << "Larry: poke eyes\n"; }
};

class Moe : public Stooge 
{
	public:
		
		Stooge * clone() { return new Moe; }
		void slapStick() { cout << "Moe: slap head\n"; }
};

class Curly : public Stooge 
{
	public:

		Stooge * clone() { return new Curly; }
		void slapStick() { cout << "Curly: suffer abuse\n"; }
};

class Mike : public Stooge 
{
	public:

		Stooge * clone() { return new Mike; }
		void slapStick() { cout << "Mike: kick clown's ass\n"; }
};


class Factory 
{
	public:
		
		static Stooge * makeStooge( int choice )
		{
			// Note that both methods work
			// return _prototypes[choice]->clone();
			return _prototypeReference.at(choice)->clone();
		}
	
	private:
		
		static Stooge * _prototypes[5];

		static map<int, Stooge *> createMap()
        {
			map<int, Stooge *> m;

			m[0] = NULL;
			m[1] = new Larry;
			m[2] = new Moe;
			m[3] = new Curly;
			m[4] = new Mike;

			return m;
        }
		
		static const map<int, Stooge *> _prototypeReference;
};

Stooge * Factory::_prototypes[] = { 0, new Larry, new Moe, new Curly, new Mike };

const map<int, Stooge *> Factory::_prototypeReference = Factory::createMap();


int main() 
{
	int choice;
	vector<Stooge *> roles;
	
   while (true) 
   {
		cout << "(1)Larry (2)Moe (3)Curly (4)Mike (0)Go: ";
		cin >> choice;

		if (choice == 0)
			break;

		roles.push_back(Factory::makeStooge(choice) );

   }

	for (unsigned int i = 0; i < roles.size(); ++i)
		roles[i]->slapStick();
   
	for (unsigned int i = 0; i < roles.size(); ++i)
		delete roles[i];

	system("pause");

	return 0;
}