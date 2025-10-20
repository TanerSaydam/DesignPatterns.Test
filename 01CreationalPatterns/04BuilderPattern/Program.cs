Console.WriteLine("Builder Pattern...");

var director = new ConstructionDirector();
IHouseBuilder houseBuilder = new HouseBuilder();

House simpleHouse = director.ConstructSimple(houseBuilder);
House luxuryHouse = director.ConstructLuxury(houseBuilder);
Console.WriteLine(simpleHouse);
Console.WriteLine(luxuryHouse);

var customHouse = new HouseBuilder()
    .WithWalls(6)
    .WithFloors(1)
    .WithDoors(2)
    .WithWindows(8)
    .WithRoof(RoofTypeEnum.Flat)
    .WithBackyard(true)
    .WithHeating(HeatingTypeEnum.Electric)
    .WithElectrical(ElectricalPlanEnum.SmartHome)
    .Build();
Console.WriteLine(customHouse);

Console.ReadLine();

enum HeatingTypeEnum { None, Gas, Electric, Geothermal }
enum ElectricalPlanEnum { Basic, SmartHome }
enum RoofTypeEnum { Flat, Gable, Hip, Dome, Mansard }
class House
{
    public House(
        int walls,
        int floors,
        int doors,
        int windows,
        RoofTypeEnum roof,
        bool backyard,
        HeatingTypeEnum heating,
        ElectricalPlanEnum electrical)
    {
        Walls = walls;
        Floors = floors;
        Doors = doors;
        Windows = windows;
        Roof = roof;
        Backyard = backyard;
        Heating = heating;
        Electrical = electrical;
    }

    public int Walls { get; }
    public int Floors { get; }
    public int Doors { get; }
    public int Windows { get; }
    public RoofTypeEnum Roof { get; }
    public bool Backyard { get; }
    public HeatingTypeEnum Heating { get; }
    public ElectricalPlanEnum Electrical { get; }

    public override string ToString()
    {
        return $"House => Walls: {Walls}, Floors: {Floors}, Doors: {Doors}, Windows: {Windows}, Roof: {Roof.ToString()}, Backyard: {Backyard}, Heating: {Heating.ToString()}, Electrical: {Electrical.ToString()}";
    }
}

interface IHouseBuilder
{
    IHouseBuilder WithWalls(int count);
    IHouseBuilder WithFloors(int count);
    IHouseBuilder WithDoors(int count);
    IHouseBuilder WithWindows(int count);
    IHouseBuilder WithRoof(RoofTypeEnum type);
    IHouseBuilder WithBackyard(bool backyard);
    IHouseBuilder WithHeating(HeatingTypeEnum type);
    IHouseBuilder WithElectrical(ElectricalPlanEnum plan);
    House Build();
}

class HouseBuilder : IHouseBuilder
{
    int _walls;
    int _floors;
    int _doors;
    int _windows;
    bool _backyard;
    RoofTypeEnum _roof;
    HeatingTypeEnum _heating;
    ElectricalPlanEnum _electrical;
    public IHouseBuilder WithWalls(int count)
    {
        _walls = count;
        return this;
    }
    public IHouseBuilder WithFloors(int count)
    {
        _floors = count;
        return this;
    }
    public IHouseBuilder WithDoors(int count)
    {
        _doors = count;
        return this;
    }
    public IHouseBuilder WithWindows(int count)
    {
        _windows = count;
        return this;
    }
    public IHouseBuilder WithRoof(RoofTypeEnum type)
    {
        _roof = type;
        return this;
    }
    public IHouseBuilder WithBackyard(bool backyard)
    {
        _backyard = backyard;
        return this;
    }
    public IHouseBuilder WithHeating(HeatingTypeEnum type)
    {
        _heating = type;
        return this;
    }
    public IHouseBuilder WithElectrical(ElectricalPlanEnum plan)
    {
        _electrical = plan;
        return this;
    }

    public House Build()
    {
        return new House(_walls, _floors, _doors, _windows, _roof, _backyard, _heating, _electrical);
    }
}

class ConstructionDirector
{
    public House ConstructSimple(IHouseBuilder houseBuilder)
    {
        return houseBuilder
            .WithWalls(4)
            .WithFloors(1)
            .WithDoors(1)
            .WithWindows(2)
            .WithRoof(RoofTypeEnum.Gable)
            .WithBackyard(false)
            .WithHeating(HeatingTypeEnum.None)
            .WithElectrical(ElectricalPlanEnum.Basic)
            .Build();
    }

    public House ConstructLuxury(IHouseBuilder houseBuilder)
    {
        return houseBuilder
            .WithWalls(8)
            .WithFloors(2)
            .WithDoors(3)
            .WithWindows(12)
            .WithRoof(RoofTypeEnum.Hip)
            .WithBackyard(true)
            .WithHeating(HeatingTypeEnum.Gas)
            .WithElectrical(ElectricalPlanEnum.SmartHome)
            .Build();
    }
}