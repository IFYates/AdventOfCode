var board = Enumerable.Range(1, 1000).Select(i => new int[1000]).ToArray();

var ln = Console.In.ReadLine();
while (ln != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(\d+),(\d+) -> (\d+),(\d+)$");
    if (!m.Success) { throw new InvalidCastException(ln); }

    var x1 = int.Parse(m.Groups[1].Value);
    var y1 = int.Parse(m.Groups[2].Value);
    var x2 = int.Parse(m.Groups[3].Value);
    var y2 = int.Parse(m.Groups[4].Value);
    if (x1 != x2 && y1 != y2)
    {
        Console.WriteLine("Ignore: " + ln);
        ln = Console.In.ReadLine();
        continue;
    }
    if (x1 > x2)
    {
        (x1, x2) = (x2, x1);
    }
    if (y1 > y2)
    {
        (y1, y2) = (y2, y1);
    }

    for (var y = y1; y <= y2; ++y)
    {
        for (var x = x1; x <= x2; ++x)
        {
            board[y][x] += 1;
        }
    }
    
    ln = Console.In.ReadLine();
}

Console.WriteLine("> " + board.Sum(y => y.Count(x => x > 1)));