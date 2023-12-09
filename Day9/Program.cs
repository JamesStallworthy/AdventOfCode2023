using System.Net.Sockets;
using System.Text;

namespace Day9;

public class Sequence{
    List<List<int>> Numbers = new List<List<int>>(); 

    public Sequence(int[] sequence){
        Numbers.Add(sequence.ToList());
        Init();
    }

    private void Init(){
        CalculateDistances(0);
    }

    private void CalculateDistances(int zIndex){
        List<int> SequenceToCalculateDistance = Numbers[zIndex];

        if (SequenceToCalculateDistance.GroupBy(x => x).Count() == 1 && SequenceToCalculateDistance.First() == 0){
            return;
        }

        List<int> Distances = new List<int>();

        for (int i = 1; i < SequenceToCalculateDistance.Count; i++)
        {
            Distances.Add(CalculateDistance(SequenceToCalculateDistance[i - 1], SequenceToCalculateDistance[i]));
        }

        Numbers.Add(Distances);
        CalculateDistances(zIndex + 1);
    }

    private int CalculateDistance(int a, int b){
        return b - a;
    }

    public void IncrementSequence(){
        Numbers.Last().Add(0);
        IncrementSequenceImpl(Numbers.Count - 2);
    }

    private void IncrementSequenceImpl(int zIndex){
        if (zIndex == -1){
            return;
        }

        int previousValue = Numbers[zIndex].Last();
        int distance = Numbers[zIndex + 1].Last();

        int newValue = previousValue + distance;

        Numbers[zIndex].Add(newValue);

        IncrementSequenceImpl(zIndex - 1);
    }

    public List<int> GetSequence(){
        return Numbers[0];
    }

    public string DebugOutput(){
        StringBuilder sb = new StringBuilder();
        foreach (var row in Numbers)
        {
            sb.AppendLine(string.Join(" ", row));
        }

        return sb.ToString();
    }
}
class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllLines("./input.txt");

        long sumPart1 = 0;
        long sumPart2 = 0;
        foreach (var line in file)
        {
            Sequence sequence = new Sequence(line.Split(" ").Select(x => int.Parse(x)).ToArray());
            sequence.IncrementSequence();

            sumPart1 += sequence.GetSequence().Last();

            Sequence sequence2 = new Sequence(line.Split(" ").Select(x => int.Parse(x)).Reverse().ToArray());
            sequence2.IncrementSequence();

            sumPart1 += sequence.GetSequence().Last();
            sumPart2 += sequence2.GetSequence().Last();
        }

        System.Console.WriteLine($"Sum part1 {sumPart1}");
        System.Console.WriteLine($"Sum part2 {sumPart2}");
    }
}
