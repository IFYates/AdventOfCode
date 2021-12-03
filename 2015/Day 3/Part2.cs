var houses = new Dictionary<string, int>()
{
    ["S0,0"] = 1
};

int dir;
var coords = new Dictionary<char, (int x, int y)>()
{
    ['S'] = (0, 0),
    ['R'] = (0, 0)
};
char who = 'S';
while ((dir = Console.In.Read()) > -1)
{
    switch ((char)dir)
    {
        case '>':
            coords[who] = (coords[who].x + 1, coords[who].y);
            break;
        case '<':
            coords[who] = (coords[who].x - 1, coords[who].y);
            break;
        case '^':
            coords[who] = (coords[who].x, coords[who].y + 1);
            break;
        case 'v':
            coords[who] = (coords[who].x, coords[who].y - 1);
            break;
        default:
            Console.Error.WriteLine("Unhandled direction: " + (char)dir);
            break;
    }

    var coord = $"{who}{coords[who].x},{coords[who].y}";
    if (houses.TryGetValue(coord, out var value))
    {
        ++value;
    }
    else
    {
        value = 1;
    }
    houses[coord] = value;

    who = who == 'S' ? 'R' : 'S';
}

var keys = houses.Keys.Select(k => k[1..]).Distinct().Count();
Console.WriteLine("> " + keys);
//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(houses));