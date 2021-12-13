using System.Drawing;

Console.WriteLine("Hello, Advent of Code 2021!");

//var lines = File.ReadAllLines("TestInput.txt");
var lines = File.ReadAllLines("Input.txt");
var isFoldInstruction = false;
var manual = new List<Dot>();
foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        isFoldInstruction = true;
    }
    else if (isFoldInstruction)
    {
        var instruction = line.Split("fold along ")[1].Split('=');
        switch (instruction[0])
        {
            case "x":
                manual = Fold(manual, FoldType.X, int.Parse(instruction[1]));
                break;
            case "y":
                manual = Fold(manual, FoldType.Y, int.Parse(instruction[1]));
                break;
            default:
                break;
        }
    }
    else
    {
        manual.Add(new Dot(line));
    }
}

Console.Clear();

foreach (var dot in manual)
{
    Console.SetCursorPosition(dot.X, dot.Y);
    Console.Write('█');
}


Console.ReadLine();


List<Dot> Fold(List<Dot> dots, FoldType foldType, int foldPosition)
{
    var dotsToFold = dots.Where(d => foldType == FoldType.X && d.X > foldPosition || foldType == FoldType.Y && d.Y > foldPosition).ToList();
    foreach (var dot in dotsToFold)
    {
        dot.X = foldType == FoldType.X ? dot.X - ((dot.X - foldPosition) * 2) : dot.X;
        dot.Y = foldType == FoldType.Y ? dot.Y - ((dot.Y - foldPosition) * 2) : dot.Y;
    }

    return dots.Distinct().ToList();
}



enum FoldType
{
    X,
    Y
}

class Dot : IEquatable<Dot>
{
    public Dot(string dotStr)
    {
        var coords = dotStr.Split(',');
        X = int.Parse(coords[0]);
        Y = int.Parse(coords[1]);
    }

    public int X { get; set; }
    public int Y { get; set; }

    public override int GetHashCode()
    {
        int tmp = (Y + ((X + 1) / 2));
        return X + (tmp * tmp);
    }

    //public override bool Equals(object? obj)
    //{
    //    var o = obj as Dot;
    //    return o != null && o.X == X && o.Y == Y;

    //}

    public bool Equals(Dot? other)
    {
        return other != null && X == other.X && Y == other.Y;
    }

    public override string ToString()
    {
        return $"{X} | {Y}";
    }
}