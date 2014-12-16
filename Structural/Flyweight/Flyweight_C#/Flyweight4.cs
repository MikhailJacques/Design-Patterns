// Flyweight Design Pattern - Structural Category

// Background
// The flyweight design pattern although not widely used can be very helpful in scenarios where memory is a constraint. 
// In a simple sentence flyweight pattern can be applied in situations where we want to limit the number of object creations. 
// According to the GOF definition- "A flyweight is a shared object that can be used in multiple contexts simultaneously".
// So think of why this pattern is called a flyweight? The word "flyweight-in software" is similar to the word 
// "flyweight- in boxing" in which flyweight refers to a lightweight boxing category. 
// In the same way we have a lightweight object (not the boxer) which can be used at various places in the system simultaneously.

// A flyweight object can be described by 2 states:
// Intrinsic state - is the one which is natural to the flyweight object and CAN be shared. 
// The intrinsic state is independent of flyweight’s context.
// Extrinsic state - is one which varies with flyweight context and therefore it CANNOT be shared.

// To make distinction between intrinsic and extrinsic state, let’s look at a very famous example (also mentioned in GOF book). 
// A word processor has to play with character objects. If we look at a character object in a word processor its state would be 
// its font, its style, its location, its actual content. Looking at just this example we can see that everything like the font, 
// style, location of a character can vary for a character, but the content of a character can be same (at least it would be one 
// among the 26 characters [a-z] forget about the constants for now). 
// Character’s font, its style, its position all are extrinsic state as they vary and cannot be shared. 
// Character’s content (like "a","b", "c", ... "z") can be shared and comprises the intrinsic state. 
// So we can derive that if we have a system where we need to create lot of objects and where each of these object can be 
// described by a common intrinsic state and extrinsic state, we smell of applying flyweight pattern in such a system.

// FlyweightFactory
// As the name says all, since it’s a factory it manages flyweights. When a flyweight object is requested the factory 
// returns the instance if it is present otherwise it creates a new flyweight, adds into the factory cache and returns.

// IFlyweight
// This is the interface which the flyweight implements. The GetExtrinsicState method gets the extrinsic state and 
// IntrinsicState holds the intrinsic state of the flyweight.

// ConcreteFlyweight
// This is the actual flyweight object and is shareable. It stores the intrinsicState and derives the extrinsic state.

// Points of Interest
// Flyweight pattern can be very useful in scenarios where similar kinds of objects are to be created with a slight variance. 
// Flyweight pattern would save us the memory space and would let us write the code which deals with fewer number of objects.

// Using the code
//
// Now consider an example where we would implement the flyweight pattern. Suppose we have to create an animated game where 
// my favorite cartoon character has to collect money falling from the sky. Do you smell flyweight pattern anywhere here? 
// Now since we need to track of how much money is collected by the "Cartoon Character" we will create "objects" of these 
// individual currency denominations, so that we can calculate the total. Since in this animated game we would have lots of 
// money falling from the sky, imagine how much memory an object of "paper currency" would take? An object of currency would 
// typically have graphical pictures of the national heroes, artistic fonts and lot of other stuff which would always be same 
// for these objects. Similarly every denomination of metallic currency would be a somewhat similar object. So instead of 
// creating thousands of objects for these falling money we would create just 2 objects one for the metallic currency and one 
// for the paper currency. Furthermore since various properties of these objects can be easily classified into intrinsic state 
// and extrinsic state, so this would be an ideal scenario where we can apply the flyweight pattern. 
// The listing below shows the intrinsic and extrinsic state of the paper currency:

// Intrinsic state:
// - Country's emblem
// - Graphical border
// - Graphical picture of the national hero
// - Various other fixed text on the back
// - Another graphical image on the back

// Extsinsic state: 
// - The currency note number
// - The currency denomination
// - Year of circulation (if any)

// Note that we should create flyweights of individual denomination currency, 
// but for the simplicity of code we create flyweights of metallic currency and paper currency only.

// http://www.codeproject.com/Articles/793623/Flyweight-Design-Pattern-Csharp

using System;
using System.Collections.Generic;

public enum EnMoneyType
{
    Metallic,
    Paper
}

// The interface "IMoney" corresponds to the IFlyweight interface. 
// The MoneyType would hold the IntrinsicState (metallic money, paper money would be the intrinsic states of the flyweight classes) 
// and the operation GetDisplayOfMoneyFalling would act on the extrinsic state of the flyweight which is the currency denomination.
public interface IMoney
{
    EnMoneyType MoneyType { get; }                  // IntrinsicState()
    void GetDisplayOfMoneyFalling(int money_value);  // GetExtrinsicSate()
}

// This is the concrete flyweight "metallic money" class which would implement the IMoney interface. 
// This is the flyweight which would be used to create different objects (each having different extrinsic state.) 
// The extrinsic state here would be the money_value and the intrinsic state would be the Metallic money.
public class MetallicMoney : IMoney
{
    public EnMoneyType MoneyType
    {
        get { return EnMoneyType.Metallic; }
    }

    public void GetDisplayOfMoneyFalling(int money_value) // GetExtrinsicState()
    {
        // This method would display graphical representation of a metallic currency like a gold coin.
        Console.WriteLine(string.Format("Displaying a graphical object of {0} currency of value ${1} falling from sky.", 
            MoneyType.ToString(), money_value));
    }
}

// This is another concrete flyweight which will be used to create multiple objects. 
// Its intrinsic state is the "Paper Money" and extrinsic state would be the money_value (currency Denomination).
class PaperMoney : IMoney
{
    public EnMoneyType MoneyType
    {
        get { return EnMoneyType.Paper; }
    }

    public void GetDisplayOfMoneyFalling(int money_value) // GetExtrinsicState()
    {
        // This method would display a graphical representation of a paper currency.
        Console.WriteLine(string.Format("Displaying a graphical object of {0} currency of value ${1} falling from sky.", 
            MoneyType.ToString(), money_value));
    }
}

// So this "MoneyFactory" class is our flyweight factory which manages the creations of flyweights and ensures that if the 
// object is already there in the dictionary it returns the instance of that object; otherwise it creates a new object. 
// The above class also keeps track of number of objects created through the ObjectsCount variable.
public class MoneyFactory
{
    public static int ObjectsCount = 0;
    private Dictionary<EnMoneyType, IMoney> money_objects;

    public IMoney GetMoneyToDisplay(EnMoneyType money_type) // Same as GetFlyWeight()
    {
        if (money_objects == null)
            money_objects = new Dictionary<EnMoneyType, IMoney>();

        if (money_objects.ContainsKey(money_type))
            return money_objects[money_type];

        switch (money_type)
        {
            case EnMoneyType.Metallic:
                money_objects.Add(money_type, new MetallicMoney());
                ObjectsCount++;
                break;

            case EnMoneyType.Paper:
                money_objects.Add(money_type, new PaperMoney());
                ObjectsCount++;
                break;

            default:
                break;
        }

        return money_objects[money_type];
    }
}

// The client gets an instance of flyweight and shows the result (graphical result of money falling from the sky - forget 
// the actual implementation for now). Over here we are looping till the count of money dropping from sky is one million. 
// We generate a few random currency denominations to drop random denominations of money from the sky. 
// If the currency denomination is 1 or 5 we are dropping metallic money otherwise we are dropping paper money and 
// that completes our game.
class MainApp
{
    static void Main(string[] args)
    {
        IMoney graphical_money_obj = null;
        int currency_display_value, sum = 0;
        const int ONE_MILLION = 10000; // Assume this is one million
        int[] currency_denominations = new[] { 1, 5, 10, 20, 50, 100 };
        MoneyFactory moneyFactory = new MoneyFactory();

        Random rand = new Random();

        while (sum <= ONE_MILLION)
        {
            currency_display_value = currency_denominations[rand.Next(0, currency_denominations.Length)];

            if (currency_display_value == 1 || currency_display_value == 5)
                graphical_money_obj = moneyFactory.GetMoneyToDisplay(EnMoneyType.Metallic);
            else
                graphical_money_obj = moneyFactory.GetMoneyToDisplay(EnMoneyType.Paper);

            graphical_money_obj.GetDisplayOfMoneyFalling(currency_display_value);

            sum = sum + currency_display_value;
        }

        Console.WriteLine("Total number of objects created is: " + MoneyFactory.ObjectsCount.ToString());

        Console.ReadLine();
    }
}



