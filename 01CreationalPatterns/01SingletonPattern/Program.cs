using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Singleton Pattern...");

Logger logger = Logger.Instance;
logger.Log("Hello world!");

Logger logger2 = Logger.Instance;
logger2.Log("Hello world2!");

ServiceCollection services = new();
services.AddSingleton<LoggerDI>();

var serviceProvider = services.BuildServiceProvider();
var loggerDI = serviceProvider.GetRequiredService<LoggerDI>();
loggerDI.Log("Hell world from DI");

var loggerDI2 = serviceProvider.GetRequiredService<LoggerDI>();
loggerDI2.Log("Hell world from DI");

Console.ReadLine();


//Normal
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

//Threat safe
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

//Dependency Injection
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
