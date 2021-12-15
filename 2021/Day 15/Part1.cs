var grid = Console.In.ReadToEnd()
    .Split('\n')
    .Where(l => l.Trim().Length > 0)
    .Select(r => r.Trim().Select(c => c - '0').ToArray())
    .ToArray();
var goal = $"{grid[0].Length - 1},{grid.Length - 1}";

record PathData ( int dist, int score );
var map = new Dictionary<string, PathData>();
void score(int x, int y, int dist, int score)
{
    var key = $"{x},{y}";
    map.TryGetValue(key, out var cur);
    ++dist;
    score += grid[y][x];
    if (score > cur?.score) return;

    map[key] = new PathData(dist, score);
}
void walk(int x, int y)
{
    var cur = map[$"{x},{y}"];
    if (x > 0) score(x - 1, y, cur.dist, cur.score);
    if (x < grid[y].Length - 1) score(x + 1, y, cur.dist, cur.score);
    if (y > 0) score(x, y - 1, cur.dist, cur.score);
    if (y < grid.Length - 1) score(x, y + 1, cur.dist, cur.score);
}

map["0,0"] = new PathData(0, 0);
for (var y = 0; y < grid.Length; ++y)
{
    for (var x = 0; x < grid[0].Length; ++x)
    {
        walk(x, y);
    }
}

//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(map));
Console.WriteLine($"> {goal}: " + (map[goal].score - 1));
// =441