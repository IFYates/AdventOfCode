var inp = File.ReadAllLines("2021/Day 21/Input.txt");
var positions = new Dictionary<int, int>();
var scores = new Dictionary<int, int>();
foreach (var ln in inp)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^Player (\d+) starting position: (\d+)$");
    if (!m.Success)
    {
        throw new Exception($"Invalid line: {ln}");
    }
    positions[int.Parse(m.Groups[1].Value)] = int.Parse(m.Groups[2].Value) - 1;
    scores[int.Parse(m.Groups[1].Value)] = 0;
}

var rolls = 0;
int die = 1;
int roll()
{
    ++rolls;
    return die++;
}

int player = 1;
while (true)
{
    var move = roll() + roll() + roll();
    positions[player] = (positions[player] + move) % 10;
    scores[player] += positions[player] + 1;
    if (scores[player] >= 1000)
    {
        break;
    }

    ++player;
    if (player > scores.Keys.Max())
    {
        player = scores.Keys.Min();
    }
}

var result = scores.Values.Min() * rolls;
Console.WriteLine("> " + result);