Console.WriteLine("Composite Pattern – Menu Example\n");

var menu = new MenuGroup("Main Menu");

var products = new MenuGroup("Products");
var electronics = new MenuGroup("Electronics");
electronics.Add(new MenuItem("Phones"));
electronics.Add(new MenuItem("Laptops"));

var furniture = new MenuGroup("Furniture");
furniture.Add(new MenuItem("Tables"));
furniture.Add(new MenuItem("Chairs"));

products.Add(electronics);
products.Add(furniture);

menu.Add(new MenuItem("Home"));
menu.Add(products);
menu.Add(new MenuItem("About"));
menu.Add(new MenuItem("Contact"));

menu.Display(0);
Console.ReadLine();


interface IMenuComponent
{
    string Name { get; }
    void Display(int depth);
}

class MenuItem : IMenuComponent
{
    public string Name { get; }

    public MenuItem(string name) => Name = name;

    public void Display(int depth)
        => Console.WriteLine(new string('-', depth) + Name);
}

class MenuGroup : IMenuComponent
{
    public string Name { get; }
    private readonly List<IMenuComponent> _children = new();

    public MenuGroup(string name) => Name = name;

    public void Add(IMenuComponent item) => _children.Add(item);
    public void Remove(IMenuComponent item) => _children.Remove(item);

    public void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + Name);
        foreach (var child in _children)
            child.Display(depth + 2);
    }
}