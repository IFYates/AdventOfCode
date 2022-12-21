int score = 0;
foreach (var ln in System.IO.File.ReadAllLines("Input.txt"))
{
    var a = ln.Split(',')[0];
    var a1 = int.Parse(a.Split('-')[0]);
    var a2 = int.Parse(a.Split('-')[1]);

    var b = ln.Split(',')[1];
    var b1 = int.Parse(b.Split('-')[0]);
    var b2 = int.Parse(b.Split('-')[1]);

    if ((b1 <= a2 && b2 >= a1) || (a1 <= b2 && a2 >= b1)) {
        ++score;
    }
}

Console.WriteLine("> " + score);