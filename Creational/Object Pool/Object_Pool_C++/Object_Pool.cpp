// Object Pool Design Pattern - Creational Category

// Performance can sometimes be the key issue and the object creation is a costly step. 
// While the Prototype pattern helps in improving the performance by cloning the objects, 
// the Object Pool pattern offers a mechanism to reuse objects that are expensive to create. 

// We use an Object Pool whenever there are several clients that need the same stateless 
// resource which is expensive to create.

// The clients of an Object Pool "feel" like they are the only owners of a service although 
// the service is shared among many other clients.

// Implementation involves the following objects:
// Resource - wraps the limited reusable resource which will be shared by several clients for a limited amount of time.
// Client - uses an instance of type Reusable.
// ObjectPool - creates and manages the reusable objects for the use by Clients.

// The Client is responsible to request the Reusable object as well to release it back to the pool. 
// If this action is not be performed the Reusable object will be lost, being considered unavailable by the ResourcePool.

// The clients are not aware that they are sharing the Reusable object. From the client's point of view 
// they are the owners of a new object which comes from the Resource pool in the same way that it comes 
// from a factory or another creational design pattern. The only difference is that the Client should mark 
// the Reusable object as available after it finishes to use it. It's not about releasing the objects. 
// For example, if we work with databases, when a connection is closed it is not necessarily destroyed 
// and it means that it can be reused by another client.

// http://en.wikipedia.org/wiki/Object_pool_pattern
// http://www.oodesign.com/object-pool-pattern.html


#include <list>
#include <string>
#include <iostream>

using namespace std;

// Resource - wraps the limited reusable resource which will be shared by several clients for a limited amount of time.
class Resource
{
	public:

		Resource()
		{
			value = 0;
		}
 
		void reset()
		{
			value = 0;
		}
 
		int getValue()
		{
			return value;
		}
 
		void setValue(int val)
		{
			value = val;
		}

	private:

		int value;
};

// ObjectPool - creates and manages the reusable objects for the use by Clients.
// Note: this class is a singleton.
class ObjectPool
{ 
	public:

		// Static method for accessing class instance.
		// Part of Singleton design pattern.
		// Returns ObjectPool instance.
		static ObjectPool * getInstance()
		{
			if (instance == 0)
			{
				instance = new ObjectPool;
			}

			return instance;
		}
 
		// Returns an instance of Resource.
		// New Resource will be created if all the resources 
		// have been used at the time of the request.
		Resource * getResource()
		{
			if (resources.empty())
			{
				cout << "Creating a new Resource." << endl;

				return new Resource;
			}
			else
			{
				cout << "Reusing an existing Resource." << endl;

				Resource * resource = resources.front();

				resources.pop_front();

				return resource;
			}
		}
 
		// Return Resource back to the pool.
		// The resource must be initialized back to the default 
		// settings before someone else attempts to use it.
		void returnResource(Resource * object)
		{
			if (object != NULL)
			{
				object->reset();
				resources.push_back(object);
			}
		}

	private:

		// The private constructor can only be accessed from static method inside the class itself.
		// By providing a private constructor we prevent class instances from being created in any 
		// place other than this very class.
		ObjectPool() {}

		list<Resource *> resources;
        
		static ObjectPool * instance;
};
 

ObjectPool * ObjectPool::instance = 0;
 

int main()
{
	Resource *r1, *r2, *r3;
	ObjectPool * pool = ObjectPool::getInstance();
 
	// Resources will be created.
	r1 = pool->getResource();
	r1->setValue(10);
	cout << "r1 = " << r1->getValue() << " [" << r1 << "]" << endl;
 
	r2 = pool->getResource();
	r2->setValue(20);
	cout << "r2 = " << r2->getValue() << " [" << r2 << "]" << endl;
 
	r3 = pool->getResource();
	r3->setValue(30);
	cout << "r3 = " << r3->getValue() << " [" << r3 << "]" << endl;

	// Return two of the resources but keep the third one in use
	pool->returnResource(r1);
	pool->returnResource(r2);
	// pool->returnResource(r3);
 
	// Resources will be reused.
	// Notice that the value of the first two returned resources has been reset back to zero,
	// and the value of the third resource has been initialized to zero as it is a new resource.
	r1 = pool->getResource();
	cout << "r1 = " << r1->getValue() << " [" << r1 << "]" << endl;
 
	r2 = pool->getResource();
	cout << "r2 = " << r2->getValue() << " [" << r2 << "]" << endl;

	r3 = pool->getResource();
	cout << "r3 = " << r3->getValue() << " [" << r3 << "]" << endl;
   
	system("pause");

	return 0;
}
