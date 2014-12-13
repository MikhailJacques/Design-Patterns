//// Abstract Factory - Creational Patterns Category

//// Definition:
//// Provide an interface for creating families of related or dependent objects without specifying their concrete classes.

//// Participants:
////
//// AbstractFactory (ContinentFactory)
//// - Declares an interface for operations that create abstract products
////
//// ConcreteFactory (AfricaFactory, AmericaFactory)
//// - Implements the operations to create concrete product objects
////
//// AbstractProduct (Herbivore, Carnivore)
//// - Declares an interface for a type of product object
////
//// Product (Wildebeest, Lion, Bison, Wolf)
//// - Defines a product object to be created by the corresponding concrete factory
//// - Implements the AbstractProduct interface
////
//// Client (AnimalWorld)
//// - Uses interfaces declared by AbstractFactory and AbstractProduct classes

//// Real-world code in C#
//// This real-world code demonstrates the creation of different animal worlds for a computer game using different factories. 
//// Although the animals created by the Continent factories are different, the interactions among the animals remain the same.

//// http://www.dofactory.com/net/abstract-factory-design-pattern

//using System;

//// The 'AbstractFactory' abstract class
//// - Declares an interface for operations that create abstract products
//abstract class ContinentFactory
//{
//    public abstract Herbivore CreateHerbivore();
//    public abstract Carnivore CreateCarnivore();
//}

//// The 'ConcreteFactory1' class
//// - Implements the operations to create concrete product objects
//class AfricaFactory : ContinentFactory
//{
//    public override Herbivore CreateHerbivore()
//    {
//        return new Wildebeest();
//    }
//    public override Carnivore CreateCarnivore()
//    {
//        return new Lion();
//    }
//}

//// The 'ConcreteFactory2' class
//// - Implements the operations to create concrete product objects
//class AmericaFactory : ContinentFactory
//{
//    public override Herbivore CreateHerbivore()
//    {
//        return new Bison();
//    }
//    public override Carnivore CreateCarnivore()
//    {
//        return new Wolf();
//    }
//}

//// The 'AbstractProductA' abstract class
//// - Declares an interface for a type of product object
//abstract class Herbivore
//{
//    public abstract void Eat(String plant);
//}

//// The 'AbstractProductB' abstract class
//// - Declares an interface for a type of product object
//abstract class Carnivore
//{
//    public abstract void Eat(Herbivore h);
//}

//// The 'ProductA1' class
//// - Defines a product object to be created by the corresponding concrete factory
//class Wildebeest : Herbivore
//{
//    public override void Eat(String plant)
//    {
//        // Eat plants
//        Console.WriteLine(this.GetType().Name + " eats " + plant);
//    }
//}

//// The 'ProductB1' class
//// - Defines a product object to be created by the corresponding concrete factory
//class Lion : Carnivore
//{
//    public override void Eat(Herbivore h)
//    {
//        // Eat Wildebeest
//        Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
//    }
//}

//// The 'ProductA2' class
//// - Defines a product object to be created by the corresponding concrete factory
//class Bison : Herbivore
//{
//    public override void Eat(String plant)
//    {
//        // Eat plants
//        Console.WriteLine(this.GetType().Name + " eats " + plant);
//    }
//}

//// The 'ProductB2' class
//// - Defines a product object to be created by the corresponding concrete factory
//class Wolf : Carnivore
//{
//    public override void Eat(Herbivore h)
//    {
//        // Eat Bison
//        Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
//    }
//}

//// The 'Client' class 
//class AnimalWorld
//{
//    private Herbivore herbivore;
//    private Carnivore carnivore;

//    // Constructor
//    public AnimalWorld(ContinentFactory factory)
//    {
//        carnivore = factory.CreateCarnivore();
//        herbivore = factory.CreateHerbivore();
//    }

//    public void RunFoodChain()
//    {
//        carnivore.Eat(herbivore);
//        herbivore.Eat("grass");
//    }
//}

//// MainApp startup class for Real-World Abstract Factory Design Pattern.
//class MainApp
//{
//    private static AnimalWorld fauna;
//    private static ContinentFactory continent;

//    // Entry point into console application.
//    public static void Main()
//    {
//        // Create and run the African animal world
//        continent = new AfricaFactory();
//        fauna = new AnimalWorld(continent);
//        fauna.RunFoodChain();

//        // Create and run the American animal world
//        continent = new AmericaFactory();
//        fauna = new AnimalWorld(continent);
//        fauna.RunFoodChain();

//        // Wait for user input
//        Console.ReadKey();
//    }
//}