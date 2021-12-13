Console.WriteLine("Hello, Advent of Code 2021!");

var lines = File.ReadAllLines("TestInput.txt");
//var lines = File.ReadAllLines("Input.txt");

var octopuses = new int[10, 10];
for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        octopuses[i, j] = int.Parse(lines[i][j].ToString());
    }
}

for (int i = 0; i < 10; i++)
{
    IncreaseEnergy(octopuses);
}

Console.WriteLine(octopuses);

void IncreaseEnergy(int[,] oct)
{
    for (int r = 0; r < 10; r++)
    {
        for (int c = 0; c < 10; c++)
        {
            oct[r, c]++;
        }
    }
}