var inp = File.ReadAllLines("2021/Day 22/Input.txt");

var cubes = new List<Cube>();

foreach (var ln in inp)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(on|off) x=(?<x1>\-?\d+)\.\.(?<x2>\-?\d+),y=(?<y1>\-?\d+)\.\.(?<y2>\-?\d+),z=(?<z1>\-?\d+)\.\.(?<z2>\-?\d+)$");
    if (!m.Success)
    {
        throw new Exception($"Invalid line: {ln}");
    }

    var x1 = long.Parse(m.Groups["x1"].Value);
    var x2 = long.Parse(m.Groups["x2"].Value);
    var y1 = long.Parse(m.Groups["y1"].Value);
    var y2 = long.Parse(m.Groups["y2"].Value);
    var z1 = long.Parse(m.Groups["z1"].Value);
    var z2 = long.Parse(m.Groups["z2"].Value);
    var cube = new Cube(x1, x2, y1, y2, z1, z2, m.Groups[1].Value == "on");

    foreach (var c in cubes.ToArray())
    {
        var i = c.FindIntersection(cube);
        if (i != null)
        {
            cubes.Add(i);
        }
    }

    if (cube.State)
    {
        cubes.Add(cube);
    }
}

Console.Write("> " + cubes.Sum(c => c.Volume()));
// >53193388187 <1214193203327131

record Cube(long X1, long X2, long Y1, long Y2, long Z1, long Z2, bool State)
{
    public Cube? FindIntersection(Cube c)
    {
        var x1 = Math.Max(c.X1, X1);
        var x2 = Math.Min(c.X2, X2);
        var y1 = Math.Max(c.Y1, Y1);
        var y2 = Math.Min(c.Y2, Y2);
        var z1 = Math.Max(c.Z1, Z1);
        var z2 = Math.Min(c.Z2, Z2);
        return x1 <= x2 && y1 <= y2 && z1 <= z2
            ? new Cube(x1, x2, y1, y2, z1, z2, !State) : null;
    }

    public long Volume()
    {
        return (X2 - X1 + 1) * (Y2 - Y1 + 1) * (Z2 - Z1 + 1) * (State ? 1 : -1);
    }
}