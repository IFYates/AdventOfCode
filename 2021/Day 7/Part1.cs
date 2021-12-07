var pos = Console.In.ReadLine().Split(',').Select(int.Parse).ToList();

//var target = pos.GroupBy(p => p).OrderBy(g => g.Count()).Last().Key;

var result = 999999;
for (var target = pos.Min(); target <= pos.Max(); ++target)
{
    var val = pos.Sum(p => Math.Abs(p - target));
    if (val < result) { result = val; }
}

Console.WriteLine("> " + result);

// <452062