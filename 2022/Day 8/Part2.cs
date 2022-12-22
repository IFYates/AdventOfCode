var lines = System.IO.File.ReadAllLines("Input.txt");

int VisibleCount(int sx, int sy, int dx, int dy)
{
    var v = lines[sy][sx] - '0';
    int x = sx, y = sy, c = 0;
    while (true)
    {
        x += dx;
        y += dy;
        if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Length)
        {
            break;
        }

        ++c;
        if (lines[y][x] - '0' >= v)
        {
            break;
        }
    }
    return c;
}

var maxScore = 0;
for (var y = 0; y < lines.Length; ++y)
{
    for (var x = 0; x < lines[0].Length; ++x)
    {
        var up = VisibleCount(x, y, 0, -1); // Up
        var dn = VisibleCount(x, y, 0, 1); // Down
        var lf = VisibleCount(x, y, -1, 0); // Left
        var rg = VisibleCount(x, y, 1, 0); // Right
        var score = up * dn * lf * rg;
        Console.WriteLine($"> {x},{y}: {up},{dn},{lf},{rg}: {score}");
        maxScore = Math.Max(score, maxScore);
    }
}

Console.WriteLine("> " + maxScore);