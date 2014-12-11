// Chain of Responsibility Design Pattern - Behavioral Category

// 1. Put a "next" pointer in the base class
// 2. The "chain" method in the base class always delegates to the next object
// 3. If the derived classes cannot handle, they delegate to the base class

// http://sourcemaking.com/design_patterns/chain_of_responsibility/cpp/1

#include <iostream>

using namespace std;

class Base
{
		// 1. "next" pointer in the base class
		Base * next; 
  
	public:
    
		Base()
		{
			next = NULL;
		}

		void setNext(Base * n)
		{
			next = n;
		}

		void add(Base * n)
		{
			if (next)
			  next->add(n);
			else
			  next = n;
		}

		// 2. The "chain" method in the base class always delegates to the next object
		virtual void handle(int i)
		{
			next->handle(i);
		}
};


class Handler1: public Base
{
	public:

		void handle(int i)
		{
			if (i % 3)
			{
				// 3. Handle ONLY multiples of 3; otherwise pass on to the next handler
				cout << "H1 passed " << i << "  ";

				// 3. Delegate to the base class to pass on to the next handler
				Base::handle(i); 
			}
			else
			{
				cout << "H1 handled " << i << " (multiple of 3)\n";
			}
		}
};

class Handler2: public Base
{
	public:

		void handle(int i)
		{
			if (i % 2)
			{
				// 3. Handle ONLY even numbers; otherwise pass on to the next handler
				cout << "H2 passed " << i << "  ";

				// 3. Delegate to the base class to pass on to the next handler
				Base::handle(i);
			}
			else
			{
			  cout << "H2 handled " << i << " (even number)\n";
			}
		}
};

class Handler3: public Base
{
	public:
	  
		void handle(int i)
		{
			if (!(i % 2))
			{
				// 3. Handle ONLY odd numbers; otherwise pass on to the next handler
				cout << "H3 passed " << i << " ";

				// 3. Delegate to the base class to pass on to the next handler
				Base::handle(i);
			}
			else
			{
				cout << "H3 handled " << i << " (odd number)\n";
			}
		}
};

int main()
{
	Handler1 one;
	Handler2 two;
	Handler3 three;

	one.add(&two);				// one.setNext(&two);
	one.add(&three);			// two.setNext(&three);
	three.setNext(&one);		// three.setNext(&one);

	for (int i = 1; i < 10; i++)
		one.handle(i);

	cin.get();
}