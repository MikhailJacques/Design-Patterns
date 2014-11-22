// Composite design pattern - Structural Category

// Composite pattern lets clients treat individual objects and compositions of objects uniformly. 
// The Composite pattern can represent both conditions. 
// In this pattern, one can develop tree structures for representing part-whole hierarchies.

// http://en.wikibooks.org/wiki/C%2B%2B_Programming/Code/Design_Patterns/Structural_Patterns#Composite

#include <vector>
#include <memory>		// std::auto_ptr
#include <iostream>		// std::cout
#include <algorithm>	// std::for_each
#include <functional>	// std::mem_fun

using namespace std;
 
class Graphic
{
	public:

		virtual ~Graphic() { }
		virtual void print() const = 0;
};
 
class Ellipse : public Graphic
{
	public:
		void print() const { cout << "Ellipse \n"; }
};

class Square : public Graphic
{
	public:
		void print() const { cout << "Square \n"; }
};

class Circle : public Graphic
{
	public:
		void print() const { cout << "Circle \n"; }
};
 
class CompositeGraphic : public Graphic
{
	public:
	
		void print() const 
		{
			// for each element in graphic_list, call the print member function
			for_each(graphic_list.begin(), graphic_list.end(), mem_fun(&Graphic::print));
		}
 
		void add(Graphic * aGraphic) 
		{
			graphic_list.push_back(aGraphic);
		}
 
	private:

		vector<Graphic *>  graphic_list;
};
 
int main()
{
	// Initialize four ellipses
	const auto_ptr<Ellipse> ellipse1(new Ellipse());
	const auto_ptr<Ellipse> ellipse2(new Ellipse());
	const auto_ptr<Ellipse> ellipse3(new Ellipse());

	// Initialize four squares
	const auto_ptr<Square> square1(new Square());
	const auto_ptr<Square> square2(new Square());
	const auto_ptr<Square> square3(new Square());

	// Initialize four circles
	const auto_ptr<Circle> circle1(new Circle());
	const auto_ptr<Circle> circle2(new Circle());
	const auto_ptr<Circle> circle3(new Circle());

	// Initialize three composite graphics
	const auto_ptr<CompositeGraphic> graphic1(new CompositeGraphic());
	const auto_ptr<CompositeGraphic> graphic2(new CompositeGraphic());
	const auto_ptr<CompositeGraphic> graphic3(new CompositeGraphic());
 
	// Compose the graphics
	graphic1->add(ellipse1.get());
	graphic1->add(ellipse2.get());
	graphic1->add(ellipse3.get());

	graphic2->add(square1.get());
	graphic2->add(square2.get());
	graphic2->add(square3.get());
 
	graphic3->add(circle1.get());
	graphic3->add(circle2.get());
	graphic3->add(circle3.get());

	graphic1->add(graphic3.get());
	graphic2->add(graphic1.get());
 

	// Print the complete graphics
	graphic1->print();
	cout << endl;
	graphic2->print();
	cout << endl;
	graphic3->print();

	cin.get();

	return 0;
}