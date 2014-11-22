// Composite design pattern - Structural Category

// 1. Identify the scalar/primitive classes and vector/container classes.
// 2. Create an "interface" (lowest common denominator) that can make all concrete classes "interchangeable".
// 3. All concrete classes declare an "is a" relationship to the interface.
// 4. All "container" classes couple themselves to the interface 
//    (recursive composition, Composite "has a" set of children up the "is a" hierarchy).
// 5. "Container" classes use polymorphism as they delegate to their children.

// http://www.oodesign.com/composite-pattern.html
// http://sourcemaking.com/design_patterns/composite/cpp/1

#include <vector>
#include <iostream>

using namespace std;

// 2. Create an "interface" (lowest common denominator)
class Component
{
	public:
	
		virtual void traverse() = 0;
};

// 1. Scalar class   
// 3. "is a" relationship
class Leaf : public Component
{
	int value;

	public:

		Leaf(int val)
		{
			value = val;
		}

		void traverse()
		{
			cout << value << ' ';
		}
};

// 1. Vector class   
// 3. "is a" relationship
class Composite : public Component
{
	// 4. "container" coupled to the interface
	vector <Component *> children; 
	
	public:

		// 4. "container" class coupled to the interface
		void add(Component * ele)
		{
			children.push_back(ele);
		}

		void traverse()
		{
			for (int i = 0; i < children.size(); i++)
				
				// 5. Use polymorphism to delegate to children
				children[i]->traverse();
		}
};

int main()
{
	int value = 0;
	const int num_containers = 4, num_leaves = 3;

	Composite containers[num_containers];

	// Fill in each of the 4 containers with 3 leaf objects
	for (int i = 0; i < num_containers; i++)
	{
		for (int j = 0; j < num_leaves; j++)
		{
			value = i * num_leaves + j;
			containers[i].add(new Leaf(value));
		}
	}

	// Append containers 3 and 4 to the container 1
	for (int i = 1; i < num_containers; i++)
	{
		containers[0].add(&(containers[i]));
	}

	for (int i = 0; i < num_containers; i++)
	{
		containers[i].traverse();

		cout << endl;
	}

	for (int i = 2; i < num_containers; i++)
	{
		containers[1].add(&(containers[i]));
	}

	for (int i = 0; i < num_containers; i++)
	{
		containers[i].traverse();

		cout << endl;
	}

	for (int i = 3; i < num_containers; i++)
	{
		containers[2].add(&(containers[i]));
	}

	for (int i = 0; i < num_containers; i++)
	{
		containers[i].traverse();

		cout << endl;
	}

	cin.get();

	return 0;
}

// Output:
/*
0 1 2 3 4 5 6 7 8 9 10 11
3 4 5
6 7 8
9 10 11
0 1 2 3 4 5 6 7 8 9 10 11 6 7 8 9 10 11
3 4 5 6 7 8 9 10 11
6 7 8
9 10 11
0 1 2 3 4 5 6 7 8 9 10 11 9 10 11 6 7 8 9 10 11 9 10 11
3 4 5 6 7 8 9 10 11 9 10 11
6 7 8 9 10 11
9 10 11
*/