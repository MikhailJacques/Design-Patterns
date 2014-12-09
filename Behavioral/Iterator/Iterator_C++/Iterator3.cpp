// Iterator Design Pattern - Behavioral Category

// Exclude rarely-used stuff from Windows headers
#define WIN32_LEAN_AND_MEAN

#include <string>
#include <vector>
#include <iostream>

using namespace std;

class IIterator
{
	public:

		virtual string FirstItem() = 0;
		virtual string NextItem() = 0;
		virtual string CurrentItem() = 0;
		virtual bool IsDone() = 0;		
};

class IAggregate
{
	public:

		virtual IIterator * GetIterator() = 0;
		virtual string & operator[](int itemIndex) = 0;
		virtual int Count() = 0;
};


class MyIterator : public IIterator
{
		int current_index;
		IAggregate * p_aggregate;

	public:

		MyIterator(IAggregate * aggregate) 
			: current_index(0), p_aggregate(aggregate) { }
	
		string FirstItem()
		{
			 current_index = 0;
			 return (*p_aggregate)[current_index];
		}
	
		string NextItem()
		{
			current_index += 1;

			if (IsDone() == false)
			{
				return (*p_aggregate)[current_index];
			}
			else
			{
				return "";
			}
		}

		string CurrentItem()
		{
			return (*p_aggregate)[current_index];
		}

		bool IsDone()
		{
			if (current_index < p_aggregate->Count())
			{
				return false;
			}
			return true;
		}

};

class MyAggregate : public IAggregate
{
		vector<string> values;

	public:

		MyAggregate(void) { }

		// Helper function to populate the collection
		void AddValue(string value)
		{
			values.push_back(value);
		}

		IIterator * GetIterator()
		{
			IIterator *iter = new MyIterator(this); 
			return iter;
		}

		string & operator[](int itemIndex)
		{
			return values[itemIndex];
		}	

		int Count()
		{
			return values.size();
		}
};

int main(int argc, char * argv[])
{
	MyAggregate aggr;

    aggr.AddValue("1");
    aggr.AddValue("2");
    aggr.AddValue("3");
    aggr.AddValue("4");
    aggr.AddValue("5");
    aggr.AddValue("6");
    aggr.AddValue("7");
    aggr.AddValue("8");
    aggr.AddValue("9");
    aggr.AddValue("Bob");

    IIterator *iter = aggr.GetIterator();

	for (string s = iter->FirstItem(); iter->IsDone() == false;  s = iter->NextItem())
    {
		cout << s << endl;
    }

	cin.get();

	return 0;
}