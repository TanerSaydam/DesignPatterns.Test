# Singleton Pattern

Üretmemiz gereken nesnenin aynı instance kullanması uygun senaryorlarda Singleton ile bir tane nesne üretip onu gereken her yerde tekrar instance üretmeden kullanmak için uyguladığımız pattern.

```csharp
class Logger
{
    private Logger()
    {

    }
    private static Logger? _instance;
    public static Logger Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = new Logger();
            }

            return _instance;
        }
    }

    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
```

## Threat safe yapısı
Eğer aynı anda Instance çağrılmaya çalışılırsa ilk üretimde birden fazla instance üretme sorunu ortaya çıkabiliyor bunu çözmek için lock ile ilk çağrıda kod kilitlenip o bittikten sonra diğerlerinin sırayla işlenmesini sağlanıyor. Bu sayede aynı anda istek yapılsa da sisteme ilk ulaşan isntance türetiyor ve diğerleri de bunu kullanıyor.

```csharp
class Logger2
{
    private Logger2()
    {

    }

    private static Logger2? _instance;
    private static readonly object _lock = new object();
    public static Logger2 Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance is null)
                {
                    _instance = new Logger2();
                }
            }

            return _instance;
        }
    }

    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
```

## Dependency Injection
Modern teknoloji ile Dependency Injection yöntemi geliştirildi. Singleton DI ile modern uygulamalarda daha basit şekilde uygulanabilir hale geldi.
önce kütüphaneyi kurmamız gerekiyor
```dash
Microsoft.Extensions.DependencyInjection
```

Sonra service collection ile container oluşturup provider ile instance türetmemiz lazım
```csharp
ServiceCollection services = new();
services.AddSingleton<LoggerDI>();

var serviceProvider = services.BuildServiceProvider();
var loggerDI = serviceProvider.GetRequiredService<LoggerDI>();

class LoggerDI
{
    public LoggerDI()
    {

    }
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
```