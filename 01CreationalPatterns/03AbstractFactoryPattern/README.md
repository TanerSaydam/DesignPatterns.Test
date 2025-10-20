# Abstract Factory Pattern

Abstract Factory Pattern, birbirleriyle ili�kili veya ba��ml� product family�lerini olu�turmak i�in kullan�lan bir creational design pattern�dir.  
Ama�, client��n somut class�lar� bilmeden, sadece soyut factory interface�leri �zerinden product family�si olu�turmas�n� sa�lamakt�r.  
B�ylece client hangi product family�sini kullanaca��n� se�er, ama �r�nlerin nas�l �retildi�ini bilmez.  
Bu pattern, factory�lerin factory�si (factory of factories) olarak da d���n�lebilir.

## Ama�
- Farkl� ama birbiriyle uyumlu product family�lerini olu�turmak.  
- Client�� somut class ba��ml�l�klar�ndan soyutlamak.  
- Yeni product family�leri ekleyerek sistemi geni�letebilmek (Open�Closed Principle).  
- Product family�leri aras�nda tutarl�l��� (consistency) korumak.

## Program.cs (�rnek kullan�m)

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

## Kullan�m Senaryolar�
- UI tema family�leri (dark/light): buton, tablo, input gibi tutarl� bile�en setleri
- �oklu platform product setleri (Windows/macOS/Web)
- Oyun d�nyalar�: ��l/orman/kar temal� asset family�leri

## �zet
- **Ne sa�lar?** Product family�leri i�in tutarl� ve geni�letilebilir �retim.
- **Client��n rol�?** Uygun factory�yi se�er; somut class�lara ba��ml� de�ildir.
- **Ne zaman?** Farkl� ama ili�kili product setlerini birlikte y�netmek istedi�inde.
