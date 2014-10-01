// Bridge Design Pattern - Structural Category

// Decouples an abstraction from its implementation so that the two can vary independently.

// The motivation is to decouple the Time interface from the Time implementation, while still 
// allowing the abstraction and the realization to each be modelled with their own inheritance hierarchy. 
// The implementation classes below are straight-forward. The interface classes are a little more subtle. 
// Routinely, a Bridge pattern interface hierarchy "has a" implementation class. 
// Here the interface base class "has a" a pointer to the implementation base class, 
// and each class in the interface hierarchy is responsible for populating the base class pointer with 
// the correct concrete implementation class. Then all requests from the client are simply delegated by 
// the interface class to the encapsulated implementation class.

#include <string>
#include <iomanip>
#include <iostream>

using namespace std;

// Implementation base class (Abstract Implementor)
class TimeImp 
{
	public:
	
		TimeImp(int hr, int min) 
		{
			_hr = hr;
			_min = min;
		}
		
		virtual void tell() 
		{					   // setfill(48)
			cout << "Time is " << setfill('0') << setw(2) << _hr << _min << endl;
		}

	protected:
		
		int _hr, _min;
};

// Implementation concrete class (Concrete Implementor)
class CivilianTimeImp: public TimeImp 
{
	public:

		CivilianTimeImp(int hr, int min, int pm): TimeImp(hr, min) 
		{
			if (pm)
				strcpy_s(_m, 4, " PM");
			else
				strcpy_s(_m, 4, " AM");
		}

		// virtual
		void tell() 
		{
			cout << "Time is " << _hr << ":" << _min << _m << endl;
		}
	
	protected:

		char _m[4];
};

// Implementation concrete class (Concrete Implementor)
class ZuluTimeImp: public TimeImp 
{
	public:

		ZuluTimeImp(int hr, int min, int zone): TimeImp(hr, min) 
		{
			if (zone == 5)
				strcpy_s(_zone, 30, " Eastern Standard Time");
			else if (zone == 6)
				strcpy_s(_zone, 30, " Central Standard Time");
			else
				strcpy_s(_zone, 30, " Undefined Time Zone");
		}

		// virtual
		void tell() 
		{
			cout << "Time is " << setfill('0') << setw(2) << _hr << _min << _zone << endl;
		}

	protected:

		char _zone[30];
};


// Interface base class (Abstraction)
class Time 
{
	public:

		Time() { }

		Time(int hr, int min) 
		{
			_imp = new TimeImp(hr, min);
		}
		
		virtual void tell() 
		{
			_imp->tell();
		}

	protected:

		//  The interface base class "has a" a pointer to the implementation base class
		TimeImp *_imp;
};

// Each class in the interface hierarchy is responsible for populating the 
// base class pointer (_imp) with the correct concrete implementation class.

// (Refined Abstraction)
class CivilianTime: public Time 
{
	public:

		CivilianTime(int hr, int min, int pm) 
		{
			_imp = new CivilianTimeImp(hr, min, pm);
		}
};

// (Refined Abstraction)
class ZuluTime: public Time 
{
	public:

		ZuluTime(int hr, int min, int zone) 
		{
			_imp = new ZuluTimeImp(hr, min, zone);
		}
};


int main() 
{
	const int times_size = 5;
	Time * times[times_size];

	times[0] = new Time(14, 30);

	times[1] = new CivilianTime(2, 30, 1);

	times[2] = new ZuluTime(14, 30, 5);

	times[3] = new ZuluTime(14, 30, 6);

	times[4] = new ZuluTime(14, 30, 7);

	for (int i = 0; i < times_size; i++)
		times[i]->tell();

	system("pause");

	return 0;
}