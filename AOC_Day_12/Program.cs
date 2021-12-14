Console.WriteLine("Hello, Advent of Code 2021!");

//var lines = File.ReadAllLines("TestInput.txt");
var lines = File.ReadAllLines("Input.txt");
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
    if (cave.IsSmall && visitedCaves.Contains(cave.Name) )
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
        IsEnd = name == "end";
    }

    public string Name { get; set; }

    public bool IsSmall { get; set; }
    public bool IsEnd { get; set; }

    public int VisitedTimes { get; set; } = 0;

    public List<Cave> Caves { get; set; } = new List<Cave>();

    public override string ToString()
    {
        return Name;
    }

}
