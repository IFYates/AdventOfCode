var fish = Console.In.ReadLine().Split(',').Select(int.Parse).GroupBy(i => i).ToDictionary(g => g.Key, g => (long)g.Count());

for (short day = 1; day <= 256; ++day)
{
    fish = fish.ToDictionary(d => d.Key - 1, d => d.Value);
    if (fish.ContainsKey(-1))
    {
        fish[8] = fish[-1];
        fish[6] = fish[-1] + (fish.TryGetValue(6, out var six) ? six : 0);
        fish.Remove(-1);
    }
}

Console.WriteLine("> " + fish.Values.Sum());