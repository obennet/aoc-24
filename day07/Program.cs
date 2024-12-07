
string[] file = File.ReadAllText("./input.txt").Split("\n");

var part = Environment.GetEnvironmentVariable("part");
var solution = part switch
{
    "part1" => Part1(file), //663613490587
    "part2" => Part2(file), //110365987435001
    _ => throw new ArgumentOutOfRangeException(nameof(part), $"Unexpected {nameof(part)} value: '{part}'")
};

Console.WriteLine(solution);

long Part1(string[] file)
{
    return TotalSum(file, "*+");
}
long Part2(string[] file)
{
    return TotalSum(file, "*+|");
}

static IEnumerable<string> CombinationsWithRepetition(string input, int length)
{
    if (length <= 0)
        yield return "";
    else
    {
        foreach (char i in input)
            foreach (var c in CombinationsWithRepetition(input, length - 1))
                yield return i + c;
    }
}

(long result, int[] equation) ParseInputRow(string row)
{
    string[] input = row.Split(": ");
    long result = long.Parse(input[0]);
    int[] equation = input[1].Split(" ").Select(int.Parse).ToArray();
    return (result, equation);
}

long TotalSum(string[] file, string operators)
{
    long sum = 0;
    foreach (string row in file)
    {
        (long trueResult, int[] equation) = ParseInputRow(row);
        foreach (string combination in CombinationsWithRepetition(operators, equation.Length - 1))
        {
            long result = equation[0];
            for (int i = 1; i < equation.Length; i++)
            {
                if (combination[i - 1] == '*') result *= equation[i];
                else if (combination[i - 1] == '+') result += equation[i];
                else result = long.Parse(result + "" + equation[i]);

            }
            if (trueResult == result)
            {
                sum += result;
                break;
            }
        }
    }
    return sum;
}