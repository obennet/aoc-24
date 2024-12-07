string[] input = File.ReadAllText("./input.txt").Split("\n");
List<List<char>> map = [[]];
foreach (string row in input)
{
    map.Add(row.ToCharArray().ToList());
}

var part = Environment.GetEnvironmentVariable("part");
var solution = part switch
{
    "part1" => Part1(map),
    "part2" => Part2(input),
    _ => throw new ArgumentOutOfRangeException(nameof(part), $"Unexpected {nameof(part)} value: '{part}'")
};

Console.WriteLine(solution);

int Part1(List<List<char>> map)
{
    (int row, int col, char guard) = findGuard(map);
    int nPositions = 0;
    while (true)
    {
        if (guard == '^')
        {
            if (row - 1 < 0) break;
            if (map[row - 1][col] == '#') guard = '>';
            else
            {

                row--;
                if (map[row][col] != 'X')
                {
                    map[row][col] = 'X';
                    nPositions++;
                }

            }

        }
        else if (guard == '>')
        {
            if (col + 1 >= map[row].Count) break;
            if (map[row][col + 1] == '#') guard = 'v';
            else
            {
                col++;
                if (map[row][col] != 'X')
                {
                    map[row][col] = 'X';
                    nPositions++;
                }
            }
        }
        else if (guard == 'v')
        {
            if (row + 1 >= map.Count) break;
            if (map[row + 1][col] == '#') guard = '<';
            else
            {
                row++;
                if (map[row][col] != 'X')
                {
                    map[row][col] = 'X';
                    nPositions++;
                }
            }
        }
        else if (guard == '<')
        {
            if (col - 1 < 0) break;
            if (map[row][col - 1] == '#') guard = '^';
            else
            {
                col--;
                if (map[row][col] != 'X')
                {
                    map[row][col] = 'X';
                    nPositions++;
                }
            }
        }
    }
    return nPositions;
}

(int, int, char) findGuard(List<List<char>> map)
{
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[i].Count; j++)
        {
            if (new char[] { '<', '^', '>', 'v' }.Contains(map[i][j])) return (i, j, map[i][j]);
        }
    }
    return (-1, -1, 'x');
}

int Part2(string[] input)
{
    int nSteps = 0;
    return nSteps;
}