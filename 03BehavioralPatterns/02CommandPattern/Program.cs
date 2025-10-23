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
