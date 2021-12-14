var str = Console.In.ReadLine();
Console.In.ReadLine();

var rules = new Dictionary<string, string>();
string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(\w+) -> (\w+)$");
    if (!m.Success) { throw new Exception($"Invalid line: {ln}"); }
    rules[m.Groups[1].Value] = m.Groups[2].Value;
}

for (var c = 0; c < 10; ++c)
{
    var next = new StringBuilder();
    for (var i = 0; i < str.Length - 1; ++i)
    {
        var pair = str[i..(i + 2)];
        next.Append(pair[0]);
        if (rules.TryGetValue(pair, out var insert))
        {
            next.Append(insert);
        }
    }
    next.Append(str[^1]);
    str = next.ToString();

    //Console.WriteLine($"> {str} ({str.Length})");
}

var chars = str.GroupBy(c => c);
Console.WriteLine("> " + (chars.Max(g => g.Count()) - chars.Min(g => g.Count())));