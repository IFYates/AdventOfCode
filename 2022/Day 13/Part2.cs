var lines = System.IO.File.ReadAllLines("Input.txt");

class Element : IComparable
{
    public bool IsArray { get; private set; }
    public int Value { get; private set; }
    public List<Element> Contents { get; private set; } = new();

    public override string ToString()
    {
        if (IsArray)
        {
            return "["
                + string.Join(",", Contents.Select(c => $"{c}"))
                + "]";
        }
        return $"{Value}";
    }

    public Element(ref string ln)
    {
        if (ln[0] == '[')
        {
            IsArray = true;
            ln = ln[1..];
            while (ln[0] != ']')
            {
                Contents.Add(new Element(ref ln));
            }
            ln = ln[1..];
        }
        else
        {
            Value = 0;
            while (ln.Length > 0 && ln[0] >= '0' && ln[0] <= '9')
            {
                Value *= 10;
                Value += ln[0] - '0';
                ln = ln[1..];
            }
        }
        if (ln.Length > 0 && ln[0] == ',')
        {
            ln = ln[1..];
        }
    }
    public Element()
    {
    }

    private Element ToArray()
    {
        if (!IsArray)
        {
            var el = new Element
            {
                IsArray = true
            };
            el.Contents.Add(new Element { Value = Value });
            return el;
        }
        return this;
    }

    public int CompareTo(object other)
        => CompareTo((Element)other);
    public int CompareTo(Element other)
    {
        if (!IsArray && !other.IsArray)
        {
            return Value < other.Value
                ? -1
                : Value > other.Value
                ? 1
                : 0;
        }
        else if (IsArray && other.IsArray)
        {
            var c = 0;
            for (var i = 0; c == 0 && i < Contents.Count; ++i)
            {
                if (i >= other.Contents.Count)
                {
                    return 1;
                }
                c = Contents[i].CompareTo(other.Contents[i]);
            }
            if (c == 0 && other.Contents.Count > Contents.Count)
            {
                return -1;
            }
            return c;
        }
        return ToArray().CompareTo(other.ToArray());
    }
}

var packets = lines.Where(l => l.Length > 0).Select(l => new Element(ref l)).ToList();
var div1 = packets[^2];
var div2 = packets[^1];
packets.Sort();

var result = (packets.IndexOf(div1) + 1) * (packets.IndexOf(div2) + 1);
Console.WriteLine("> " + result);