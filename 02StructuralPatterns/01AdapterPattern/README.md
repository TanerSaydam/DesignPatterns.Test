# Adapter Pattern

Adapter Pattern, birbiriyle uyumsuz arayüzlere sahip class’larýn birlikte çalýþabilmesini saðlar.  
Amaç, mevcut bir class’ýn kodunu deðiþtirmeden, onu sisteme uygun hale getiren bir “çevirmen” class yazmaktýr.  

Bu pattern, farklý interface’leri birbiriyle entegre etmek gerektiðinde kullanýlýr.  
Yani **client** baþka bir yapýyý beklerken, **adapter** aradaki farký kapatýr.

## Amaç

- Uyumsuz class’lar arasýnda köprü (bridge) oluþturmak.  
- Mevcut class koduna dokunmadan sisteme entegre etmek.  
- Eski sistemlerle yeni sistemlerin birlikte çalýþabilmesini saðlamak.  

## Program.cs (örnek kullaným)

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

## Gerçek Hayatta Kullanýmý

- Farklý API veya servislerin birleþtirilmesinde.  
- Loglama, bildirim veya ödeme sistemlerinde farklý altyapýlarý entegre etmek için.  
- Eski (legacy) kodlarý yeni uygulama mimarilerine uyarlamak için.  
- Üçüncü taraf servisleri mevcut sistemin interface yapýsýna uydurmak için.
