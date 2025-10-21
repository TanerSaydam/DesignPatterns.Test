# Decorator Pattern

Decorator Pattern, bir object’in class’ını değiştirmeden ona yeni davranışlar veya özellikler eklemeyi sağlar.  
Bu pattern, miras (inheritance) yerine bileşim (composition) kullanarak dinamik genişletme imkânı sunar.  
Bir object başka bir object tarafından “sarmalanarak” (wrapped) yeni işlevler kazanır.

## Amaç

- Object’e yeni özellikler eklerken temel class’ı değiştirmemek.  
- Farklı özellikleri dinamik olarak birleştirebilmek.  
- Kodun esnekliğini ve yeniden kullanılabilirliğini artırmak.  

## Gerçek Hayat Analojisi

Bir kahve dükkanında temel içecek olarak sade bir kahve düşün ☕  
Kahveye süt, şeker veya krema eklemek istiyorsan, her kombinasyon için yeni bir class oluşturmak yerine,  
Decorator Pattern kullanarak bu ekstraları dinamik olarak ekleyebilirsin.  

Her eklenti (milk, sugar, cream) bir “decorator” gibi davranır ve temel kahveyi sarmalayarak yeni özellikler kazandırır.  
Sonuçta içeceklerin farklı kombinasyonlarını, aynı temel object üzerinden kolayca oluşturabilirsin.


## Program.cs (örnek kullanım)

```csharp
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
```

## Gerçek Hayatta Kullanımı

- Kahve örneğinde olduğu gibi içeceklere dinamik olarak malzeme (süt, şeker, krema) eklemek.  
- Logger sistemlerinde log mesajına timestamp veya renk eklemek.  
- Stream API’lerinde (ör. `BufferedStream`, `GZipStream`) veri akışına özellik eklemek.  
- UI component’lerinde bir elementin etrafına border, shadow veya stil sarmalayıcıları eklemek.
