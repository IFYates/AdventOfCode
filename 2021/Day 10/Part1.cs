string ln;
var result = 0;
while ((ln = Console.In.ReadLine()) != null)
{
    var copy1 = ln;
    while (copy1.Length > 0)
    {
        var copy2 = copy1.Replace("()", "").Replace("[]", "").Replace("{}", "").Replace("<>", "");
        if (copy1.Length == copy2.Length)
        {
            break;
        }
        copy1 = copy2;
    }
    
    var chr = ")]}>".ToDictionary(c => c, copy1.IndexOf).Where(t => t.Value >= 0).OrderBy(t => t.Value).FirstOrDefault().Key;
    if (chr != '\0')
    {
        result += chr switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
        };
    }
}

Console.WriteLine("> " + result);