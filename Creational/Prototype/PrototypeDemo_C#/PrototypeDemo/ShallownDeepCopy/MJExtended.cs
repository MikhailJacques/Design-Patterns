// Prototype Design Pattern - Creational Category

// If the the Concrete class still performs a clone operation using 
// Memberwise copy function the problem with this is that after copy 
// both the versions are still pointing to the same object of the AdditionalDetails.

// To avoid this what we create a copy of the internal reference type on 
// the heap and then assign that new object with the copy being returned.

// Note: Care must be taken to perform a deep copy because the reference type could 
// still contain reference types internally. The ideal way to do a deep copy is to 
// use reflections and keep copying recursively until the primitive types are reached.

// So having a shallow copy and deep copy in our object is totally the decision based 
// on the functionality required but we have seen both the ways of doing the copy.

// Object.MemberwiseClone Method:
// http://msdn.microsoft.com/en-us/library/system.object.memberwiseclone.aspx

namespace PrototypeDemo
{
    class MJExtended : AProtagonistExtended
    {
        public override AProtagonistExtended Clone()
        {
            // return ShallowCopy();
            return DeepCopy();
        }

        private AProtagonistExtended ShallowCopy()
        {
            return this.MemberwiseClone() as AProtagonistExtended;
        }

        private AProtagonistExtended DeepCopy()
        {
            MJExtended cloned = this.MemberwiseClone() as MJExtended;
            cloned.Details = new AdditionalDetails();
            cloned.Details.Charisma = this.Details.Charisma;
            cloned.Details.Fitness = this.Details.Fitness;

            return cloned as AProtagonistExtended;
        }
    }
}
