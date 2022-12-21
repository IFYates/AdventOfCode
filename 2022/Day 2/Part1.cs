var score = 0;
foreach (var ln in System.IO.File.ReadAllLines("Input.txt"))
{
    var x = ln.Split(' ');
    switch (x[1])
    {
        case "X": // Rock
            score += 1;
            score += x[0] == "A" ? 3 : 0; // Draw
            score += x[0] == "C" ? 6 : 0; // Win
            break;
        case "Y": // Paper
            score += 2;
            score += x[0] == "B" ? 3 : 0; // Draw
            score += x[0] == "A" ? 6 : 0; // Win
            break;
        case "Z": // Scissors
            score += 3;
            score += x[0] == "C" ? 3 : 0; // Draw
            score += x[0] == "B" ? 6 : 0; // Win
            break;
    }
}

Console.WriteLine("> " + score);