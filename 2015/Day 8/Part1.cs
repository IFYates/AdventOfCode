// Gotchas: Json doesn't support \x and doing Replace(@"\x", @"\u00") breaks in cases of "ab\\\x27cd"

string ln;
long result = 0;
while ((ln = Console.In.ReadLine()) != null)
{
    var val = "";
    for (var i = 0; i < ln.Length; ++i)
    {
        if (ln[i] == '\\')
        {
            switch (ln[++i])
            {
                case 'x':
                    i += 2;
                    val += '?';
                    break;
                case '\\':
                case '"':
                    val += ln[i];
                    break;
                default:
                    throw new NotImplementedException("Esc: " + ln[i]);
            }
        }
        else
        {
            val += ln[i];
        }
    }
    val = val[1..^1];

    Console.WriteLine($"{ln} -> {val}");
    result += ln.Length - val.Length;
}

Console.WriteLine($"> {result}");
// x 1255, 1342, 1362