int segsNum(char[] segs, string segsOn, string num1, string num9)
{
    switch (segsOn.Length)
    {
        case 2: return 1;
        case 3: return 7;
        case 4: return 4;
        case 7: return 8;

        case 5: // 2, 3, 5
            if (segsOn.Contains(segs[5])) // 5
                return 5;
            if (segsOn.Contains(num1[0]) && segsOn.Contains(num1[1]))
                return 3;
            return 2;
        case 6: // 6, 9, 0
            if (segsOn.All(num9.Contains))
                return 9;
            if (!segsOn.Contains(num1[0]) || !segsOn.Contains(num1[1]))
                return 6;
            return 0;
    }
    return -1;
}

int result = 0;
string ln;
while ((ln = Console.In.ReadLine()) != null)
{
    var inp = ln.Split('|')[0].Trim().Split(' ');
    var oup = ln.Split('|')[1].Trim().Split(' ');

    var segs = new char[7]; // clockwise: T, TR, BR, B, BL, TL C
    var segsPoss = Enumerable.Range(1, 7).Select(i => new List<char>()).ToArray();
    var bySize = inp.GroupBy(s => s.Length).ToDictionary(g => g.Key);
    // T  = 2, 3, 5, 6, 7, 8, 9, 0
    // TR = 1, 2, 3, 4, 7, 8, 9, 0
    // BR = 1, 3, 4, 5, 6, 7, 8, 9, 0
    // B  = 2, 3, 5, 6, 8, 9, 0
    // BL = 2, 6, 8, 9, 0
    // TL = 4, 5, 6, 8, 9, 0
    // C  = 2, 3, 4, 5, 6, 8, 9

    // 1, 7, 4, 8
    segs[0] = bySize[3].Single().Where(c => !bySize[2].Single().Contains(c)).Single(); // T = 7 ^ 1
    var num3 = bySize[5].Where(s => bySize[3].Single().All(s.Contains)).Single(); // 3 &= 7
    // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(num3));
    // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(bySize));
    var num9 = bySize[6].Where(s => num3.All(s.Contains)).Single(); // 9 &= 3;
    segs[5] = num9.Where(c => !num3.Contains(c)).Single(); // TL = 9 ^ 3
    //segs[1] = bySize[2].Single().Where(c => !bySize[2].Single().Contains(c)).Single(); // 1 ^ 5
    //segs[2] = bySize[2].Single().Where(c => !bySize[2].Single().Contains(c)).Single(); // 1 ^ 2
    result += oup.Select((o, i) => segsNum(segs, o, bySize[2].Single(), num9) * (int)Math.Pow(10, 3 - i)).Sum();
}

Console.WriteLine("> " + result);