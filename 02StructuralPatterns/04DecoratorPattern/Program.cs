Console.WriteLine("Decorator Pattern...");

IDrink coffee = new Coffee();
Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");

IDrink milkCoffee = new MilkDecorator(coffee);
Console.WriteLine($"{milkCoffee.GetDescription()} - ${milkCoffee.GetCost()}");

IDrink sugarMilkCoffee = new SugarDecorator(milkCoffee);
Console.WriteLine($"{sugarMilkCoffee.GetDescription()} - ${sugarMilkCoffee.GetCost()}");

Console.ReadLine();

interface IDrink
{
    string GetDescription();
    double GetCost();
}

class Coffee : IDrink
{
    public string GetDescription()
    {
        return "Plain Coffe";
    }
    public double GetCost()
    {
        return 2.0;
    }

}

abstract class DrinkDecorator : IDrink
{
    protected readonly IDrink _drink;

    protected DrinkDecorator(IDrink drink)
    {
        _drink = drink;
    }

    public abstract string GetDescription();
    public abstract double GetCost();

}

class MilkDecorator : DrinkDecorator
{
    public MilkDecorator(IDrink drink) : base(drink)
    {

    }
    public override string GetDescription()
    {
        return _drink.GetDescription() + ", Milk";
    }
    public override double GetCost()
    {
        return _drink.GetCost() + 0.5;
    }
}

class SugarDecorator : DrinkDecorator
{
    public SugarDecorator(IDrink drink) : base(drink)
    {
    }
    public override double GetCost()
    {
        return _drink.GetCost() + 0.3;
    }

    public override string GetDescription()
    {
        return _drink.GetDescription() + ", Sugar";
    }
}