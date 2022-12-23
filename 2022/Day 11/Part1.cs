using System.Text.RegularExpressions;

class Monkey
{
    public List<int> Items { get; } = new();
    public string[] Operation { get; set; }
    public int TestDivisor { get; set; }
    public int TestPassMonkey { get; set; }
    public int TestFailMonkey { get; set; }

    public int InspectCount { get; set; }

    public void TestAll()
    {
        while (Items.Any())
        {
            var item = Items[0];
            Items.RemoveAt(0);
            ++InspectCount;

            var L = Operation[0] == "old" ? item : int.Parse(Operation[0]);
            var R = Operation[2] == "old" ? item : int.Parse(Operation[2]);
            if (Operation[1] == "+")
            {
                item = L + R;
            }
            else if (Operation[1] == "*")
            {
                item = L * R;
            }
            
            item /= 3;

            var target = item % TestDivisor == 0 ? TestPassMonkey : TestFailMonkey;
            monkeys[target].Items.Add(item);
        }
    }
}
static Dictionary<int, Monkey> monkeys = new();

var reM = new Regex(@"^Monkey (\d+):$");
var reI = new Regex(@"^\s+Starting items: ((\d+, )*\d+)$");
var reO = new Regex(@"^\s+Operation: new = (old|\d+) (\+|\*) (old|\d+)$");
var reT = new Regex(@"^\s+Test: divisible by (\d+)$");
var reTT = new Regex(@"^\s+If true: throw to monkey (\d+)$");
var reTF = new Regex(@"^\s+If false: throw to monkey (\d+)$");

Monkey cur;
var lines = System.IO.File.ReadAllLines("Input.txt");
foreach (var ln in lines)
{
    if (ln.Trim().Length == 0) continue;

    var m = reM.Match(ln);
    if (m.Success)
    {
        cur = new Monkey();
        monkeys[int.Parse(m.Groups[1].Value)] = cur;
        continue;
    }

    m = reI.Match(ln);
    if (m.Success)
    {
        cur.Items.AddRange(m.Groups[1].Value.Split(',').Select(i => int.Parse(i.Trim())));
        continue;
    }
    
    m = reO.Match(ln);
    if (m.Success)
    {
        cur.Operation = new[] { m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value };
        continue;
    }
    
    m = reT.Match(ln);
    if (m.Success)
    {
        cur.TestDivisor = int.Parse(m.Groups[1].Value);
        continue;
    }
    
    m = reTT.Match(ln);
    if (m.Success)
    {
        cur.TestPassMonkey = int.Parse(m.Groups[1].Value);
        continue;
    }
    
    m = reTF.Match(ln);
    if (m.Success)
    {
        cur.TestFailMonkey = int.Parse(m.Groups[1].Value);
        continue;
    }
    
    Console.WriteLine(">>> BAD LINE: " + ln);
}

for (var round = 1; round <= 20; ++round)
{
    foreach (var monkey in monkeys.Values)
    {
        monkey.TestAll();
    }
}

foreach (var kvp in monkeys)
{
    Console.WriteLine($"Monkey {kvp.Key} inspected items {kvp.Value.InspectCount} times.");
}

//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(monkeys, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

var score = monkeys.Values.Select(m => m.InspectCount).OrderByDescending(v => v).Take(2).ToArray();
Console.WriteLine("> " + (score[0] * score[1]));