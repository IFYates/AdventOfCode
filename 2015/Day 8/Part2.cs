// Gotchas: Json doesn't support \x and doing Replace(@"\x", @"\u00") breaks in cases of "ab\\\x27cd"

string ln;
long result = 0;
while ((ln = Console.In.ReadLine()) != null)
{
    var val = System.Text.Json.JsonSerializer.Serialize(ln)
        .Replace("\\u0022", "\\\"");

    Console.WriteLine($"{ln} -> {val}");
    result += ln.Length - val.Length;
}

Console.WriteLine($"> {result}");