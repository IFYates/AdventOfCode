var lines = System.IO.File.ReadAllLines("Input.txt");

class Element
{
    public bool IsArray { get; private set; }
    public int Value { get; private set; }
    public List<Element> Contents { get; private set; } = new();

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
    public Element(int value)
    {
        Value = value;
    }

    private Element ToArray()
    {
        if (!IsArray)
        {
            IsArray = true;
            Contents.Add(new Element(Value));
        }
        return this;
    }

    public int Compare(Element other)
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
                c = Contents[i].Compare(other.Contents[i]);
            }
            if (c == 0 && other.Contents.Count > Contents.Count)
            {
                return -1;
            }
            return c;
        }
        return ToArray().Compare(other.ToArray());
    }
}

var result = 0;
for (var i = 0; i < lines.Length; i += 3)
{
    var idx = (i / 3) + 1;
    string left = lines[i], right = lines[i + 1];
    var L = new Element(ref left);
    var R = new Element(ref right);
    if (L.Compare(R) <= 0)
    {
        result += idx;
    }
}

Console.WriteLine("> " + result);