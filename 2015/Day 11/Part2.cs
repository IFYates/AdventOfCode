static bool passReq1(string str)
{
    for (var i = 0; i < str.Length - 2; ++i)
    {
        var c1 = str[i];
        var c2 = str[i + 1];
        var c3 = str[i + 2];
        if ((int)c3 == ((int)c2 + 1) && (int)c2 == ((int)c1 + 1))
        {
            return true;
        }
    }
    return false;
}
static bool passReq2(string str)
{
    return !str.Any(c => c is 'i' or 'o' or 'l');
}
static bool passReq3(string str)
{
    return System.Text.RegularExpressions.Regex.IsMatch(str, @"([a-z])\1.*([a-z])\2");
}
static bool isValid(string str) => passReq1(str) && passReq2(str) && passReq3(str);
static string nextPassword(string str, int pos)
{
    var chars = str.ToArray();
    if (chars[pos] == 'z')
    {
        chars[pos] = 'a';
        str = string.Join("", chars);
        return nextPassword(str, pos - 1);
    }
    chars[pos] = (char)((int)chars[pos] + 1);
    return string.Join("", chars);
}

var ln = Console.In.ReadToEnd();
ln = nextPassword(ln, ln.Length - 1);
while (!isValid(ln))
{
    //Console.WriteLine($"{ln} - {passReq1(ln)} - {passReq2(ln)} - {passReq3(ln)}");
    ln = nextPassword(ln, ln.Length - 1);
}

Console.WriteLine($"> {ln}");