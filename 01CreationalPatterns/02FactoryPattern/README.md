# Factory Pattern

Factory Pattern, object oluşturma sürecini merkezi bir noktada toplar.  
Bu sayede object oluşturmak için `new` anahtar sözcüğünü doğrudan kullanmadan, **factory** aracılığıyla objectleri dinamik olarak oluşturabiliriz.  
Amaç, object oluşturmayı soyutlamak ve kod bağımlılığını azaltmaktır.

## Ne İşe Yarar?

- Nesne oluşturma sürecini tek bir yerde toplar.  
- Kodun, hangi classın oluşturulacağını bilmesine gerek kalmaz.  
- Yeni türler eklendiğinde mevcut kod değiştirilmeden genişletilebilir.

> **Not:** Factory Pattern, SOLID prensiplerinden **Open–Closed Principle** (Açık–Kapalı İlkesi) ile uyumludur.  
> Yeni bir notification türü eklemek için mevcut kodu değiştirmeden yeni bir class oluşturmak yeterlidir.  
> Böylece kod **genişletmeye açık**, **değişikliğe kapalı** olur.


## Kod Örneği

Aşağıda `NotificationFactory`, `EmailNotification` ve `SmsNotification` classlarıyla örnek bir kullanım gösterilmiştir.

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

## Gerçek Hayatta Kullanımı

- Bildirim sistemlerinde (Email, SMS, Push Notification)
- Farklı ödeme sistemlerinde (Stripe, PayPal, Iyzico)