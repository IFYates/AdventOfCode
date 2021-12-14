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

static void incDictKey<T>(Dictionary<T, long> dict, T key, long add)
{
    if (dict.TryGetValue(key, out var c))
    {
        add += c;
    }
    dict[key] = add;
}

var pairs = new Dictionary<string, long>();
for (var i = 0; i < str.Length - 1; ++i)
{
    incDictKey(pairs, str[i..(i + 2)], 1);
}

Console.WriteLine($"{0,2}: ({pairs.Values.Sum()})");
for (var c = 1; c <= 40; ++c)
{
    var newpairs = new Dictionary<string, long>();
    foreach (var pair in pairs.Keys)
    {
        var x = pairs[pair];
        if (rules.TryGetValue(pair, out var insert))
        {
            incDictKey(newpairs, pair[0] + insert, x);
            incDictKey(newpairs, insert + pair[1], x);
        }
        else
        {
            incDictKey(newpairs, pair, x);
        }
    }
    pairs = newpairs;

    Console.WriteLine($"{c,2}: ({pairs.Values.Sum() + 1})");
    //Console.WriteLine($"> {pairs.Aggregate("", (a, b) => a + $", {b.Key} ({b.Value})")}");
}

var chars = new Dictionary<char, long>();
incDictKey(chars, str[0], 1);
incDictKey(chars, str[^1], 1);
foreach (var pair in pairs)
{
    incDictKey(chars, pair.Key[0], pair.Value);
    incDictKey(chars, pair.Key[1], pair.Value);
}
foreach (var ch in chars)
{
    chars[ch.Key] = ch.Value / 2;
}

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(chars));
Console.WriteLine("> " + (chars.Values.Max() - chars.Values.Min()));