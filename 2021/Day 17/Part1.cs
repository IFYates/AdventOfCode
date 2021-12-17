using System.Drawing;

var ln = File.ReadAllText("2021/Day 17/Input.txt");

var m = System.Text.RegularExpressions.Regex.Match(ln, @"^target area: x=(?<x1>\-?\d+)\.\.(?<x2>\-?\d+), y=(?<y1>\-?\d+)\.\.(?<y2>\-?\d+)$");
if (!m.Success) throw new Exception($"Invalid input: {ln}");

var tl = new Point(int.Parse(m.Groups["x1"].Value), int.Parse(m.Groups["y1"].Value));
var br = new Point(int.Parse(m.Groups["x2"].Value), int.Parse(m.Groups["y2"].Value));
var targetArea = new Rectangle(tl, new Size(br.X - tl.X + 1, br.Y - tl.Y + 1));

bool run(int velX, int velY, out int maxY)
{
    maxY = 0;
    int posX = 0, posY = 0;
    while (posX <= br.X && posY >= br.Y)
    {
        posX += velX;
        posY += velY;
        velX = velX > 0 ? Math.Max(0, velX - 1) : Math.Min(0, velX + 1);
        velY = velY - 1;
        maxY = Math.Max(maxY, posY);

        if (targetArea.Contains(posX, posY))
        {
            return true;
        }
    }
    return false;
}

var result = 0;
foreach (var velX in Enumerable.Range(1, 100))
{
    foreach (var velY in Enumerable.Range(1, 100))
    {
        if (run(velX, velY, out var maxY))
        {
            Console.WriteLine($"{velX},{velY} = {maxY}");
            result = Math.Max(result, maxY);
        }
    }
}
Console.WriteLine("> " + result);