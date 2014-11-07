// Prototype Design Pattern - Creational Category

// Implement ICloneable interface for Prototype Pattern
// The ICloneable interface in C# serves the purpose of defining the
// the clone method in the objects. We can use the ICloneable interface 
// as Prototype (from the above class diagram).

using System;

namespace PrototypeDemo
{
    public class MJFinal : ICloneable
    {
        int m_health;
        int m_felony;
        double m_money;
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

        private object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        private object DeepCopy()
        {
            MJFinal cloned = this.MemberwiseClone() as MJFinal;
            cloned.Details = new AdditionalDetails();
            cloned.Details.Charisma = this.Details.Charisma;
            cloned.Details.Fitness = this.Details.Fitness;

            return cloned;
        }

        #region ICloneable Members

        public object Clone()
        {
            return DeepCopy();
        }

        #endregion
    }
}