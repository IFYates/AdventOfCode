var coords = new List<(int x, int y)>();
string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    if (ln.Length == 0) { break; }
    var xy = ln.Split(',').Select(int.Parse).ToArray();
    coords.Add((xy[0], xy[1]));
}

var maxX = coords.Max(c => c.x) + 1;
var maxY = coords.Max(c => c.y) + 1;
var grid = Enumerable.Range(0, maxY)
    .Select(y => Enumerable.Range(0, maxX).Select(x => false).ToArray()).ToArray();
foreach (var coord in coords)
{
    grid[coord.y][coord.x] = true;
}

void print()
{
    for (var y = 0; y < maxY; ++y)
    {
        for (var x = 0; x < maxX; ++x)
        {
            Console.Write(grid[y][x] ? '#' : '.');
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}
//print();

while ((ln = Console.In.ReadLine()) != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^fold along ([xy])=(\d+)$");
    if (!m.Success) { throw new Exception($"Bad line: {ln}"); }

    if (m.Groups[1].Value == "y")
    {
        var v = int.Parse(m.Groups[2].Value);
        for (var y = 0; y < v; ++y)
        {
            for (var x = 0; x < maxX; ++x)
            {
                grid[y][x] |= grid[maxY - y - 1][x];
                grid[maxY - y - 1][x] = false;
            }
        }
        maxY = v;
    }
    else
    {
        var v = int.Parse(m.Groups[2].Value);
        for (var x = 0; x < v; ++x)
        {
            for (var y = 0; y < maxY; ++y)
            {
                grid[y][x] |= grid[y][maxX - x - 1];
                grid[y][maxX - x - 1] = false;
            }
        }
        maxX = v;
    }
    break;
}
//print();

var result = grid.Sum(r => r.Count(c => c));
Console.WriteLine("> " + result);
// >636