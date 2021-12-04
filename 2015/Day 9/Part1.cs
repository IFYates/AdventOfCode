static Dictionary<string, Node> nodes = new();
static Node getNode(string name)
{
    if (!nodes.TryGetValue(name, out var node))
    {
        node = new Node(name);
        nodes[name] = node;
    }
    return node;   
}

record Node(string Name)
{
    public Dictionary<string, int> Distances { get; } = new();
}
class Route
{
    public Node Node { get; init; }
    public int Steps { get; init; }
    public int Distance { get; init; }
    public Route Previous { get; init; }
    public string[] Path { get; init; }

    public Route Map()
    {
        Route shortest = null;
        foreach (var next in nodes.Keys.Where(k => !Path.Contains(k)))
        {
            var newPath = new List<string>(Path);
            newPath.Add(next);

            var r = new Route
            {
                Node = getNode(next),
                Steps = Steps + 1,
                Distance = Distance + Node.Distances[next],
                Previous = this,
                Path = newPath.ToArray()
            };
            var s = r.Map();
            if (shortest == null || s.Distance < shortest.Distance)
            {
                shortest = s;
            }
        }
        return shortest ?? this;
    }
}

var ln = Console.In.ReadLine();
while (ln != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(\w+) to (\w+) = (\d+)$");
    if (!m.Success)
    {
        Console.Error.WriteLine("Bad line: " + ln);
        break;
    }

    var lNode = getNode(m.Groups[1].Value);
    var rNode = getNode(m.Groups[2].Value);
    lNode.Distances[rNode.Name] = int.Parse(m.Groups[3].Value);
    rNode.Distances[lNode.Name] = int.Parse(m.Groups[3].Value);

    ln = Console.In.ReadLine();
}

Route shortest = null;
foreach (var node in nodes.Values)
{
    var r = new Route { Node = node, Path = new[] { node.Name } };
    var s = r.Map();
    if (shortest == null || s.Distance < shortest.Distance)
    {
        shortest = s;
    }
}

//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(nodes));
Console.WriteLine("> " + shortest?.Distance);