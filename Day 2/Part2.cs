var moves = System.IO.File.ReadAllLines("Input.txt")
    .Select(l => { var p = l.Split(' '); return (Action: p[0].ToLower(), Arg: int.Parse(p[1])); })
    .ToArray();

int aim = 0, horizontal = 0, depth = 0;
foreach (var move in moves)
{
    switch (move.Action)
    {
        case "forward":
            horizontal += move.Arg;
            depth += aim * move.Arg;
            break;
        case "down":
            aim += move.Arg;
            break;
        case "up":
            aim -= move.Arg;
            break;
    }
}

//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(moves));
Console.WriteLine($"> H: {horizontal}, D: {depth}");
Console.WriteLine($"> {horizontal * depth}");