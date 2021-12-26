var grid = File.ReadAllLines("2021/Day 15/Input.txt")
    .Where(l => l.Trim().Length > 0)
    .Select(r => r.Trim().Select(c => (byte)(c - '0')).ToArray())
    .ToArray();
var gridX = Enumerable.Range(1, grid.Length * 5).Select(i => new byte[grid[0].Length * 5]).ToArray();

for (var y = 0; y < grid.Length; ++y)
{
    for (var x = 0; x < grid[0].Length; ++x)
    {
        gridX[y][x] = (byte)grid[y][x];
        for (var i = 1; i < 5; ++i)
        {
            gridX[y][(i * grid[0].Length) + x] = (byte)Math.Max(1, (gridX[y][((i - 1) * grid[0].Length) + x] + 1) % 10);
        }
    }
}
for (var y = 0; y < grid.Length; ++y)
{
    for (var x = 0; x < gridX[0].Length; ++x)
    {
        for (var i = 1; i < 5; ++i)
        {
            gridX[(i * grid.Length) + y][x] = (byte)Math.Max(1, (gridX[((i - 1) * grid.Length) + y][x] + 1) % 10);
        }
    }
}
grid = gridX;

var map = new Dictionary<string, PathData>();
var queue = new Queue<(int X, int Y)>();
void score(int x, int y, int dist, int score)
{
    var key = $"{x},{y}";
    map.TryGetValue(key, out var cur);
    ++dist;
    score += gridX[y][x];
    if (score > cur?.score) return;

    map[key] = new PathData(dist, score);
    if (!queue.Any(c => c.X == x && c.Y == y)) queue.Enqueue((x, y));
}
void walk(int x, int y)
{
    if (x == 3 && y == 6) { }
    var cur = map[$"{x},{y}"];
    if (x > 0) score(x - 1, y, cur.dist, cur.score);
    if (x < gridX[y].Length - 1) score(x + 1, y, cur.dist, cur.score);
    if (y > 0) score(x, y - 1, cur.dist, cur.score);
    if (y < gridX.Length - 1) score(x, y + 1, cur.dist, cur.score);
}

map["0,0"] = new PathData(0, 0);
queue.Enqueue((0, 0));
while (queue.TryDequeue(out var coord))
{
    AOC.Helpers.PrintPerSecond("[{0}]", queue.Count);
    walk(coord.X, coord.Y);
}

//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(map));
var goal = $"{grid[0].Length - 1},{grid.Length - 1}";
Console.WriteLine($"> {goal}: " + map[goal].score);
// <2850

record PathData(int dist, int score);
