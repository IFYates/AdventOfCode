int score = 0;
var lines = System.IO.File.ReadAllLines("Input.txt");
for (var i = 0; i < lines.Length; i += 3)
{
    var r1 = lines[i];
    var r2 = lines[i + 1];
    var r3 = lines[i + 2];

    var dupes = r1.Intersect(r2).Intersect(r3);
    var chr = dupes.Single();
    var val = chr is >= 'a' ? (chr - 'a' + 1) : (chr - 'A' + 27);
    score += val;
    Console.WriteLine($"> {chr} ({val})");
}

Console.WriteLine("> " + score);