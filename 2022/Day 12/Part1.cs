using System.Drawing;

var lines = System.IO.File.ReadAllLines("Input.txt");
var y = lines.Select((l, i) => l.Contains('S') ? i : -1).Max();
var x = lines[y].IndexOf('S');

class Step
{
    public int X { get; set; }
    public int Y { get; set; }
    public Step From { get; set; }
    public int Height { get; set; }
    public int Steps { get; set; } = int.MaxValue;
}

var map = new Dictionary<Point, Step>();
var p = new Point(x, y);
map[p] = new Step { X = x, Y = y, Height = (int)'a', Steps = 0 };

var queue = new Queue<Step>();
queue.Enqueue(map[p]);

Step GetStep(int x, int y)
{
    if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Length)
    {
        return null;
    }

    var p = new Point(x, y);
    if (!map.ContainsKey(p))
    {
        var c = lines[y][x];
        if (c == 'E') { c = 'z'; }
        map[p] = new Step { X = p.X, Y = p.Y, Height = (int)c };
    }
    return map[p];
}
void DoStep(Step l, int dx, int dy)
{
    var s = GetStep(l.X + dx, l.Y + dy);
    if (s?.Height <= l.Height + 1 && s?.Steps > l.Steps + 1)
    {
        s.From = l;
        s.Steps = l.Steps + 1;
        queue.Enqueue(s);
    }
}

while (queue.Any())
{
    var l = queue.Dequeue();

    DoStep(l, 0, -1); // Up
    DoStep(l, 0, +1); // Down
    DoStep(l, -1, 0); // Left
    DoStep(l, +1, 0); // Right
}

y = lines.Select((l, i) => l.Contains('E') ? i : -1).Max();
x = lines[y].IndexOf('E');
Console.WriteLine("> " + x + "," + y);
Console.WriteLine("> " + GetStep(x, y).Steps);