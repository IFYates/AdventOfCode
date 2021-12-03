var instruction = System.IO.File.ReadAllText("Input.txt");

var delta = instruction.Select(c => c == '(' ? 1 : -1).ToArray();
var sum = delta.Select((v, i) => delta.Take(i + 1).Sum()).TakeWhile(v => v >= 0).ToArray();
Console.WriteLine("> " + (sum.Length + 1));