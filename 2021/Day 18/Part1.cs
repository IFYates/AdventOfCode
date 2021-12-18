using System.Text;
using System.Text.RegularExpressions;

var inp = File.ReadAllLines("2021/Day 18/Input.txt");

var data = new StringBuilder();
foreach (var ln in inp)
{
    data = new StringBuilder(data.Length == 0 ? ln : $"[{data},{ln}]");
    //Console.WriteLine(data.ToString());
    while (process(data))
    {
        //Console.WriteLine(data.ToString());
    }
}

bool process(StringBuilder ln)
{
    var depth = 0;
    for (var i = 0; i < ln.Length; ++i)
    {
        if (ln[i] == '[')
        {
            if (depth == 4)
            {
                var start = i++;
                var lft = readNum(ln, ref i);
                ++i;
                var rgt = readNum(ln, ref i);
                ln.Remove(start, i - start + 1);
                ln.Insert(start, '0');

                var num = findNum(ln, start, +1);
                if (num.start > -1)
                {
                    i = num.start;
                    var val = readNum(ln, ref i);
                    ln.Remove(num.start, num.len);
                    ln.Insert(num.start, val + rgt);
                }

                num = findNum(ln, start, -1);
                if (num.start > -1)
                {
                    i = num.start;
                    var val = readNum(ln, ref i);
                    ln.Remove(num.start, num.len);
                    ln.Insert(num.start, val + lft);
                }

                return true;
            }
            ++depth;
        }
        else if (ln[i] == ']')
        {
            --depth;
        }
    }
    for (var i = 0; i < ln.Length; ++i)
    {
        if (ln[i] is >= '0' and <= '9')
        {
            var start = i;
            var num = readNum(ln, ref i);
            if (num > 9)
            {
                ln.Remove(start, i - start);
                ln.Insert(start, $"[{num / 2},{num - (num / 2)}]");
                return true;
            }
        }
    }
    return false;
}
int readNum(StringBuilder ln, ref int pos)
{
    var value = 0;
    while (ln[pos] is >= '0' and <= '9')
    {
        value *= 10;
        value += ln[pos] - '0';
        ++pos;
    }
    return value;
}
(int start, int len) findNum(StringBuilder ln, int pos, int dir)
{
    pos += dir;
    while (ln[pos] is < '0' or > '9')
    {
        pos += dir;
        if (pos < 0 || pos >= ln.Length) return (-1, -1);
    }
    var a = pos;
    while (ln[pos] is >= '0' and <= '9')
    {
        pos += dir;
    }
    return dir < 0
        ? (pos + 1, a - pos)
        : (a, pos - a);
}

Console.WriteLine(data.ToString());

var str = data.ToString();
while (true)
{
    var m = Regex.Match(str, @"\[(\d+),(\d+)\]");
    if (!m.Success) break;

    var val = (int.Parse(m.Groups[1].Value) * 3)
        + (int.Parse(m.Groups[2].Value) * 2);
    str = str.Replace(m.Value, val.ToString());
}

Console.WriteLine("> " + str);
