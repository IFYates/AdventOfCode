var inp = File.ReadAllLines("2021/Day 20/Input.txt");

var mask = inp[0].Select(c => c == '#').ToArray();

var image = inp[2..].Select(l => l.Select(c => c == '#').ToArray()).ToArray();
output(image);

for (var r = 1; r <= 2; ++r)
{
    image = enhance(image, r % 2 == 1);
    output(image);
}

Console.WriteLine("> " + image.Sum(r => r.Count(c => c)));

void output(bool[][] image)
{
    Console.WriteLine(image.Select(r => r.Aggregate("", (a, b) => a + (b ? '#' : '.'))).Aggregate("", (a, b) => $"{a}\r\n{b}"));
}
bool[][] enhance(bool[][] input, bool odd)
{
    var zoom = odd ? 3 : 0;
    var infmask = mask[0] ? (!odd ? mask[0] : mask[511]) : false;
    var result = Enumerable.Range(0, input.Length + zoom + zoom).Select(i => new bool[input[0].Length + zoom + zoom]).ToArray();
    for (var y = -zoom; y < input.Length + zoom; ++y)
    {
        for (var x = -zoom; x < input[0].Length + zoom; ++x)
        {
            result[y + zoom][x + zoom] = resolve(input, x, y, infmask);
        }
    }
    return result;
}
bool resolve(bool[][] input, int x, int y, bool infmask)
{
    var block = new bool[9];
    for (var i = -1; i < 2; ++i)
    {
        for (var j = -1; j < 2; ++j)
        {
            block[j + ((i + 1) * 3) + 1] = read(input, x + j, y + i) ?? infmask;
        }
    }

    var val = block.Aggregate(0, (a, b) => (a << 1) + (b ? 1 : 0));
    return mask![val];
}
bool? read(bool[][] input, int x, int y) => x >= 0 && x < input[0].Length && y >= 0 && y < input.Length ? input[y][x] : null;

// <5426