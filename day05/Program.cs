string[] input = File.ReadAllText("./input.txt").Split("\n\n");
List<List<int>> rules = [];
foreach (string rule in input[0].Split("\n")) rules.Add(rule.Split("|").Select(Int32.Parse).ToList());

List<List<int>> numbers = [];
foreach (string number in input[1].Split("\n")) numbers.Add(number.Split(",").Select(Int32.Parse).ToList());


var part = Environment.GetEnvironmentVariable("part");
var solution = part switch
{
    "part1" => Part1(rules, numbers),
    "part2" => Part2(input),
    _ => throw new ArgumentOutOfRangeException(nameof(part), $"Unexpected {nameof(part)} value: '{part}'")
};

Console.WriteLine(solution);

int Part1(List<List<int>> rules, List<List<int>> numbers)
{
    List<int> validRowsIndex = [];
    for (int i = 0; i < numbers.Count; i++)
    {
        bool isValidRow = true;
        for (int j = 0; j < numbers[i].Count; j++)
        {
            if (!isValidPos(j, numbers[i], rules)){
                isValidRow=false;
                break;
            }
        }
        if(isValidRow) validRowsIndex.Add(i);
    }
    int medianSum = 0;
    foreach(int index in validRowsIndex)
    {
        medianSum += numbers[index][numbers[index].Count/2];
    }
    return medianSum;
}

bool isValidPos(int index, List<int> numbers, List<List<int>> rules)
{
    for (int i = 0; i < rules.Count; i++)
    {
        if(rules[i][0] == numbers[index])
        {
            int nextIndex = numbers.FindIndex(n => n == rules[i][1]);
            if(nextIndex != -1 && nextIndex <= index) return false;
        }
        if(rules[i][1] == numbers[index])
        {
            int prevIndex = numbers.FindIndex(n => n == rules[i][0]);
            if(prevIndex != -1 && prevIndex >= index) return false;
        }
    }
    return true;
}

int Part2(string[] input)
{
    return 0;
}