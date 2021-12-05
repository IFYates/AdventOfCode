static string lookSay(string str)
{
    var result = "";
    for (var i = 0; i < str.Length; ++i)
    {
        var c = str[i];
        var j = i + 1;
        for (; j < str.Length && str[j] == c; ++j);
        result += $"{j - i}{c}";
        i = j - 1;
    }
    return result;
}

var ln = Console.In.ReadToEnd();
for (var i = 0; i < 40; ++i)
{
    Console.WriteLine($"> {i} ({ln.Length})");
    ln = lookSay(ln);
}

Console.WriteLine($"> {ln.Length}");