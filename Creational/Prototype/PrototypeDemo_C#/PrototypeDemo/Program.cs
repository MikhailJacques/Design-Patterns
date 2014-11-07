// Prototype Design Pattern - Creational Category

// GoF defines prototype pattern as "Specify the kind of objects to create using 
// a prototypical instance, and create new objects by copying this prototype." 

// In Prototype pattern three major players are involved:
// - Prototype: This is an interface or abstract class that defined the method to clone itself.
// - ConcretePrototype: This is the concrete class that will clone itself.
// - Client: The application object that need the cloned copy of the object.

// http://www.codeproject.com/Articles/476807/Understanding-and-Implementing-Prototype-Pattern-i

// The following is a client code.

// Th ebelow code perfectly illustrates the prototype pattern in action. 
// There is one problem though. We are using the method MemberwiseCopy in our implementation. 
// The problem with the memberwise copy is that it creates a shallow copy of the object i.e. 
// if the object contains any reference types then only the address of that reference type will 
// be copied from source to target and both the versions will keep pointing to the same object.

// To illustrate this point lets have a class called AdditionalDetails containing more information about the Protagonist.

using System;

namespace PrototypeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code to demonstrate the classic Prototype Pattern
            MJ player = new MJ();
            player.Health = 1;
            player.Felony = 10;
            player.Money = 2.0;

            Console.WriteLine("Original Player stats:");
            Console.WriteLine("Health: {0}, Felony: {1}, Money: {2}", 
                player.Health.ToString(), 
                player.Felony.ToString(), 
                player.Money.ToString());

            // We enter the cheat code here and we have a new 
            // player with his health fully restored.
            MJ playerToSave = player.Clone() as MJ;            

            Console.WriteLine("\nCopy of player to save on disk:");
            Console.WriteLine("Health: {0}, Felony: {1}, Money: {2}", 
                playerToSave.Health.ToString(), 
                playerToSave.Felony.ToString(), 
                playerToSave.Money.ToString());

            PerformShallowCopy();
            
            PerformDeepCopy();

            ICloneableVersionCopy();

            // Wait for user
            Console.ReadKey();

        }

        private static void PerformShallowCopy()
        {
            // The code to demonstrate the shallow copy
            MJExtended playerEx = new MJExtended();
            playerEx.Health = 1;
            playerEx.Felony = 10;
            playerEx.Money = 2.0;
            playerEx.Details.Fitness = 5;
            playerEx.Details.Charisma = 5;

            // Lets clone the above object and change the 
            // proprties of contained object
            MJExtended shallowClonedPlayer = playerEx.Clone() as MJExtended;
            shallowClonedPlayer.Details.Charisma = 10;
            shallowClonedPlayer.Details.Fitness = 10;

            // Lets see what happened to the original object
            Console.WriteLine("\nOriginal Object:");
            Console.WriteLine("Charisma: {0}, Fitness: {1}",
                playerEx.Details.Charisma.ToString(),
                playerEx.Details.Fitness.ToString());
            Console.WriteLine("\nShallow Cloned Object:");
            Console.WriteLine("Charisma: {0}, Fitness: {1}",
                shallowClonedPlayer.Details.Charisma.ToString(),
                shallowClonedPlayer.Details.Fitness.ToString());
        }

        private static void PerformDeepCopy()
        {
            // Let us see how we can perform the deep copy now
            MJExtended playerEx2 = new MJExtended();
            playerEx2.Health = 1;
            playerEx2.Felony = 10;
            playerEx2.Money = 2.0;
            playerEx2.Details.Fitness = 5;
            playerEx2.Details.Charisma = 5;

            // lets clone the object but this time perform a deep copy
            MJExtended shallowClonedPlayer2 = playerEx2.Clone() as MJExtended;
            shallowClonedPlayer2.Details.Charisma = 10;
            shallowClonedPlayer2.Details.Fitness = 10;

            // Lets see what happened to the original object
            Console.WriteLine("\nOriginal Object:");
            Console.WriteLine("Charisma: {0}, Fitness: {1}",
                playerEx2.Details.Charisma.ToString(),
                playerEx2.Details.Fitness.ToString());
            Console.WriteLine("\nDeep Cloned Object:");
            Console.WriteLine("Charisma: {0}, Fitness: {1}",
                shallowClonedPlayer2.Details.Charisma.ToString(),
                shallowClonedPlayer2.Details.Fitness.ToString());
        }

        private static void ICloneableVersionCopy()
        {
            // Let us see how we can perform the deep copy now
            MJFinal player = new MJFinal();
            player.Health = 1;
            player.Felony = 10;
            player.Money = 2.0;
            player.Details.Fitness = 5;
            player.Details.Charisma = 5;

            // lets clone the object but this time perform a deep copy
            MJFinal clonedPlayer = player.Clone() as MJFinal;
            clonedPlayer.Details.Charisma = 10;
            clonedPlayer.Details.Fitness = 10;

            // Lets see what happened to the original object
            Console.WriteLine("\nOriginal Object:");
            Console.WriteLine("Charisma: {0}, Fitness: {1}",
                player.Details.Charisma.ToString(),
                player.Details.Fitness.ToString());
            Console.WriteLine("\nICloneable Deep Cloned Object:");
            Console.WriteLine("Charisma: {0}, Fitness: {1}",
                clonedPlayer.Details.Charisma.ToString(),
                clonedPlayer.Details.Fitness.ToString());
        }
    }
}