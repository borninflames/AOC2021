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
