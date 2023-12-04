using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Xml.Schema;

namespace Day4;

class ScratchCard{
    public int CardNum {get => Index + 1; }
    public int Index {get; set;}
    public HashSet<int> WinningNumbers {get; set;}
    public List<int> NumbersOnCard {get; set;}

    public IEnumerable<int> WonNumbers {get => NumbersOnCard.Where(x => WinningNumbers.Contains(x));}

    public int Part1Sum(){
        int sum = 0;

        foreach (var num in WonNumbers)
        {
            if (sum == 0){
                sum = 1;
            } 
            else{
                sum = sum *2;
            }
        }

        return sum;
    }

    public void Part2Sum(List<ScratchCard> cards,ref int total){
        total += 1;

        int cardIndex = Index;
        foreach (var num in WonNumbers)
        {
            cardIndex ++;
            if (cardIndex >= cards.Count){
                return;
            }
            cards[cardIndex].Part2Sum(cards,ref total);
        }

        return;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadLines("./input.txt");

        List<ScratchCard> scratchCards = new List<ScratchCard>();

        int index = 0;
        foreach (var line in file)
        {
            //Trim the start off
            string[] game = line.Split(":")[1].Split("|");

            HashSet<int> winningNumbers = game[0].Trim().Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToHashSet();

            List<int> scratchCardNumbers = game[1].Trim().Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();

            scratchCards.Add(new ScratchCard(){
                Index = index,
                WinningNumbers = winningNumbers,
                NumbersOnCard = scratchCardNumbers
            });

            index++;
        }

        int part1Sum = 0;
        int part2Sum = 0;
        foreach (var sc in scratchCards)
        {
            part1Sum += sc.Part1Sum();
            sc.Part2Sum(scratchCards, ref part2Sum);
        }

        System.Console.WriteLine($"Part one answer: {part1Sum}");
        System.Console.WriteLine($"Part two answer: {part2Sum}");
    }
}
