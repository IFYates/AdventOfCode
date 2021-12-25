var scores = new List<long>();
var inp = File.ReadAllLines("2021/Day 10/Input.txt");
foreach (var ln in inp)
{
    var copy1 = ln;
    while (copy1.Length > 0)
    {
        var copy2 = copy1.Replace("()", "").Replace("[]", "").Replace("{}", "").Replace("<>", "");
        if (copy1.Length == copy2.Length)
        {
            break;
        }
        copy1 = copy2;
    }
    
    if (")]}>".Any(copy1.Contains))
    {
        continue;
    }

    scores.Add(copy1.Reverse().Aggregate(0L, (a, b) =>
        (a * 5) + (b switch
        {
            '(' => 1,
            '[' => 2,
            '{' => 3,
            '<' => 4,
        })));
}

scores.Sort();
var result = scores[scores.Count / 2];

Console.WriteLine("> " + result);
// >617930596 >622536213 >722146034