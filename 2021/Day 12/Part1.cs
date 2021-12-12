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
    cave1.Paths.Add(cave2);
    cave2.Paths.Add(cave1);
}

List<string> Map(Cave cave, string path)
{
    var res = new List<string>();
    if (cave.Name == "end")
    {
        res.Add(path);
        return res;
    }
    foreach (var c in cave.Paths)
    {
        if (!c.IsSmall || !path.Contains($",{c.Name},"))
        {
            var next = path + c.Name + ",";
            res.AddRange(Map(c, next));
        }
    }
    return res;
}
var paths = Map(Caves["start"], ",start,");

Console.WriteLine("> " + paths.Count);