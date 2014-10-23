// Observer Design Pattern - Behavioral Category

// Observer in C++: Before and After

// Before
//
// The number and type of "user interface" (or dependent) 
// objects is hard-wired in the Subject class. 
// The user has no ability to affect this configuration.

// http://sourcemaking.com/design_patterns/observer/cpp/1

#include <iostream>

using namespace std;

class DivObserver
{
	int _div;

	public:

		DivObserver(int div)
		{
			_div = div;
		}

		void update(int val)
		{
			cout << val << " div " << _div << " is " << val / _div << '\n';
		}
};

class ModObserver
{
	int _mod;

	public:

		ModObserver(int mod)
		{
			_mod = mod;
		}

		void update(int val)
		{
			cout << val << " mod " << _mod << " is " << val % _mod << '\n';
		}
};

class Subject
{
	int _value;
	DivObserver _div_obj;
	ModObserver _mod_obj;

	public:

		Subject() : _div_obj(4), _mod_obj(3) { }

		void set_value(int value)
		{
			_value = value;
			notify();
		}

		void notify()
		{
			_div_obj.update(_value);
			_mod_obj.update(_value);
		}
};

int main()
{
	Subject subj;

	subj.set_value(14);

	cin.get();
}