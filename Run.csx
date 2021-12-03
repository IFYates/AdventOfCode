// Args: year, day, part
// e.g., 2015 1 1
if (Args.Count() != 3
    || !int.TryParse(Args[0], out var year)
    || !int.TryParse(Args[1], out var day)
    || !int.TryParse(Args[2], out var part))
{
    Console.Error.WriteLine("You must supply arguments as follows:");
    Console.Error.WriteLine("    [year]  The year to execute");
    Console.Error.WriteLine("    [day]   The day to execute");
    Console.Error.WriteLine("    [part]  The part number to execute");
    Console.Error.WriteLine("");
    Console.Error.WriteLine("Example: 2015 01 1");
    return;
}

if (!Directory.Exists($"{year}"))
{
    Console.Error.WriteLine($"Unknown year: {year}");
    return;
}
if (!Directory.Exists($"{year}/Day {day}"))
{
    Console.Error.WriteLine($"Unknown date: {year} Day {day}");
    return;
}
if (!File.Exists($"{year}/Day {day}/Part{part}.cs"))
{
    Console.Error.WriteLine($"Unknown challenge part: Part {part} of {year} Day {day}");
    return;
}

// Pipe input in to stdin
string input = null;
if (File.Exists($"{year}/Day {day}/Input{part}.txt"))
{
    input = File.ReadAllText($"{year}/Day {day}/Input{part}.txt");
}
else if (File.Exists($"{year}/Day {day}/Input.txt"))
{
    input = File.ReadAllText($"{year}/Day {day}/Input.txt");
}

Console.WriteLine($"Executing {year} Day {day} Part {part}...");
Console.WriteLine();

Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, $"{year}/Day {day}");
var process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
{
    FileName = "dotnet-script",
    Arguments = $"Part{part}.cs",
    RedirectStandardInput = input?.Length > 0
});
if (input?.Length > 0)
{
    process.StandardInput.Write(input);
    process.StandardInput.Close();
}
process.WaitForExit();