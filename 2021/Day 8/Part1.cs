int result = 0;
string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    var str = ln.Split('|')[1].Trim().Split(' ');
    result += str.Count(s => s.Length is 2 or 3 or 4 or 7);
}

Console.WriteLine("> " + result);