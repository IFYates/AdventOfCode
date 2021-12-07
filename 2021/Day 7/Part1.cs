var pos = Console.In.ReadLine().Split(',').Select(int.Parse).ToList();

var result = int.MaxValue;
for (var target = pos.Min(); target <= pos.Max(); ++target)
{
    var val = pos.Sum(p => Math.Abs(p - target));
    result = Math.Min(val, result);
}

Console.WriteLine("> " + result);

// <452062