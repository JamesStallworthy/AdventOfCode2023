using System.Diagnostics;

namespace Day6;

//Another day, another brute force! Also implemented with a hacky binary search to find the lower and high bounds, this run much faster
class Race
{
    private long Time {get; set;}
    private long RecordDistance {get; set;}
    public Race(long time, long distance)
    {
        Time = time;
        RecordDistance = distance;
    }

    public long CalculateDistance(long timePressingButton){
        if (timePressingButton >= Time){
            return 0;
        }

        long remainingTime = Time - timePressingButton;

        long distance = timePressingButton * remainingTime;

        return distance;
    }

    private bool IsWinningDistance(long timePressingButton){
        return CalculateDistance(timePressingButton) > RecordDistance;
    }

    public long GetNumberOfWaysToWin(){
        long waysToWin = 0;

        for (int i = 0; i < Time; i++)
        {
            if (IsWinningDistance(i)){
                waysToWin ++;
            }
        }

        return waysToWin;
    }

    //Basically a binary search
    public long GetNumberOfWaysToWinOptermised(){
        var lowestWinner = GetLowestWinner(0, Time/2);
        var greatestWinner = GetGreatestWinner(Time/2, Time);

        return greatestWinner - lowestWinner;
    }

    private long GetLowestWinner(long start, long end){
        if (Math.Abs(end - start) == 1){
            return start;
        }

        long midPoint = start + ((end - start)/2);

        if (IsWinningDistance(midPoint)){
            return GetLowestWinner(start, midPoint);
        }
        else{
            return GetLowestWinner(midPoint, end);
        }
    }

    private long GetGreatestWinner(long start, long end){
        if (Math.Abs(end - start) == 1){
            return start;
        }

        long midPoint = start + ((end - start)/2);

        if (IsWinningDistance(midPoint)){
            return GetGreatestWinner(midPoint, end);
        }
        else{
            return GetGreatestWinner(start, midPoint);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Race> Races = new List<Race>();

        var file = File.ReadLines("./input.txt").ToList();

        var times = file[0].Split(":")[1].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
        var distances = file[1].Split(":")[1].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();

        for (int i = 0; i < times.Count; i++)
        {
            Races.Add(new Race(times[i], distances[i]));
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        long part1Sum = 0;
        foreach (var race in Races)
        {
            long result = race.GetNumberOfWaysToWin();

            if (part1Sum == 0){
                part1Sum = result;
            }
            else{
                part1Sum *= result;
            }
        }
        System.Console.WriteLine($"Part1 Answer: {part1Sum}, Took {sw.Elapsed.TotalSeconds} seconds");

        sw = new Stopwatch();
        sw.Start();
        Race part2Race = new Race(long.Parse(string.Join("", times)), long.Parse(string.Join("", distances)));

        long part2Answer = part2Race.GetNumberOfWaysToWin();
        sw.Stop();
        System.Console.WriteLine($"Part2 Answer: {part2Answer}. Took {sw.Elapsed.TotalSeconds} seconds");

        sw = new Stopwatch();
        sw.Start();
        
        long part2AnswerOptimised = part2Race.GetNumberOfWaysToWinOptermised();
        sw.Stop();
        System.Console.WriteLine($"Part2 Answer Optermised: {part2AnswerOptimised}. Took {sw.Elapsed.TotalSeconds} seconds");
    }
}

