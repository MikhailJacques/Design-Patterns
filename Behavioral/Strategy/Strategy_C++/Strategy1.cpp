// Strategy Design Pattern - Behavioral Category

// Strategy Design Pattern defines a family of algorithms, encapsulates each one, and makes them interchangeable. 
// Strategy lets the algorithm vary independently from clients who use it.

// Motivation
// There are common situations where classes differ only in their behaviour. 
// For those cases it is a good idea to isolate the algorithms in separate classes 
// in order to have the ability to select different algorithms at runtime. 

// Intent
// Define a family of algorithms, encapsulate each one, and make them interchangeable. 
// Strategy lets the algorithm vary independently from the clients that use it.


// The following application simulates basic robots interaction. 
// For the beginning a simple application is created to simulate an arena where robots are interacting.

// It has the following classes:

// IBehaviour (Strategy) - an interface that defines the behavior of a robot.

// Concrete Strategies: AggressiveBehaviour, DefensiveBehaviour, NormalBehaviour.
// Each of them defines a specific behavior. In order to decide the action this class needs information 
// that is passed from robot sensors like position, close obstacles, etc.

// Robot - The robot is the context class. 
// It keeps or gets context information such as position, close obstacles, etc. 
// and passes necessary information to the Strategy class.

// In the main section of the application three robots are created and several different behaviors are created. 
// Each robot has a different behavior assigned: 
// 'Tom' is an aggressive one and attacks any other robot found.
// 'Jerry' is really scared and runs away in the opposite direction when it encounters another robot.
// 'Bob' is pretty calm and ignores any other robot. At some point the behaviors are changed for each robot.

#include <string>
#include <iostream>

using namespace std;

class IBehaviour
{
    public:

        virtual int moveCommand() const = 0;
};

class AgressiveBehaviour : public IBehaviour
{
    public:

        virtual int moveCommand() const
        {
            cout << "Agressive Behaviour: Upon encountering another robot attack it." << endl;
			return 1;
        }
};

class DefensiveBehaviour : public IBehaviour
{
    public:

        virtual int moveCommand() const
        {
            cout << "Defensive Behaviour: Upon encountering another robot run from it." << endl;
			return -1;
        }
};
 
class NormalBehaviour : public IBehaviour
{
    public:

        virtual int moveCommand() const
        {
            cout << "Normal Behaviour: Upon encountering another robot ignore it." << endl;
			return 0;
        }
};


class Robot 
{
	private:

		string name;
		IBehaviour * behaviour;
	
	public: 

		Robot(string name)
		{
			this->name = name;
		}

		void setBehaviour(IBehaviour * behaviour)
		{
			this->behaviour = behaviour;
		}

		IBehaviour * getBehaviour()
		{
			return behaviour;
		}

		void move()
		{
			cout << this->name + ": Based on the current behaviour decide upon the next move: " << endl;
			int command = behaviour->moveCommand();
			
			// ... send the command to mechanisms
			cout << "The returned behaviour id is sent to the movement mechanism for robot " 
				<< this->name << ".\n" << endl;
		}

		string getName() 
		{
			return name;
		}

		void setName(string name) 
		{
			this->name = name;
		}
};


int main(int argc, char *argv[])
{
	cout << "Strategy Design Pattern\n" << endl;

	Robot r1("Tom"), r2("Jerry"), r3("Bob");
	
	r1.setBehaviour(new AgressiveBehaviour());
	r2.setBehaviour(new DefensiveBehaviour());
	r3.setBehaviour(new NormalBehaviour());

	r1.move();
	r2.move();
	r3.move();

	cout << "Original behaviours: \n\nTom gets really scared."
		<< "\nJerry becomes really violent because it is always attacked by other robots." 
		<< "\nBob keeps calm and does care about his surroundings.\n" << endl;

	r1.setBehaviour(new DefensiveBehaviour());
	r2.setBehaviour(new AgressiveBehaviour());

	cout << "New behaviours: \n\nTom is sick of getting scared and becomes really violent now."
		<< "\nJerry on the other hand starts being a pussy and runs away from others." 
		<< "\nBob acts as usuall and does not give a shit this way or the other.\n" << endl;

	r1.move();
	r2.move();
	r3.move();

	cin.get();
	// system("pause");
}