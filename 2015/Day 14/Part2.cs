class Reindeer
{
    public string Name { get; set; }
    public int Speed { get; set; }
    public int FlySecs { get; set; }
    public int RestSecs { get; set; }
    public int Points { get; set; }
    
    public int DistanceAt(int secs)
    {
        var dist = 0;
        while (secs > 0)
        {
            int flyFor = Math.Min(secs, FlySecs);
            secs -= flyFor;
            dist += Speed * flyFor;

            int restFor = Math.Min(secs, RestSecs);
            secs -= restFor;
        }
        return dist;
    }
}

static Dictionary<string, Reindeer> reindeers = new();

string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(?<reindeer>\w+) can fly (?<speed>\d+) km/s for (?<flysecs>\d+) seconds, but then must rest for (?<restsecs>\d+) seconds\.$");
    if (!m.Success)
    {
        Console.WriteLine("Bad line: " + ln);
        continue;
    }

    var name = m.Groups["reindeer"].Value;
    if (!reindeers.TryGetValue(name, out var reindeer))
    {
        reindeer = new Reindeer
        {
            Name = name
        };
        reindeers[name] = reindeer;
    }
    reindeer.Speed = int.Parse(m.Groups["speed"].Value);
    reindeer.FlySecs = int.Parse(m.Groups["flysecs"].Value);
    reindeer.RestSecs = int.Parse(m.Groups["restsecs"].Value);
}

for (var i = 1; i <= 2503; ++i)
{
    int farthest = 0;
    var leaders = new List<Reindeer>();
    foreach (var reindeer in reindeers.Values)
    {
        var dist = reindeer.DistanceAt(i);
        if (dist > farthest)
        {
            farthest = dist;
            leaders.Clear();
        }
        if (farthest == dist)
        {
            leaders.Add(reindeer);
        }
    }
    foreach (var reindeer in leaders)
    {
        reindeer.Points += 1;
    }
}
Console.WriteLine("> " + reindeers.Values.OrderBy(r => r.Points).Last().Points);