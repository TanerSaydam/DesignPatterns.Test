Console.WriteLine("Interpreter Pattern - Math Expression Example...");

// Context'e değerleri ekle
var context = new Context();
context.SetValue("a", 5);
context.SetValue("b", 3);
context.SetValue("c", 2);

// Expression: (a + b) * c
IExpression expression = new MultiplyExpression(
    new AddExpression(
        new VariableExpression("a"),
        new VariableExpression("b")
    ),
    new VariableExpression("c")
);

Console.WriteLine($"(a + b) * c = {expression.Interpret(context)}"); // (5 + 3) * 2 = 16

Console.ReadLine();

// ---------------- Context ----------------
class Context
{
    private readonly Dictionary<string, double> _vars = new();

    public void SetValue(string name, double value) => _vars[name] = value;
    public double GetValue(string name) => _vars[name];
}

// ---------------- Expression Abstraction ----------------
interface IExpression
{
    double Interpret(Context context);
}

// ---------------- Terminal Expression ----------------
class VariableExpression : IExpression
{
    private readonly string _name;
    public VariableExpression(string name) => _name = name;
    public double Interpret(Context context) => context.GetValue(_name);
}

class NumberExpression : IExpression
{
    private readonly double _value;
    public NumberExpression(double value) => _value = value;
    public double Interpret(Context context) => _value;
}

// ---------------- Non-Terminal Expressions ----------------
class AddExpression : IExpression
{
    private readonly IExpression _left, _right;
    public AddExpression(IExpression left, IExpression right)
    {
        _left = left;
        _right = right;
    }
    public double Interpret(Context context) => _left.Interpret(context) + _right.Interpret(context);
}

class SubtractExpression : IExpression
{
    private readonly IExpression _left, _right;
    public SubtractExpression(IExpression left, IExpression right)
    {
        _left = left;
        _right = right;
    }
    public double Interpret(Context context) => _left.Interpret(context) - _right.Interpret(context);
}

class MultiplyExpression : IExpression
{
    private readonly IExpression _left, _right;
    public MultiplyExpression(IExpression left, IExpression right)
    {
        _left = left;
        _right = right;
    }
    public double Interpret(Context context) => _left.Interpret(context) * _right.Interpret(context);
}

class DivideExpression : IExpression
{
    private readonly IExpression _left, _right;
    public DivideExpression(IExpression left, IExpression right)
    {
        _left = left;
        _right = right;
    }
    public double Interpret(Context context)
    {
        double divisor = _right.Interpret(context);
        if (divisor == 0) throw new DivideByZeroException();
        return _left.Interpret(context) / divisor;
    }
}