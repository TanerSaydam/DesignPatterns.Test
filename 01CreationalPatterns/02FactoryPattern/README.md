# Factory Pattern

Factory Pattern, object oluþturma sürecini merkezi bir noktada toplar.  
Bu sayede object oluþturmak için `new` anahtar sözcüðünü doðrudan kullanmadan, **factory** aracýlýðýyla objectleri dinamik olarak oluþturabiliriz.  
Amaç, object oluþturmayý soyutlamak ve kod baðýmlýlýðýný azaltmaktýr.

## Ne Ýþe Yarar?

- Nesne oluþturma sürecini tek bir yerde toplar.  
- Kodun, hangi classýn oluþturulacaðýný bilmesine gerek kalmaz.  
- Yeni türler eklendiðinde mevcut kod deðiþtirilmeden geniþletilebilir.

> **Not:** Factory Pattern, SOLID prensiplerinden **Open–Closed Principle** (Açýk–Kapalý Ýlkesi) ile uyumludur.  
> Yeni bir notification türü eklemek için mevcut kodu deðiþtirmeden yeni bir class oluþturmak yeterlidir.  
> Böylece kod **geniþletmeye açýk**, **deðiþikliðe kapalý** olur.


## Kod Örneði

Aþaðýda `NotificationFactory`, `EmailNotification` ve `SmsNotification` classlarýyla örnek bir kullaným gösterilmiþtir.

```csharp
Console.WriteLine("Factory Pattern...");

INotification emailNotification = NotificationFactory.CreateNotification(NotificationTypeEnum.Email);
emailNotification.Send("Hello world");

INotification smsNotification = NotificationFactory.CreateNotification(NotificationTypeEnum.Sms);
smsNotification.Send("Hello world");

Console.ReadLine();

interface INotification
{
    void Send(string message);
}

class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Email sent: {0}", message);
    }
}

class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Sms sent: {0}", message);
    }
}

class NotificationFactory
{
    private NotificationFactory() { }
    public static INotification CreateNotification(NotificationTypeEnum notificationType)
    {
        switch (notificationType)
        {
            case NotificationTypeEnum.Email:
                return new EmailNotification();
            case NotificationTypeEnum.Sms:
                return new SmsNotification();
            default:
                throw new ArgumentException("Invalid notification type");
        }
    }
}

enum NotificationTypeEnum
{
    Email,
    Sms
}
```

## Gerçek Hayatta Kullanýmý

- Bildirim sistemlerinde (Email, SMS, Push Notification)
- Farklý ödeme sistemlerinde (Stripe, PayPal, Iyzico)