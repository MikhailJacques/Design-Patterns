//// Flyweight Design Pattern - Structural Category

//// What is Flyweight Pattern?
//// According to the GOF the "Flyweight pattern is used when one instance of a class can be used to provide many virtual instances."

//// When to Use?
//// Flyweight is used when there is a need to create a large number of objects of almost similar nature. 
//// The large number of objects consume a large amount of memory and the Flyweight design pattern provides 
//// a solution for reducing the load on memory by sharing objects.

//// For instance we can use the example of a popular video game Mario. It has the two characters, Mario and Luigi. 
//// Both have the same abilities and characteristics, the only difference is their color and name.

//// Take an example of an adventure game where we may see a huge number (maybe millions) of characters and 
//// most of them are the same, having a slight difference in some characteristics (many times all are 100% same).

//// Creating a new instance for every character may impact performance significantly.

//// Flyweight Pattern as solution

//// In the Flyweight Pattern:
//// - An Object is created for one character.
//// - The Character is rendered using the render method.
//// - The same Object is reused if another similar character is required.

//// Now when we talk about objects, they have states which may either be an intrinsic state or an extrinsic state, as in:
//// - Intrinsic states are things that are constant such as what a type of weapon character has 
////   (All Gun Soldiers will have a Gun as a weapon, a Sword solider will have a sword as a weapon etc.)
//// - Whereas Extrinsic states are things that are not constant and need to be calculated on the fly like color 
////   (some characters equipped with a gun may be in red color and some in blue).

//// The big question is how to work with an extrinsic state if we are going to share the same object. 
//// We can understand this once we look at the sample.

//// Components involved in the Flyweight pattern are:
////
//// Abstract Flyweight
//// ConcreteFlyweight
//// FlyweightFactory

//// http://www.c-sharpcorner.com/UploadFile/SukeshMarla/learn-design-pattern-flyweight-pattern/

//using System;
//using System.Collections.Generic;

//// Create an Abstract Flyweight class Soldier as:
//public abstract class Soldier
//{
//    public string Weapon { get; set; }
//    public abstract void RenderSoldier(string StrPriName, string Color);
//}

//// Create a Concrete Flyweight classes GunFighter and Swordsman as:
//public class GunFighter : Soldier
//{
//    public GunFighter()
//    {
//        this.Weapon = "Gun";
//    }

//    public override void RenderSoldier(string StrPriName, string Color)
//    {
//        Console.WriteLine("Character " + StrPriName + " Rendered with " + Weapon + " in " + Color + " Color");
//    }
//}

//public class Swordsman : Soldier
//{
//    public Swordsman()
//    {
//        this.Weapon = "Sword";
//    }

//    public override void RenderSoldier(string StrPriName, string Color)
//    {
//        Console.WriteLine("Character " + StrPriName + " Rendered with " + Weapon + " in " + Color + " Color");
//    }
//}

//public class Fistfighter : Soldier
//{
//    public Fistfighter()
//    {
//        this.Weapon = "Fist";
//    }

//    public override void RenderSoldier(string StrPriName, string Color)
//    {
//        Console.WriteLine("Character " + StrPriName + " Rendered with " + Weapon + " in " + Color + " Color");
//    }
//}

//// Create a Flyweight Factory SoldierFactory
//public class SoldierFactory
//{
//    Dictionary<string, Soldier> SoldierCollection;

//    public SoldierFactory()
//    {
//        SoldierCollection = new Dictionary<string, Soldier>();
//    }

//    // This class holds the references of already created flyweight objects.
//    // When the GetFlyweight method is called from client code, these references are checked to determine 
//    // if an appropriate flyweight object is already present or not. If present, it is returned. 
//    // Otherwise a new object is generated, added to the collection and returned.
//    public Soldier GetSoldier(string SoldierIndex)
//    {
//        if (!SoldierCollection.ContainsKey(SoldierIndex))
//        {
//            Console.Write("Object created: ");

//            switch (SoldierIndex)
//            {
//                case "0": SoldierCollection.Add(SoldierIndex, new GunFighter()); break;
//                case "1": SoldierCollection.Add(SoldierIndex, new Swordsman()); break;
//                case "2": SoldierCollection.Add(SoldierIndex, new Fistfighter()); break;
//            }
//        }
//        else
//        {
//            Console.Write("Object reused: ");
//        }

//        return SoldierCollection[SoldierIndex];
//    }
//}

//// Write the client code where we create just 2 objects, one for GunFighter and another for Sword soldier as:
//class MainApp
//{
//    private static Soldier soldier;
//    private static SoldierFactory factory;

//    // Entry point into console application.
//    static void Main()
//    {
//        SoldierFactory factory = new SoldierFactory();

//        soldier = factory.GetSoldier("0");
//        soldier.RenderSoldier("Bob", "Red");

//        soldier = factory.GetSoldier("0");
//        soldier.RenderSoldier("Bob", "Blue");

//        soldier = factory.GetSoldier("1");
//        soldier.RenderSoldier("Tom", "White");

//        soldier = factory.GetSoldier("1");
//        soldier.RenderSoldier("Jerry", "Black");

//        // Console.WriteLine(soldier);

//        // Wait for user
//        Console.ReadKey();
//    }
//}
