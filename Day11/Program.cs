using System.Runtime.InteropServices;
using System.Text;

namespace Day11;

public class Universe{
    List<List<Point>> Grid = new List<List<Point>>();
    Dictionary<int, Galaxy> Galaxies = new Dictionary<int, Galaxy>();

    public void Init(string[] input){
        int y = 0;
        int x = 0;
        int galaxyId = 1;
        foreach (var row in input)
        {
            x = 0;
            Grid.Add(new List<Point>());
            foreach (var c in row)
            {
                if (c is '.'){
                    Grid.Last().Add(new EmptySpace(x,y));
                }
                else if (c is '#'){
                    Galaxy galaxy = new Galaxy(galaxyId, x, y);
                    Galaxies.Add(galaxyId, galaxy);

                    Grid.Last().Add(galaxy);
                    galaxyId ++;
                }

                x++;
            }

            y++;
        }
    }

    public void Expand(){
        ExpandRows();
        ExpandColumns();
        ComputePositions();
    }

    private void ComputePositions(){
        for (int y = 0; y < Grid.Count; y++)
        {
            for (int x = 0; x < Grid[y].Count; x++)
            {
                Grid[y][x].SetLoc(x, y);
            }
        }
    }

    private void ExpandRows(){
        for (int i = 0; i < Grid.Count; i++)
        {
            if (Grid[i].Where(x => !(x is EmptySpace)).Count() == 0){
                //All the values must be empty space
                var newRow = CreateEmptyRow(Grid[i].Count);

                Grid.Insert(i, newRow);
                i++;
            }
        }
    }

    private void ExpandColumns(){
        for (int i = 0; i < Grid[0].Count; i++)
        {
            var emptyColumn = Grid.Select(x => x[i]).Where(x => !(x is EmptySpace)).Count() == 0;
            if (emptyColumn){
                for (int y = 0; y < Grid.Count; y++)
                {
                    Grid[y].Insert(i,new EmptySpace(0,0));
                }
                i++;
            }
        }
    }

    private List<Point> CreateEmptyRow(int width){
        List<Point> temp = new List<Point>();

        for (int i = 0; i < width; i++)
        {
            temp.Add(new EmptySpace(0,0));
        }

        return temp;
    }

    public string Display(){
        StringBuilder sb = new StringBuilder();

        foreach (var row in Grid)
        {
            foreach (var point in row)
            {
                if (point is Galaxy u){
                    sb.Append(u.ID);
                }
                if (point is EmptySpace s){
                    sb.Append('.');
                }
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public int SumOfAllDistances(){
        List<int> GalaxyIds = Galaxies.Select(x => x.Key).ToList();
        var combinations = GalaxyIds.SelectMany((x, i) => GalaxyIds.Skip(i + 1), (x, y) => Tuple.Create(x, y));

        int sum = 0;
        foreach (var item in combinations)
        {
            sum += FindDistanceBetweenGalaxies(item.Item1, item.Item2);
        }

        return sum;
    }

    public int FindDistanceBetweenGalaxies(int src, int dest){
        Galaxy srcGalaxy = Galaxies[src];
        Galaxy destGalaxy = Galaxies[dest];

        return TraverseBetween(srcGalaxy, destGalaxy, 0);
    }

    private int TraverseBetween(Point currentPos, Point EndGoal, int DistanceCovered){
        if (currentPos == EndGoal){
            return DistanceCovered;
        }

        Point? up = GetAbove(currentPos);
        Point? down = GetBelow(currentPos);
        Point? left = GetLeft(currentPos);
        Point? right = GetRight(currentPos);

        List<KeyValuePair<double, Point>> Distances = new List<KeyValuePair<double, Point>>();

        if (up != null){
            Distances.Add(new KeyValuePair<double, Point>(up.DistanceToGoal(EndGoal), up));
        }

        if (down != null){
            Distances.Add(new KeyValuePair<double, Point>(down.DistanceToGoal(EndGoal), down));
        }

        if (left != null){
            Distances.Add(new KeyValuePair<double, Point>(left.DistanceToGoal(EndGoal), left));
        }

        if (right != null){
            Distances.Add(new KeyValuePair<double, Point>(right.DistanceToGoal(EndGoal), right));
        }

        var closest = Distances.OrderBy(x => x.Key).First();
        
        return TraverseBetween(closest.Value, EndGoal, DistanceCovered + 1);
    }

    private Point? GetAbove(Point currentPos){
        int y = currentPos.Y - 1;
        int x = currentPos.X;

        if (y < 0){
            return null;
        }

        return Grid[y][x];
    }

    private Point? GetBelow(Point currentPos){
        int y = currentPos.Y + 1;
        int x = currentPos.X;

        if (y >= Grid.Count){
            return null;
        }

        return Grid[y][x];
    }

    private Point? GetLeft(Point currentPos){
        int y = currentPos.Y;
        int x = currentPos.X - 1;

        if (x < 0){
            return null;
        }

        return Grid[y][x];
    }
    private Point? GetRight(Point currentPos){
        int y = currentPos.Y;
        int x = currentPos.X + 1;

        if (x >= Grid[y].Count){
            return null;
        }

        return Grid[y][x];
    }
}

public class Galaxy: Point{
    public int ID {get; private set;}
    public Galaxy(int id, int x, int y): base(x, y)
    {
        ID = id;
    }
}

public class EmptySpace: Point{
    public EmptySpace(int x, int y): base(x, y)
    {
        
    }
}

public class Point{
    public int X {get; private set;}
    public int Y {get; private set;}
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;   
    }

    public void SetLoc(int x, int y){
        this.X = x;
        this.Y = y;  
    }

    internal double DistanceToGoal(Point goal)
    {
        int a = goal.X - X;
        int b = goal.Y - Y;

        return Math.Sqrt(Math.Pow(a,2) + Math.Pow(b, 2));
    }
}

class Program
{
    static void Main(string[] args)
    {
        Universe universe = new Universe();

        var file = File.ReadAllLines("./input.txt");

        universe.Init(file);

        universe.Expand();

        System.Console.WriteLine(universe.SumOfAllDistances());
    }
}
