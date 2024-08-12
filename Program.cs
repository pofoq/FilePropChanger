// See https://aka.ms/new-console-template for more information
Console.Write("Files path: ");
var path = Console.ReadLine();

Console.Write("Year: ");
var year = int.Parse(Console.ReadLine());
Console.Write("Month: ");
var moth = int.Parse(Console.ReadLine());
Console.Write("Day: ");
var day = int.Parse(Console.ReadLine());

var d = new DateTime(year, moth, day);

var dirInfo = new DirectoryInfo(path);

if (dirInfo.Exists)
{
    var files = dirInfo.GetFiles();
    var fileNum = 0;
    foreach (var file in files.OrderBy(f => f.CreationTime))
    {
        fileNum++;
        d = d.Date.Add(file.CreationTime.TimeOfDay);
        file.CreationTime = d;
        file.LastWriteTime = d;
        file.LastAccessTime = d;
        var newFileName = Path.Combine(path, GetFileName(fileNum) + file.Extension);
        File.Move(file.FullName, newFileName);
    }
}
else
{
    Console.WriteLine($"Directory is not exists: {path}");
}

string GetFileName(int fileNum)
{
    var fileNumStr = string.Empty;

    fileNumStr = fileNum.ToString().Length switch
    {
        1 => $"00{fileNum}",
        2 => $"0{fileNum}",
        _ => $"{fileNum}",
    };

    return $"Image {fileNumStr}";
}
