using System.Text;

Console.WriteLine("Hello, Advent of Code 2021!");

var lines = File.ReadAllLines("Day3a.txt");
var powerConsumption = 0;
var gammaRate = 0;
var epsilonRate = 0;
var gammaRateBin = new StringBuilder();
var epsilonRateBin = new StringBuilder();

var bitCounts = new List<BitCount>();
for (int i = 0; i < lines[0].Length; i++)
{
    bitCounts.Add(new BitCount());
}

for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    for (int j = 0; j < line.Length; j++)
    {
        if (line[j] == '0')
        {
            bitCounts[j].Zeros++;
        }
        else
        {
            bitCounts[j].Ones++;
        }
    }
}

foreach (var bitCount in bitCounts)
{
    gammaRateBin.Append(bitCount.Ones > bitCount.Zeros ? "1" : "0");
    epsilonRateBin.Append(bitCount.Ones > bitCount.Zeros ? "0" : "1");
}

gammaRate = Convert.ToInt32(gammaRateBin.ToString(), 2);
epsilonRate = Convert.ToInt32(epsilonRateBin.ToString(), 2);

powerConsumption = gammaRate * epsilonRate;

Console.WriteLine(powerConsumption);









class BitCount
{
    public int Zeros { get; set; } = 0;
    public int Ones { get; set; } = 0;
}

