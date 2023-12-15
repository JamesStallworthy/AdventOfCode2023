using System.Collections.Specialized;
using System.Drawing;

namespace Day15;

public class Box{
    //Could do something with the hashes to make lookup quicker, but Linq was fast enough for this problem
    public List<Lense> Lenses = new List<Lense>();

    public void Add(Lense lense){
        if (Lenses.Where(x => x.LenseIdentifier == lense.LenseIdentifier).Any()){
            int index = Lenses.Select((len, index) => (len, index)).First(x => x.len.LenseIdentifier == lense.LenseIdentifier).index;
            Lenses[index] = lense;
        }
        else{
            Lenses.Add(lense);
        }
    }

    public void Remove(Lense lense){
        Lenses.RemoveAll(x => x.LenseIdentifier == lense.LenseIdentifier);
    }
}

public class Lense{
    public char Action {get; private set;}
    public int Focal {get; private set;}

    public string LenseIdentifier {get;private set;}
    public Lense(string Label)
    {
        if (Label.Last() == '-'){
            Action = '-';
            Label = Label.Substring(0, Label.Count() - 1);
        }
        else{
            Action = '=';
            Focal = Label.Last() - '0';
            Label = Label.Substring(0, Label.Count() - 2);
        }

        LenseIdentifier = Label;
    }
}

public class Word{
    List<char> characters = new List<char>();
    public Word(string word){
        characters = word.Select(x => x).ToList();
    }

    public int Hash(){
        int currentValue = 0;

        foreach (var c in characters)
        {
            currentValue = HashImpl(currentValue, c);
        }

        return currentValue;
    }

    private int HashImpl(int currentValue, char character){
        currentValue += (int)character;

        currentValue *= 17;

        currentValue = currentValue % 256;

        return currentValue;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllText("./input.txt");

        var words = file.Split(',');

        long sum1 = 0;
        foreach (var w in words)
        {
            Word word = new Word(w);

            sum1 += word.Hash();
        }

        System.Console.WriteLine($"Part1 Sum: {sum1}");

		List<Box> Boxes = new List<Box>();
        for (int i = 0; i < 256; i++)
        {
            Boxes.Add(new Box());
        }

        foreach (var item in words)
		{
			Lense lense = new Lense(item);

			if (lense.Action == '='){
				Boxes[new Word(lense.LenseIdentifier).Hash()].Add(lense);
			}
			else{
				Boxes[new Word(lense.LenseIdentifier).Hash()].Remove(lense);
			}
		}

        System.Console.WriteLine($"Part2 Sum: {Part2Sum(Boxes)}");
    }

    public static long Part2Sum(List<Box> boxes){
        long sum = 0;
        for (int b = 0; b < boxes.Count; b++)
        {
            for (int l = 0; l < boxes[b].Lenses.Count; l++)
            {
                int bNumber = b + 1;
                int lensePos = l + 1;

                sum += (bNumber * lensePos * boxes[b].Lenses[l].Focal);
            }
        }

        return sum;
    }
}
