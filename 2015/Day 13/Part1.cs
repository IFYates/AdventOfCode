class Person
{
    public string Name { get; set; }
    public Dictionary<string, int> NeighbourEffect { get; } = new();
}

static Dictionary<string, Person> people = new();

string ln;
long result = 0;
while ((ln = Console.In.ReadLine()) != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(?<subject>\w+) would (?<dir>gain|lose) (?<amount>\d+) happiness units by sitting next to (?<target>\w+)\.$");
    if (!m.Success)
    {
        Console.WriteLine("Bad line: " + ln);
        continue;
    }

    var subject = m.Groups["subject"].Value;
    var amount = int.Parse(m.Groups["amount"].Value) * (m.Groups["dir"].Value == "lose" ? -1 : 1);
    if (!people.TryGetValue(subject, out var guest))
    {
        guest = new Person { Name = subject };
        people[subject] = guest;
    }
    guest.NeighbourEffect[m.Groups["target"].Value] = amount;
}

void buildList(List<string> nameLists, string list, string[] names)
{
    if (names.Length == 0)
    {
        nameLists.Add(list);
        return;
    }
    foreach (var name in names)
    {
        if (!list.Contains($",{name},"))
        {
            buildList(nameLists, list + name + ",", names.Where(n => n != name).ToArray());
        }
    }
}

List<string> nameLists = new();
buildList(nameLists, ",", people.Keys.ToArray());

foreach (var list in nameLists)
{
    var names = list[1..^1].Split(',');
    names = names.Append(names.First()).ToArray();

    var value = 0;
    for (var i = 0; i < names.Count() - 1; ++i)
    {
        value += people[names[i]].NeighbourEffect[names[i + 1]];
        value += people[names[i + 1]].NeighbourEffect[names[i]];
    }

    result = value > result ? value : result;
}

Console.WriteLine($"> {result}");