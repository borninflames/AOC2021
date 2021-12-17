Console.WriteLine("Hello, Advent of Code 2021!");

var lines = File.ReadAllLines("TestInput.txt");
//var lines = File.ReadAllLines("Input.txt");
var caveSystem = new Dictionary<string, Cave>();

foreach (var line in lines)
{
    var cavePair = line.Split('-');
    if (!caveSystem.ContainsKey(cavePair[0]))
    {
        caveSystem[cavePair[0]] = new Cave(cavePair[0]);
    }

    if (!caveSystem.ContainsKey(cavePair[1]))
    {
        caveSystem[cavePair[1]] = new Cave(cavePair[1]);
    }

    caveSystem[cavePair[0]].Caves.Add(caveSystem[cavePair[1]]);
    caveSystem[cavePair[1]].Caves.Add(caveSystem[cavePair[0]]);
}

Traverse(caveSystem, caveSystem["start"], new List<string>());

Console.WriteLine(caveSystem["end"].VisitedTimes);
Console.ReadLine();


void Traverse(Dictionary<string, Cave> caveSystem, Cave cave, List<string> visitedCaves)
{
    //if ((cave.IsSingleSmall && visitedCaves.Where(c => c == cave.Name).Count() == 2) || (cave.IsSmall && visitedCaves.Contains(cave.Name) && !cave.IsSingleSmall))
    //{
    //    return;
    //}
    var visitedCount = visitedCaves.Where(c => c == cave.Name).Count();
    if ((cave.IsSmall && visitedCount == 1 && !cave.IsSingleSmall()) || (cave.IsSingleSmall() && visitedCount == 2))
    {
        return;
    }
    visitedCaves.Add(cave.Name);
    if (cave.IsEnd)
    {
        Console.WriteLine(string.Join(',', visitedCaves));
        cave.VisitedTimes++;
        //Console.ReadKey();
    }
    else
    {
        foreach (var c in cave.Caves)
        {
            var newVisitedCaves = new List<string>(visitedCaves);
            Traverse(caveSystem, c, newVisitedCaves);
        }
    }
}

class Cave
{
    public Cave(string name)
    {
        Name = name;
        IsSmall = name.ToLower() == name;
        IsBig = name.ToUpper() == name;
        IsEnd = name == "end";
        IsStart = name == "start";
    }

    public string Name { get; set; }

    public bool IsSmall { get; set; }
    public bool IsBig { get; set; }
    public bool IsSingleSmall() 
    {
        return !IsStart && !IsEnd && !IsBig && Caves.Any(c => c.IsBig);
    }
    public bool IsEnd { get; set; }
    public bool IsStart { get; set; }

    public int VisitedTimes { get; set; } = 0;

    public List<Cave> Caves { get; set; } = new List<Cave>();

    public override string ToString()
    {
        return Name;
    }

}
