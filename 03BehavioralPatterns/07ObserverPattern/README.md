# Observer Pattern

Observer Pattern, bir object’te meydana gelen değişikliklerin diğer bağlı object’lere  
**otomatik olarak bildirilmesini** sağlayan behavioral pattern’dir.  

Bu yapı sayesinde, “bir yerde bir değişiklik olursa, bunu dinleyen herkes haberdar olsun” mantığıyla  
loosely-coupled (gevşek bağlı) sistemler kurulabilir.

## Gerçek Hayat Analojisi

Bir **hava durumu istasyonu** düşün 🌦️  
- İstasyon (Subject) sıcaklık ve nem verilerini toplar.  
- Telefon, pencere ekranı ve TV gibi cihazlar (Observer’lar) bu veriyi dinler.  
- İstasyonda bir değişiklik olduğunda tüm bağlı ekranlar otomatik güncellenir.  

Yani:  
- **WeatherStation** = Subject (veri kaynağı, gözlenen)  
- **Displays** = Observers (veriyi izleyen bileşenler)

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Observer Pattern");

var station = new WeatherStation();

var phoneDisplay = new PhoneDisplay("Taner’s Phone");
var windowDisplay = new WindowDisplay("Office Window");
var tvDisplay = new TvDisplay("Lobby TV");

station.Subscribe(phoneDisplay);
station.Subscribe(windowDisplay);
station.Subscribe(tvDisplay);

station.SetMeasurements(22.4, 55);
station.SetMeasurements(25.1, 48);

station.Unsubscribe(windowDisplay);

station.SetMeasurements(27.0, 40);

Console.ReadLine();

// ----- Subject (Observable) -----
interface IWeatherStation
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void Notify();
}

class WeatherStation : IWeatherStation
{
    private readonly List<IObserver> _observers = new();
    private double _temperature;
    private int _humidity;

    public void SetMeasurements(double temperature, int humidity)
    {
        _temperature = temperature;
        _humidity = humidity;
        Console.WriteLine($"\n[Station] New measurements: Temp={_temperature}°C, Humidity={_humidity}%");
        Notify();
    }

    public (double temp, int humidity) GetState() => (_temperature, _humidity);

    public void Subscribe(IObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var o in _observers)
            o.Update(this);
    }
}

// ----- Observer -----
interface IObserver
{
    void Update(WeatherStation station);
}

// ----- Concrete Observers -----
class PhoneDisplay : IObserver
{
    private readonly string _name;
    public PhoneDisplay(string name) => _name = name;

    public void Update(WeatherStation station)
    {
        var (t, h) = station.GetState();
        Console.WriteLine($"📱 [{_name}] => Temp:{t}°C, Humidity:{h}%");
    }
}

class WindowDisplay : IObserver
{
    private readonly string _location;
    public WindowDisplay(string location) => _location = location;

    public void Update(WeatherStation station)
    {
        var (t, h) = station.GetState();
        Console.WriteLine($"🪟 [{_location}] => Temp:{t}°C, Humidity:{h}%");
    }
}

class TvDisplay : IObserver
{
    private readonly string _name;
    public TvDisplay(string name) => _name = name;

    public void Update(WeatherStation station)
    {
        var (t, h) = station.GetState();
        Console.WriteLine($"📺 [{_name}] => Temp:{t}°C, Humidity:{h}%");
    }
}
```

## Not

Observer Pattern, günümüzde **event-driven (olay tabanlı)** mimarilerin temelini oluşturur.  
C#’ta bu yapı genellikle **Event** ve **Delegate** kullanılarak uygulanır:  

```csharp
public event Action<double> TemperatureChanged;
```