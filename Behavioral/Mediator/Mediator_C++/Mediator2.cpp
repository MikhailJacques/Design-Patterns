// Mediator Design Pattern - Behavioral Category

#include <map>
#include <list>
#include <vector>
#include <string>
#include <iostream>

using namespace std;

template <typename T>
class Mediator
{
		typedef void (*Action)(T);

	private:

		multimap<string, list<Action> > actions;

	public:

		Mediator(void) { }
		~Mediator(void) { }
    
		void Register(string message, Action action)
		{
			multimap<string, list<Action>>::iterator it = actions.find(message);

			if (it != actions.end())
			{
				it->second.push_back(action);
			}
			else
			{
				list<Action> list;
				list.push_back(action);
				actions.insert(pair<string, list<Action>>(message, list));
			}
		}

		void UnRegister(string message, Action action)
		{
			multimap<string, list<Action>>::iterator it = actions.find(message);

			if (it != actions.end())
			{
				it->second.remove(action);
			}
		}

		void Send(string message, T param)
		{
			multimap<string, list<Action>>::iterator it = actions.find(message);

			if (it != actions.end())
			{
				list<Action>::iterator list_it = it->second.begin();

				while (list_it != it->second.end())
				{
					(*list_it)(param);
					list_it++;
				}
			}
		}
};


class Client
{
	private:

		int ID;
		Mediator<string> mediator;

	public:

		Client(void);
		Client(int ID, Mediator<string> mediator);
		~Client(void);

		static void Notify(string message);    
		void SendMessages();
};

Client::Client(void) { }

Client::Client(int ID, Mediator<string> mediator)
{
	this->ID = ID;
    this->mediator = mediator;
}

Client::~Client(void) { }

void Client::Notify(string message)
{
    cout << "[Client]" << '\t' << message << endl;
}

void Client::SendMessages()
{
    mediator.Send("1", "message 1 from Client");
    mediator.Send("2", "message 2 from Client");
}


int main(int argc, char * argv[])
{
    Mediator<string> mediator;

    mediator.Register("1", Client::Notify);
    mediator.Register("1", Client::Notify);
    mediator.Register("1", Client::Notify);
    mediator.Register("1", Client::Notify);
    mediator.Register("2", Client::Notify);
    mediator.Register("2", Client::Notify);
    mediator.Register("3", Client::Notify);

    Client fc(1, mediator);
    Client sc(2, mediator);
    Client tc(3, mediator);
    Client foc(4, mediator);

    fc.SendMessages();
    sc.SendMessages();
    tc.SendMessages();
    foc.SendMessages();

	cin.get(); 

    return 0;
}