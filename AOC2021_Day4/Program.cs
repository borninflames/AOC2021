Console.WriteLine("Hello, Advent of Code 2021!");

var lines = File.ReadAllLines("Input.txt");
var numbers = lines.First().Split(',');
lines = lines.Skip(2).ToArray();
var boards = new List<Board>();
while (lines.Length > 0)
{
    var boardLines = lines.Take(5).ToList();
    lines = lines.Skip(6).ToArray();

    boards.Add(new Board(boardLines));
}

Board? lastWinnerBoard = null;
int lastWinnerNumber = 0;
for (int n = 0; n < numbers.Length; n++)
{
    var num = numbers[n];
    for (int b = 0; b < boards.Count; b++)
    {
        var board = boards[b];
        var markedNum = board.Mark(num);
        if (markedNum != null)
        {
            if (!board.IsWinner && (board.BingoRow(markedNum) || board.BingoColumn(markedNum)))
            {
                lastWinnerBoard = board.Clone();
                lastWinnerNumber = markedNum.Value;
                board.IsWinner = true;

                //var sumOfUnmarked2 = lastWinnerBoard?.SumOfUnmarkedNumbers();
                //Console.WriteLine($"The answer is: {sumOfUnmarked2 * lastWinnerNumber}");
                //Console.ReadLine();

                Console.WriteLine($"Last winner Number: {lastWinnerNumber}, n: {n}, b: {b}");
            }
        }
    }
}

var sumOfUnmarked = lastWinnerBoard?.SumOfUnmarkedNumbers();
Console.WriteLine($"The answer is: {sumOfUnmarked * lastWinnerNumber}");


Console.WriteLine(boards.Count);



class Board
{
    public Board(List<string> lines)
    {
        Nums = new Num[5, 5];

        for (int row = 0; row < lines.Count; row++)
        {
            var line = lines[row].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < line.Length; i++)
            {
                Nums[row, i] = new Num(int.Parse(line[i]), row, i);
            }
        }
    }

    public Board Clone()
    { 
        var clone = new Board(new List<string>());

        for (int row = 0; row < 5; row++)
        {
            for (int i = 0; i < 5; i++)
            {
                clone.Nums[row, i] = new Num(Nums[row, i].Value, row, i);
                clone.Nums[row, i].IsMarked = Nums[row, i].IsMarked;
            }
        }

        return clone;
    }

    public Num[,] Nums { get; set; }

    public bool IsWinner { get; set; }

    public Num? Mark(string num)
    {
        var val = int.Parse(num);
        Num? retVal = null;
        for (int row = 0; row < 5 && retVal == null; row++)
        {
            for (int i = 0; i < 5 && retVal == null; i++)
            {
                if (Nums[row, i].Value == val)
                {
                    retVal = Nums[row, i];
                    retVal.IsMarked = true;
                }
            }
        }

        return retVal;
    }

    public bool BingoRow(Num num)
    {
        var nums = new List<Num>();
        for (int i = 0; i < 5; i++)
        {
            nums.Add(Nums[num.Row, i]);
        }

        return nums.All(n => n.IsMarked);
    }

    public bool BingoColumn(Num num)
    {
        var nums = new List<Num>();
        for (int row = 0; row < 5; row++)
        {
            nums.Add(Nums[row, num.I]);
        }

        return nums.All(n => n.IsMarked);
    }

    public int SumOfUnmarkedNumbers()
    {
        var sum = 0;    
        for (int row = 0; row < 5; row++)
        {
            for (int i= 0; i< 5; i++)
            {
                if (!Nums[row, i].IsMarked)
                {
                    sum += Nums[row, i].Value;
                }
            }
        }

        return sum;
    }
}

class Num
{
    public int Value { get; set; }
    public bool IsMarked { get; set; }
    public int Row { get; set; }
    public int I { get; set; }
    public Num(int num, int row, int i)
    {
        Value = num;
        Row = row;
        I = i;
    }
}