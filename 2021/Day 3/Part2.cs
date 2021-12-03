var bins = System.IO.File.ReadAllLines("Input.txt");
var LEN = bins.Max(l => l.Length);

static string[] filterFor(char filter, int pos, string[] array)
{
    var oneArr = array.Where(e => e[pos] == '1').ToArray();
    var zeroArr = array.Where(e => e[pos] == '0').ToArray();
    var comp = oneArr.Length.CompareTo(zeroArr.Length);

    // I know this can be simplified!
    if (filter == '1')
    {
        return comp >= 0 ? oneArr : zeroArr;
    }
    return comp >= 0 ? zeroArr : oneArr;
}

var pos = 0;
var oneArr = bins;
while (oneArr.Length > 1)
{
    oneArr = filterFor('1', pos++, oneArr);
}

pos = 0;
var zeroArr = bins;
while (zeroArr.Length > 1)
{
    zeroArr = filterFor('0', pos++, zeroArr);
}

Console.WriteLine("Oxygen: " + oneArr.Single());
Console.WriteLine("CO2: " + zeroArr.Single());

var oxygen = oneArr.Single().Select((b, i) => b == '1' ? Math.Pow(2, (LEN - 1) - i) : 0).Sum();
var co2 = zeroArr.Single().Select((b, i) => b == '1' ? Math.Pow(2, (LEN - 1) - i) : 0).Sum();
Console.WriteLine("> " + (oxygen * co2));