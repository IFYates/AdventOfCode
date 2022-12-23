var lines = System.IO.File.ReadAllLines("Input.txt");

int x = 1, c = 0;
var screen = "";
void Tick()
{
    var h = c++ % 40;
    if (c > 1 && h == 0) screen += "\n";
    screen += Math.Abs(h - x) <= 1 ? "#" : ".";
}

foreach (var ln in lines)
{
    Tick();
    
    if (ln.StartsWith("addx "))
    {
        Tick();
        x += int.Parse(ln[5..]);
    }
}
Console.WriteLine(screen);