var dimensions = System.IO.File.ReadAllLines("Input.txt");

var areas = dimensions
    .Select(d => d.Split('x').Select(int.Parse).ToArray())
    .Select(lwh =>
    {
        var len = lwh.OrderBy(v => v).Take(2).Sum() * 2;
        var bow = lwh[0] * lwh[1] * lwh[2];
        return len + bow;
    }).ToArray();

Console.WriteLine("> " + areas.Sum());