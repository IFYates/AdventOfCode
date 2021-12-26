var inp = new Queue<string>(File.ReadAllLines("2021/Day 13/Input.txt"));
var coords = new List<(int X, int Y)>();

while (inp.TryDequeue(out var ln))
{
    if (ln.Length == 0) { break; }
    var xy = ln.Split(',').Select(int.Parse).ToArray();
    coords.Add((xy[0], xy[1]));
}

while (inp.TryDequeue(out var ln))
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^fold along ([xy])=(\d+)$");
    if (!m.Success) { throw new Exception($"Bad line: {ln}"); }

    var fX = m.Groups[1].Value == "x" ? int.Parse(m.Groups[2].Value) : int.MaxValue / 2;
    var fY = m.Groups[1].Value == "y" ? int.Parse(m.Groups[2].Value) : int.MaxValue / 2;

    coords = coords.Select(p => (p.X > fX ? (2 * fX) - p.X : p.X, p.Y > fY ? (2 * fY) - p.Y : p.Y)).ToList();
}

int maxX = coords.Max(p => p.X), maxY = coords.Max(p => p.Y);
for (var y = 0; y <= maxY; ++y)
{
    for (var x = 0; x <= maxX; ++x)
    {
        Console.Write(coords.Any(p => p.X == x && p.Y == y) ? '█' : ' ');
    }
    Console.WriteLine();
}