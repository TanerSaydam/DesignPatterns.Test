# Composite Pattern

Composite Pattern, tek bir object ile bir grup object’i aynı şekilde ele almayı sağlar.  
Yani “parça-bütün” ilişkisini temsil eden yapılarda, **tek bir nesne gibi davranan hiyerarşiler** kurar.  
Bu sayede client, bir object mi yoksa alt object’lerden oluşan bir grup mu olduğunu bilmeden işlem yapabilir.

## 🧱 Gerçek Senaryo: Header Menü Yapısı

## 🎯 Amaç  
Composite Pattern, **tekil menü** ve **alt menü gruplarını** aynı arayüzle yönetmemizi sağlar.  
Yani, sistem “bu bir alt menü mü yoksa normal menü mü?” diye ayırt etmeden işlem yapabilir.

```dash
Home
Products
├── Electronics
│ ├── Phones
│ └── Laptops
└── Furniture
├── Tables
└── Chairs
About
Contact
```

Burada:
- “Products” → alt menüleri olan **grup menü (Composite)**  
- “Phones”, “Tables”, “Home” → alt menüsü olmayan **tekil menüler (Leaf)**  
- Her iki tip de aynı arayüz (`IMenuComponent`) üzerinden yönetilir.

## 🧠 Yapı

- **IMenuComponent** → Ortak arayüz. Her menüde bulunması gereken `Name` ve `Display(int depth)` metodunu tanımlar.  
- **MenuItem** → Tekil menü (alt menüsü yok).  
- **MenuGroup** → Alt menülere sahip composite yapı.  
- **Client (Program.cs)** → Menü ağacını oluşturur ve `Display()` ile tüm yapıyı ekrana basar.


## Program.cs (örnek kullanım)

```csharp
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
```