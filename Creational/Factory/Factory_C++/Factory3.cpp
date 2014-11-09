// Factory Design Pattern - Creational Category

#include <map>
#include <string>
#include <memory>
#include <iostream>
#include <stdexcept>

using namespace std;
 
class Pizza 
{
	public:

		// Without this, no destructor for derived Pizza's will be called.
		virtual ~Pizza() {};
		virtual int getPrice() const = 0;
	
};
 
class HamAndMushroomPizza : public Pizza 
{
	public:

		virtual ~HamAndMushroomPizza() {}; 
		virtual int getPrice() const { return 850; }; 
};
 
class DeluxePizza : public Pizza 
{
	public:

		virtual ~DeluxePizza() {};
		virtual int getPrice() const { return 1050; }; 
};
 
class HawaiianPizza : public Pizza 
{
	public:

		virtual ~HawaiianPizza() {};
		virtual int getPrice() const { return 1150; }; 
};
 
class PizzaFactory 
{
	public:

		enum PizzaType 
		{
			HAMMUSHROOM,
			DELUXE,
			HAWAIIAN
		};

		static Pizza * createPizza(PizzaType pizzaType) 
		{
			switch (pizzaType) 
			{
				case HAMMUSHROOM:	return new HamAndMushroomPizza();
				case DELUXE:		return new DeluxePizza();
				case HAWAIIAN:		return new HawaiianPizza();
			}

			throw "Invalid pizza type.";
		}

		static string getPizzaName(PizzaType type)
		{
			return PizzaName.at(type);
		}

	private:

		static const map<PizzaType, string> PizzaName;

		static map<PizzaType, string> createPizzaMap()
        {
			map<PizzaType, string> pizzaMap;

			pizzaMap[DELUXE] = "Deluxe";
			pizzaMap[HAWAIIAN] = "Hawaiian";
			pizzaMap[HAMMUSHROOM] = "HamMushroom";
			
			return pizzaMap;
        }
};

const map<PizzaFactory::PizzaType, string> PizzaFactory::PizzaName = PizzaFactory::createPizzaMap();

// Create all available pizzas and print their prices
void printPizzaInfo( PizzaFactory::PizzaType pizzatype )
{
	Pizza * pizza = PizzaFactory::createPizza(pizzatype);
	cout << "Price of " << PizzaFactory::getPizzaName(pizzatype) << " is " << pizza->getPrice() << endl;
	delete pizza;
}
 
int main ()
{
	printPizzaInfo( PizzaFactory::DELUXE );
	printPizzaInfo( PizzaFactory::HAWAIIAN );
	printPizzaInfo( PizzaFactory::HAMMUSHROOM );

	system("pause");

	return 0;
}