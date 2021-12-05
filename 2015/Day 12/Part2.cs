string stripBadObjects(string str)
{
    int idx;
    while ((idx = str.IndexOf(":\"red\"")) >= 0)
    {
        var start = walk(str, idx, -1);
        var end = walk(str, idx, +1);
        str = str[..start] + str[end..];
    }
    return str;
}
int walk(string str, int pos, int delta)
{
    int depth = delta;
    do
    {
        switch (str[pos])
        {
            case '}':
                if (--depth == 0)
                {
                    return pos + 1;
                }
                break;
            case '{':
                if (++depth == 0)
                {
                    return pos;
                }
                break;
            case '[':
                ++depth;
                break;
            case ']':
                --depth;
                break;
        }

        pos += delta;
    } while (pos > 0 && pos < str.Length);
    if (depth != 0)
    {
        throw new Exception("End of string at depth " + depth);
    }
    return pos;
}

string ln;
long result = 0;
while ((ln = Console.In.ReadLine()) != null)
{
    ln = stripBadObjects(ln);

    var mm = System.Text.RegularExpressions.Regex.Matches(ln, @"-?\d+");
    result = mm.Cast<System.Text.RegularExpressions.Match>().Select(m => long.Parse(m.Value)).Sum();
Console.WriteLine($"> {result}");
}
// >235 <110902