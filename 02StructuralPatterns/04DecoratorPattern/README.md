# 🧱 Decorator Pattern

## 🎯 Amaç
Decorator Pattern, bir class’ın yapısını değiştirmeden ona **dinamik olarak yeni davranışlar eklemeyi** sağlar.  
Yani miras almak yerine, mevcut nesneyi **sarmalayarak (wrap)** genişletir.  

Gerçek yazılımda bu genellikle şurada kullanılır:
- Log ekleme  
- Cache ekleme  
- Validation / Authorization işlemleri  
- Mail veya HTTP isteklerine ek özellikler kazandırma  

---

## 🧩 Senaryo: Mail Gönderim Servisi

Bir uygulamada mail gönderim altyapısı var.  
Temel servis sadece mail gönderiyor ama zamanla şu ihtiyaçlar doğuyor:

- Gönderilen mailleri loglamak  
- Mailin sonuna otomatik imza eklemek  
- Gerekirse başka politikalar eklemek (ör. spam kontrolü, hata tekrarı)

Bu değişiklikleri yapmak için `SmtpMailService`’i değiştirmek yerine **Decorator Pattern** ile yeni davranışlar ekliyoruz.

---

## 💻 Kod Örneği

```csharp
Console.WriteLine("Decorator Pattern – Mail Service Example\n");

IMailService mail = new SmtpMailService();
mail = new LoggingMailDecorator(mail);
mail = new SignatureMailDecorator(mail, "\n--\nBest regards,\nACME Corp");

mail.Send("user@example.com", "Welcome", "Hello, welcome to our platform!");
Console.ReadLine();


public interface IMailService
{
    void Send(string to, string subject, string body);
}

public class SmtpMailService : IMailService
{
    public void Send(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}\nSubject: {subject}\nBody:\n{body}\n");
    }
}

// Base decorator
public abstract class MailDecorator : IMailService
{
    protected readonly IMailService _inner;

    protected MailDecorator(IMailService inner)
    {
        _inner = inner;
    }

    public abstract void Send(string to, string subject, string body);
}

// Concrete decorators
public class LoggingMailDecorator : MailDecorator
{
    public LoggingMailDecorator(IMailService inner) : base(inner) { }

    public override void Send(string to, string subject, string body)
    {
        Console.WriteLine($"[LOG] Sending mail to {to} at {DateTime.Now}");
        _inner.Send(to, subject, body);
    }
}

public class SignatureMailDecorator : MailDecorator
{
    private readonly string _signature;
    public SignatureMailDecorator(IMailService inner, string signature) : base(inner)
    {
        _signature = signature;
    }

    public override void Send(string to, string subject, string body)
    {
        var signedBody = body + _signature;
        _inner.Send(to, subject, signedBody);
    }
}
```

---

## 🔍 Nasıl Çalışıyor?

1️⃣ `SmtpMailService` → mail gönderen temel servis  
2️⃣ `LoggingMailDecorator` → gönderim öncesi log ekler  
3️⃣ `SignatureMailDecorator` → mail gövdesine imza ekler  
4️⃣ Tüm dekoratörler aynı `IMailService` arayüzünü uygular  

Çağrı sırası:  
```powershell
LoggingMailDecorator.Send()
 → SignatureMailDecorator.Send()
   → SmtpMailService.Send()
```

Her dekoratör, çağrıyı `_inner.Send()` ile bir sonraki katmana iletir.

---

## 📦 Çıktı

```powershell
[LOG] Sending mail to user@example.com at 10/22/2025 18:40:00
Sending email to user@example.com
Subject: Welcome
Body:
Hello, welcome to our platform!
--
Best regards,
ACME Corp
```

---

## 💬 Gerçek Kullanım Alanları

- `.NET Stream` sınıflarında (`GZipStream`, `BufferedStream`, `CryptoStream`)  
- `ILogger` zincirlerinde log verisini zenginleştirme  
- `HttpClient` middleware zincirlerinde request/response sarmalama  
- Business katmanında audit, caching, validation ekleme  

---

## 🧩 Özet  
> Decorator Pattern, nesnelere dinamik olarak yeni davranışlar eklemenin esnek yoludur.  
> Mevcut sınıfı değiştirmeden davranışlarını “sarmalayarak” genişletir.
