# Singleton Pattern

Üretmemiz gereken nesnenin ayný instance kullanmasý uygun senaryorlarda Singleton ile bir tane nesne üretip onu gereken her yerde tekrar instance üretmeden kullanmak için uyguladýðýmýz pattern.

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

## Threat safe yapýsý
Eðer ayný anda Instance çaðrýlmaya çalýþýlýrsa ilk üretimde birden fazla instance üretme sorunu ortaya çýkabiliyor bunu çözmek için lock ile ilk çaðrýda kod kilitlenip o bittikten sonra diðerlerinin sýrayla iþlenmesini saðlanýyor. Bu sayede ayný anda istek yapýlsa da sisteme ilk ulaþan isntance türetiyor ve diðerleri de bunu kullanýyor.

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
Modern teknoloji ile Dependency Injection yöntemi geliþtirildi. Singleton DI ile modern uygulamalarda daha basit þekilde uygulanabilir hale geldi.
önce kütüphaneyi kurmamýz gerekiyor
```dash
Microsoft.Extensions.DependencyInjection
```

Sonra service collection ile container oluþturup provider ile instance türetmemiz lazým
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