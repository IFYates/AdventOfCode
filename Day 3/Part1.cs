var bins = System.IO.File.ReadAllLines("Input.txt");
var LEN = bins.Max(l => l.Length);

var byPos = bins.SelectMany(l => l.Select((b, i) => (Position: i, Bit: b)))
    .GroupBy(r => r.Position)
    .ToArray();

var gamma = 0;
for (var i = 0; i < LEN; ++i)
{
    if (byPos[i].Count(r => r.Bit == '1') > byPos[i].Count(r => r.Bit == '0'))
    {
        gamma += (int)Math.Pow(2, (LEN - 1) - i);
    }
}

var mask = (int)Math.Pow(2, LEN) - 1;
var epsilon = gamma ^ mask;

//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(g));
Console.WriteLine("Gamma: " + gamma);
Console.WriteLine("Epsilon: " + epsilon);
Console.WriteLine("> " + (gamma * epsilon));
