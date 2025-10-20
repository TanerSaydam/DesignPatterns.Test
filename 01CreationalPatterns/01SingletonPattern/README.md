# Singleton Pattern

�retmemiz gereken nesnenin ayn� instance kullanmas� uygun senaryorlarda Singleton ile bir tane nesne �retip onu gereken her yerde tekrar instance �retmeden kullanmak i�in uygulad���m�z pattern.

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

## Threat safe yap�s�
E�er ayn� anda Instance �a�r�lmaya �al���l�rsa ilk �retimde birden fazla instance �retme sorunu ortaya ��kabiliyor bunu ��zmek i�in lock ile ilk �a�r�da kod kilitlenip o bittikten sonra di�erlerinin s�rayla i�lenmesini sa�lan�yor. Bu sayede ayn� anda istek yap�lsa da sisteme ilk ula�an isntance t�retiyor ve di�erleri de bunu kullan�yor.

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
Modern teknoloji ile Dependency Injection y�ntemi geli�tirildi. Singleton DI ile modern uygulamalarda daha basit �ekilde uygulanabilir hale geldi.
�nce k�t�phaneyi kurmam�z gerekiyor
```dash
Microsoft.Extensions.DependencyInjection
```

Sonra service collection ile container olu�turup provider ile instance t�retmemiz laz�m
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