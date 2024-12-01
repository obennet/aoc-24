StreamReader sr = new("./input.txt");
string? line = sr.ReadLine();
List<int> left = [];
List<int> right = [];
while (line != null)
{
    string[] locations = line.Split("   ");
    left.Add(int.Parse(locations[0]));
    right.Add(int.Parse(locations[1]));
    line = sr.ReadLine();
}
sr.Close();

var part = Environment.GetEnvironmentVariable("part");
var solution = part switch
{
    "part1" => Part1(left, right), // 1666427
    "part2" => Part2(left, right), // 24316233
    _ => throw new ArgumentOutOfRangeException(nameof(part), $"Unexpected {nameof(part)} value: '{part}'")
};

Console.WriteLine(solution);

int Part1(List<int> left, List<int> right)
{
    left.Sort((a, b) => a.CompareTo(b));
    right.Sort((a, b) => a.CompareTo(b));

    int distance = 0;
    for (int i = 0; i < left.Count; i++)
    {
        distance += Math.Abs(left[i] - right[i]);
    }

    return distance;
}

int Part2(List<int> left, List<int> right)
{
    IEnumerable<IGrouping<int, int>> groupedRight = right.GroupBy(i => i);
    Dictionary<int, int> counts = [];
    int score = 0;

    foreach (var group in groupedRight)
    {
        counts.Add(group.Key, group.Count());
    }
    foreach (int l in left)
    {
        if (counts.ContainsKey(l))
        {
            score += l * counts[l];
        }
    }
    return score;
}