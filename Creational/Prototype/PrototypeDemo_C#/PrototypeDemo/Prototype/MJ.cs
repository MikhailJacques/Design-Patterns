// Prototype Design Pattern - Creational Category

// Now lets say we need to spawn a concrete player named MJ in the game. 
// This player should be Cloneable so that whenever we need to save the game we can 
// simply clone the object and intitiate the serialization process asynchronously.

// Now this class is the ConcretePrototype that provides the facility to clone itself. 
// In this example the ConcretePrototype does not contain any specialized members of its own 
// but in some cases this class could contain some member variable or functions of its own.

namespace PrototypeDemo
{
    class MJ : AProtagonist
    {
        public override AProtagonist Clone()
        {
            return this.MemberwiseClone() as AProtagonist;
        }
    }
}