using System.Drawing;

var s = new Point(0, 0);
Point H = s;
var tails = new Point[] { s, s, s, s, s, s, s, s, s };

int Delta(int p) => p switch { >0 => +1, <0 => -1, _ => 0 };

var tailPoints = new List<Point>();
tailPoints.Add(tails.Last());
void MoveHead(int x, int y)
{
    int dX = Delta(x), dY = Delta(y);
    while (x != 0 || y != 0)
    {
        H = new Point(H.X + dX, H.Y + dY);
        for (var i = 0; i < tails.Length; ++i)
        {
            MoveTail(i);
        }
        tailPoints.Add(tails.Last());

        x -= dX;
        y -= dY;
    }
}
void MoveTail(int i)
{
    var T = tails[i];
    var S = i > 0 ? tails[i - 1] : H;
    int dX = S.X - T.X, dY = S.Y - T.Y;
    if (Math.Abs(dX) < 2 && Math.Abs(dY) < 2)
    {
        return;
    }
    
    dX = Delta(dX);
    dY = Delta(dY);
    tails[i] = new Point(T.X + dX, T.Y + dY);
}

var lines = System.IO.File.ReadAllLines("Input.txt");
foreach (var ln in lines)
{
    var v = int.Parse(ln[2..]);
    switch (ln[0])
    {
        case 'U':
            MoveHead(0, -v);
            break;
        case 'D':
            MoveHead(0, v);
            break;
        case 'L':
            MoveHead(-v, 0);
            break;
        case 'R':
            MoveHead(v, 0);
            break;
    }
}

Console.WriteLine("> " + tailPoints.Distinct().Count());