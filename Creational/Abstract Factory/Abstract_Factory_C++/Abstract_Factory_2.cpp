// Abstract Factory - Creational Patterns Category

// "Think of constructors as factories that churn out objects". 
// Here we are allocating the constructor responsibility to a factory object, 
// and then using inheritance and virtual member functions to provide a "virtual constructor" capability. 
// So there are two dimensions of decoupling occurring. 
// The client uses the factory object instead of "new" to request instances; and, 
// the client "hard-wires" the family, or class, of that factory only once, and 
// throughout the remainder of the application only relies on the abstract base class.

// http://sourcemaking.com/design_patterns/abstract_factory/cpp/2

#include <iostream>
 
using namespace std;

#define SIMPLE
//#define ROBUST

class Shape 
{
	public:

		Shape() 
		{
			id = total++;
		}

		virtual void draw() = 0;

	protected:

		int id;
		static int total;
};

int Shape::total = 0;

class Circle : public Shape 
{
	public:

		void draw() 
		{
			cout << "circle " << id << ": draw" << endl;
		}
};

class Square : public Shape 
{
	public:

		void draw() 
		{
			cout << "square " << id << ": draw" << endl;
		}
};

class Ellipse : public Shape 
{
	public:

	void draw() 
	{
		cout << "ellipse " << id << ": draw" << endl;
	}
};

class Rectangle : public Shape 
{
	public:

	void draw() 
	{
		cout << "rectangle " << id << ": draw" << endl;
	}
};

class Factory 
{
	public:

		virtual Shape * createCurvedInstance() = 0;
		virtual Shape * createStraightInstance() = 0;
};

class SimpleShapeFactory : public Factory 
{
	public:

	Shape * createCurvedInstance() 
	{
		return new Circle;
	}

	Shape * createStraightInstance() 
	{
		return new Square;
	}
};

class RobustShapeFactory : public Factory 
{
	public:

		Shape * createCurvedInstance() 
		{
			return new Ellipse;
		}

		Shape * createStraightInstance() 
		{
			return new Rectangle;
		}
};

int main() 
{
	const int num_shapes = 3;

	#ifdef SIMPLE
		Factory * factory = new SimpleShapeFactory;
	#else ROBUST
		Factory * factory = new RobustShapeFactory;
	#endif
	
		Shape * shapes[num_shapes];

	shapes[0] = factory->createCurvedInstance();   // shapes[0] = new Circle;
	shapes[1] = factory->createStraightInstance(); // shapes[1] = new Square;
	shapes[2] = factory->createCurvedInstance();   // shapes[2] = new Circle;

	for (int i = 0; i < num_shapes; i++) 
		shapes[i]->draw();

	cin.get();

	return 0;
}