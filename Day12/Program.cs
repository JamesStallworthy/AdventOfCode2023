using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Day12;

public class Row{
    public Row(string row)
    {
        var splitRow = row.Split(" ");

        Springs = splitRow[0];

        Groups = splitRow[1].Split(',').Select(x => long.Parse(x)).ToList();
    }

    string Springs;
    List<long> Groups = new List<long>();

    Dictionary<string, long> Cache = new Dictionary<string, long>();

    public long FindValidCombinations(){
        return Find(Springs, ' ', -1, 0, 0);
    }

    private long FindLru(string springs, char previousChar,int groupIndex, long remainingGroupCount, long totalCount){
        string key = $"{previousChar}{springs}_{groupIndex}_{remainingGroupCount}";
        if (Cache.ContainsKey(key) && !key.StartsWith('?'))
            return Cache[key];
        else{
            var result = Find(springs, previousChar, groupIndex, remainingGroupCount, totalCount);
            if (!key.StartsWith('?'))
                Cache.Add(key, result);
            return result;
        }
    }

    //#.#.### 1,1,3
    private long Find(string springs, char previousChar,int groupIndex, long remainingGroupCount, long totalCount){
        if (springs.Length == 0){
            if (remainingGroupCount != 0){
                return 0;
            }

            if (groupIndex != Groups.Count() - 1){
                return 0;
            }

            return 1;
        }

        char currentChar = springs.First();

        if (currentChar == '#'){
            if (remainingGroupCount == 0){
                if (previousChar == '#'){
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
            string newSprings1 = "#" + springs[1..];
            var total1 = FindLru(newSprings1,previousChar,groupIndex,remainingGroupCount,totalCount);

            //Act like the current char is .
            string newSprings2 = "." + springs[1..];
            var total2 = FindLru(newSprings2,previousChar,groupIndex,remainingGroupCount,totalCount);

            return totalCount + total1 + total2;
        }

        return FindLru(springs[1..],currentChar, groupIndex, remainingGroupCount, totalCount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllLines("./input.txt");
        Stopwatch sw = Stopwatch.StartNew();
        long sum = 0;
        long sum2 = 0;
        foreach (var item in file)
        {
            Row row = new Row(item);

            sum += row.FindValidCombinations();
        }
        sw.Stop();
        System.Console.WriteLine($"Part1 Sum: {sum}");
        System.Console.WriteLine($"Part1 time: {sw.Elapsed.TotalSeconds}");

        sw = Stopwatch.StartNew();
        foreach (var item in file)
        {
            string multiCopies = "";

            List<string> Springs = new List<string>();
            List<string> Conts = new List<string>();
            var splitRow = item.Split(' ');
            for (long i = 0; i < 5; i++)
            {
                Springs.Add(splitRow[0]); 
                Conts.Add(splitRow[1]); 
            }

            multiCopies = string.Join("?", Springs) + " " + string.Join(",",Conts);

            Row row2 = new Row(multiCopies);

            sum2 += row2.FindValidCombinations();
        }

        System.Console.WriteLine($"Part2 Sum: {sum2}");
        System.Console.WriteLine($"Part2 time: {sw.Elapsed.TotalSeconds}");
    }
}
