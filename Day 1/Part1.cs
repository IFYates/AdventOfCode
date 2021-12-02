var depths = System.IO.File.ReadAllLines("Input.txt")
    .Where(d => d.Length > 0)
    .Select(int.Parse);

var count = 0;
var last = 0;
foreach (var depth in depths)
{
    if (last > 0 && depth > last)
    {
        ++count;
    }
    last = depth;
}
Console.WriteLine($"> {count}");