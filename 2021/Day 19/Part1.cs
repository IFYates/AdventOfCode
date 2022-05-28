var inp = File.ReadAllLines("2021/Day 19/Input.txt");

var points = new List<Point>();
var scanners = new List<Point[]>();
foreach (var ln in inp)
{
    if (ln.Length == 0 || ln[0..3] == "---")
    {
        if (points.Count > 0)
        {
            scanners.Add(points.ToArray());
            points = new();
        }
    }
    else
    {
        var xyz = ln.Split(',').Select(int.Parse).ToArray();
        points.Add(new Point(xyz[0], xyz[1], xyz[2]));
    }
}

// Signature each point
// Locate overlap w/ rotation

Console.WriteLine("> 0");

record Point(int X, int Y, int Z)
{

}