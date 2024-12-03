using System.Text.RegularExpressions;

StreamReader sr = new("./input.txt");
string? line = sr.ReadLine();
string input = "";
while (line != null)
{
    input += line;
    line = sr.ReadLine();
}

var part = Environment.GetEnvironmentVariable("part");
var solution = part switch
{
    "part1" => Part1(input), //196826776
    "part2" => Part2(input), //106780429
    _ => throw new ArgumentOutOfRangeException(nameof(part), $"Unexpected {nameof(part)} value: '{part}'")
};

Console.WriteLine(solution);

int Part1(string input)
{
    MatchCollection matches = Regex.Matches(input, @"mul\((\d+),(\d+)\)");
    int product = 0;
    foreach (Match match in matches) product += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
    return product;
}

int Part2(string input)
{
    string mulPattern = @"mul\((\d+),(\d+)\)";
    string dontPattern = @"don't\(\)";
    string doPattern = @"do\(\)";
    MatchCollection matches = Regex.Matches(input, $"{mulPattern}|{dontPattern}|{doPattern}");
    int product = 0;
    bool prevIsDont = false;
    foreach (Match match in matches)
    {
        if (Regex.IsMatch(match.Value, dontPattern)) prevIsDont = true;
        else if (Regex.IsMatch(match.Value, doPattern)) prevIsDont = false;
        else
        {
            if (!prevIsDont) product += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

    }
    return product;
}