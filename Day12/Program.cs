namespace Day12;

public class Row{
    public Row(string row)
    {
        var splitRow = row.Split(" ");

        SourceSpring = splitRow[0].ToCharArray().ToList();

        Groups = splitRow[1].Split(',').Select(x => int.Parse(x)).ToList();
    }

    List<char> SourceSpring = new List<char>();
    List<int> Groups = new List<int>();

    public int FindValidCombinations(){
        return Find(0, -1, 0, 0);
    }

    //#.#.### 1,1,3
    private int Find(int index, int groupIndex, int remainingGroupCount, int totalCount){
        if (index >= SourceSpring.Count){
            if (remainingGroupCount != 0){
                return 0;
            }

            if (groupIndex != Groups.Count() - 1){
                return 0;
            }

            return 1;
        }

        char currentChar = SourceSpring[index];

        if (currentChar == '#'){
            if (remainingGroupCount == 0){
                if (index > 0 && SourceSpring[index - 1] == '#'){
                    return 0;
                }
                groupIndex ++;

                if (groupIndex >= Groups.Count){
                    return 0;
                }

                remainingGroupCount = Groups[groupIndex];
            }

            remainingGroupCount -= 1;
        }
        else if (currentChar == '.'){
            if (remainingGroupCount != 0){
                return 0;
            }
        }
        else if (currentChar == '?'){
            //Act like the current char is #
            SourceSpring[index] = '#';
            var total1 = Find(index,groupIndex,remainingGroupCount,totalCount);

            //Act like the current char is .
            SourceSpring[index] = '.';
            var total2 = Find(index,groupIndex,remainingGroupCount,totalCount);

            SourceSpring[index] = '?';
            return totalCount + total1 + total2;
        }

        return Find(index + 1, groupIndex, remainingGroupCount, totalCount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllLines("./input.txt");

        int sum = 0;
        int sum2 = 0;
        foreach (var item in file)
        {
            Row row = new Row(item);

            sum += row.FindValidCombinations();

            sum += row.FindValidCombinations();
        }

        System.Console.WriteLine($"Part1 Sum: {sum}");

        long index = 0;

        foreach (var item in file)
        {
            Row row = new Row(item);

            sum += row.FindValidCombinations();

            sum += row.FindValidCombinations();

            string multiCopies = "";

            List<string> Springs = new List<string>();
            List<string> Conts = new List<string>();
            var splitRow = item.Split(' ');
            for (int i = 0; i < 5; i++)
            {
                Springs.Add(splitRow[0]); 
                Conts.Add(splitRow[1]); 
            }

            multiCopies = string.Join("?", Springs) + " " + string.Join(",",Conts);

            Row row2 = new Row(multiCopies);

            sum2 += row2.FindValidCombinations();

            System.Console.WriteLine($"{index}/{file.Length}");
            index ++;
        }

        System.Console.WriteLine($"Part2 Sum: {sum2}");
    }
}
