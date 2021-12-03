var prefix = Console.In.ReadToEnd();

var algo = System.Security.Cryptography.MD5.Create();
var i = 0;
while (++i <= int.MaxValue)
{
    if (i % 10000 == 0) { Console.WriteLine($"{i}"); }
    var bytes = Encoding.Default.GetBytes($"{prefix}{i}");
    var hash = algo.ComputeHash(bytes);
    if (hash[0] == 0 & hash[1] == 0 && hash[2] <= 15)
    {
        Console.WriteLine($"> {prefix} {i}");
        break;
    }
}