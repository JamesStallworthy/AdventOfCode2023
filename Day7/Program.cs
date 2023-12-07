namespace Day7;

public class Hand{
    public static Dictionary<char, int> CardMap = new Dictionary<char, int>(){
        {'2', 0},
        {'3', 1},
        {'4', 2},
        {'5', 3},
        {'6', 4},
        {'7', 5},
        {'8', 6},
        {'9', 7},
        {'T', 8},
        {'J', 9},
        {'Q', 10},
        {'K', 11},
        {'A', 12},
    };

    static Dictionary<int, char> ReversedCardMap {get => CardMap.ToDictionary(x => x.Value, x => x.Key); }

    public List<int> Cards = new List<int>();

    public int Bid {get; private set;}


    //0: Five of a Kind
    //1: Four of a Kind
    //2: Full house
    //3: Three of a kind
    //4: Two pair 
    //5: One pair
    //6: High card
    public int Type {get; private set;}

    public Hand(string hand, bool part2 = false)
    {
        var splitHand = hand.Split(" ");
        Bid = int.Parse(splitHand[1]);
        foreach (var c in splitHand[0])
        {
            Cards.Add(CardMap[c]);
        }

        if(part2)
            CalculateType();
        else
            CalculateTypePart2();
    }

    class Pairs{
        public int Element;
        public int Counter;
    }
    private void CalculateTypePart2(){

        //Filter out jokers
        var query = Cards.Where(x => x != CardMap['J']).GroupBy(x => x)
              .Select(y => new Pairs(){ Element = y.Key, Counter = y.Count() })
              .OrderByDescending(x => x.Counter)
              .ToList();


        if (query.Count == 0){
            Type = 0;
            return;
        }

        int numberOfJacks = Cards.Where(x => x == CardMap['J']).Count();

        query.First().Counter += numberOfJacks;
        //5 of a kind
        if (query.First().Counter == 5){
            Type = 0;
        }
        //4 of a kind
        else if (query.First().Counter == 4){
            Type = 1;
        }
        else if (query.First().Counter == 3){
            //Full house
            if (query.Count == 2){
                Type = 2;
            }
            //3 of a kind
            else{
                Type = 3;
            }
        }
        //2 pair
        else if (query.First().Counter == 2 && query.Count == 3){
            Type = 4;
        }
        //1 pair
        else if (query.First().Counter == 2 && query.Count == 4){
            Type = 5;
        }
        else{
            Type = 6;
        }
    }

    private void CalculateType(){
        var query = Cards.GroupBy(x => x)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .OrderByDescending(x => x.Counter)
              .ToList();

        //5 of a kind
        if (query.First().Counter == 5){
            Type = 0;
        }
        //4 of a kind
        else if (query.First().Counter == 4){
            Type = 1;
        }
        else if (query.First().Counter == 3){
            //Full house
            if (query.Count == 2){
                Type = 2;
            }
            //3 of a kind
            else{
                Type = 3;
            }
        }
        //2 pair
        else if (query.First().Counter == 2 && query.Count == 3){
            Type = 4;
        }
        //1 pair
        else if (query.First().Counter == 2 && query.Count == 4){
            Type = 5;
        }
        else{
            Type = 6;
        }
    }

    public override string ToString(){
        return string.Join("",Cards.Select(x => ReversedCardMap[x]));
    }
}

public class CustomHandComparer : IComparer<Hand>
{
    public int Compare(Hand? x, Hand? y)
    {
        if (x.Type == y.Type){
            return StrongestCard(0, x, y);
        }
        
        //Smaller the type, the more power it has
        if (x.Type < y.Type){
            return 1;
        }
        else{
            return -1;
        }
    }

    private int StrongestCard(int i, Hand x, Hand y){
        if (x.Cards[i] == y.Cards[i]){
            return StrongestCard(i + 1, x, y);
        }

        if (x.Cards[i] > y.Cards[i]){
            return 1;
        }
        else{
            return -1;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Hand.CardMap['J'] = -1;
        List<Hand> hands = new List<Hand>();
        var file = File.ReadAllLines("./input.txt");

        foreach (var line in file)
        {
            hands.Add(new Hand(line, true));
        }

        hands.Sort(new CustomHandComparer());

        long sum = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            sum += hands[i].Bid * (i+1);
        }

        System.Console.WriteLine($"Answer: {sum}");
    }
}
