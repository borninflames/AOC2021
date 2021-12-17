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

            rows = lines.Length;
            cols = lines[0].Length;

            var cavern = new RiskPoint[rows, cols];
            var caveSystem = new Dictionary<Point, RiskPoint>();

            for (int r = 0; r < lines.Length; r++)
            {
                for (int c = 0; c < lines[r].Length; c++)
                {
                    //cavern[r, c] = new RiskPoint(Convert.ToInt32(lines[r][c].ToString()), r, c);
                    //caveSystem.Add(new Point(c, r), new RiskPoint(Convert.ToInt32(lines[r][c].ToString()), r, c));
                    caveSystem[new Point(c, r)] = new RiskPoint(Convert.ToInt32(lines[r][c].ToString()), r, c);
                    if (r == 0 && c == 0)
                    {
                        //cavern[r, c].RiskToReach = 0;
                        caveSystem[new Point(0, 0)].RiskToReach = 0;
                    }
                }
            }

            Console.CursorVisible = false;

            //Traverse(cavern, cavern[0, 0]);
            Dijkstra(caveSystem, caveSystem[new Point(0, 0)]);

            //ShowMatrix(cavern);

            Console.CursorVisible = true;

            Console.WriteLine();
            Console.WriteLine(caveSystem[new Point(cols - 1, rows - 1)].RiskToReach);
        }

        public static void Dijkstra(Dictionary<Point, RiskPoint> caveSystem, RiskPoint startingPoint)
        {
            var cavesToCheck = new List<RiskPoint>();
            cavesToCheck.Add(startingPoint);
            //startingPoint.IsVisited = true;
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

        public static void FindPath(RiskPoint[,] cavern, RiskPoint startingPoint)
        {
            //var cavesToCheck = new List<Point>();
            //cavesToCheck.Add(startingPoint);
            startingPoint.IsVisited = true;
            var visitedcount = 0;

            while (visitedcount < rows * cols)
            {
                //cavesToCheck = cavesToCheck.OrderBy(c => c.RiskToReach).ToList();
                var currentCave = cavern[0, 0]; //cavesToCheck.First();
                //cavesToCheck.RemoveAt(0);

                visitedcount++;

                //ShowMatrix(cavern);

                if (currentCave.Col < cols - 1 && !cavern[currentCave.Row, currentCave.Col + 1].IsVisited)
                {
                    var nextCave = cavern[currentCave.Row, currentCave.Col + 1];
                    //cavesToCheck.Add(nextCave);
                    SetLowestRisk(currentCave, nextCave);
                    //nextCave.IsVisited = true;
                }

                if (currentCave.Row < rows - 1 && !cavern[currentCave.Row + 1, currentCave.Col].IsVisited)
                {
                    var nextCave = cavern[currentCave.Row + 1, currentCave.Col];
                    //cavesToCheck.Add(nextCave);
                    SetLowestRisk(currentCave, nextCave);
                    //nextCave.IsVisited = true;
                }

                if (currentCave.Col > 0 && !cavern[currentCave.Row, currentCave.Col - 1].IsVisited)
                {
                    var nextCave = cavern[currentCave.Row, currentCave.Col - 1];
                    //cavesToCheck.Add(nextCave);
                    SetLowestRisk(currentCave, nextCave);
                    //nextCave.IsVisited = true;
                }

                if (currentCave.Row > 0 && !cavern[currentCave.Row - 1, currentCave.Col].IsVisited)
                {
                    var nextCave = cavern[currentCave.Row - 1, currentCave.Col];
                    //cavesToCheck.Add(nextCave);
                    SetLowestRisk(currentCave, nextCave);
                    //nextCave.IsVisited = true;
                }

                currentCave.IsVisited = true;
            }
        }

        public static void Traverse(RiskPoint[,] cavern, RiskPoint currentCave)
        {
            if (currentCave.IsVisited)
            {
                return;
            }

            currentCave.IsVisited = true;

            ShowMatrix(cavern);
            var cavesToVisit = new List<RiskPoint>();
            if (currentCave.Col < cols - 1 && !cavern[currentCave.Row, currentCave.Col + 1].IsVisited)
            {
                var nextCave = cavern[currentCave.Row, currentCave.Col + 1];
                cavesToVisit.Add(nextCave);
                SetLowestRisk(currentCave, nextCave);
            }

            if (currentCave.Row < rows - 1 && !cavern[currentCave.Row + 1, currentCave.Col].IsVisited)
            {
                var nextCave = cavern[currentCave.Row + 1, currentCave.Col];
                cavesToVisit.Add(nextCave);
                SetLowestRisk(currentCave, nextCave);
            }

            if (currentCave.Col > 0 && !cavern[currentCave.Row, currentCave.Col - 1].IsVisited)
            {
                var nextCave = cavern[currentCave.Row, currentCave.Col - 1];
                cavesToVisit.Add(nextCave);
                SetLowestRisk(currentCave, nextCave);
            }

            if (currentCave.Row > 0 && !cavern[currentCave.Row - 1, currentCave.Col].IsVisited)
            {
                var nextCave = cavern[currentCave.Row - 1, currentCave.Col];
                cavesToVisit.Add(nextCave);
                SetLowestRisk(currentCave, nextCave);
            }

            cavesToVisit = cavesToVisit.OrderBy(c => c.RiskToReach).ToList();
            foreach (var cave in cavesToVisit)
            {
                Traverse(cavern, cave);
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