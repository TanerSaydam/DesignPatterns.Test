# Adapter Pattern

Adapter Pattern, birbiriyle uyumsuz aray�zlere sahip class�lar�n birlikte �al��abilmesini sa�lar.  
Ama�, mevcut bir class��n kodunu de�i�tirmeden, onu sisteme uygun hale getiren bir ��evirmen� class yazmakt�r.  

Bu pattern, farkl� interface�leri birbiriyle entegre etmek gerekti�inde kullan�l�r.  
Yani **client** ba�ka bir yap�y� beklerken, **adapter** aradaki fark� kapat�r.

## Ama�

- Uyumsuz class�lar aras�nda k�pr� (bridge) olu�turmak.  
- Mevcut class koduna dokunmadan sisteme entegre etmek.  
- Eski sistemlerle yeni sistemlerin birlikte �al��abilmesini sa�lamak.  

## Program.cs (�rnek kullan�m)

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

## Ger�ek Hayatta Kullan�m�

- Farkl� API veya servislerin birle�tirilmesinde.  
- Loglama, bildirim veya �deme sistemlerinde farkl� altyap�lar� entegre etmek i�in.  
- Eski (legacy) kodlar� yeni uygulama mimarilerine uyarlamak i�in.  
- ���nc� taraf servisleri mevcut sistemin interface yap�s�na uydurmak i�in.
