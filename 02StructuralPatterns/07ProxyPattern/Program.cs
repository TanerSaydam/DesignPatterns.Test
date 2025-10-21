Console.WriteLine("Proxy Pattern...");

IImage img = new ImageProxy("big_photo.jpg");
img.Display();
img.Display();

Console.ReadLine();


interface IImage
{
    void Display();
}

class RealImage : IImage
{
    private readonly string _fileName;
    private bool _loaded;

    public RealImage(string fileName)
    {
        _fileName = fileName;
    }

    private void LoadFromDisk()
    {
        if (_loaded) return;
        Console.WriteLine($"[RealImage] loading '{_fileName}' from disk...");
        System.Threading.Thread.Sleep(300);
        _loaded = true;
    }


    public void Display()
    {
        LoadFromDisk();
        Console.WriteLine($"[RealImage] Displaying '{(_fileName)}'");
    }
}

class ImageProxy : IImage
{
    private readonly string _fileName;
    private RealImage? _realImage;
    public ImageProxy(string fileName)
    {
        _fileName = fileName;
    }
    public void Display()
    {
        _realImage ??= new RealImage(_fileName); // ??= Eğer değişken null ise, sağdaki değeri ata; değilse hiçbir şey yapma
        _realImage.Display();
    }
}