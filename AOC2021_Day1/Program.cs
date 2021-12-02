Console.WriteLine("Hello, Advent of Code 2021!");

var lines = File.ReadAllLines("Inputs/Day1a.txt");
var measurements = new List<int>();

for (int i = 2; i < lines.Length; i++)
{
    var depth1 = int.Parse(lines[i-2]);
    var depth2 = int.Parse(lines[i - 1]);
    var depth3 = int.Parse(lines[i]);
    measurements.Add(depth1 + depth2 + depth3);
}


var count = 0;
for (int i = 1; i < measurements.Count; i++)
{
    var previousDepth = measurements[i - 1];
    var currentDepth = measurements[i];

    if (previousDepth < currentDepth) count++;
}

Console.WriteLine(count);