var lines = System.IO.File.ReadAllLines("Input.txt");
var i = 0;

// Build stacks
var re = new System.Text.RegularExpressions.Regex(@"\[([A-Z])\]");
var stacks = new List<List<char>>();
for (; i < lines.Length; ++i)
{
    if (lines[i].Length == 0) break;
    var mc = re.Matches(lines[i]);
    foreach (System.Text.RegularExpressions.Match m in mc)
    {
        foreach (var g in m.Groups.Cast<System.Text.RegularExpressions.Group>().Where(g => g.Value.Length == 1))
        {
            var idx = (g.Index - 1) / 4;
            while (idx >= stacks.Count)
            {
                stacks.Add(new List<char>());
            }
            stacks[idx].Add(g.Value[0]);
        }
    }
}
Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(stacks));

// Move items
re = new System.Text.RegularExpressions.Regex(@"^move\s+(?<Count>\d+)\s+from\s+(?<From>\d+)\s+to\s+(?<Dest>\d+)$");
for (; i < lines.Length; ++i)
{
    var m = re.Match(lines[i]);
    if (m.Success)
    {
        Console.WriteLine("> " + lines[i]);

        for (var c = int.Parse(m.Groups["Count"].Value); c > 0; --c)
        {
            var from = int.Parse(m.Groups["From"].Value) - 1;
            var dest = int.Parse(m.Groups["Dest"].Value) - 1;
            var pop = stacks[from][0];
            stacks[from].RemoveAt(0);
            stacks[dest].Insert(0, pop);
        }

        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(stacks));
    }
}

var result = stacks.Select(s => s.Count > 0 ? s[0].ToString() : "");
Console.WriteLine("> " + string.Join("", result));