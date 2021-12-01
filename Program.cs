// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Advent of Code 2021!");

var lines = File.ReadAllLines("Inputs/Day1a.txt");
var count = 0;

for (int i = 1; i < lines.Length; i++)
{
    var previousDepth = int.Parse(lines[i-1]);
    var currentDepth = int.Parse(lines[i]);

    if (previousDepth < currentDepth) count++;
}

Console.WriteLine(count);