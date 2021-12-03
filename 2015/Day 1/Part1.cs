var instruction = System.IO.File.ReadAllText("Input.txt");
var up = instruction.Count(c => c == '(');
var dn = instruction.Count(c => c == ')');
Console.WriteLine("> " + (up - dn));