using System.Text;

namespace AOC2021_Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2021!");

            //var lines = File.ReadAllLines("TestInput.txt");
            var lines = File.ReadAllLines("Input.txt");
            var rules = new Dictionary<string, char>();
            var pairs = new Dictionary<string, long>();
            var letters = new Dictionary<char, long>();
            var template = lines[0];
            lines = lines.Skip(2).ToArray();

            foreach (var line in lines)
            {
                var map = line.Split(" -> ");
                rules.Add(map[0], map[1][0]);
            }

            for (int i = 0; i < template.Length; i++)
            {
                if (letters.ContainsKey(template[i])) {
                    letters[template[i]]++;
                }
                else
                {
                    letters.Add(template[i], 1);
                }

                if (i < template.Length - 1)
                {
                    var pair = template.Substring(i, 2);
                    if (pairs.ContainsKey(pair))
                    {
                        pairs[pair]++;
                    }
                    else
                    {
                        pairs.Add(pair, 1);
                    }
                }
            }


            for (int step = 1; step <= 40; step++)
            {
                var newPairs = new Dictionary<string, long>();
                foreach (var pair in pairs.Keys)
                {
                    var newChar = rules[pair];                    
                    var newPair1 = $"{pair[0]}{newChar}";
                    var newPair2 = $"{newChar}{pair[1]}";

                    var count = pairs[pair];
                    if (newPairs.ContainsKey(newPair1))
                    {
                        newPairs[newPair1] += count;
                    }
                    else
                    {
                        newPairs[newPair1] = count;
                    }

                    if (newPairs.ContainsKey(newPair2))
                    {
                        newPairs[newPair2] += count;
                    }
                    else
                    {
                        newPairs[newPair2] = count;
                    }

                    if (letters.ContainsKey(newChar))
                    {
                        letters[newChar] += count;
                    }
                    else
                    {
                        letters[newChar] = count;
                    }
                }
                pairs = newPairs;
            }

            var answer = letters.Values.Max() - letters.Values.Min();

            Console.WriteLine(answer);
        }
    }
}
