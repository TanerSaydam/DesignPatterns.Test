# Flyweight Pattern

Flyweight Pattern, çok sayıda küçük object oluşturmak gerektiğinde bellek kullanımını azaltmak için kullanılır.  
Bu pattern, benzer object’lerin **ortak durumlarını (intrinsic state)** paylaşarak tekrar eden verilerin yalnızca bir kopyasını tutar.  
Böylece bellek tasarrufu sağlanır ve performans artar.

## Amaç

- Ortak (değişmeyen) verileri paylaşarak bellek kullanımını azaltmak.  
- Her object’in kendi verisini kopyalaması yerine paylaşılan bir state kullanmak.  
- Büyük miktarda tekrar eden object’leri verimli bir şekilde yönetmek.

## Gerçek Hayat Analojisi

Bir orman simülasyonu düşün 🌲  
Her ağaç (Tree) sahnede ayrı bir konumda bulunur, ancak birçok ağacın türü, rengi ve dokusu aynıdır.  
Bu ortak özellikleri her ağaç için ayrı ayrı bellekte tutmak büyük bir israf olur.  

Flyweight Pattern sayesinde aynı türdeki ağaçlar ortak bir `TreeType` object’ini paylaşır.  
Sistemde binlerce ağaç olsa bile, sadece birkaç `TreeType` oluşturulur ve her ağaç kendi konum bilgisini (`x`, `y`) ayrı tutar.  
Sonuç olarak bellekten ciddi tasarruf edilir ve performans artar.


## Program.cs (örnek kullanım)

```csharp
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
```
