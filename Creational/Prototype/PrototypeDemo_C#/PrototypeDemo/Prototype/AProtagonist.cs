// Prototype Design Pattern - Creational Category

// Lets say we have a protagonist in the game. 
// The protagonist has some statistics to define his playing variables. 
// Whenever we need to save the game the clone of this object will be 
// taken and serialized on the disk (only for this example, this is 
// seldom the case in real games).

// So to get a copy of this Protagonist object the Protagonist object 
// is defined in such a way that cloning it should be possible. 
// So the following abstract class defines the Prototype object.

// This interface defines all the vital information required for 
// the player and a clone method so that the object can be cloned. 

namespace PrototypeDemo
{
    public abstract class AProtagonist
    {
        int m_health;
        int m_felony;
        double m_money;

        public int Health
        {
            get { return m_health; }
            set { m_health = value; }
        }
       
        public int Felony
        {
            get { return m_felony; }
            set { m_felony = value; }
        }

        public double Money
        {
            get { return m_money; }
            set { m_money = value; }
        }

        public abstract AProtagonist Clone();
    }
}