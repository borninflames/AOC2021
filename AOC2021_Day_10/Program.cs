Console.WriteLine("Hello, Advent of Code 2021!");

//var lines = File.ReadAllLines("TestInput.txt");
var lines = File.ReadAllLines("Input.txt");

var brackets = new Dictionary<char, char>
{
    { '[', ']' },
    { '(', ')' },
    { '{', '}' },
    { '<', '>' }
};

var characterScore = new Dictionary<char, int>
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 }
};

var sum = 0;

foreach (var line in lines)
{
    var stack = new Stack<char>();
    Console.WriteLine(IsValid(line, stack));
}

Console.WriteLine();
Console.WriteLine($"The answer is: {sum}");

bool IsValid(string chunks, Stack<char> stack)
{
    if (chunks.Length == 0)
    {
        return true;
    }

    var ch = chunks[0];
    var subString = chunks.Substring(1);

    if (brackets.ContainsKey(ch))
    {
        stack.Push(ch);
        return IsValid(subString, stack);
    }

    if (stack.Count == 0)
    {
        return false;
    }

    var lastOpen = stack.Pop();

    if (brackets[lastOpen] != ch)
    {
        Console.WriteLine($"Illegal character found: {ch}");
        sum += characterScore[ch];
        return false;
    }

    return IsValid(subString, stack);
}