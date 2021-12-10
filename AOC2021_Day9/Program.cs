Console.WriteLine("Hello, Advent of Code 2021!");

//var lines = File.ReadAllLines("TestInput.txt");
var lines = File.ReadAllLines("Input.txt");

var width = lines[0].Length + 2;
var height = lines.Length + 2;

var depthMatrix = new HeightPoint[height, width];

for (int i = 0; i < width; i++)
{
    depthMatrix[0, i] = new HeightPoint(9);
    depthMatrix[height - 1, i] = new HeightPoint(9);
}

for (int i = 0; i < height; i++)
{
    depthMatrix[i, 0] = new HeightPoint(9);
    depthMatrix[i, width - 1] = new HeightPoint(9);
}

for (int row = 0; row < lines.Length; row++)
{
    for (int col = 0; col < lines[0].Length; col++)
    {
        depthMatrix[row+1, col+1] = new HeightPoint(int.Parse(lines[row][col].ToString())); 
    }
}

var sum = 0;
var basinSizes = new List<int>();

for (int row = 1; row <= lines.Length; row++)
{
    for (int col = 1; col <= lines[0].Length; col++)
    {
        if (IsLowest(depthMatrix, row, col))
        {
            sum += depthMatrix[row, col].Height + 1;
            //Console.WriteLine(depthMatrix[row, col].Height);

            basinSizes.Add(CheckBasinPoint(depthMatrix, row, col));
        }
    }
}

WriteOutMatrix(depthMatrix);

Console.WriteLine(sum);

var threeLargestSize = basinSizes.OrderByDescending(x => x).Take(3).Aggregate((x1, x2) => x1*x2);
Console.WriteLine(threeLargestSize);

bool IsLowest(HeightPoint[,] m, int row, int col)
{
    return m[row, col].Height < m[row - 1, col].Height && m[row, col].Height < m[row + 1, col].Height &&
        m[row, col].Height < m[row, col-1].Height && m[row, col].Height < m[row, col+1].Height;
}

int CheckBasinPoint(HeightPoint[,] m, int row, int col)
{
    if (m[row, col].Height >= 9 || m[row, col].IsVisited) return 0;
    var basinPointCount = 1;
    m[row, col].IsVisited = true;

    basinPointCount += CheckBasinPoint(m, row-1, col);
    basinPointCount += CheckBasinPoint(m, row + 1, col);
    basinPointCount += CheckBasinPoint(m, row, col-1);
    basinPointCount += CheckBasinPoint(m, row, col + 1);

    return basinPointCount;
}




void WriteOutMatrix(HeightPoint[,] depthMatrix)
{
    for (int row = 0; row < height; row++)
    {
        for (int col = 0; col < width; col++)
        {
            if (depthMatrix[row, col].Height == 9)
            {
                Console.Write("X");
            }
            else
            {
                Console.Write(depthMatrix[row, col].Height);
            }

        }
        Console.WriteLine();
    }
}

class HeightPoint
{    public int Height { get; set; }
    public bool IsVisited { get; set; }

    public HeightPoint(int height)
    {
        Height = height;
    }
}