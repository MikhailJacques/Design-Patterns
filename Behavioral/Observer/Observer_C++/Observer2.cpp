// Observer Design Pattern - Behavioral Category

#include <vector>
#include <iostream>

using namespace std;

// http://sourcemaking.com/design_patterns/observer/cpp/3

// 1. Model the "independent" functionality with a "subject" abstraction
// 2. Model the "dependent" functionality with "observer" hierarchy
// 3. The Subject is coupled only to the Observer base class
// 4. Observers register themselves with the Subject
// 5. The Subject broadcasts events to all registered Observers
// 6. Observers "pull" the information they need from the Subject
// 7. Client configures the number and type of Observers

// Forward declaration
class Observer;

// 1. Model the "independent" functionality with a "subject" abstraction
class Subject 
{
	public:

		void attach(Observer * observer) 
		{
			views.push_back(observer);
		}

		void setVal(int val) 
		{
			_value = val;

			notify();
		}

		int getVal() 
		{
			return _value;
		}

		void notify();

	private:

		int _value;

		// 3. The Subject is coupled only to the Observer base class (interface)
		vector <class Observer *> views; 
};


class Observer 
{	
	public:

		Observer(Subject * subject, int divisor) 
		{
			_subject = subject;
			_divisor = divisor;

			// 4. Observers register themselves with the Subject
			_subject->attach(this);
		}

		virtual void update() = 0;

	protected:

			Subject * getSubject() 
			{
				return _subject;
			}

			int getDivisor() 
			{
				return _divisor;
			}

	private:

		int _divisor;

		// 2. Model the "dependent" functionality with "observer" hierarchy
		// It is necessary for the Observer to have a handle to the Subject so
		// that the Observer can pull the information it needs from the Subject
		Subject * _subject;
};


class DivisionObserver : public Observer 
{
	public:

		DivisionObserver(Subject * subject, int divisor) : Observer(subject, divisor) { }
		
		// 6. Observers "pull" the information they need from the Subject
		// (in response to broadcasted notification by the Subject)
		void update() 
		{
			int v = getSubject()->getVal(), d = getDivisor();
			
			cout << v << " divided by " << d << " is " << v / d << endl;
		}
};


class ModulusObserver : public Observer 
{
	public:

		ModulusObserver(Subject * subject, int subjectulus) : Observer(subject, subjectulus) { }
		
		// 6. Observers "pull" the information they need from the Subject
		// (in response to broadcasted notification by the Subject)
		void update() 
		{
			int v = getSubject()->getVal(), d = getDivisor();

			cout << v << " modulus " << d << " is " << v % d << endl;
		}
};


void Subject::notify() 
{
	// 5. The Subject broadcasts events to all registered Observers
	for (unsigned int i = 0; i < views.size(); i++)
		views[i]->update();
}


int main() 
{
	Subject subj;

	// 7. Client configures the number and type of Observers
	ModulusObserver modObs1(&subj, 3);
	DivisionObserver divObs1(&subj, 4), divObs2(&subj, 3);

	// Once the value is set, the subject notifies all registered observers
	subj.setVal(14);

	cin.get();

	return 0;
}