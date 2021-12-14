Console.WriteLine("Hello, Advent of Code 2021!");

//var lines = File.ReadAllLines("TestInput.txt");
var lines = File.ReadAllLines("Input.txt");

var octopuses = new Octopus[10, 10];
for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        octopuses[i, j] = new Octopus(int.Parse(lines[i][j].ToString()), octopuses, i, j);
    }
}


//ShowOctopuses(octopuses, "Before any steps:");
//Console.ReadKey();

var flashes = 0;

for (int i = 0; i < 100000; i++)
{
    var flashesInStep = Step(octopuses, i + 1);
    if (flashesInStep == 100)
    {
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++");
        Console.WriteLine(i);
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++");
        break;
    }
    flashes += flashesInStep;
}

Console.WriteLine();
Console.WriteLine(flashes);


int Step(Octopus[,] oct, int step)
{
    var flashesInStep = 0;
    for (int r = 0; r < 10; r++)
    {
        for (int c = 0; c < 10; c++)
        {
            oct[r, c].IncreaseEnergy();
        }
    }

    for (int r = 0; r < 10; r++)
    {
        for (int c = 0; c < 10; c++)
        {
            if (oct[r, c].IsFlashed)
            {
                flashesInStep++;
                oct[r, c].Energy = 0;
                oct[r, c].IsFlashed = false;
            }
        }
    }

    //ShowOctopuses(octopuses, $"After step {step}");
    //Console.ReadKey();
    return flashesInStep;
}

void ShowOctopuses(Octopus[,] oct, string line)
{
    Console.Clear();
    Console.WriteLine(line);

    for (int r = 0; r < 10; r++)
    {
        for (int c = 0; c < 10; c++)
        {
            Console.SetCursorPosition(c, r + 1);
            if (oct[r, c].Energy == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.Write(oct[r, c].Energy);
        }
    }
}

class Octopus
{
    private Octopus[,] Octopuses { get; set; }
    public int Energy { get; set; }
    public bool IsFlashed { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }

    public Octopus(int e, Octopus[,] octopuses, int row, int col)
    {
        Energy = e;
        Octopuses = octopuses;
        Row = row;
        Col = col;
    }

    public void IncreaseEnergy()
    {
        Energy++;
        if (Energy == 10)
        {
            IsFlashed = true;
            for (int row = Row - 1 >= 0 ? Row - 1 : Row; row <= (Row + 1 <= 9 ? Row + 1 : Row); row++)
            {
                for (int col = Col - 1 >= 0 ? Col - 1 : Col; col <= (Col + 1 <= 9 ? Col + 1 : Col); col++)
                {
                    if ((Row != row || Col != col) && !Octopuses[row, col].IsFlashed)
                    {
                        Octopuses[row, col].IncreaseEnergy();
                    }
                }
            }
        }
    }

    public override string ToString()
    {
        return $">>{Energy}|{IsFlashed}<<";
    }
}