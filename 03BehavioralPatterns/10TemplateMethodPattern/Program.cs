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
