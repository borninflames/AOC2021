Console.WriteLine("Hello, Advent of Code 2021!");

//var lines = File.ReadAllLines("TestInput2.txt");
var lines = File.ReadAllLines("Input.txt");
var caveSystem = new Dictionary<string, Cave>();

List<List<string>> paths = new();

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


void Traverse(Dictionary<string, Cave> caveSystem, Cave cave, List<string> visitedCaves)
{
    visitedCaves.Add(cave.Name);
    if (cave.IsEnd)
    {
        Console.WriteLine(string.Join(',', visitedCaves));
        paths.Add(visitedCaves.ToList());
        cave.VisitedTimes++;
        return;
    }

    var hasMultipleSmallCavesVisited = visitedCaves.Any(c => c.ToLower() == c && visitedCaves.Count(v => v == c) == 2);

    var nextCaves = hasMultipleSmallCavesVisited ? 
        cave.Caves.Where(c => !c.IsSmall || cave.IsEnd || !visitedCaves.Contains(c.Name) && !c.IsStart).ToList() :
        cave.Caves.Where(c => !c.IsStart).ToList();

    foreach (var c in nextCaves)
    {
        var newVisitedCaves = visitedCaves.ToList();

        Traverse(caveSystem, c, newVisitedCaves);
    }

}

class Cave
{
    public Cave(string name)
    {
        Name = name;
        IsSmall = name.ToLower() == name;
        IsEnd = name == "end";
        IsStart = name == "start";
    }

    public string Name { get; set; }

    public bool IsSmall { get; set; }
    public bool IsEnd { get; set; }
    public bool IsStart { get; set; }

    public int VisitedTimes { get; set; } = 0;

    public List<Cave> Caves { get; set; } = new List<Cave>();

    public override string ToString()
    {
        return Name;
    }

}