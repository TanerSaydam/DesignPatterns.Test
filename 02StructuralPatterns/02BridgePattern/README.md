# Bridge Pattern

Bridge Pattern, soyutlama (abstraction) ile uygulamanın (implementation) birbirinden bağımsız olarak geliştirilebilmesini sağlar.  
Bu sayede bir yapının hem soyut tarafını hem de uygulama tarafını ayrı ayrı genişletebiliriz.  

## Amaç

- Soyutlama (ör. Remote) ile uygulamayı (ör. Device) birbirinden ayırmak.  
- Yeni abstraction veya implementation türleri eklenirken mevcut kodu değiştirmemek.  
- Kodun genişletilebilirliğini ve yeniden kullanılabilirliğini artırmak.  

## Gerçek Hayat Analojisi

Bir uzaktan kumanda düşün 🎮  
- Kumanda, televizyon veya radyo gibi farklı cihazları kontrol edebilir.  
- Her cihazın açma, kapama ve ses ayarlama gibi kendi işlemleri vardır.  

Kumanda (abstraction) ile cihaz (implementation) birbirinden bağımsızdır.  
Yeni bir cihaz eklendiğinde kumandayı değiştirmene gerek kalmaz;  
aynı şekilde yeni bir kumanda türü eklendiğinde de cihaz kodlarını güncellemezsin.  
**Bridge Pattern**, bu iki yapıyı birbirine bağlayan köprü (bridge) görevi görür.


## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Bridge Pattern....");

IDevice tv = new TvDevice();
IDevice radio = new RadioDevice();

IRemote tvRemote = new Remote(tv);
tvRemote.TogglePower();

IRemote radioRemote = new Remote(radio);
radioRemote.TogglePower();
radioRemote.Mute();

Console.ReadLine();

interface IDevice
{
    bool IsEnabled { get; }
    void Enable();
    void Disable();
    void SetVolume(int volume);
    int GetVolume();
}

class TvDevice : IDevice
{
    private bool _enabled;
    private int _volume;
    public bool IsEnabled => _enabled;

    public void Enable()
    {
        _enabled = true;
        Console.WriteLine("TV is ON");
    }

    public void Disable()
    {
        _enabled = false;
        Console.WriteLine("TV is OFF");
    }
    public void SetVolume(int volume)
    {
        _volume = volume;
        Console.WriteLine("TV volume set to {0}", volume);
    }
    public int GetVolume()
    {
        return _volume;
    }
}

class RadioDevice : IDevice
{
    private bool _enabled;
    private int _volume;
    public bool IsEnabled => _enabled;

    public void Enable()
    {
        _enabled = true;
        Console.WriteLine("Radio is ON");
    }

    public void Disable()
    {
        _enabled = false;
        Console.WriteLine("Radio is OFF");
    }
    public void SetVolume(int volume)
    {
        _volume = volume;
        Console.WriteLine("Radio volume set to {0}", volume);
    }
    public int GetVolume()
    {
        return _volume;
    }
}

interface IRemote
{
    void TogglePower();
    void Mute();
}

class Remote(IDevice device) : IRemote
{
    public void TogglePower()
    {
        if (device.IsEnabled)
        {
            device.Disable();
        }
        else
        {
            device.Enable();
        }
    }
    public void Mute()
    {
        device.SetVolume(0);
        Console.WriteLine("Muted the device.");
    }
}
```

## Gerçek Hayatta Kullanımı

- Farklı cihaz türlerini (TV, Radio, vb.) kontrol eden uzaktan kumandalar.  
- Farklı platformlarda (Windows, Linux, Mac) çalışan UI renderer yapıları.  
- Veri erişim katmanlarında farklı veritabanlarını destekleyen abstraction yapıları.  
- Oyun motorlarında farklı render engine veya input sistemlerini soyutlamak için.
