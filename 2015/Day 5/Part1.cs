static string vowels = "aeiou";
static string[] badPairs = new [] { "ab", "cd", "pq", "xy" };
static bool isStringNice(string str)
{
    return str.Count(vowels.Contains) > 2
        && System.Text.RegularExpressions.Regex.IsMatch(str, @"(?i)([a-z])\1")
        && !badPairs.Any(str.Contains);
}

var nice = 0;
var ln = Console.In.ReadLine();
while (ln != null)
{
    if (isStringNice(ln))
    {
        ++nice;
    }

    ln = Console.In.ReadLine();
}

Console.WriteLine("> " + nice);