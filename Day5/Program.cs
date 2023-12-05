using System.Diagnostics;

namespace Day5;

class MapGroup {
    List<Map> Maps {get; set;} = new List<Map>();

    public MapGroup(string line)
    {
        var maps = line.Split(":")[1].Split(Environment.NewLine).Where(x => !string.IsNullOrEmpty(x));

        foreach (var map in maps)
        {
            var values = map.Split(" ").Select(x => long.Parse(x)).ToList();
            Maps.Add(new Map(values[0], values[1], values[2]));
        }
    }

    public long Process(long input){
        long output = input;
        foreach (var map in Maps)
        {
            if (map.Process(input, out output))
                break;
        }

        return output;
    }
}

class Map{
    private long DestRangeStart {get; set;}
    private long SrcRangeStart {get; set;}
    private long SrcRangeEnd {get => SrcRangeStart + (Range - 1);}
    private long Range {get; set;}
    public Map(long destRangeStart, long srcRangeStart, long range)
    {
        DestRangeStart = destRangeStart;
        SrcRangeStart = srcRangeStart;
        Range = range;
    }

    public bool Process(long input, out long output){
        //Check if input is in the src range
        if (SrcRangeStart <= input && input <= SrcRangeEnd){
            long offset = input - SrcRangeStart;

            output = DestRangeStart + offset;
            return true;
        }
        output = input;
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string input = File.ReadAllText("./input.txt");

        var splitInput = input.Split($"{Environment.NewLine}{Environment.NewLine}");

        List<long> Seeds = new List<long>();
        List<MapGroup> MapGroups = new List<MapGroup>();

        //Process Seeds
        Seeds = splitInput[0].Split(": ")[1].Split(" ").Select(x => long.Parse(x)).ToList();

        for (long i = 1; i < splitInput.Length; i++)
        {
            MapGroups.Add(new MapGroup(splitInput[i]));
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        List<long> AnswerPart1 = ParseSeeds(Seeds, MapGroups);

        System.Console.WriteLine($"Part one answer: {AnswerPart1.OrderBy(x => x).First()}");

        long AnswerPart2 = GetPart2Seeds(Seeds, MapGroups);

        System.Console.WriteLine($"Part two answer: {AnswerPart2}");
        sw.Stop();
        System.Console.WriteLine($"Total time {sw.Elapsed.TotalSeconds}");
    }

    private static List<long> ParseSeeds(List<long> Seeds, List<MapGroup> MapGroups)
    {
        List<long> Answers = new List<long>();
        foreach (var seed in Seeds)
        {
            var temp = seed;

            foreach (var mg in MapGroups)
            {
                temp = mg.Process(temp);
            }

            Answers.Add(temp);
        }

        return Answers;
    }

    //This is just brute forcing it! Maybe ill come back and update this to be smarter.
    //On a Apple M1 Pro this took 776 seconds. So not too bad...
    private static long GetPart2Seeds(List<long> seeds, List<MapGroup> MapGroups){
        long numberOfSeeds = 0;
        for (int i = 0; i < seeds.Count; i+=2)
        {
            numberOfSeeds += seeds[i+1];
        }
        System.Console.WriteLine(numberOfSeeds);

        Stopwatch sw = new Stopwatch();
        sw.Start();

        long currentPos = 0;
        long lowestOutput = -1;
        for (int i = 0; i < seeds.Count; i+=2)
        {
            long startNumber = seeds[i];
            long seedsInRange = seeds[i + 1];
            for (long y = 0; y < seedsInRange; y++)
            {
                currentPos ++;
                long seed = startNumber + y;

                var temp = seed;

                foreach (var mg in MapGroups)
                {
                    temp = mg.Process(temp);
                }

                if (temp < lowestOutput){
                    lowestOutput = temp;
                }
                else if (lowestOutput == -1){
                    lowestOutput = temp;
                }

                if (currentPos % 1000000 == 0){
                    sw.Stop();
                    System.Console.WriteLine($"{currentPos}/{numberOfSeeds}: {sw.Elapsed.TotalSeconds}");
                    System.Console.WriteLine($"{Math.Round((double)currentPos/(double)numberOfSeeds*100,2)}%");
                    System.Console.WriteLine($"Range {i} of {seeds.Count}");
                    System.Console.WriteLine($"Current lowest: {lowestOutput}");
                    sw.Reset();
                    sw.Start();
                }
            }
        }
        
        return lowestOutput;
    }
}
