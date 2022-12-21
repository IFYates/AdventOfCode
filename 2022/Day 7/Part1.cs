#nullable enable

record File([property: System.Text.Json.Serialization.JsonIgnore] Dir Parent, string Name, long Size);
record Dir([property: System.Text.Json.Serialization.JsonIgnore] Dir? Parent, string Name)
{
    public string FullPath => Parent != null ? $"{Parent.FullPath.TrimEnd('/')}/{Name}" : Name;
    public List<Dir> Dirs { get; } = new();
    public List<File> Files { get; } = new();
    public long FileSizeTotal => Files.Sum(f => f.Size) + Dirs.Sum(d => d.FileSizeTotal);
}
var dirs = new List<Dir>();
var root = new Dir(null, "/");
dirs.Add(root);

var lines = new List<string>(System.IO.File.ReadAllLines("Input.txt"));
string? Pop(Func<string, bool>? predicate = null)
{
    if (!lines.Any()) { return null; }
    var ln = lines[0];
    if (predicate?.Invoke(ln) == false)
    {
        return null;
    }
    lines.RemoveAt(0);
    return ln;
}

var re = new System.Text.RegularExpressions.Regex(@"^(\d+)\s+(.*?)$");
var pwd = root;
while (lines.Any())
{
    var ln = Pop()!;
    if (ln.StartsWith("$ cd "))
    {
        // CD
        var path = ln[4..].Trim();
        if (path == "/")
        {
            pwd = root;
        }
        else if (path == "..")
        {
            pwd = pwd.Parent!;
        }
        else
        {
            var dir = pwd.Dirs.FirstOrDefault(d => d.Name == path);
            if (dir == null)
            {
                pwd = new Dir(pwd, path);
                pwd.Parent?.Dirs.Add(pwd);
                dirs.Add(pwd);
            }
            else
            {
                pwd = dir;
            }
        }
    }
    else if (ln == "$ ls")
    {
        // List
        while ((ln = Pop(p => p[0] != '$')) != null)
        {
            var m = re.Match(ln);
            if (m.Success)
            {
                pwd.Files.Add(new File(pwd, m.Groups[2].Value, long.Parse(m.Groups[1].Value)));
            }
        }
    }
    else
    {
        Console.Error.WriteLine("> Unexpected line: " + ln);
    }
}

//Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(root, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

var sum = 0L;
foreach (var dir in dirs.Where(d => d.FileSizeTotal <= 100_000))
{
    sum += dir.FileSizeTotal;
    Console.WriteLine($"> {dir.Name}: {dir.FileSizeTotal}");
}
Console.WriteLine("> " + sum);