namespace AOC;
public static class Helpers
{
    public static T[] ArrayOf<T>(int length, Func<T> fact)
    {
        return Enumerable.Range(1, length).Select(i => fact()).ToArray();
    }

    private static readonly Dictionary<string, int> _printTimes = new();
    public static void PrintPerSecond(string template, params object[] args)
    {
        var secs = (int)DateTime.UtcNow.TimeOfDay.TotalSeconds;
        if (!_printTimes.TryGetValue(template, out var last) || last < secs)
        {
            _printTimes[template] = secs;
            Console.WriteLine(string.Format(template, args));
        }
    }
}
