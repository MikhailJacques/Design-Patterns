// Composite design pattern - Structural Category

// Structural patterns deal with decoupling the interface and implementation of classes and objects
// Composite pattern forms a tree structure of simple and composite objects 

// http://advancedcppwithexamples.blogspot.co.il/2010/09/c-example-for-composite-design-pattern.html

#include<string>
#include<vector>
#include<iostream>

using namespace std;

//The 'Component' Treenode
class Element
{
	public:

		Element(string name) : name(name) { };
		virtual void Add(Element * d) = 0;
		virtual void Remove(Element * d) = 0;
		virtual void Display(int indent) = 0;
		virtual ~Element() {};

	protected:

		string name;
		
	private:

		Element(); // disallowed
};

// The 'Leaf' class
class PrimitiveElement : public Element
{
	public:

		PrimitiveElement(string name) : Element(name) { };

		void Add(Element * d)
		{
			cout << "Cannot add to a PrimitiveElement" << endl;
		}
	
		void Remove(Element * d)
		{
			cout << "Cannot remove from a PrimitiveElement" << endl;
		}
	
		void Display(int indent)
		{
			string newStr(indent, '-');
			cout << newStr << " " << name << endl;
		}
	
		virtual ~PrimitiveElement() { };
	
	private:

		PrimitiveElement(); // not allowed
};

// The 'Composite' class
class CompositeElement : public Element
{
	public:

		CompositeElement(string name) : Element(name) { };

		void Add(Element * d)
		{
			elements.push_back(d);
		}
	
		void Remove(Element * d)
		{
			vector<Element *>::iterator it = elements.begin();
	
			while (it != elements.end())
			{
				if (*it == d)
				{
					delete d;
					elements.erase(it);
					break;
				}

				++it;
			}
		}

		void Display(int indent)
		{
			string newStr(indent, '-');

			cout << newStr << "+ " << name << endl;

			vector<Element *>::iterator it = elements.begin();
	
			while(it != elements.end())
			{
				(*it)->Display(indent + 2);
				++it;
			}
		}
	
		virtual ~CompositeElement()
		{
			while(!elements.empty())
			{
				vector<Element *>::iterator it = elements.begin();
				delete *it;
				elements.erase(it);
			}
		}
	
	private:
	
		CompositeElement(); // not allowed

		vector<Element *> elements;

};

int main()
{
	// Create a Tree Structure
	CompositeElement * root = new CompositeElement("Paintings");
	root->Add(new PrimitiveElement("Storm"));
	root->Add(new PrimitiveElement("Seashore"));
	root->Add(new PrimitiveElement("Night in Venice"));
	root->Add(new PrimitiveElement("Ninth Wave"));

	// Create first Branch
	CompositeElement * comp1 = new CompositeElement("Geometric figures");
	comp1->Add(new PrimitiveElement("Black Circle"));
	comp1->Add(new PrimitiveElement("White Triangle"));
	comp1->Add(new PrimitiveElement("Red Square"));
	comp1->Add(new PrimitiveElement("Blue Line"));
	root->Add(comp1);

	// Create second Branch
	CompositeElement * comp2 = new CompositeElement("Animals");
	comp2->Add(new PrimitiveElement("Horse"));
	comp2->Add(new PrimitiveElement("Dolphin"));
	comp2->Add(new PrimitiveElement("Elephant"));
	root->Add(comp2);

	// Add a primitive element to the second branch
	PrimitiveElement * pe1 = new PrimitiveElement("Cat");
	comp2->Add(pe1);

	// Add a primitive element to the second branch
	comp2->Add(new PrimitiveElement("Dog"));

	// Add a primitive element to the tree
	PrimitiveElement * pe2 = new PrimitiveElement("Sunset at Sea");
	root->Add(pe2);

	// Add a primitive element to first branch
	PrimitiveElement * pe3 = new PrimitiveElement("Orange Trapezoid");
	comp1->Add(pe3);

	// Add a primitive element to the tree
	PrimitiveElement * pe4 = new PrimitiveElement("Golden Horn");
	root->Add(pe4);

	// Remove a primitive element from the secodn branch
	comp2->Remove(pe1);

	// Recursively display nodes
	root->Display(1);

	// Delete the allocated memory
	delete root;

	cin.get();

	return 0;
}

// Output
/*
-+ Paintings
--- Storm
--- Seashore
--- Night in Venice
--- Ninth Wave
---+ Geometric figures
----- Black Circle
----- White Triangle
----- Red Square
----- Blue Line
----- Orange Trapezoid
---+ Animals
----- Horse
----- Dolphin
----- Elephant
----- Dog
--- Sunset at Sea
--- Golden Horn
*/