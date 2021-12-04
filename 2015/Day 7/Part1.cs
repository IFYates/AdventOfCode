static Dictionary<string, Wire> wires = new Dictionary<string, Wire>();

enum Logic
{
    AND,
    LSHIFT,
    NOT,
    OR,
    RSHIFT,
    SET,
}
class Wire
{
    public Logic Logic { get; set; }
    public string LValue { get; set; }
    public string RValue { get; set; }
    public int? Result { get; set; }

    public int Resolve()
    {
        if (!Result.HasValue)
        {
            int lVal = 0, rVal = 0;
            if (!int.TryParse(LValue, out lVal))
            {
                lVal = wires[LValue].Resolve();
                Console.WriteLine($"L {LValue} = {lVal}");
            }
            if (RValue?.Length > 0 && !int.TryParse(RValue, out rVal))
            {
                rVal = wires[RValue].Resolve();
                Console.WriteLine($"R {RValue} = {rVal}");
            }

            Result = Logic switch
            {
                Logic.AND => lVal & rVal,
                Logic.LSHIFT => lVal << rVal,
                Logic.NOT => 65535 - lVal,
                Logic.OR => lVal | rVal,
                Logic.RSHIFT => lVal >> rVal,
                Logic.SET => lVal,
                _ => throw new NotSupportedException($"Logic: {Logic}")
            };
            Console.WriteLine($"{Logic} ({lVal}, {rVal}) = {Result}");
        }
        return Result.Value;
    }
}
// e.g., SET 5 -> a: wires["a"] = new Wire { Logic = Logic.SET, LValue = 5 }
// e.g., a AND b -> c: wires["c"] = new Wire { Logic = Logic.AND, LValue = "a", RValue = "b" }

var ln = Console.In.ReadLine();
while (ln != null)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^(?:([a-z]+|\d+)(?: (AND|OR|LSHIFT|RSHIFT) ([a-z]+|\d+))?|NOT ([a-z]+)) -> ([a-z]+)$");
    if (!m.Success)
    {
        Console.Error.WriteLine($"Invalid line: {ln}");
        continue;
    }

    var subj = m.Groups[5].Value;
    var lVal = m.Groups[1].Value;
    if (m.Groups[2].Success)
    {
        wires[subj] = new Wire
        {
            Logic = Enum.Parse<Logic>(m.Groups[2].Value),
            LValue = lVal,
            RValue = m.Groups[3].Value
        };
    }
    else if (m.Groups[4].Success) // Not
    {
        wires[subj] = new Wire { Logic = Logic.NOT, LValue = m.Groups[4].Value };
    }
    else if (!m.Groups[2].Success) // Set
    {
        wires[subj] = new Wire { Logic = Logic.SET, LValue = lVal };
    }
    else
    {
        Console.Error.WriteLine($"No action: {ln}");
    }

    ln = Console.In.ReadLine();
}

Console.WriteLine("> " + wires["a"].Resolve());