var dimensions = System.IO.File.ReadAllLines("Input.txt");

var areas = dimensions
    .Select(d => d.Split('x').Select(int.Parse).ToArray())
    .Select(lwh =>
    {
        var (l, w, h) = (lwh[0], lwh[1], lwh[2]);
        var (a, b, c) = (l * w, w * h, h * l);
        var area = 2 * (a + b + c);
        return area + Math.Min(Math.Min(a, b), c);
    }).ToArray();

Console.WriteLine("> " + areas.Sum());