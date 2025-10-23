# Command Pattern

Command Pattern, bir işlemi (komutu) bir object olarak temsil eden behavioral pattern’dir.  
Bir işlemi (örneğin sipariş oluşturmak, kullanıcı silmek, ışığı açmak) nesneleştirerek  
bu işlemleri sıraya koymayı, kaydetmeyi, geri almayı (undo) veya yeniden yürütmeyi (redo) mümkün kılar.  

## Gerçek Hayat Analojisi

Bir restoranda müşteri siparişini garsona verir 🍽️  
Garson (Invoker), siparişi mutfağa iletir ama yemeği kendisi yapmaz.  
Sipariş notu (Command) ne yapılacağını tanımlar — hangi yemek, hangi masa, ne zaman.  
Aşçı (Receiver) bu komutu alır ve uygular.  
**Command Pattern** bu etkileşimi yazılımda nesneleştirir.

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Command Pattern");

var light = new Light();

// create commands
ICommand turnOn = new TurnOnCommand(light);
ICommand turnOff = new TurnOffCommand(light);

// invoker
var remote = new RemoteControl();
remote.ExecuteCommand(turnOn);
remote.ExecuteCommand(turnOff);

// demonstrate undo stack
remote.UndoLastCommand();

Console.ReadLine();

// ----- Command Abstraction -----
interface ICommand
{
    void Execute();
    void Undo();
}

// ----- Receiver -----
class Light
{
    public void TurnOn() => Console.WriteLine("Light is ON");
    public void TurnOff() => Console.WriteLine("Light is OFF");
}

// ----- Concrete Commands -----
class TurnOnCommand : ICommand
{
    private readonly Light _light;

    public TurnOnCommand(Light light) => _light = light;

    public void Execute() => _light.TurnOn();
    public void Undo() => _light.TurnOff();
}

class TurnOffCommand : ICommand
{
    private readonly Light _light;

    public TurnOffCommand(Light light) => _light = light;

    public void Execute() => _light.TurnOff();
    public void Undo() => _light.TurnOn();
}

// ----- Invoker -----
class RemoteControl
{
    private readonly Stack<ICommand> _history = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_history.Count == 0)
        {
            Console.WriteLine("Nothing to undo.");
            return;
        }

        var last = _history.Pop();
        last.Undo();
    }
}
```