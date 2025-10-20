# Abstract Factory Pattern

Abstract Factory Pattern, birbirleriyle iliþkili veya baðýmlý product family’lerini oluþturmak için kullanýlan bir creational design pattern’dir.  
Amaç, client’ýn somut class’larý bilmeden, sadece soyut factory interface’leri üzerinden product family’si oluþturmasýný saðlamaktýr.  
Böylece client hangi product family’sini kullanacaðýný seçer, ama ürünlerin nasýl üretildiðini bilmez.  
Bu pattern, factory’lerin factory’si (factory of factories) olarak da düþünülebilir.

## Amaç
- Farklý ama birbiriyle uyumlu product family’lerini oluþturmak.  
- Client’ý somut class baðýmlýlýklarýndan soyutlamak.  
- Yeni product family’leri ekleyerek sistemi geniþletebilmek (Open–Closed Principle).  
- Product family’leri arasýnda tutarlýlýðý (consistency) korumak.

## Program.cs (örnek kullaným)

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

## Kullaným Senaryolarý
- UI tema family’leri (dark/light): buton, tablo, input gibi tutarlý bileþen setleri
- Çoklu platform product setleri (Windows/macOS/Web)
- Oyun dünyalarý: çöl/orman/kar temalý asset family’leri

## Özet
- **Ne saðlar?** Product family’leri için tutarlý ve geniþletilebilir üretim.
- **Client’ýn rolü?** Uygun factory’yi seçer; somut class’lara baðýmlý deðildir.
- **Ne zaman?** Farklý ama iliþkili product setlerini birlikte yönetmek istediðinde.
