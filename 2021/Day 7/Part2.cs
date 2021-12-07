var pos = Console.In.ReadLine().Split(',').Select(int.Parse).ToList();

var result = 0;
for (var target = pos.Min(); target <= pos.Max(); ++target)
{
    var val = pos.Sum(p => { var n = Math.Abs(p - target); return ((n * n) + n) / 2; });
    if (result == 0 || val < result) { result = val; }
}

Console.WriteLine("> " + result);