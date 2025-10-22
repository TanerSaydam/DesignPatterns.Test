Console.WriteLine("Bridge Pattern....");

var consoleLogger = new ConsoleLogProvider();
var fileLogger = new FileLogProvider();

LogBase appLog = new AppLog(consoleLogger);
appLog.Write("Application started.");

LogBase auditLog = new AuditLog(fileLogger);
auditLog.Write("User deleted account.");

Console.ReadLine();
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

public abstract class LogBase
{
    protected readonly ILogProvider _provider;

    protected LogBase(ILogProvider provider) => _provider = provider;

    public abstract void Write(string message);
}

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