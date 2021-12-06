var fish = Console.In.ReadLine().Split(',').Select(int.Parse).ToList();

for (var day = 1; day <= 80; ++day)
{
    var fishCount = fish.Count;
    for (var i = 0; i < fishCount; ++i)
    {
        if (fish[i] == 0)
        {
            fish[i] = 7;
            fish.Add(8);
        }
        fish[i] -= 1;
    }
    //Console.WriteLine($"Day {day}: " + System.Text.Json.JsonSerializer.Serialize(fish));
}

Console.WriteLine("> " + fish.Count);