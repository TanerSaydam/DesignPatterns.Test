# Proxy Pattern

Proxy Pattern, bir object’e erişimi kontrol etmek için onun yerine geçen (“vekil”) bir object tanımlayan structural pattern’dir.  
Gerçek object ile client arasına bir ara katman eklenir ve bu katman erişimi yönetir, önbellekleme, loglama veya tembel yükleme gibi ek işlemler yapabilir.

## Amaç

- Gerçek object’e erişimi kontrol etmek.  
- Kaynak tüketimini azaltmak (örneğin büyük dosyalar, uzak sunucu bağlantıları).  
- Güvenlik, önbellekleme veya loglama gibi ek davranışlar eklemek.  
- Nesneye doğrudan erişimi engelleyerek sistemin esnekliğini artırmak.  

## Gerçek Hayat Analojisi

Bir fotoğraf galerisinde yüksek çözünürlüklü bir fotoğraf arşivin olduğunu düşün 📸  
Her fotoğrafı açmadan önce, uygulama proxy üzerinden çalışır.  
Proxy önce kontrol eder: fotoğraf daha önce yüklenmiş mi?  
Eğer yüklenmemişse, diskteki gerçek dosyayı (RealImage) yükler ve belleğe alır.  
Sonraki görüntülemelerde artık fotoğraf doğrudan bellekteki veriden alınır.  

Bu sayede gereksiz disk erişimi engellenir, performans artar ve kaynak kullanımı azalır.  
Yani **Proxy Pattern**, fotoğrafın “tembel yükleme” (lazy loading) sürecini yönetir.

## Program.cs (örnek kullanım)

```csharp
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
```

## Gerçek Hayatta Kullanımı

- **Virtual Proxy:** Ağır nesnelerin yalnızca gerektiğinde oluşturulması (ör. resim veya video yükleme).  
- **Protection Proxy:** Erişim yetkilerinin kontrol edilmesi (ör. kullanıcı rolüne göre).  
- **Caching Proxy:** Sık kullanılan verilerin önbelleğe alınması.  
- **Remote Proxy:** Uzak sunuculardaki nesnelere erişimin yönetilmesi.  
- **Logging Proxy:** Nesneye yapılan işlemlerin kaydedilmesi (ör. debug veya audit amaçlı).
