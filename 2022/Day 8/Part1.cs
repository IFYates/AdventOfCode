var lines = System.IO.File.ReadAllLines("Input.txt");

bool IsVisible(int sx, int sy, int dx, int dy)
{
    var v = lines[sy][sx] - '0';
    int x = sx, y = sy;
    while (true)
    {
        x += dx;
        y += dy;
        if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Length)
        {
            break;
        }
        if (lines[y][x] - '0' >= v)
        {
            return false;
        }
    }
    return true;
}

var count = 0;
for (var y = 0; y < lines.Length; ++y)
{
    for (var x = 0; x < lines[0].Length; ++x)
    {
        Console.Write($"> {x},{y}: ");
        if (IsVisible(x, y, 0, -1) // Up
            || IsVisible(x, y, 0, 1) // Down
            || IsVisible(x, y, -1, 0) // Left
            || IsVisible(x, y, 1, 0)) // Right
        {
            Console.WriteLine("Yes");
            ++count;
        }
        else
        {
            Console.WriteLine("No");
        }
    }
}

Console.WriteLine("> " + count);