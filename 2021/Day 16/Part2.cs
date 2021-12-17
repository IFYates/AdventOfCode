using System.Text;

var ln = File.ReadAllText("Input.txt");

int buff = 0, bufflen = 0, pos = 0;
int take(int len)
{
    while (len > bufflen)
    {
        var n = ln[pos++] - '0';
        if (n > 9) n -= 7;
        bufflen += 4;
        buff = (buff << 4) + n;
    }

    var offset = bufflen - len;
    var res = buff >> offset;
    bufflen -= len;
    buff -= res << offset;
    return res;
}

long parsePacket()
{
    // Version
    var ver = take(3);

    // Type
    var type = take(3);

    // Literal
    if (type == 4)
    {
        bool next = true;
        long value = 0;
        while (next)
        {
            next = take(1) == 1;
            value = (value << 4) + take(4);
        }
        return value;
    }

    // Get sub values
    var values = new List<long>();
    var typelen = take(1);
    if (typelen == 0)
    {
        var len = take(15);
        var fin = (len / 4) + pos;
        while (pos < fin)
        {
            values.Add(parsePacket());
        }
    }
    else
    {
        var count = take(11);
        while (count-- > 0)
        {
            values.Add(parsePacket());
        }
    }

    return type switch
    {
        0 => values.Sum(),
        1 => values.Aggregate(1L, (a, b) => a * b),
        2 => values.Min(),
        3 => values.Max(),
        5 => values.First() > values.Last() ? 1 : 0,
        6 => values.First() < values.Last() ? 1 : 0,
        7 => values.First() == values.Last() ? 1 : 0,
    };
}

var result = parsePacket();
Console.WriteLine($"> {result}");
// >3744134304