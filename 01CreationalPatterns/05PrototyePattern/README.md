# Prototype Pattern

Prototype Pattern, mevcut bir object�in kopyalanarak yeni bir object olu�turulmas�n� sa�layan bir creational pattern�dir.  
Yeni bir instance olu�turmak yerine, var olan bir object�in klonlanmas� performans a��s�ndan avantaj sa�lar ve karma��k initialization s�re�lerinden ka��n�l�r.  

## Ama�

- Karma��k veya maliyetli object�leri s�f�rdan �retmek yerine klonlamak.  
- Object�in kopyalanma s�recini kontrol alt�na almak.  
- Yeni bir class hiyerar�isi olu�turmadan mevcut object�leri yeniden kullanmak.  

## Program.cs (�rnek kullan�m)

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

## Ger�ek Hayatta Kullan�m�

- Veritaban�ndan al�nan prototip object�lerin haf�zada klonlanmas�.  
- Oyunlarda ayn� karakter veya nesnelerin kopyalanmas�.  
- Grafik uygulamalar�nda �ekil veya bile�enlerin �o�alt�lmas�.  
- Karma��k konfig�rasyonlara sahip object�lerin �o�alt�lmas�.
