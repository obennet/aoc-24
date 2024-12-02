StreamReader sr = new("./input.txt");

var part = Environment.GetEnvironmentVariable("part");
var solution = part switch
{
    "part1" => Part1(sr), //220
    "part2" => Part2(sr),
    _ => throw new ArgumentOutOfRangeException(nameof(part), $"Unexpected {nameof(part)} value: '{part}'")
};

Console.WriteLine(solution);

bool checkSafety(List<int> levels)
{
    bool isAsc = levels[0] < levels[1];
    for (int i = 0; i < levels.Count - 1; i++)
        if ((levels[i] == levels[i + 1]) || (isAsc != levels[i] < levels[i + 1]) || Math.Abs(levels[i] - levels[i + 1]) > 3) return false;
    return true;
}

int Part1(StreamReader sr)
{
    int nSafe = 0;
    string? line = sr.ReadLine();
    while (line != null)
    {
        List<int>? levels = line.Split(" ")?.Select(int.Parse)?.ToList();
        if (levels == null) continue;
        if (checkSafety(levels)) nSafe++;
        line = sr.ReadLine();
    }
    sr.Close();
    return nSafe;
}

int Part2(StreamReader sr)
{
    int nSafe = 0;
    string? line = sr.ReadLine();
    while (line != null)
    {
        List<int>? levels = line.Split(" ")?.Select(int.Parse)?.ToList();
        if (levels == null) continue;
        if (checkSafety(levels)) nSafe++;
        else
        {
            for (int i = 0; i < levels.Count; i++)
            {
                List<int> modifiedLevels = new(levels);
                modifiedLevels.RemoveAt(i);
                if (checkSafety(modifiedLevels))
                {
                    nSafe++;
                    break;
                }
            }
        }
        line = sr.ReadLine();
    }
    sr.Close();
    return nSafe;
}