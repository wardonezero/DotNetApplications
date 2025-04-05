using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

// var salesFiles = FindFiles("stores");
var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

// var salesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");

// var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

// Console.WriteLine(salesData.Total);

// var data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

// File.WriteAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", data.Total.ToString());

// var data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

// File.AppendAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", $"{data.Total}{Environment.NewLine}");

// File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

// foreach (var file in salesFiles)
// {
//     Console.WriteLine(file);
// }

// //.NET exposes the full path to the current directory via the Directory.GetCurrentDirectory method.
// Console.WriteLine(Directory.GetCurrentDirectory());

// //The following code returns the path to the equivalent of the Windows My Documents folder,
// //or the user's HOME directory for any operating system, even if the code is running on Linux:
// string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
// Console.WriteLine(docPath);

// Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");

// Console.WriteLine(Path.Combine("stores","201"));

// Console.WriteLine(Path.GetExtension("sales.json"));

// string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";

// FileInfo info = new FileInfo(fileName);

// Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}");

// bool doesDirectoryExist = Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "stores","201","newDir"));

// Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "stores","201","newDir"));

// File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "greeting.txt"), "Hello World!");

// System.Console.WriteLine( File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json"));

IEnumerable<string> FindFiles(string folderName)
{
    LinkedList<string> salesFiles = new LinkedList<string>();
    // List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        // The file name will contain the full path, so only check the end of it
        if (extension == ".json")
        {
            salesFiles.AddLast(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

class SalesTotal
{
  public double Total { get; set; }
}

record SalesData (double Total);
