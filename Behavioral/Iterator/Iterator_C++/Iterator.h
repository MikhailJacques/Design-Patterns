//************************************************************************/
//* Iterator.h                                                           */
//************************************************************************/

// http://en.wikibooks.org/wiki/C%2B%2B_Programming/Code/Design_Patterns#Iterator

#ifndef MY_ITERATOR_HEADER
#define MY_ITERATOR_HEADER
 
#include <set>
#include <vector>
#include <iterator>

using namespace std;
 
template<class T, class U>
class Iterator
{
	public:

		typedef typename vector<T>::iterator iter_type;

		Iterator(U *pData) : m_pData(pData)
		{
			m_it = m_pData->m_data.begin();
		}
 
		void first()
		{
			m_it = m_pData->m_data.begin();
		}
 
		void next()
		{
			m_it++;
		}
 
		bool isDone()
		{
			return (m_it == m_pData->m_data.end());
		}
 
		iter_type current()
		{
			return m_it;
		}

	private:

		U *m_pData;
		iter_type m_it;
};


template<class T, class U, class A>
class SetIterator
{
	public:

		typedef typename set<T, U>::iterator iter_type;
 
		SetIterator(A *pData) : m_pData(pData)
		{
			m_it = m_pData->m_data.begin();
		}
 
		void first()
		{
			m_it = m_pData->m_data.begin();
		}
 
		void next()
		{
			m_it++;
		}
 
		bool isDone()
		{
			return (m_it == m_pData->m_data.end());
		}
 
		iter_type current()
		{
			return m_it;
		}
 
	private:

		A			*m_pData;		
		iter_type		m_it;
};

#endif