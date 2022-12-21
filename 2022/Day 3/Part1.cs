int score = 0;
foreach (var ln in System.IO.File.ReadAllLines("Input.txt"))
{
    var r1 = ln[0..(ln.Length / 2)];
    var r2 = ln[(ln.Length / 2)..];

    var dupes = r1.Intersect(r2);
    var chr = dupes.Single();
    var val = chr is >= 'a' ? (chr - 'a' + 1) : (chr - 'A' + 27);
    score += val;
    Console.WriteLine($"> {chr} ({val})");
}

Console.WriteLine("> " + score);