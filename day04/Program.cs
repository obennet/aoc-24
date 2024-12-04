using System.Text.RegularExpressions;

string[] input = File.ReadAllText("./input.txt").Split("\n");

var part = Environment.GetEnvironmentVariable("part");
var solution = part switch
{
    "part1" => Part1(input), //2599
    "part2" => Part2(input), //1948
    _ => throw new ArgumentOutOfRangeException(nameof(part), $"Unexpected {nameof(part)} value: '{part}'")
};

Console.WriteLine(solution);

int Part1(string[] input)
{
    int nXmas = 0;
    for (int row = 0; row < input.Length; row++)
    {
        nXmas += getHorizontal(input[row]);
        for (int col = 0; col < input[row].Length; col++)
        {
            if (input[row][col] == 'X')
            {
                nXmas += getVertical(input, row, col);
                nXmas += getDiagonal(input, row, col);
            }
        }
    }
    return nXmas;
}

int getHorizontal(string text)
{
    MatchCollection matchesForwards = Regex.Matches(text, "XMAS");
    MatchCollection matchesBackwards = Regex.Matches(text, "SAMX");
    return matchesForwards.Count + matchesBackwards.Count;
}

int getDiagonal(string[] input, int row, int col)
{
    string xmas = "XMAS";
    int[] diagonalCounts = [0, 0, 0, 0];
    int nDiagonal = 0;
    for (int i = 0; i < xmas.Length; i++)
    {
        try
        {
            if (input[row + i][col - i] == xmas[i]) diagonalCounts[0]++;
        }
        catch (Exception) { }
        try
        {
            if (input[row - i][col - i] == xmas[i]) diagonalCounts[1]++;
        }
        catch (Exception) { }
        try
        {
            if (input[row - i][col + i] == xmas[i]) diagonalCounts[2]++;
        }
        catch (Exception) { }
        try
        {
            if (input[row + i][col + i] == xmas[i]) diagonalCounts[3]++;
        }
        catch (Exception) { }
    }
    foreach(int count in diagonalCounts) if(count == 4) nDiagonal++;

    return nDiagonal;
}

int getVertical(string[] input, int row, int col)
{
    int nVertical = 0;
    string xmas = "XMAS";
    if (row > 2 && isVerticalUp(input, row, col, xmas)) nVertical++;
    if (row < input.Length - 3 && isVerticalDown(input, row, col, xmas)) nVertical++;
    return nVertical;
}

bool isVerticalUp(string[] input, int row, int col, string xmas)
{
    for (int i = 0; i < xmas.Length; i++)
    {
        if (input[row - i][col] != xmas[i])
        {
            return false;
        }
    }
    return true;
}

bool isVerticalDown(string[] input, int row, int col, string xmas)
{
    for (int i = 0; i < xmas.Length; i++)
    {
        if (input[row + i][col] != xmas[i])
        {
            return false;
        }
    }
    return true;
}

int Part2(string[] input)
{
    int nXmas = 0;
    for (int i = 1; i < input.Length - 1; i++)
    {
        MatchCollection matches = Regex.Matches(input[i], "A");
        foreach (Match match in matches)
        {
            int index = match.Index;
            if(index<1 || index > input[i-1].Length - 2 || index > input[i+1].Length - 2) continue;
            if (Regex.IsMatch(input[i - 1].Substring(index - 1, 3), "M.S") && Regex.IsMatch(input[i + 1].Substring(index - 1, 3), "M.S")) nXmas++;
            else if (Regex.IsMatch(input[i - 1].Substring(index - 1, 3), "S.S") && Regex.IsMatch(input[i + 1].Substring(index - 1, 3), "M.M")) nXmas++;
            else if (Regex.IsMatch(input[i - 1].Substring(index - 1, 3), "M.M") && Regex.IsMatch(input[i + 1].Substring(index - 1, 3), "S.S")) nXmas++;
            else if (Regex.IsMatch(input[i - 1].Substring(index - 1, 3), "S.M") && Regex.IsMatch(input[i + 1].Substring(index - 1, 3), "S.M")) nXmas++;
        }
    }
    return nXmas;
}