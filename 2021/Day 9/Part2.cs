static int[][] grid = Console.In.ReadToEnd()
    .Split('\n')
    .Select(r => r.Select(c => c - '0').ToArray())
    .ToArray();

// FLood fill
int getBasinSize(int x, int y)
{
    if (grid[y][x] == 9) return 0;
    grid[y][x] = 9;

    var score = 1;
    if (x > 0) score += getBasinSize(x - 1, y);
    if (x < grid[y].Length - 1) score += getBasinSize(x + 1, y);
    if (y > 0) score += getBasinSize(x, y - 1);
    if (y < grid.Length - 1) score += getBasinSize(x, y + 1);
    return score;
}

var basins = new List<int>();
for (var y = 0; y < grid.Length; ++y)
{
    for (var x = 0; x < grid[y].Length; ++x)
    {
        basins.Add(getBasinSize(x, y));
    }
}

var result = basins.OrderByDescending(v => v).Take(3).Aggregate(1, (a, b) => a * b);
Console.WriteLine("> " + result);