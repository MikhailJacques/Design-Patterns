// Chain of Responsibility Design Pattern - Behavioral Category

// http://www.coderslexicon.com/chain-of-responsibility-pattern-c/

#include <iostream>
using namespace std;
 
// Abstract class called Handler
// It is abstract because it has a pure virtual function.
// This prevents instances of Handler being created directly.
class Handler 
{
	protected:

		Handler * next;

	public:

		// Constructor
		Handler() { next = NULL; }
 
		// Pure virtual function
		virtual void request(int value) = 0;
 
		// Sets next handler in the chain
		void setNextHandler(Handler * nextInChain) 
		{
			next = nextInChain;
		}
};
 
 
// SpecialHandler is a type of Handler but has a limit and an ID
// It also determines if it can handle the request or needs to send it on
// If it is the last in the chain and can't handle it, it lets the user know.
class SpecialHandler : public Handler 
{
    private:

		int ID;
		int limit;

    public:

        SpecialHandler(int ID, int limit) 
		{
			this->ID = ID;
			this->limit = limit; 
        }

        // Handles incoming request
        void request(int value) 
		{
            if (value < limit) 
			{
                cout << "Handler " << ID << " handled the request with a limit of " << limit << endl;
            }
            else if (next != NULL) 
			{
                // Passes it on to the next handler in the chain of responsibility
                next->request(value);
            }
            else 
			{ 
                // Last in chain, so let the user know it was unhandled.
                cout << "I am the last handler (" << ID << ") and I couldn't handle that request." << endl; 
            }
        }
};
 
int main ()
{
    // Create three special handlers with IDs "1, 2 and 3"
    // Since a SpecialHandler is a type of "Handler" they are legal statements.
    Handler * h1 = new SpecialHandler(1, 10);
    Handler * h2 = new SpecialHandler(2, 20);
    Handler * h3 = new SpecialHandler(3, 30);

	SpecialHandler h4(4, 40);
 
    // Chain up the handlers together
    h1->setNextHandler(h2);
    h2->setNextHandler(h3);
	h3->setNextHandler(&h4);
 
	// Handler 1 handles this request.
	h1->request(5);

    // Handler 1 cannot handle the request so it passes it on to Handler 2 which can handle it since the limit is less than 20.
    h1->request(14);
 
	// Handler 1 cannot handle the request so it passes it on to Handler 2, which also cannot handle it so it passes 
	// the request on to Handler 3, which can handle it since the limit is less than 30.
	h1->request(25);
	
	// Handler 1 cannot handle the request so it passes it on to Handler 2, which also cannot handle it so it passes 
	// the request on to Handler 3, which also cannot handle it so it passes the request on to Handler 4, 
	// which can handle it since the limit is less than 40.
    h1->request(37);

    // No handler can handle this request, so it triggers the last handler's else statement showing that it is the last 
	// and still cannot handle the request.
    h1->request(42);

    // Free memory
    delete h1;
    delete h2;
    delete h3;
   
	cin.get(); 

    return 0;
}