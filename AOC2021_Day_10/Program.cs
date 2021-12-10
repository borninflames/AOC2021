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

var autoCompleteScore = new Dictionary<char, int>
{
    { ')', 1 },
    { ']', 2 },
    { '}', 3 },
    { '>', 4 }
};

var sum = 0;

var autocompleteScores = new List<long>();


foreach (var line in lines)
{
    var stack = new Stack<char>();
    var isValid = IsValid(line, stack);
    Console.WriteLine(isValid);

    if (isValid && stack.Count > 0)
    {
        var score = AutoCompleteScore(stack);
        Console.WriteLine($"AutoComplete score: {score}");
        autocompleteScores.Add(score);
    }
}

Console.WriteLine();
Console.WriteLine($"The answer is: {sum}");

autocompleteScores = autocompleteScores.OrderBy(x => x).ToList();
var answer = autocompleteScores[autocompleteScores.Count / 2];

Console.WriteLine($"The second part's answer: {answer}");

long AutoCompleteScore(Stack<char> stack) 
{
    long sum = 0;

    while (stack.Count > 0)
    {
        var chOpen = stack.Pop();
        var chClose = brackets[chOpen];
        sum *= 5;
        sum += autoCompleteScore[chClose];
    }

    return sum;
}

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