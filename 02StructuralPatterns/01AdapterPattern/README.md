# Adapter Pattern

Adapter Pattern, birbiriyle uyumsuz arayüzlere sahip class’ların birlikte çalışabilmesini sağlar.  
Amaç, mevcut bir class’ın kodunu değiştirmeden, onu sisteme uygun hale getiren bir “çevirmen” class yazmaktır.  

Bu pattern, farklı interface’leri birbiriyle entegre etmek gerektiğinde kullanılır.  
Yani **client** başka bir yapıyı beklerken, **adapter** aradaki farkı kapatır.

## Amaç

- Uyumsuz class’lar arasında köprü (bridge) oluşturmak.  
- Mevcut class koduna dokunmadan sisteme entegre etmek.  
- Eski sistemlerle yeni sistemlerin birlikte çalışabilmesini sağlamak.  

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Adapter Pattern...");

INotificationSender emailSender = new EmailNotificationSender();
INotificationSender smsSender = new SmsAdapter(new SmsService());

emailSender.Send("user@example.com", "Hello via Email!");
smsSender.Send("+905551112233", "Hello via SMS!");

Console.ReadLine();

interface INotificationSender
{
    void Send(string to, string message);
}

class EmailNotificationSender : INotificationSender
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"Email to {to}: {message}");
    }
}

class SmsService
{
    public void SendSms(string phoneNumber, string text)
    {
        Console.WriteLine($"SMS to {phoneNumber}: {text}");
    }
}

class SmsAdapter : INotificationSender
{
    private readonly SmsService _smsService;

    public SmsAdapter(SmsService smsService)
    {
        _smsService = smsService;
    }

    public void Send(string to, string message)
    {
        _smsService.SendSms(to, message);
    }
}
```

## Gerçek Hayatta Kullanımı

- Farklı API veya servislerin birleştirilmesinde.  
- Loglama, bildirim veya ödeme sistemlerinde farklı altyapıları entegre etmek için.  
- Eski (legacy) kodları yeni uygulama mimarilerine uyarlamak için.  
- Üçüncü taraf servisleri mevcut sistemin interface yapısına uydurmak için.
