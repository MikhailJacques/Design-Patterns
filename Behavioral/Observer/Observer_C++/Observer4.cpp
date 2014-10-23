// Observer Design Pattern - Behavioral Category

// Observer in C++: Before and After

// After
// 
// The Subject class is now decoupled from the number and type of Observer objects. 
// The client has asked for two DivObserver delegates (each configured differently) 
// and two ModObserver delegates (each configured differently as well).

// http://sourcemaking.com/design_patterns/observer/cpp/1

#include <vector>
#include <iostream>

using namespace std;

// Forward declaration
class Observer;

class Subject
{
    int _value;
    vector<Observer *> _views;

	void notify();

	public:

		void attach(Observer * obs)
		{
			_views.push_back(obs);
		}

		void set_val(int value)
		{
			_value = value;
			notify();
		}
};

class Observer
{
	public:

		virtual void update(int value) = 0;
};

class DivObserver : public Observer
{
	int _div;

	public:

		DivObserver(Subject * model, int div)
		{
			model->attach(this);
			_div = div;
		}
		
		void update(int v) // virtual
		{
			cout << v << " div " << _div << " is " << v / _div << '\n';
		}
};

class ModObserver : public Observer
{
	int _mod;

	public:

		ModObserver(Subject * model, int mod)
		{
			model->attach(this);
			_mod = mod;
		}
		
		void update(int v) // virtual
		{
			cout << v << " mod " << _mod << " is " << v % _mod << '\n';
		}
};

void Subject::notify()
{
	for (unsigned int i = 0; i < _views.size(); ++i)
		_views[i]->update(_value);
}

int main()
{
	Subject subj;

	DivObserver divObs1(&subj, 4);
	DivObserver divObs2(&subj, 3);
	ModObserver modObs3(&subj, 3);
	ModObserver modObs4(&subj, 5);

	subj.set_val(14);

	cin.get();
}