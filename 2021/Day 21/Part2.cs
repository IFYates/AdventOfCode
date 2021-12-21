var inp = File.ReadAllLines("2021/Day 21/Input.txt");
var positions = new Dictionary<int, byte>();
foreach (var ln in inp)
{
    var m = System.Text.RegularExpressions.Regex.Match(ln, @"^Player (\d+) starting position: (\d+)$");
    if (!m.Success)
    {
        throw new Exception($"Invalid line: {ln}");
    }
    positions[int.Parse(m.Groups[1].Value)] = (byte)(int.Parse(m.Groups[2].Value) - 1);
}

var universes = new Stack<Universe>();

var start = new Universe();
start.Players[0] = new Player(positions[1], 0);
start.Players[1] = new Player(positions[2], 0);
universes.Push(start);

long player1Wins = 0L, player2Wins = 0L;
while (universes.TryPop(out var universe))
{
    roll(universe, 3, 1); // 1,1,1
    roll(universe, 4, 3); // 1,1,2
    roll(universe, 5, 6); // 1,1,3 1,2,2
    roll(universe, 6, 7); // 1,2,3 2,2,2
    roll(universe, 7, 6); // 1,3,3 2,2,3
    roll(universe, 8, 3); // 2,3,3
    roll(universe, 9, 1); // 3,3,3
}
void roll(Universe universe, int roll, int count)
{
    var u = universe.DoRoll(roll, count);
    if (!hasWon(u)) { universes.Push(u); }
}
bool hasWon(Universe u)
{
    if (u.Players[1 - u.Player].Score >= 21)
    {
        if (u.Player == 0)
        {
            player1Wins += u.Mult;
        }
        else
        {
            player2Wins += u.Mult;
        }
        return true;
    }
    return false;
}

Console.WriteLine("> " + player1Wins + ", " + player2Wins);

//----------

record struct Player(byte Position, byte Score, int Moves = 0, int Mult = 1);
class Universe
{
    public Player[] Players { get; init; } = new Player[2];
    public long Mult { get; init; } = 1;
    public byte Player { get; set; } = 0;

    public Universe DoRoll(int num, int mult)
    {
        var u = new Universe
        {
            Players = new[] { Players[0], Players[1] },
            Mult = mult * Mult,
            Player = Player,
        };

        u.Players[Player].Moves += 1;
        u.Players[Player].Position = (byte)((Players[Player].Position + num) % 10);
        u.Players[Player].Score += (byte)(u.Players[Player].Position + 1);

        u.Player = (byte)(1 - Player);
        return u;
    }
}