# Template Method Pattern

Template Method Pattern, bir algoritmanın **iskele yapısını (template)** tanımlayıp  
bazı adımlarının alt class’lar tarafından **özelleştirilmesine** izin veren behavioral pattern’dir.  

Bu sayede ana algoritmanın akışı sabit kalır,  
ancak alt class’lar yalnızca değişmesi gereken adımları kendi ihtiyaçlarına göre yeniden tanımlar.

## Gerçek Hayat Analojisi

Bir **veri dışa aktarma (data export)** sürecini düşün 💾  
- Her format (Excel, PDF, CSV vb.) için veri çekilir, işlenir ve kaydedilir.  
- Tüm süreç aynı sırayı izler, fakat her formatta adımlar farklı şekilde yapılır.  

Yani:
- **DataExporter** → şablonun (template) belirlendiği abstract class  
- **ExcelExporter / PdfExporter** → adımların nasıl uygulanacağını belirleyen alt class’lar  

Ana akış (`ExportData`) değişmez ama alt adımlar (`FetchData`, `ProcessData`, `SaveFile`)  
her alt class’ta farklı şekilde uygulanır.

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Template Method Pattern");

var excelExporter = new ExcelExporter();
var pdfExporter = new PdfExporter();

excelExporter.ExportData();
Console.WriteLine();
pdfExporter.ExportData();

Console.ReadLine();

// ----- Abstract Class (Template) -----
abstract class DataExporter
{
    // Template Method
    public void ExportData()
    {
        FetchData();
        ProcessData();
        SaveFile();
        Console.WriteLine("✅ Export completed.\n");
    }

    protected abstract void FetchData();
    protected abstract void ProcessData();
    protected abstract void SaveFile();
}

// ----- Concrete Classes -----
class ExcelExporter : DataExporter
{
    protected override void FetchData()
    {
        Console.WriteLine("📊 Fetching data for Excel...");
    }

    protected override void ProcessData()
    {
        Console.WriteLine("🔢 Formatting data as Excel sheet...");
    }

    protected override void SaveFile()
    {
        Console.WriteLine("💾 Saving Excel file (report.xlsx)...");
    }
}

class PdfExporter : DataExporter
{
    protected override void FetchData()
    {
        Console.WriteLine("📄 Fetching data for PDF...");
    }

    protected override void ProcessData()
    {
        Console.WriteLine("🖨 Converting data into PDF layout...");
    }

    protected override void SaveFile()
    {
        Console.WriteLine("💾 Saving PDF file (report.pdf)...");
    }
}
```

## Önemli Bilgi

Template Method Pattern şu durumlarda kullanılır:
- Bir sürecin **genel akışı sabit**, ancak bazı adımları farklıysa,  
- Alt class’ların **aynı iskeleti koruyarak** özelleştirilmiş davranış eklemesi gerekiyorsa,  
- Kod tekrarını önlemek ve süreç kontrolünü merkezileştirmek istiyorsan.  

Bu pattern:
✅ Kodun tutarlılığını sağlar  
✅ Tekrarlayan işlemleri soyutlayarak sadeleştirir  
✅ Yeni türlerin yalnızca özelleşmiş kısımlarını yazmasına olanak tanır  
