# Visitor Pattern

Visitor Pattern, mevcut class yapısını değiştirmeden  
object’ler üzerinde **yeni işlemler (operasyonlar)** tanımlamayı sağlayan behavioral pattern’dir.  

Bu pattern sayesinde “iş mantığı” ile “veri yapısı” birbirinden ayrılır.  
Yeni davranış eklemek için class’ları değiştirmek gerekmez,  
sadece yeni bir **Visitor** eklenir.

## Gerçek Hayat Analojisi

Bir alışveriş sepetinde Book ve Fruit nesnelerin var:

Fiyat hesaplamak,

Vergi uygulamak gibi işlemler yapılacak.

Bu işlemleri nesnelerin içine koymak yerine visitor’larla ayırırız:

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Visitor Pattern");

var items = new IItem[]
{
    new Book { Title = "C# Patterns", Price = 120 },
    new Fruit { Name = "Apple", Weight = 2, PricePerKg = 10 }
};

var visitor = new PriceVisitor();
foreach (var item in items)
    item.Accept(visitor);

Console.ReadLine();

interface IVisitor
{
    void Visit(Book book);
    void Visit(Fruit fruit);
}

interface IItem
{
    void Accept(IVisitor visitor);
}

class Book : IItem
{
    public string Title { get; set; }
    public double Price { get; set; }
    public void Accept(IVisitor visitor) => visitor.Visit(this);
}

class Fruit : IItem
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double PricePerKg { get; set; }
    public void Accept(IVisitor visitor) => visitor.Visit(this);
}

class PriceVisitor : IVisitor
{
    public void Visit(Book book)
        => Console.WriteLine($"{book.Title} book costs {book.Price} ₺");

    public void Visit(Fruit fruit)
        => Console.WriteLine($"{fruit.Name} costs {fruit.Weight * fruit.PricePerKg} ₺");
}


```

## Önemli Bilgi

Visitor Pattern şu durumlarda kullanılır:
- Nesne yapısı sabit, ancak bu yapı üzerinde yapılacak işlemler değişkense,  
- Class’ları değiştirmeden yeni davranışlar eklemek istiyorsan,  
- Farklı türde object’lere göre özel davranışlar tanımlamak gerekiyorsa.  

✅ Yeni Visitor eklemek kolaydır (Open/Closed Principle’a uygundur).  
✅ Veri yapısını değiştirmeden yeni işlemler eklenebilir.  
⚠️ Ancak yeni element türü eklersen, tüm Visitor’lar güncellenmelidir.

Bu pattern özellikle:
- **Document export** sistemlerinde  
- **Syntax tree işlemlerinde (compiler/interpreter)**  
- **Raporlama ve analiz sistemlerinde** sıkça kullanılır.  
