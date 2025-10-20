# Abstract Factory Pattern

Abstract Factory Pattern, birbirleriyle ilişkili veya bağımlı product family’lerini oluşturmak için kullanılan bir creational design pattern’dir.  
Amaç, client’ın somut class’ları bilmeden, sadece soyut factory interface’leri üzerinden product family’si oluşturmasını sağlamaktır.  
Böylece client hangi product family’sini kullanacağını seçer, ama ürünlerin nasıl üretildiğini bilmez.  
Bu pattern, factory’lerin factory’si (factory of factories) olarak da düşünülebilir.

## Amaç
- Farklı ama birbiriyle uyumlu product family’lerini oluşturmak.  
- Client’ı somut class bağımlılıklarından soyutlamak.  
- Yeni product family’leri ekleyerek sistemi genişletebilmek (Open–Closed Principle).  
- Product family’leri arasında tutarlılığı (consistency) korumak.

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Abstract Factory Pattern...");

IFurnitureFactory classicFurniture = FurnitureFactoryProvider.Create(FurnitureTypeEnum.Classic);
IFurnitureFactory modernFurniture = FurnitureFactoryProvider.Create(FurnitureTypeEnum.Modern);

IChair classicChair1 = classicFurniture.CreateChair();
IChair classicChair2 = classicFurniture.CreateChair();
ITable modernTable = classicFurniture.CreateTable();

Console.ReadLine();

interface IChair
{
    void SitOn();
}

interface ITable
{
    void Use();
}
class ClassicChair : IChair
{
    public void SitOn()
    {
        Console.WriteLine("Sitting on a classic chair");
    }
}
class ModernChair : IChair
{
    public void SitOn()
    {
        Console.WriteLine("Sitting on a modern chair");
    }
}

class ClassicTable : ITable
{
    public void Use()
    {
        Console.WriteLine("Using a classic table");
    }
}

class ModernTable : ITable
{
    public void Use()
    {
        Console.WriteLine("Using a modern table");
    }
}

interface IFurnitureFactory
{
    IChair CreateChair();
    ITable CreateTable();
}

class FurnitureFactoryProvider
{
    private FurnitureFactoryProvider() { }
    public static IFurnitureFactory Create(FurnitureTypeEnum furnitureType)
    {
        switch (furnitureType)
        {
            case FurnitureTypeEnum.Classic:
                return new ClassicFurnitureFactory();
            case FurnitureTypeEnum.Modern:
                return new ModernFurnitureFactory();
            default:
                throw new ArgumentException("Invalid furniture type!");
        }
    }
}

enum FurnitureTypeEnum
{
    Classic,
    Modern
}

class ClassicFurnitureFactory : IFurnitureFactory
{
    public IChair CreateChair()
    {
        return new ClassicChair();
    }

    public ITable CreateTable()
    {
        return new ClassicTable();
    }
}

class ModernFurnitureFactory : IFurnitureFactory
{
    public IChair CreateChair()
    {
        return new ModernChair();
    }

    public ITable CreateTable()
    {
        return new ModernTable();
    }
}
```

## Kullanım Senaryoları
- UI tema family’leri (dark/light): buton, tablo, input gibi tutarlı bileşen setleri
- Çoklu platform product setleri (Windows/macOS/Web)
- Oyun dünyaları: çöl/orman/kar temalı asset family’leri