Console.WriteLine("Hello, Advent of Code 2021!");

//var lines = File.ReadAllLines("TestInput.txt");
var lines = File.ReadAllLines("Input.txt");
var sum = 0;
foreach (var line in lines)
{
    var inputOutput = line.Split(" | ");
    var output = inputOutput[1];
    var oDigits = output.Split(' ');
    foreach (var digit in oDigits)
    {
        //if digit is 1, 4, 7 or 8
        if (digit.Length == 2 || digit.Length == 3 || digit.Length == 4 || digit.Length == 7)
        {
            sum++;
        }
    }
}

Console.WriteLine(sum);
var outputSum = 0;
foreach (var line in lines)
{
    var inputOutput = line.Split(" | ");
    var iDigits = inputOutput[0].Split(' ').OrderBy(id => id.Length == 5 ? int.MaxValue : id.Length).ToArray();
    var oDigits = inputOutput[1].Split(' ').Select(od => string.Concat(od.OrderBy(c => c))).Reverse().ToArray();
    var mapping = new Dictionary<int, string>();
    char upperHalfOfOne = 'x';
    foreach (var digit in iDigits)
    {
        var num = -1;
        switch (digit.Length)
        {
            case 2:
                num = 1;
                break;
            case 3:
                num = 7;
                break;
            case 4:
                num = 4;
                break;
            case 5:
                if (mapping[7].All(c => digit.Contains(c)))
                {
                    num = 3;
                }
                else if (digit.Contains(upperHalfOfOne))
                {
                    num = 2;
                }
                else
                {
                    num = 5;
                }
                break;
            case 6:
                if (mapping[4].All(c => digit.Contains(c)))
                {
                    num = 9;
                }
                else if (mapping[1].All(c => digit.Contains(c)))
                {
                    num = 0;
                }
                else
                {
                    num = 6;
                    upperHalfOfOne = digit.Contains(mapping[1][1]) ? mapping[1][0] : mapping[1][1];
                }
                break;
            case 7:
                num = 8;
                break;
        }
        mapping[num] = string.Concat(digit.OrderBy(c => c));
    }

    for (int i = 0; i < oDigits.Length; i++)
    {
        var num = mapping.First(m => m.Value == oDigits[i]).Key;
        outputSum += num * (int)Math.Pow(10, i);
    }
}

Console.WriteLine(outputSum);
