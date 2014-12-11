// Chain of Responsibility Design Pattern - Behavioral Category

// The approach of the five-year-old.

// This NOT really an example of a chain of responsibility design pattern but rather a simulation of it.

// http://www.linuxtopia.org/online_books/programming_books/c++_practical_programming/c++_practical_programming_247.html

#include <vector>
#include <iostream>

using namespace std;
 
enum Answer { NO, YES };


class GimmeStrategy 
{
	public:

		virtual Answer canIHave() = 0;
		virtual ~GimmeStrategy() { }
};


class AskMom : public GimmeStrategy 
{
	public:

		Answer canIHave() 
		{
			cout << "Mooom? Can I have this?" << endl;
			cout << "Nope.\n" << endl;
			return NO;
		}
};


class AskDad : public GimmeStrategy 
{
	public:

		Answer canIHave() 
		{
			cout << "Dad, I really need this!" << endl;
			cout << "Not now.\n" << endl;
			return NO;
		}
};


class AskGrandpa : public GimmeStrategy 
{
	public:

		Answer canIHave() 
		{
			cout << "Grandpa, is it my birthday yet?" << endl;
			cout << "Not yet.\n" << endl;
			return NO;
		}
};


class AskGrandma : public GimmeStrategy 
{
	public:

		Answer canIHave() 
		{
			cout << "Grandma, I really love you!" << endl;
			cout << "I love you too. You can have it now my dear :)\n" << endl;
			return YES;
		}
};

class AskBob : public GimmeStrategy 
{
	public:

		Answer canIHave() 
		{
			cout << "Bob, can you give it to me please?" << endl;
			cout << "I do not think I want to. Sorry.\n" << endl;
			return NO;
		}
};


class Gimme : public GimmeStrategy 
{
		vector<GimmeStrategy *> chain;

	public:

		Gimme() 
		{
			chain.push_back(new AskMom());
			chain.push_back(new AskDad());
			chain.push_back(new AskGrandpa());
			chain.push_back(new AskGrandma());
			chain.push_back(new AskBob());
		}

		Answer canIHave() 
		{
			vector<GimmeStrategy *>::iterator it = chain.begin();

			while(it != chain.end())
				if((*it++)->canIHave() == YES)
					return YES;
			
			// Reached end without success...
			cout << "Whiiiiinnne!" << endl;

			return NO;
		}

		~Gimme() { }
};
 
int main() 
{
	Gimme chain;

	if(chain.canIHave() == YES)
		cout << "Yesssssss!!!" << endl;

	cin.get(); 

    return 0;
}