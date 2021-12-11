List<int[]> grid = new();

string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    grid.Add(ln.Select(c => c - '0').ToArray());
}

void step()
{
    for (var y = 0; y < grid.Count; ++y)
    {
        for (var x = 0; x < grid[y].Length; ++x)
        {
            inc(x, y);
        }
    }
    for (var y = 0; y < grid.Count; ++y)
    {
        for (var x = 0; x < grid[y].Length; ++x)
        {
            if (grid[y][x] > 9)
            {
                grid[y][x] = 0;
            }
        }
    }
}
void inc(int x, int y)
{
    grid[y][x] += 1;
    if (grid[y][x] == 10)
    {
        if (x > 0) inc(x - 1, y);
        if (x < grid[0].Length - 1) inc(x + 1, y);
        if (y > 0) inc(x, y - 1);
        if (y < grid.Count - 1) inc(x, y + 1);
        if (x > 0 && y > 0) inc(x - 1, y - 1);
        if (x > 0 && y < grid.Count - 1) inc(x - 1, y + 1);
        if (x < grid[0].Length - 1 && y > 0) inc(x + 1, y - 1);
        if (x < grid[0].Length - 1 && y < grid.Count - 1) inc(x + 1, y + 1);
    }
}

var steps = 0;
while (grid.Max(r => r.Max()) > 0)
{
    step();
    steps += 1;
}
Console.WriteLine("> " + steps);