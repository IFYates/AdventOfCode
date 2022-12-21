var elf = 0;
var packs = new List<int>();
packs.Add(0);
foreach (var ln in System.IO.File.ReadAllLines("Input.txt"))
{
    if (ln.Length > 0)
    {
        packs[elf] += int.Parse(ln);
    }
    else
    {
        packs.Add(0);
        ++elf;
    }
}

Console.WriteLine("> " + packs.Max());