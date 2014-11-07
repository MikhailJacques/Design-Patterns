// Prototype Design Pattern - Creational Category

// The extended version of our abstract class contains 
// a member variable for the AdditionalDetails object.

namespace PrototypeDemo
{
    public abstract class AProtagonistExtended
    {
        int m_health;
        int m_felony;
        double m_money;

        // This is a reference type now
        AdditionalDetails m_details = new AdditionalDetails();

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

        public AdditionalDetails Details
        {
            get { return m_details; }
            set { m_details = value; }
        }

        public abstract AProtagonistExtended Clone();
    }
}