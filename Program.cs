using System.IO;
using System.Collections.Generic;
using System.Reflection;

var currentDiretory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDiretory, "stores");
var salesFiles = FindFiles(storesDirectory);

foreach (var file in salesFiles)
{
  Console.WriteLine(file);
}

IEnumerable<string> FindFiles(string folderName)
{
  List<string> salesFiles = new List<string>();

  var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

  foreach (var file in foundFiles)
  {
    var extension = Path.GetExtension(file);
    // The file name will contain the full path, so only check the end of it
    if (extension == ".json")
    {
      salesFiles.Add(file);
    }
  }
  return salesFiles;
}
