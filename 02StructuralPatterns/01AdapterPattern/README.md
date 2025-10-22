# Adapter Pattern

Adapter Pattern, birbiriyle uyumsuz arayüzlere sahip class’ların birlikte çalışabilmesini sağlar.  
Amaç, mevcut bir class’ın kodunu değiştirmeden, onu sisteme uygun hale getiren bir “çevirmen” class yazmaktır.  

Bu pattern, farklı interface’leri birbiriyle entegre etmek gerektiğinde kullanılır.  
Yani **client** başka bir yapıyı beklerken, **adapter** aradaki farkı kapatır.

## Amaç

Elimizde iki farklı “bildirim gönderici” var:
1. EmailNotificationSender → Send(to, message) metodu var.
2. SmsService → ama bu sınıfın metodu SendSms(phoneNumber, text) (farklı isim ve parametre yapısı).

Yeni sistem tüm bildirimleri tek bir ortak arayüzden (INotificationSender) göndermek istiyor.
Ama SmsService bu arayüze uymuyor.

## Çözüm (Adapter)
- SmsAdapter sınıfı, INotificationSender arayüzünü uyguluyor (yani yeni sistemin beklediği formda davranıyor).
- İçeride, gelen çağrıyı (Send) eski sistemin anlayacağı hale çeviriyor (_smsService.SendSms(to, message)).
Yani SmsAdapter iki taraf arasında çevirmen (adapter) görevi görüyor.

## Akış
1. Kod smsSender.Send("+9055...", "Hello via SMS!") der.
2. SmsAdapter.Send() çalışır.
3. Adapter, gelen to ve message’ı alıp _smsService.SendSms() olarak çağırır.
4. Böylece SmsService sanki INotificationSender gibi davranmış olur.

## Özet
- EmailNotificationSender → zaten arayüze uygun, direkt kullanılıyor.
- SmsService → farklı arayüzde, o yüzden Adapter ile sarılıyor.
- Client (Program.cs) → ikisinin farkını bilmeden aynı interface ile çağırıyor.
“Farklı arayüzlere sahip nesnelerin, ortak bir arayüz üzerinden birlikte çalışmasını sağlamak.”

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