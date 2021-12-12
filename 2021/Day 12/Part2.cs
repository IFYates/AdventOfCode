class Cave
{
    public string Name { get; set; }
    public bool IsSmall { get; set; }
    public List<Cave> Paths { get; } = new();
}

Dictionary<string, Cave> Caves { get; } = new();

string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    var p = ln.Split('-');
    if (!Caves.TryGetValue(p[0], out var cave1))
    {
        cave1 = new Cave { Name = p[0], IsSmall = p[0].ToLower() == p[0] };
        Caves[p[0]] = cave1;
    }
    if (!Caves.TryGetValue(p[1], out var cave2))
    {
        cave2 = new Cave { Name = p[1], IsSmall = p[1].ToLower() == p[1] };
        Caves[p[1]] = cave2;
    }
    if (cave1.Name != "end" && cave2.Name != "start")
    {
        cave1.Paths.Add(cave2);
    }
    if (cave2.Name != "end" && cave1.Name != "start")
    {
        cave2.Paths.Add(cave1);
    }
}

static bool IsPathValid(List<Cave> path)
{
    // Each small once, except one can be twice
    var groups = path.Where(c => c.IsSmall).GroupBy(c => c.Name);
    return groups.All(g => g.Count() < 3)
        && groups.Count(g => g.Count() == 2) < 2;
}
List<List<Cave>> Map(Cave cave, List<Cave> path)
{
    var res = new List<List<Cave>>();
    if (cave.Name == "end")
    {
        res.Add(path);
        return res;
    }
    foreach (var c in cave.Paths)
    {
        var next = new List<Cave>(path);
        next.Add(c);
        if (IsPathValid(next))
        {
            res.AddRange(Map(c, next));
        }
    }
    return res;
}
var paths = Map(Caves["start"], new List<Cave>(new[] { Caves["start"] }));

Console.WriteLine("> " + paths.Count);