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

int result = 0;
void parsePacket()
{
    // Version
    var ver = take(3);
    result += ver;

    // Type
    var type = take(3);
    Console.WriteLine($"Ver:{ver} Type:{type}");

    // Logic
    switch (type)
    {
        case 4:
            int value = 0, end;
            do
            {
                end = take(1);
                value = (value << 4) + take(4);
            }
            while (end == 1);
            Console.WriteLine("Value: " + value);
            break;
        default:
            var typelen = take(1);
            if (typelen == 0)
            {
                var len = take(15);
                Console.WriteLine("TL0: " + len);
                var fin = (len / 4) + pos;
                while (pos < fin)
                {
                    parsePacket();
                }
            }
            else
            {
                var count = take(11);
                Console.WriteLine("TL1: " + count);
                while (count-- > 0)
                {
                    parsePacket();
                }
            }
            break;
    }
}

parsePacket();
Console.WriteLine($"{ln.Length} :: {pos}");
Console.WriteLine($"> {result}");