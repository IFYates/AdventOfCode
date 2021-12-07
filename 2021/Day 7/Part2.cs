var pos = Console.In.ReadLine().Split(',').Select(int.Parse).ToList();

static int tri(int n) => (n * (n + 1)) / 2;

var result = int.MaxValue;
for (var target = pos.Min(); target <= pos.Max(); ++target)
{
    var val = pos.Sum(p => tri(Math.Abs(p - target)));
    result = Math.Min(val, result);
}

Console.WriteLine("> " + result);