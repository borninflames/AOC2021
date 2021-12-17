using System.Drawing;

namespace AOC2021_Day_15
{
    class Program
    {
        static int rows = 0;
        static int cols = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2021!");

            //var lines = File.ReadAllLines("TestInput.txt");
            var lines = File.ReadAllLines("Input.txt");

            var caveSystem = GenerateCaveSystem(lines, 5);

            Console.CursorVisible = false;

            Dijkstra(caveSystem, caveSystem[new Point(0, 0)]);

            //ShowMatrix(cavern);

            Console.CursorVisible = true;

            Console.WriteLine();
            Console.WriteLine(caveSystem[new Point(cols - 1, rows - 1)].RiskToReach);
        }

        static Dictionary<Point, RiskPoint> GenerateCaveSystem(string[] lines, int times)
        {
            rows = lines.Length * times;
            cols = lines[0].Length * times;

            var caveSystem = new Dictionary<Point, RiskPoint>();

            for (int horiz = 0; horiz < times; horiz++)
            {
                for (int vert = 0; vert < times; vert++)
                {

                    for (int r = 0; r < lines.Length; r++)
                    {
                        for (int c = 0; c < lines[r].Length; c++)
                        {
                            var col = c + horiz * lines[r].Length;
                            var row = r + vert * lines.Length;
                            var point = new Point(col, row);
                            var newRisk = Convert.ToInt32(lines[r][c].ToString());
                            newRisk += horiz + vert;
                            if (newRisk > 9)
                            {
                                newRisk -= 9;
                            }
                            caveSystem[point] = new RiskPoint(newRisk, row, col);
                            if (r == 0 && c == 0)
                            {
                                caveSystem[new Point(0, 0)].RiskToReach = 0;
                            }
                        }
                    }

                }
            }

            

            return caveSystem;
        }

        public static void Dijkstra(Dictionary<Point, RiskPoint> caveSystem, RiskPoint startingPoint)
        {
            var cavesToCheck = new List<RiskPoint>();
            cavesToCheck.Add(startingPoint);
            while (cavesToCheck.Any(c => !c.IsVisited))
            {
                cavesToCheck = cavesToCheck.Where(c => !c.IsVisited).OrderBy(c => c.RiskToReach).ToList();
                var currentCave = cavesToCheck.First();
                cavesToCheck.RemoveAt(0);
                currentCave.IsVisited = true;
                //ShowMatrix(cavern);

                if (currentCave.Col < cols - 1 && !caveSystem[new Point(currentCave.Col + 1, currentCave.Row)].IsVisited)
                {
                    var nextCave = caveSystem[new Point(currentCave.Col + 1, currentCave.Row)];
                    cavesToCheck.Add(nextCave);
                    SetLowestRisk(currentCave, nextCave);
                }

                if (currentCave.Row < rows - 1 && !caveSystem[new Point(currentCave.Col, currentCave.Row + 1)].IsVisited)
                {
                    var nextCave = caveSystem[new Point(currentCave.Col, currentCave.Row + 1)];
                    cavesToCheck.Add(nextCave);
                    SetLowestRisk(currentCave, nextCave);
                }

                if (currentCave.Col > 0 && !caveSystem[new Point(currentCave.Col - 1, currentCave.Row)].IsVisited)
                {
                    var nextCave = caveSystem[new Point(currentCave.Col - 1, currentCave.Row)];
                    cavesToCheck.Add(nextCave);
                    SetLowestRisk(currentCave, nextCave);
                }

                if (currentCave.Row > 0 && !caveSystem[new Point(currentCave.Col, currentCave.Row - 1)].IsVisited)
                {
                    var nextCave = caveSystem[new Point(currentCave.Col, currentCave.Row - 1)];
                    cavesToCheck.Add(nextCave);
                    SetLowestRisk(currentCave, nextCave);
                }
            }
        }

        public static void ShowMatrix(RiskPoint[,] cavern)
        {
            for (int r = 0; r < cavern.GetLength(0); r++)
            {
                for (int c = 0; c < cavern.GetLength(1); c++)
                {
                    Console.SetCursorPosition(c, r + 1);
                    if (cavern[r, c].IsVisited)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.Write(cavern[r, c].Risk);
                }
            }
        }

        private static void SetLowestRisk(RiskPoint currentCave, RiskPoint cave)
        {
            if (cave.RiskToReach > cave.Risk + currentCave.RiskToReach)
            {
                cave.RiskToReach = cave.Risk + currentCave.RiskToReach;
            }
        }
    }

    class RiskPoint
    {
        public RiskPoint(int risk, int row, int col)
        {
            Risk = risk;
            Row = row;
            Col = col;
        }

        public int Risk { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public bool IsVisited { get; set; }
        public int RiskToReach { get; set; } = int.MaxValue;

        public override string ToString()
        {
            return $"[{Row}, {Col}] <<{RiskToReach}>> | {Risk}";
        }
    }
}