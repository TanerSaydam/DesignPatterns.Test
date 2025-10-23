Console.WriteLine("Visitor Pattern - Document Export Example...");

var document = new List<IDocumentPart>
{
    new PlainText("Hello World!"),
    new BoldText("This is bold."),
    new Hyperlink("Click here", "https://example.com")
};

var htmlExporter = new HtmlExporter();
var markdownExporter = new MarkdownExporter();

Console.WriteLine("📄 HTML Export:");
foreach (var part in document)
    part.Accept(htmlExporter);

Console.WriteLine("\n📄 Markdown Export:");
foreach (var part in document)
    part.Accept(markdownExporter);

Console.ReadLine();

// ----- Element (Visitable) -----
interface IDocumentPart
{
    void Accept(IDocumentVisitor visitor);
}

// ----- Concrete Elements -----
class PlainText : IDocumentPart
{
    public string Text { get; }
    public PlainText(string text) => Text = text;

    public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
}

class BoldText : IDocumentPart
{
    public string Text { get; }
    public BoldText(string text) => Text = text;

    public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
}

class Hyperlink : IDocumentPart
{
    public string Text { get; }
    public string Url { get; }
    public Hyperlink(string text, string url)
    {
        Text = text;
        Url = url;
    }

    public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
}

// ----- Visitor Interface -----
interface IDocumentVisitor
{
    void Visit(PlainText part);
    void Visit(BoldText part);
    void Visit(Hyperlink part);
}

// ----- Concrete Visitors -----
class HtmlExporter : IDocumentVisitor
{
    public void Visit(PlainText part) => Console.WriteLine($"<p>{part.Text}</p>");
    public void Visit(BoldText part) => Console.WriteLine($"<b>{part.Text}</b>");
    public void Visit(Hyperlink part) => Console.WriteLine($"<a href='{part.Url}'>{part.Text}</a>");
}

class MarkdownExporter : IDocumentVisitor
{
    public void Visit(PlainText part) => Console.WriteLine(part.Text);
    public void Visit(BoldText part) => Console.WriteLine($"**{part.Text}**");
    public void Visit(Hyperlink part) => Console.WriteLine($"[{part.Text}]({part.Url})");
}
