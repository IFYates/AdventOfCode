var grid = Console.In.ReadToEnd()
    .Split('\n')
    .Select(r => r.Select(c => c - '0').ToArray())
    .ToArray();

var lows = new List<int>();
for (var y = 0; y < grid.Length; ++y)
{
    for (var x = 0; x < grid[y].Length; ++x)
    {
        var h = grid[y][x];
        var score = 0;
        if (x == 0 || grid[y][x - 1] > h) ++score;
        if (x == grid[y].Length - 1 || grid[y][x + 1] > h) ++score;
        if (y == 0 || grid[y - 1][x] > h) ++score;
        if (y == grid.Length - 1 || grid[y + 1][x] > h) ++score;
        if (score == 4)
        {
            lows.Add(h);
        }
    }
}

var result = lows.Select(v => v + 1).Sum();

Console.WriteLine("> " + result);