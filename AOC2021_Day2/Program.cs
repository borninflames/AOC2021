using AOC2021_Day2;

Console.WriteLine("Hello, Advent of Code 2021!");

var lines = File.ReadAllLines("Day2a.txt");
var submarine = new Submarine();

foreach (var line in lines)
{
    submarine.Move(line);
}

Console.WriteLine(submarine.WhatsTheAnswer());

