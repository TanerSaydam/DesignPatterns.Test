# Prototype Pattern

Prototype Pattern, mevcut bir object’in kopyalanarak yeni bir object oluşturulmasını sağlayan bir creational pattern’dir.  
Yeni bir instance oluşturmak yerine, var olan bir object’in klonlanması performans açısından avantaj sağlar ve karmaşık initialization süreçlerinden kaçınılır.  

## Amaç

- Karmaşık veya maliyetli object’leri sıfırdan üretmek yerine klonlamak.  
- Object’in kopyalanma sürecini kontrol altına almak.  
- Yeni bir class hiyerarşisi oluşturmadan mevcut object’leri yeniden kullanmak.  

## Program.cs (örnek kullanım)

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

## Gerçek Hayatta Kullanımı

- Veritabanından alınan prototip object’lerin hafızada klonlanması.  
- Oyunlarda aynı karakter veya nesnelerin kopyalanması.  
- Grafik uygulamalarında şekil veya bileşenlerin çoğaltılması.  
- Karmaşık konfigürasyonlara sahip object’lerin çoğaltılması.
