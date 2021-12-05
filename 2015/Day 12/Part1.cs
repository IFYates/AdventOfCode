string ln;
long result = 0;
while ((ln = Console.In.ReadLine()) != null)
{
    var mm = System.Text.RegularExpressions.Regex.Matches(ln, @"-?\d+");
    result += mm.Cast<System.Text.RegularExpressions.Match>().Select(m => long.Parse(m.Value)).Sum();
}

Console.WriteLine($"> {result}");