var data = System.IO.File.ReadAllText("Input.txt");
var marker = "";
var state = 0; // 0 Looking for marker
for (var i = 0; i < data.Length; ++i)
{
    var c = data[i];
    if (state == 0)
    {
        marker += c;
        if (marker.Length == 14)
        {
            if (marker.Distinct().Count() == 14)
            {
                Console.WriteLine($"> {marker} @ {i + 1}");
                break;
            }
            marker = marker[1..];
        }
    }
}