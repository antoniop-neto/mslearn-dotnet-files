using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

var currentDiretory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDiretory, "stores");

var salesTotalDir = Path.Combine(currentDiretory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");


IEnumerable<string> FindFiles(string folderName)
{
  List<string> salesFiles = new List<string>();

  var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

  foreach (var file in foundFiles)
  {
    var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
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
    // Read the contents os the file
    string salesJson = File.ReadAllText(file);

    // Parse the contents as JSON
    SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

    // Add the amount found in the Total field to the salesTotal variable
    salesTotal += data?.Total ?? 0;
  }
  return salesTotal;
}
record SalesData (double Total);
