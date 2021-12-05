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

    var yStep = y1 < y2 ? 1 : y1 > y2 ? -1 : 0;
    var xStep = x1 < x2 ? 1 : x1 > x2 ? -1 : 0;
    for (int y = y1, x = x1; (yStep == 0 || y - yStep != y2) && (xStep == 0 || x - xStep != x2); y += yStep, x += xStep)
    {
        board[y][x] += 1;
    }
    
    ln = Console.In.ReadLine();
}

Console.WriteLine("> " + board.Sum(y => y.Count(x => x > 1)));