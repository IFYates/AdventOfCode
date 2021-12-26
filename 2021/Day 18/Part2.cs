using System.Drawing;

var ln = File.ReadAllText("2021/Day 17/Input.txt");

var m = System.Text.RegularExpressions.Regex.Match(ln, @"^target area: x=(?<x1>\-?\d+)\.\.(?<x2>\-?\d+), y=(?<y1>\-?\d+)\.\.(?<y2>\-?\d+)$");
if (!m.Success) throw new Exception($"Invalid input: {ln}");

int x1 = int.Parse(m.Groups["x1"].Value), x2 = int.Parse(m.Groups["x2"].Value);
int y1 = int.Parse(m.Groups["y1"].Value), y2 = int.Parse(m.Groups["y2"].Value);
var tl = new Point(Math.Min(x1, x2), Math.Min(y1, y2));
var br = new Point(Math.Max(x1, x2), Math.Max(y1, y2));
var targetArea = new Rectangle(tl, new Size(br.X - tl.X + 1, br.Y - tl.Y + 1));

bool run(int velX, int velY, out int maxY)
{
    maxY = 0;
    int posX = 0, posY = 0;
    while (posX <= br.X && posY >= tl.Y)
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
for (var velX = 1; velX <= 1000; ++velX)
{
    for (var velY = -1000; velY <= 1000; ++velY)
    {
        if (run(velX, velY, out var maxY))
        {
            ++result;
            Console.WriteLine(velX + "," + velY);
        }
    }
}
Console.WriteLine("> " + result);