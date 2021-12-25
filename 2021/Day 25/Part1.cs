using static AOC.Helpers;

var inp = File.ReadAllLines("2021/Day 25/Input.txt");
var map = ArrayOf(inp.Length, () => ArrayOf<bool?>(inp[0].Trim().Length, () => null));
var row = 0;
foreach (var ln in inp)
{
    map[row++] = ln.Select(c => c switch { 'v' => true, '>' => false, _ => (bool?)null }).ToArray();
}
//print(map);

var count = 0;
while (true)
{
    ++count;
    map = step(map, out var moved);
    if (!moved)
    {
        break;
    }
    //Console.WriteLine($"After {count} steps:");
    //print(map);
}

Console.WriteLine("> " + count);

static void print(bool?[][] map)
{
    for (var y = 0; y < map.Length; ++y)
    {
        for (var x = 0; x < map[0].Length; ++x)
        {
            Console.Write(map[y][x] switch { true => "v", false => ">", _ => "." });
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

static bool?[][] step(bool?[][] map, out bool moved)
{
    moved = false;
    var res = map.Select(r => r.Select(c => c).ToArray()).ToArray();
    for (var y = 0; y < map.Length; ++y)
    {
        for (var x = 0; x < map[0].Length; ++x)
        {
            if (map[y][x] == false)
            {
                var tx = x >= map[0].Length - 1 ? 0 : x + 1;
                if (map[y][tx] != false && res[y][tx] == null)
                {
                    res[y][x] = null;
                    res[y][tx] = false;
                    moved = true;
                    ++x;
                }
            }
        }
    }
    for (var x = 0; x < map[0].Length; ++x)
    {
        for (var y = 0; y < map.Length; ++y)
        {
            if (map[y][x] == true)
            {
                var ty = y >= map.Length - 1 ? 0 : y + 1;
                if (map[ty][x] != true && res[ty][x] == null)
                {
                    res[y][x] = null;
                    res[ty][x] = true;
                    moved = true;
                    ++y;
                }
            }
        }
    }
    return res;
}