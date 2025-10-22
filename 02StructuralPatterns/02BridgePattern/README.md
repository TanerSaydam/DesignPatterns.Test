# Bridge Pattern

Bridge Pattern, soyutlama (abstraction) ile uygulamanın (implementation) birbirinden bağımsız olarak geliştirilebilmesini sağlar.  
Bu sayede bir yapının hem soyut tarafını hem de uygulama tarafını ayrı ayrı genişletebiliriz.  

## Bridge Pattern’in Temel Fikri
“Bir sınıfın soyut tarafını (abstraction), onu gerçekleştiren tarafından (implementation) ayır, böylece ikisi bağımsız olarak geliştirilebilsin.”<br>
Basitçe:
- “Ne yapıyorum?” (Abstraction)
- “Nasıl yapıyorum?” (Implementation)
- Bu iki taraf birbirinden ayrılır.

## Gerçek Yazılım Senaryosu: Farklı Loglama Altyapıları

Diyelim ki sisteminde loglama yapmak istiyorsun.
Ama loglar bazen:
- Konsola (ConsoleLogger),
- Dosyaya (FileLogger),
- ElasticSearch’e (ElasticLogger)
gönderilecek.<br>
Ayrıca log türleri de farklı:
- AppLog (uygulama logları)
- AuditLog (kullanıcı işlemleri logları)
- ErrorLog (hata kayıtları)<br>
Bu durumda log türleri (abstraction) ile log altyapıları (implementation) birbirine sıkı bağlı olursa, kod patlar — her kombinasyon için ayrı class yazman gerekir (AppConsoleLogger, AppFileLogger, ErrorElasticLogger, ...).


## Bridge Yaklaşımı Program.cs (örnek kullanım)
- Amaç: “Log türü” (abstraction) ile “log altyapısı” (implementation)’nı ayır.

### Implementation tarafı
```csharp
public interface ILogProvider
{
    void Log(string message);
}

public class ConsoleLogProvider : ILogProvider
{
    public void Log(string message) => Console.WriteLine($"[Console] {message}");
}

public class FileLogProvider : ILogProvider
{
    public void Log(string message) => File.AppendAllText("log.txt", message + "\n");
}
```

### Abstraction tarafı (ne loglanır?)
```csharp
public abstract class LogBase
{
    protected readonly ILogProvider _provider;

    protected LogBase(ILogProvider provider) => _provider = provider;

    public abstract void Write(string message);
}
```

### Abstraction’ın genişletilmiş halleri
```csharp
public class AppLog : LogBase
{
    public AppLog(ILogProvider provider) : base(provider) { }

    public override void Write(string message)
        => _provider.Log($"[App] {message}");
}

public class AuditLog : LogBase
{
    public AuditLog(ILogProvider provider) : base(provider) { }

    public override void Write(string message)
        => _provider.Log($"[Audit] {message}");
}
```

### Kullanım
```csharp
var consoleLogger = new ConsoleLogProvider();
var fileLogger = new FileLogProvider();

LogBase appLog = new AppLog(consoleLogger);
appLog.Write("Application started.");

LogBase auditLog = new AuditLog(fileLogger);
auditLog.Write("User deleted account.");
```