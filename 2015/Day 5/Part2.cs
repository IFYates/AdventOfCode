static bool isStringNice(string str)
{
    return System.Text.RegularExpressions.Regex.IsMatch(str, @"(?i)([a-z]{2}).*\1")
        && System.Text.RegularExpressions.Regex.IsMatch(str, @"(?i)([a-z]).\1");
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