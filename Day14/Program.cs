using System.Text;

namespace Day14;

public class Point{

}

public class SquareRock: Point{

}

public class RoundRock: Point{

}

public class Dish{
    public List<List<Point>> Grid {get; private set;} = new List<List<Point>>();

    public Dish(Dish dish){
        this.Grid = dish.Grid;
    }

    public Dish(List<string> rows)
    {
        foreach (var row in rows)
        {
            Grid.Add(new List<Point>());

            foreach (var c in row)
            {
                if (c == '.'){
                    Grid.Last().Add(new Point());
                }
                else if (c == 'O'){
                    Grid.Last().Add(new RoundRock());
                }
                else if (c == '#'){
                    Grid.Last().Add(new SquareRock());
                }
            }
        }
    }

    public string OutputGrid(){
        StringBuilder sb = new StringBuilder();
        foreach (var row in Grid)
        {
            foreach (var c in row)
            {   
                if (c is RoundRock)
                    sb.Append('O');
                else if (c is SquareRock)
                    sb.Append('#');
                else
                    sb.Append('.');
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public void Cycle(){
        TiltNorth();
        TiltWest();
        TiltSouth();
        TiltEast();
    }

    public void TiltNorth()
    {
        for (int r = 0; r < Grid.Count; r++)
        {
            for (int c = 0; c < Grid[r].Count; c++)
            {
                RollNorth(r, c);
            }
        }
    }

    private void RollNorth(int r, int c){
        if (r == 0){
            return;
        }

        Point currentPoint = Grid[r][c];

        if (!(currentPoint is RoundRock)){
            return;
        }

        Point pointAbove = Grid[r - 1][c];

        if (pointAbove is RoundRock || pointAbove is SquareRock){
            return;
        }
        
        //Swap the points
        Grid[r - 1][c] = currentPoint;
        Grid[r][c] = pointAbove;

        RollNorth(r - 1, c);
    }

    public void TiltSouth()
    {
        for (int r = Grid.Count - 1; r >= 0; r--)
        {
            for (int c = 0; c < Grid[r].Count; c++)
            {
                RollSouth(r, c);
            }
        }
    }

    private void RollSouth(int r, int c){
        if (r == Grid.Count - 1){
            return;
        }

        Point currentPoint = Grid[r][c];

        if (!(currentPoint is RoundRock)){
            return;
        }

        Point pointBelow = Grid[r + 1][c];

        if (pointBelow is RoundRock || pointBelow is SquareRock){
            return;
        }
        
        //Swap the points
        Grid[r + 1][c] = currentPoint;
        Grid[r][c] = pointBelow;

        RollSouth(r + 1, c);
    }

    public void TiltWest()
    {
        for (int c = 0; c < Grid[0].Count; c++)
        {
            for (int r = 0; r < Grid.Count; r++)
            {
                RollWest(r, c);
            }
        }
    }

    private void RollWest(int r, int c){
        if (c == 0){
            return;
        }

        Point currentPoint = Grid[r][c];

        if (!(currentPoint is RoundRock)){
            return;
        }

        Point pointAbove = Grid[r][c - 1];

        if (pointAbove is RoundRock || pointAbove is SquareRock){
            return;
        }
        
        //Swap the points
        Grid[r][c - 1] = currentPoint;
        Grid[r][c] = pointAbove;

        RollWest(r, c - 1);
    }

    public void TiltEast()
    {
        for (int c = Grid[0].Count - 1; c >= 0; c--)
        {
            for (int r = 0; r < Grid.Count; r++)
            {
                RollEast(r, c);
            }
        }
    }

    private void RollEast(int r, int c){
        if (c == Grid[0].Count - 1){
            return;
        }

        Point currentPoint = Grid[r][c];

        if (!(currentPoint is RoundRock)){
            return;
        }

        Point pointBelow = Grid[r][c + 1];

        if (pointBelow is RoundRock || pointBelow is SquareRock){
            return;
        }
        
        //Swap the points
        Grid[r][c + 1] = currentPoint;
        Grid[r][c] = pointBelow;

        RollEast(r, c + 1);
    }

    public long Sum1(){
        long sum1 = 0;
        for (int r = 0; r < Grid.Count; r++)
        {
            for (int c = 0; c < Grid[r].Count; c++)
            {
                if (Grid[r][c] is RoundRock){
                    sum1 += Grid.Count - r;
                }
            }
        }

        return sum1;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllLines("./input.txt");

        Dish dish = new Dish(file.ToList());

        dish.TiltNorth();

        System.Console.WriteLine($"Sum Part1: {dish.Sum1()}");

        HashSet<string> Grids = new HashSet<string>();
        Dish dish2 = new Dish(file.ToList());
        
        long totalCycles = 1000000000;
        for (long i = 1; i <= totalCycles; i++)
		{
			dish2.Cycle();
            string currentGrid = string.Join("", dish2.Grid.SelectMany(x => x));
            //Once we hit the same starting grid then we are just looping now
            if (Grids.Contains(currentGrid)){
                HandlePattern(dish2, i, totalCycles);
                break;
            }

            Grids.Add(currentGrid);
		}
    }

    private static void HandlePattern(Dish dish, long currentCyle, long totalCycles){
        //How long is the cycle of patterns
        long patternLength = FindPatternLength(new Dish(dish));

        long remainingCycles = totalCycles - currentCyle;

        long remainder = (remainingCycles % patternLength) - 1;

        for (int i = 0; i <= remainder; i++)
        {
            dish.Cycle();
        }

        System.Console.WriteLine($"Sum Part2: {dish.Sum1()}");
    }

    private static long FindPatternLength(Dish dish){
        string startGrid = string.Join("", dish.Grid.SelectMany(x => x));

        string currentGrid = "";
        long patternLength = 0;
        while (startGrid != currentGrid){
            dish.Cycle();

            currentGrid = string.Join("", dish.Grid.SelectMany(x => x));
            patternLength ++;
        }

        return patternLength;
    }
}
