var moves = System.IO.File.ReadAllLines("Input.txt")
    .Select(l => { var p = l.Split(' '); return (Action: p[0].ToLower(), Arg: int.Parse(p[1])); })
    .ToArray();

var horizontal = moves.Where(m => m.Action == "forward").Sum(m => m.Arg);
var depth = moves.Where(m => m.Action == "down").Sum(m => m.Arg)
    - moves.Where(m => m.Action == "up").Sum(m => m.Arg);

//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(moves));
Console.WriteLine($"> H: {horizontal}, D: {depth}");
Console.WriteLine($"> {horizontal * depth}");