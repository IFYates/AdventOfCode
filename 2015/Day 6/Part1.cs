var lights = Enumerable.Range(0, 1000).Select(i => new bool[1000]).ToArray(); // Easier to use Linq on deep array
var ln = Console.In.ReadLine();
while (ln != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)$");
    if (!m.Success)
    {
        Console.Error.WriteLine("Invalid line: " + ln);
        continue;
    }

    var x1 = int.Parse(m.Groups[2].Value);
    var y1 = int.Parse(m.Groups[3].Value);
    var x2 = int.Parse(m.Groups[4].Value);
    var y2 = int.Parse(m.Groups[5].Value);
    
    Func<bool, bool> fn = null;
    switch (m.Groups[1].Value)
    {
        case "turn on":
            fn = (_) => true;
            break;
        case "turn off":
            fn = (_) => false;
            break;
        case "toggle":
            fn = (s) => !s;
            break;
    }

    for (var x = x1; x <= x2; ++x)
    {
        for (var y = y1; y <= y2; ++y)
        {
            lights[x][y] = fn(lights[x][y]);
        }
    }

    ln = Console.In.ReadLine();
}

Console.WriteLine("> " + lights.Sum(a => a.Count(b => b)));