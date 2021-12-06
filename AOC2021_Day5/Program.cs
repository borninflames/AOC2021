Console.WriteLine("Hello, Advent of Code 2021!");

var lines = File.ReadAllLines("Input.txt");
var space = new Space();
foreach (var line in lines)
{
    space.AddLine(line);
}

Console.WriteLine(space.Points.Where(p => p.Value > 1).Count());

struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Intersects { get; set; } = 1;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(string xy)
    {
        var coordsStr = xy.Split(',');
        X = Convert.ToInt32(coordsStr[0]);
        Y = Convert.ToInt32(coordsStr[1]);
    }
    public override bool Equals(object? p)
    {
        var result = p is Point point && X == point.X && Y == point.Y;
        return result;
    }

    public override string ToString()
    {
        return $"X: {X}; Y: {Y}; I: {Intersects}";
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

class Space
{
    public Dictionary<Point, int> Points { get; set; } = new Dictionary<Point, int>();

    public void AddLine(string line)
    {
        var endPointsStr = line.Split(" -> ");
        var p1 = new Point(endPointsStr[0]);
        var p2 = new Point(endPointsStr[1]);

        var xDirection = p1.X > p2.X ? -1 : p1.X == p2.X ? 0 : 1;
        var yDirection = p1.Y > p2.Y ? -1 : p1.Y == p2.Y ? 0 : 1;
                
        AddOrUpdatePoint(p1);
        do
        {           
            p1 = new Point(p1.X + xDirection, p1.Y + yDirection);
            AddOrUpdatePoint(p1);

        } while (!p1.Equals(p2));
    }

    private void AddOrUpdatePoint(Point p)
    {
        if (!Points.ContainsKey(p))
        {
            Points[p] = 1;
        }
        else
        {
            Points[p]++;
        }
    }
}