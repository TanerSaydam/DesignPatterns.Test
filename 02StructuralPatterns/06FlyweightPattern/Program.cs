Console.WriteLine("Flyweight Pattern...");

var forest = new Forest();
forest.PlantTree(5, 10, "Oak", "Green", "Rough");
forest.PlantTree(15, 25, "Oak", "Green", "Rough");
forest.PlantTree(7, 14, "Pine", "DarkGreen", "Needle");
forest.PlantTree(30, 40, "Pine", "DarkGreen", "Needle");
forest.PlantTree(50, 60, "Birch", "LightGreen", "Smooth");

for (int i = 0; i < 1000; i++)
{
    forest.PlantTree(i, i + 1, "Oak", "Green", "Rough");
    forest.PlantTree(i, i + 2, "Pine", "DarkGreen", "Needle");
}

forest.Draw();

Console.WriteLine($"\nUnique TreeTypes (flyweights): {TreeFactory.CacheCount}");

Console.ReadLine();

class TreeType
{
    public TreeType(string name, string color, string texture)
    {
        Name = name;
        Color = color;
        Texture = texture;
    }

    public string Name { get; set; }
    public string Color { get; set; }
    public string Texture { get; set; }

    public void Draw(int x, int y)
    {
        Console.WriteLine($"Draw {Name} at ({x},{y}) with Color={Color}, Texture={Texture}");
    }
}

class Tree
{
    private readonly int _x;
    private readonly int _y;
    private readonly TreeType _type;

    public Tree(int x, int y, TreeType type)
    {
        _x = x;
        _y = y;
        _type = type;
    }

    public void Draw() => _type.Draw(_x, _y);
}

static class TreeFactory
{
    private static readonly Dictionary<string, TreeType> _cache = new();
    public static int CacheCount => _cache.Count;
    public static TreeType GetTreeType(string name, string color, string texture)
    {
        string key = $"{name} | {color} | {texture}";
        if (_cache.TryGetValue(key, out var type))
        {
            return type;
        }

        type = new TreeType(name, color, texture);
        _cache[key] = type;
        return type;
    }
}

class Forest
{
    private readonly List<Tree> _trees = new();

    public void PlantTree(int x, int y, string name, string color, string texture)
    {
        var type = TreeFactory.GetTreeType(name, color, texture);
        _trees.Add(new Tree(x, y, type));
    }

    public void Draw()
    {
        foreach (var tree in _trees)
        {
            tree.Draw();
        }
    }
}