using System.Drawing;

var s = new Point(0, 0);
Point H = s, T = s;

int Delta(int p) => p switch { >0 => +1, <0 => -1, _ => 0 };

var tailPoints = new List<Point>();
tailPoints.Add(T);
void MoveHead(int x, int y)
{
    int dX = Delta(x), dY = Delta(y);
    while (x != 0 || y != 0)
    {
        H = new Point(H.X + dX, H.Y + dY);
        MoveTail();

        x -= dX;
        y -= dY;
    }
}
void MoveTail()
{
    int dX = H.X - T.X, dY = H.Y - T.Y;
    if (Math.Abs(dX) < 2 && Math.Abs(dY) < 2)
    {
        return;
    }
    
    dX = Delta(dX);
    dY = Delta(dY);
    T = new Point(T.X + dX, T.Y + dY);
    tailPoints.Add(T);
}

var lines = System.IO.File.ReadAllLines("Input.txt");
foreach (var ln in lines)
{
    //Console.WriteLine(">> " + ln);

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