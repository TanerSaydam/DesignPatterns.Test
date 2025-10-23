# Visitor Pattern

Visitor Pattern, mevcut class yapısını değiştirmeden  
object’ler üzerinde **yeni işlemler (operasyonlar)** tanımlamayı sağlayan behavioral pattern’dir.  

Bu pattern sayesinde “iş mantığı” ile “veri yapısı” birbirinden ayrılır.  
Yeni davranış eklemek için class’ları değiştirmek gerekmez,  
sadece yeni bir **Visitor** eklenir.

## Gerçek Hayat Analojisi

Bir **belge (document)** düşün 📄  
Belgede farklı içerik türleri vardır: düz metin, kalın metin, bağlantılar vb.  
Bu belgenin HTML veya Markdown gibi farklı formatlarda dışa aktarılması gerekir.  

Ancak her formatta (HTML, Markdown, PDF…) belgeyi farklı şekilde işlemek gerekir.  
Yeni bir export tipi geldiğinde document class’larını değiştirmek istemezsin — sadece yeni bir ziyaretçi (Visitor) eklersin.  

Yani:
- **IDocumentPart** → belge içindeki parça (PlainText, BoldText, Hyperlink)  
- **IDocumentVisitor** → ziyaretçi (işlem tanımlayıcı)  
- **HtmlExporter / MarkdownExporter** → farklı davranışları uygulayan ziyaretçiler  

## Program.cs (örnek kullanım)

```csharp
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

```

## Önemli Bilgi

Visitor Pattern şu durumlarda kullanılır:
- Nesne yapısı sabit, ancak bu yapı üzerinde yapılacak işlemler değişkense,  
- Class’ları değiştirmeden yeni davranışlar eklemek istiyorsan,  
- Farklı türde object’lere göre özel davranışlar tanımlamak gerekiyorsa.  

✅ Yeni Visitor eklemek kolaydır (Open/Closed Principle’a uygundur).  
✅ Veri yapısını değiştirmeden yeni işlemler eklenebilir.  
⚠️ Ancak yeni element türü eklersen, tüm Visitor’lar güncellenmelidir.

Bu pattern özellikle:
- **Document export** sistemlerinde  
- **Syntax tree işlemlerinde (compiler/interpreter)**  
- **Raporlama ve analiz sistemlerinde** sıkça kullanılır.  
