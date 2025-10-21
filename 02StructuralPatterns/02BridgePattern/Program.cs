Console.WriteLine("Bridge Pattern....");

IDevice tv = new TvDevice();
IDevice radio = new RadioDevice();

IRemote tvRemote = new Remote(tv);
tvRemote.TogglePower();

IRemote radioRemote = new Remote(radio);
radioRemote.TogglePower();
radioRemote.Mute();

Console.ReadLine();

interface IDevice
{
    bool IsEnabled { get; }
    void Enable();
    void Disable();
    void SetVolume(int volume);
    int GetVolume();
}

class TvDevice : IDevice
{
    private bool _enabled;
    private int _volume;
    public bool IsEnabled => _enabled;

    public void Enable()
    {
        _enabled = true;
        Console.WriteLine("TV is ON");
    }

    public void Disable()
    {
        _enabled = false;
        Console.WriteLine("TV is OFF");
    }
    public void SetVolume(int volume)
    {
        _volume = volume;
        Console.WriteLine("TV volume set to {0}", volume);
    }
    public int GetVolume()
    {
        return _volume;
    }
}

class RadioDevice : IDevice
{
    private bool _enabled;
    private int _volume;
    public bool IsEnabled => _enabled;

    public void Enable()
    {
        _enabled = true;
        Console.WriteLine("Radio is ON");
    }

    public void Disable()
    {
        _enabled = false;
        Console.WriteLine("Radio is OFF");
    }
    public void SetVolume(int volume)
    {
        _volume = volume;
        Console.WriteLine("Radio volume set to {0}", volume);
    }
    public int GetVolume()
    {
        return _volume;
    }
}

interface IRemote
{
    void TogglePower();
    void Mute();
}

class Remote(IDevice device) : IRemote
{
    public void TogglePower()
    {
        if (device.IsEnabled)
        {
            device.Disable();
        }
        else
        {
            device.Enable();
        }
    }
    public void Mute()
    {
        device.SetVolume(0);
        Console.WriteLine("Muted the device.");
    }
}