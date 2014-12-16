// Flyweight Design Pattern - Structural Category

// A flyweight is an object that minimizes memory use by sharing as much data as possible with other similar objects.
// It is a way to use objects in large numbers when a simple repeated representation would use an unacceptable amount of memory. 
// Often some parts of the object state can be shared, and it is common practice to hold them in external data structures and 
// pass them to the flyweight objects temporarily when they are used.
// A classic example usage of the flyweight pattern is the data structures for graphical representation of characters in a word 
// processor. It might be desirable to have, for each character in a document, a glyph object containing its font outline, 
// font metrics, and other formatting data, but this would amount to hundreds or thousands of bytes for each character. 
// Instead, for every character there might be a reference to a flyweight glyph object shared by every instance of the same 
// character in the document; only the position of each character (in the document and/or the page) would need to be stored 
// internally.

// The Flyweight pattern describes how to share objects to allow their use at fine granularities without prohibitive cost. 
// Each “flyweight” object is divided into two pieces: 
// - the state-dependent (extrinsic) part, and 
// - the state-independent (intrinsic) part. 
// Intrinsic state is stored (shared) in the Flyweight object. 
// Extrinsic state is stored or computed by client objects, and passed to the Flyweight when its operations are invoked.

// Structural Patterns deal with decoupling the interface and implementation of classes and objects.
// A Flyweight uses sharing to support large numbers of fine-grained objects efficiently. 

// Below is an example of charachter class. 
// Each character is unique and can have different size, but the rest of the features remain the same.

// http://advancedcppwithexamples.blogspot.co.il/2010/10/c-example-of-flyweight-design-pattern.html

#include <map>
#include <string>
#include <iostream>

using namespace std;

// The 'Flyweight' abstract class
class Character
{
	public:
		
		virtual void Display(int point_size) = 0;

	protected:

		char symbol;
		int width;
		int height;
		int ascent;
		int descent;
		int point_size;
};

// A 'ConcreteFlyweight' class
class CharacterA : public Character
{
	public:

		CharacterA()
		{
			symbol    = 'A';
			width     = 120;
			height    = 100;
			ascent    = 70;
			descent   = 0;
			point_size = 0; // Initialize
		}

	void Display(int point_size)
	{
		this->point_size = point_size;
		cout << symbol << " (Point size " << point_size << " )" << endl;
	}
};

// A 'ConcreteFlyweight' class
class CharacterB : public Character
{
	public:

	CharacterB()
	{
		symbol    = 'B';
		width     = 140;
		height    = 100;
		ascent    = 72;
		descent   = 0;
		point_size = 0; // Initialize
	}

	void Display(int point_size)
	{
		this->point_size = point_size;
		cout << symbol << " (Point size " << point_size << " )" << endl;
	}
};

// C, D, E, ...

// A 'ConcreteFlyweight' class
class CharacterZ : public Character
{
	public:

		CharacterZ()
		{
			symbol    = 'Z';
			width     = 100;
			height    = 100;
			ascent    = 68;
			descent   = 0;
			point_size = 0; // Initialize
		}

	  void Display(int point_size)
	  {
		this->point_size = point_size;
		cout << symbol << " (Point size " << point_size << " )" << endl;
	  }
};


class MyException : public exception
{
	private:

		string s;

	public:

		MyException(string ss) : s(ss) { }
		~MyException() throw () { } // Updated
		const char * what() const throw() { return s.c_str(); }
};

// The 'FlyweightFactory' class
class CharacterFactory
{
	public:

		virtual ~CharacterFactory()
		{
			while(!characters.empty())
			{
				map<char, Character *>::iterator it = characters.begin();
				delete it->second;
				characters.erase(it);
			}
		}

		Character * GetCharacter(char key)
		{
			Character * character = NULL;

			if(characters.find(key) != characters.end())
			{
				character = characters[key];
			}
			else
			{
				switch(key)
				{
					case 'A':
						character = new CharacterA();
						break;

					case 'B':
						character = new CharacterB();
						break;

					// ...

					case 'Z':
						character = new CharacterZ();
						break;

					default:

						string msg = "Character ";
						msg += static_cast<char>(key);
						msg += " is NOT implemented.";
						
						// throw msg;
						throw MyException(msg);						
				}

				characters[key] = character;
			}

		return character;
	}

	private:

		map<char, Character *> characters;
};


//The Main method
int main()
{
	string document = "AAZZBRBZBCDAB";
	const char * chars = document.c_str();

	Character * character;

	CharacterFactory * factory = new CharacterFactory;

	// Extrinsic state
	int point_size = 10;

	// For each character use a flyweight object
	for(size_t i = 0; i < document.length(); i++)
	{
		try
		{
			character = factory->GetCharacter(chars[i]);
		}
		//catch (string s)
		//{ 
		//	cout << s << endl;
		//  continue;
		//}
		catch(MyException & e)
		{
			cout << e.what() << endl;
			continue;
		}
		catch(...)
		{
			cout << "An exception occurred." << endl;
			continue;
		}

		character->Display(point_size++);
	}

	// Clean memory
	delete factory;

	cin.get();

	return 0;
}