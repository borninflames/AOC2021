Console.WriteLine("Hello, Advent of Code 2021!");

//var lines = File.ReadAllLines("TestInput.txt");
var lines = File.ReadAllLines("Input.txt");

var width = lines[0].Length + 2;
var height = lines.Length + 2;

var depthMatrix = new int[height, width];

for (int i = 0; i < width; i++)
{
    depthMatrix[0, i] = int.MaxValue;
    depthMatrix[height - 1, i] = int.MaxValue;
}

for (int i = 0; i < height; i++)
{
    depthMatrix[i, 0] = int.MaxValue;
    depthMatrix[i, width - 1] = int.MaxValue;
}

for (int row = 0; row < lines.Length; row++)
{
    for (int col = 0; col < lines[0].Length; col++)
    {
        depthMatrix[row+1, col+1] = int.Parse(lines[row][col].ToString());        
    }
}

var sum = 0;

for (int row = 1; row <= lines.Length; row++)
{
    for (int col = 1; col <= lines[0].Length; col++)
    {
        if (IsLowest(depthMatrix, row, col))
        {
            sum += depthMatrix[row, col]+1;
            Console.WriteLine(depthMatrix[row, col]);
        }
    }
}

WriteOutMatrix(depthMatrix);

Console.WriteLine(sum);

bool IsLowest(int[,] m, int row, int col)
{
    return m[row, col] < m[row - 1, col] && m[row, col] < m[row + 1, col] &&
        m[row, col] < m[row, col-1] && m[row, col] < m[row, col+1];
}


void WriteOutMatrix(int[,] depthMatrix)
{
    for (int row = 0; row < height; row++)
    {
        for (int col = 0; col < width; col++)
        {
            if (depthMatrix[row, col] == int.MaxValue)
            {
                Console.Write("X");
            }
            else
            {
                Console.Write(depthMatrix[row, col]);
            }

        }
        Console.WriteLine();
    }
}