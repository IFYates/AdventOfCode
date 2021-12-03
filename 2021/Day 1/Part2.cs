var depths = System.IO.File.ReadAllLines("Input.txt")
    .Where(d => d.Length > 0)
    .Select(int.Parse)
    .ToArray();

var count = 0;
var last = 0;
for (var i = 0; i < depths.Length - 2; ++i)
{
    var sum = depths[i] + depths[i + 1] + depths[i + 2];
    if (last > 0 && sum > last)
    {
        ++count;
    }
    last = sum;
}
Console.WriteLine($"> {count}");