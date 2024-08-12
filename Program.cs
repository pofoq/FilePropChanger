// See https://aka.ms/new-console-template for more information
Console.Write("Files path: ");
var path = Console.ReadLine();

Console.Write("Year: ");
if (!int.TryParse(Console.ReadLine(), out int year))
    year = 2021;
Console.Write("Month: ");
if (!int.TryParse(Console.ReadLine(), out int month))
    month = 5;
    Console.Write("Day: ");
if (!int.TryParse(Console.ReadLine(), out int day))
    day = 16;

var d = new DateTime(year, month, day);

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
        if (!files.Any(f => f.FullName == newFileName))
            File.Move(file.FullName, newFileName);
    }
}
else
{
    Console.WriteLine($"Directory is not exists: {path}");
}

Console.ReadKey();

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
