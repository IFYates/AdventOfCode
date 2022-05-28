var result = 0;
string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(?<ingredient>\w+): capacity (?<capacity>\-?\d+), durability (?<durability>\-?\d+), flavor (?<flavor>\-?\d+), texture (?<texture>\-?\d+), calories (?<calories>\-?\d+)$");
    if (!m.Success)
    {
        Console.WriteLine("Bad line: " + ln);
        continue;
    }
}

//Console.WriteLine(perm + "> " + System.Text.Json.JsonSerializer.Serialize(values) + " > " + res);
Console.WriteLine("> " + result);