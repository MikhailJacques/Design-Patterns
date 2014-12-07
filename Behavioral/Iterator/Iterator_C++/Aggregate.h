
//************************************************************************/
//* Aggregate.h                                                          */
//************************************************************************/

// http://en.wikibooks.org/wiki/C%2B%2B_Programming/Code/Design_Patterns#Iterator

#ifndef MY_DATACOLLECTION_HEADER
#define MY_DATACOLLECTION_HEADER

#include "Iterator.h"
 
template <class T>
class Aggregate
{
	friend class Iterator<T, Aggregate>;
	
	public:
	
		void add(T a)
		{
			m_data.push_back(a);
		}
 
		Iterator<T, Aggregate> *create_iterator()
		{
			return new Iterator<T, Aggregate>(this);
		}
 
	private:

		std::vector<T> m_data;
};

template <class T, class U>
class AggregateSet
{
	friend class SetIterator<T, U, AggregateSet>;
	
	public:

		void add(T a)
		{
			m_data.insert(a);
		}
 
		SetIterator<T, U, AggregateSet> *create_iterator()
		{
			return new SetIterator<T, U, AggregateSet>(this);
		}
 
		void Print()
		{
			copy(m_data.begin(), m_data.end(), std::ostream_iterator<T>(std::cout, "\n"));
		}
 
	private:

		std::set<T, U> m_data;
};
 
#endif