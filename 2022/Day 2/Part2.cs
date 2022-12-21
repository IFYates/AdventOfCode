var score = 0;
foreach (var ln in System.IO.File.ReadAllLines("Input.txt"))
{
    var x = ln.Split(' ');
    switch (x[1])
    {
        case "X": // Lose
            score += 0;
            score += x[0] switch { "A" => 3, "B" => 1, "C" => 2 };
            break;
        case "Y": // Draw
            score += 3;
            score += x[0] switch { "A" => 1, "B" => 2, "C" => 3 };
            break;
        case "Z": // Win
            score += 6;
            score += x[0] switch { "A" => 2, "B" => 3, "C" => 1 };
            break;
    }
}

Console.WriteLine("> " + score);