// Builder Design Pattern - Creational Category

// GoF defines Builder Design Pattern as follows:
// The Builder Design Pattern is used to separate the construction of a complex object from its representation 
// so that the same construction process can create different objects representations.

// What this means is that we will have to design the system in such a way that the client application will 
// simply specify the parameters that should be used to create the complex object and the builder will 
// take care of building the complex object.

// Problem
// We want to construct a complex object, however we do not want to have a complex constructor member or 
// one that would need many arguments.

// Solution
// Define an intermediate object whose member functions define the desired object part by part before 
// the object is available to the client. Builder Pattern lets us defer the construction of the complex object
// until all the options for creation have been specified and then creates the object in a step-by-step fashion.


#include <string>
#include <iostream>
 
using namespace std;
 
// Product
// Represents the complex object under construction. 
// Concrete Builders build the products' internal representation and define the process by which they are assembled.
// Includes classes that define the constituent parts, including interfaces for assembling the parts into the final object.
// This is the object that will be created by assembling many parts.
class Pizza
{
	public:

		void setName(const string & name) { m_name = name; }

		void setDough(const string & dough) { m_dough = dough; }

		void setSauce(const string & sauce) { m_sauce = sauce; }

		void setTopping(const string & topping) { m_topping = topping; }

		void open() const
		{
			cout << m_name << " pizza with " << m_dough << " dough, " << m_sauce << " sauce and " 
				<< m_topping << " topping." << endl << endl;
		}

	private:

		string m_name;
		string m_dough;
		string m_sauce;
		string m_topping;
};


// Abstract Builder
// This is an abstract interface for creating parts of the actual products.
class PizzaBuilder
{
	public:
	
		virtual ~PizzaBuilder() {}; 
 
		Pizza* getPizza() { return m_pizza; }
	
		void createNewPizzaProduct() { m_pizza = new Pizza; }
	
		virtual void buildName() = 0;
		virtual void buildDough() = 0;
		virtual void buildSauce() = 0;
		virtual void buildTopping() = 0;

	protected:

		Pizza* m_pizza;
};
 

// Concrete Builders
// Construct and assemble parts of the complex individual products by implementing the Builder interface.
// Define and keep track of the representation they create.
// Provide an interface for retrieving the products.
// The this handle will keep track of what Product it has created, i.e. which pizza it backed, 
// and the this reference will be used by the client to get the Product object.
// The member functions of the Concrete Builders define the desired objects part by part 
// before the objects become available to the client.

class SpicyPizzaBuilder : public PizzaBuilder
{
	public:

		virtual ~SpicyPizzaBuilder() {}; 
 
		virtual void buildName() { m_pizza->setName("Spicy"); }

		virtual void buildDough() { m_pizza->setDough("pan baked"); }

		virtual void buildSauce() { m_pizza->setSauce("hot"); }

		virtual void buildTopping() { m_pizza->setTopping("pepperoni + salami"); }
};

class IsraeliPizzaBuilder : public PizzaBuilder
{
	public:
	
		virtual ~IsraeliPizzaBuilder() {}; 
 
		virtual void buildName() { m_pizza->setName("Israeli"); }

		virtual void buildDough() { m_pizza->setDough("oven baked"); }

		virtual void buildSauce() { m_pizza->setSauce("tomato sauce"); }

		virtual void buildTopping() { m_pizza->setTopping("olives + onion"); }
};


class HawaiianPizzaBuilder : public PizzaBuilder
{
	public:
	
		virtual ~HawaiianPizzaBuilder() {}; 
 
		virtual void buildName() { m_pizza->setName("Hawaiian"); }

		virtual void buildDough() { m_pizza->setDough("cross"); }

		virtual void buildSauce() { m_pizza->setSauce("mild"); }

		virtual void buildTopping() { m_pizza->setTopping("ham + pineapple"); }
};
 
// Director
// Constructs an object using the Builder interface
// This is the Client code that will specify the parts needed to be put tegether 
// to create the actual concrete Product.
class Cook
{
	public:

		void setPizzaBuilder(PizzaBuilder* pb) { m_pizzaBuilder = pb; }

		void constructPizza()
		{
			m_pizzaBuilder->createNewPizzaProduct();
			m_pizzaBuilder->buildName();
			m_pizzaBuilder->buildDough();
			m_pizzaBuilder->buildSauce();
			m_pizzaBuilder->buildTopping();
		}

		Pizza* getPizza() { return m_pizzaBuilder->getPizza(); }

	private:
		
		PizzaBuilder* m_pizzaBuilder;
};
 

int main()
{
	Cook cook;
 
	cook.setPizzaBuilder(new SpicyPizzaBuilder);
	cook.constructPizza();
	cook.getPizza()->open();

	cook.setPizzaBuilder(new IsraeliPizzaBuilder);
	cook.constructPizza();
	cook.getPizza()->open();

	cook.setPizzaBuilder(new HawaiianPizzaBuilder);
	cook.constructPizza();
	cook.getPizza()->open();

	system("pause");
}