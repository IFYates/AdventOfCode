var props1 = new [] { "capacity", "durability", "flavor", "texture", "calories" };
var props2 = new [] { "capacity", "durability", "flavor", "texture" };
var ingredients = new List<Dictionary<string, int>>();
string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(?<ingredient>\w+): capacity (?<capacity>\-?\d+), durability (?<durability>\-?\d+), flavor (?<flavor>\-?\d+), texture (?<texture>\-?\d+), calories (?<calories>\-?\d+)$");
    if (!m.Success)
    {
        Console.WriteLine("Bad line: " + ln);
        continue;
    }

    ingredients.Add(props1.ToDictionary(p => p, p => int.Parse(m.Groups[p].Value)));
}

static string[] getPermutations(int total, int count)
{
    if (count == 1)
    {
        return new[] { total.ToString() };
    }

    var perms = new List<string>();
    for (var i = 0; i <= total; ++i)
    {
        var perm = i + ",";
        perms.AddRange(getPermutations(total - i, count - 1).Select(p => perm + p));
    }
    return perms.ToArray();
}

var perms = getPermutations(100, ingredients.Count);

var values = new Dictionary<string, int>();
var result = 0;
foreach (var perm in perms)
{
    var spoons = perm.Split(',').Select(int.Parse).ToArray();
    var cals = Enumerable.Range(0, ingredients.Count).Select(i => spoons[i] * ingredients[i]["calories"]).Sum();
    if (cals != 500) { continue; }
    var values = props2.Select(p => Enumerable.Range(0, ingredients.Count).Select(i => spoons[i] * ingredients[i][p]).Sum());
    var res = values.Select(v => Math.Max(0, v)).Aggregate(1, (a, b) => a * b);
    result = Math.Max(result, res);
}

Console.WriteLine("> " + result);