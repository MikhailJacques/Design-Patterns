// Composite Design Pattern - Structural Category

// Motivation
// There are times when a program needs to manipulate a tree data structure and it is necessary to treat both 
// Branches as well as Leaf Nodes uniformly. Consider for example a program that manipulates a file system. 
// A file system is a tree structure that contains Branches which are Folders as well as Leaf nodes which are Files. 
// Note that a folder object usually contains one or more file or folder objects and thus is a complex object where a 
// file is a simple object. Note also that since files and folders have many operations and attributes in common, 
// such as moving and copying a file or a folder, listing file or folder attributes such as file name and size, 
// it would be easier and more convenient to treat both file and folder objects uniformly by defining a File System 
// Resource Interface.

// Intent
// The intent of this pattern is to compose objects into tree structures to represent part-whole hierarchies.
// Composite lets clients treat individual objects and compositions of objects uniformly.

// Implementation
// Component - Component is the abstraction for leafs and composites. 
// It defines the interface that must be implemented by the objects in the composition. 
// For example a file system resource defines move, copy, rename, and getSize methods for files and folders.
// Leaf - Leafs are objects that have no children. They implement services described by the Component interface. 
// For example a file object implements move, copy, rename, as well as getSize methods which are related to the Component interface.
// Composite - A Composite stores child components in addition to implementing methods defined by the component interface. 
// Composites implement methods defined in the Component interface by delegating to child components. 
// In addition Composites provide additional methods for adding, removing, as well as getting components.
// Client - The client manipulates objects in the hierarchy using the component interface.
// A client has a reference to a tree data structure and needs to perform operations on all nodes independent of the fact that 
// a node might be a branch or a leaf. The client simply obtains reference to the required node using the component interface, 
// and deals with the node using this interface; it doesn't matter if the node is a composite or a leaf.

// Applicability
// The composite pattern applies when there is a part-whole hierarchy of objects and a client needs to deal with objects uniformly 
// regardless of the fact that an object might be a leaf or a branch. 

// http://www.oodesign.com/composite-pattern.html

// Example

// 1. Identify the scalar/primitive classes and vector/container classes.
// 2. Create an "interface" (lowest common denominator) that can make all concrete classes "interchangeable".
// 3. All concrete classes declare an "is a" relationship to the interface.
// 4. All "container" classes couple themselves to the interface 
//    (recursive composition, Composite "has a" set of children up the "is a" hierarchy).
// 5. "Container" classes use polymorphism as they delegate to their children.

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