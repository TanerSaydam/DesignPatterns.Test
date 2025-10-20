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