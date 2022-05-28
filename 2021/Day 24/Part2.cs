//         ABCDEFGHIJKLMN
var num = "16130800700805";
var pos = 0;
var z = 0L;

void push(int num)
{
    z *= 26;
    z += num;
}
void block(int a, int b, int c = 1)
{
    var w = num[pos++] - '0';

    var x = z % 26;
    z /= c;

    if (x + a != w)
    {
        push(w + b);
    }
}

block(14, 16); // A
block(11, 3); // B
block(12, 2); // C
block(11, 7); // D
block(-10, 13, 26); // E -> D - 3
block(15, 6); // F
block(-14, 10, 26); // G -> F - 8
block(10, 11); // H
block(-4, 6, 26); // I -> H + 7
block(-3, 5, 26); // J -> C - 1
block(13, 11); // K
block(-3, 4, 26); // L -> K + 8
block(-9, 4, 26); // M -> B - 6
block(-12, 6, 26); // N -> A + 4

Console.WriteLine("> " + z);
