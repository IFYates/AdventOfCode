var houses = new Dictionary<string, int>()
{
    ["0,0"] = 1
};

int dir;
int x = 0, y = 0;
while ((dir = Console.In.Read()) > -1)
{
    switch ((char)dir)
    {
        case '>':
            ++x;
            break;
        case '<':
            --x;
            break;
        case '^':
            ++y;
            break;
        case 'v':
            --y;
            break;
        default:
            Console.Error.WriteLine("Unhandled direction: " + (char)dir);
            break;
    }

    var coord = $"{x},{y}";
    if (houses.TryGetValue(coord, out var value))
    {
        ++value;
    }
    else
    {
        value = 1;
    }
    houses[coord] = value;
}

Console.WriteLine("> " + houses.Count);