var lines = System.IO.File.ReadAllLines("Input.txt");
var cycles = new List<int>();
var x = 1;
foreach (var ln in lines)
{
    cycles.Add(x);
    if (ln.StartsWith("addx "))
    {
        cycles.Add(x);
        x += int.Parse(ln[5..]);
    }
}
cycles.Add(x);

var result = 0;
for (var i = 20; i < cycles.Count; i += 40)
{
    Console.WriteLine($"{i}: " + cycles[i - 1]);
    result += i * cycles[i - 1];
}
Console.WriteLine("> " + result);