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

Console.WriteLine($"Executing {year} Day {day} Part {part}...");
Console.WriteLine();

Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, $"{year}/Day {day}");
System.Diagnostics.Process.Start("dotnet-script", $"Part{part}.cs").WaitForExit();