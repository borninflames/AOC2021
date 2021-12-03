using System.Text;

Console.WriteLine("Hello, Advent of Code 2021!");

var linesOriginal = File.ReadAllLines("Day3a.txt");
var linesForOxigenGenRating = linesOriginal.ToArray();
var linesForCo2GenRating = linesOriginal.ToArray();

var i = 0;
while (linesForOxigenGenRating.Length > 1 && i < linesForOxigenGenRating[0].Length)
{
    var bc = new BitCount(linesForOxigenGenRating, i);
    bc.Count();

    var filterBit = bc.Ones>= bc.Zeros ? '1' : '0';

    linesForOxigenGenRating = linesForOxigenGenRating.Where(l => l[i] == filterBit).ToArray();
    i++;
}

var oxigenGeneratorRating = Convert.ToInt32(linesForOxigenGenRating[0], 2);
Console.WriteLine(oxigenGeneratorRating);

i = 0;
while (linesForCo2GenRating.Length > 1 && i < linesForCo2GenRating[0].Length)
{
    var bc = new BitCount(linesForCo2GenRating, i);
    bc.Count();

    var filterBit = bc.Zeros <= bc.Ones ? '0' : '1';

    linesForCo2GenRating = linesForCo2GenRating.Where(l => l[i] == filterBit).ToArray();
    i++;
}

var co2GeneratorRating = Convert.ToInt32(linesForCo2GenRating[0], 2);
Console.WriteLine(co2GeneratorRating);
Console.WriteLine(co2GeneratorRating * oxigenGeneratorRating);








class BitCount
{
    public BitCount(string[] lines, int pos)
    {
        Lines = lines;
        Pos = pos;
    }

    public string[] Lines { get; set; }

    public int Pos { get; set; }

    public int Zeros { get; set; } = 0;
    public int Ones { get; set; } = 0;

    public void Count()
    {
        for (int i = 0; i < Lines.Length; i++)
        {

            if (Lines[i][Pos] == '0')
            {
                Zeros++;
            }
            else
            {
                Ones++;
            }

        }
    }

}

