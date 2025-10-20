# Prototype Pattern

Prototype Pattern, mevcut bir object’in kopyalanarak yeni bir object oluþturulmasýný saðlayan bir creational pattern’dir.  
Yeni bir instance oluþturmak yerine, var olan bir object’in klonlanmasý performans açýsýndan avantaj saðlar ve karmaþýk initialization süreçlerinden kaçýnýlýr.  

## Amaç

- Karmaþýk veya maliyetli object’leri sýfýrdan üretmek yerine klonlamak.  
- Object’in kopyalanma sürecini kontrol altýna almak.  
- Yeni bir class hiyerarþisi oluþturmadan mevcut object’leri yeniden kullanmak.  

## Program.cs (örnek kullaným)

```csharp
Console.WriteLine("Prototype Pattern...");

Product product = new()
{
    Name = "Bilgisayar",
    Stock = 10,
    Price = 150000
};

Product product2 = (Product)product.Clone();
product2.Price = 250000;

Console.WriteLine("Product1: {0}", product);
Console.WriteLine("Product2: {0}", product2);

Console.ReadLine();

class Product : ICloneable
{
    public Product()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Stock { get; set; }
    public decimal Price { get; set; }

    public object Clone()
    {
        return new Product()
        {
            Name = Name,
            Stock = Stock,
            Price = Price
        };
    }

    public override string ToString()
    {
        return $"Name: {Name}, Stock: {Stock}, Price: {Price}";
    }
}
```

## Gerçek Hayatta Kullanýmý

- Veritabanýndan alýnan prototip object’lerin hafýzada klonlanmasý.  
- Oyunlarda ayný karakter veya nesnelerin kopyalanmasý.  
- Grafik uygulamalarýnda þekil veya bileþenlerin çoðaltýlmasý.  
- Karmaþýk konfigürasyonlara sahip object’lerin çoðaltýlmasý.
