Console.WriteLine("Composite Pattern...");

var file1 = new FileItem("File1.txt", 120);
var file2 = new FileItem("File2.txt", 80);
var file3 = new FileItem("File3.txt", 200);

var subFolder = new FolderItem("SubFolder");
subFolder.Add(file2);
subFolder.Add(file3);

var rootFolder = new FolderItem("RootFolder");
rootFolder.Add(file1);
rootFolder.Add(subFolder);

rootFolder.Display(0);
Console.WriteLine($"Total size: {rootFolder.GetSize()} KB");

Console.ReadLine();

interface IFileSystemItem
{
    string Name { get; }
    void Display(int depth);
    int GetSize();
}

class FileItem : IFileSystemItem
{
    private readonly int _size;

    public FileItem(string name, int size)
    {
        Name = name;
        _size = size;
    }

    public string Name { get; }

    public void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + Name);
    }

    public int GetSize()
    {
        return _size;
    }
}

class FolderItem : IFileSystemItem
{
    public FolderItem(string name)
    {
        Name = name;
    }

    public string Name { get; }
    private readonly List<IFileSystemItem> _children = new();

    public void Add(IFileSystemItem item)
    {
        _children.Add(item);
    }

    public void Remove(IFileSystemItem item)
    {
        _children.Remove(item);
    }

    public void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + Name);
        foreach (var child in _children)
        {
            child.Display(depth + 2);
        }
    }

    public int GetSize()
    {
        int total = 0;
        foreach (var item in _children)
        {
            total += item.GetSize();
        }

        return total;
    }
}