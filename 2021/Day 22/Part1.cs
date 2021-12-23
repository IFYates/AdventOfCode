using static AOC.Helpers;

var inp = File.ReadAllLines("2021/Day 22/Input.txt");
var space = ArrayOf(101, () => ArrayOf(101, () => ArrayOf(101, () => false)));
foreach (var ln in inp)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(on|off) x=(?<x1>\-?\d+)\.\.(?<x2>\-?\d+),y=(?<y1>\-?\d+)\.\.(?<y2>\-?\d+),z=(?<z1>\-?\d+)\.\.(?<z2>\-?\d+)$");
    if (!m.Success)
    {
        throw new Exception($"Invalid line: {ln}");
    }

    var val = m.Groups[1].Value == "on";

    var x1 = int.Parse(m.Groups["x1"].Value) + 50;
    var x2 = int.Parse(m.Groups["x2"].Value) + 50;
    var y1 = int.Parse(m.Groups["y1"].Value) + 50;
    var y2 = int.Parse(m.Groups["y2"].Value) + 50;
    var z1 = int.Parse(m.Groups["z1"].Value) + 50;
    var z2 = int.Parse(m.Groups["z2"].Value) + 50;
    if (x1 is < 0 or > 100 || x2 is < 0 or > 100 || y1 is < 0 or > 100 || y2 is < 0 or > 100 || z1 is < 0 or > 100 || z2 is < 0 or > 100) { continue; }

    for (var z = z1; z <= z2; ++z)
    {
        for (var y = y1; y <= y2; ++y)
        {
            for (var x = x1; x <= x2; ++x)
            {
                space[z][y][x] = val;
            }
        }
    }
}

var result = space.Sum(z => z.Sum(y => y.Count(x => x)));
Console.Write("> " + result);