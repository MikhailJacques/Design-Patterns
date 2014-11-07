// Prototype Design Pattern - Creational Category

namespace PrototypeDemo
{
    public class AdditionalDetails
    {
        int m_charisma;
        int m_fitness;

        public int Charisma
        {
            get { return m_charisma; }
            set { m_charisma = value; }
        }
        
        public int Fitness
        {
            get { return m_fitness; }
            set { m_fitness = value; }
        }
    }
}