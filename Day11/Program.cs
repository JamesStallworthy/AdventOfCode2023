using System.Runtime.InteropServices;
using System.Text;

namespace Day11;

public class Universe{
    public List<List<Point>> Grid = new List<List<Point>>();
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

    public void Expand(int by){
        ExpandRows(by);
        ExpandColumns(by);
    }

    private void ExpandRows(int by){
        for (int y = 0; y < Grid.Count; y++)
        {
            if (Grid[y].Where(x => !(x is EmptySpace)).Count() == 0){
                for (int x = 0; x < Grid[y].Count; x++)
                {
                    Grid[y][x].Height = Grid[y][x].Height  * by;
                }
            }
        }
    }

    private void ExpandColumns(int by){
        for (int x = 0; x < Grid[0].Count; x++)
        {
            var emptyColumn = Grid.Select(p => p[x]).Where(p => !(p is EmptySpace)).Count() == 0;
            if (emptyColumn){
                for (int y = 0; y < Grid.Count; y++)
                {
                    Grid[y][x].Width = Grid[y][x].Width * by;
                }
            }
        }
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

    public long SumOfAllDistances(){
        List<int> GalaxyIds = Galaxies.Select(x => x.Key).ToList();
        var combinations = GalaxyIds.SelectMany((x, i) => GalaxyIds.Skip(i + 1), (x, y) => Tuple.Create(x, y));

        long sum = 0;
        foreach (var item in combinations)
        {
            sum += FindDistanceBetweenGalaxies(item.Item1, item.Item2);
        }

        return sum;
    }

    public long FindDistanceBetweenGalaxies(int src, int dest){
        Galaxy srcGalaxy = Galaxies[src];
        Galaxy destGalaxy = Galaxies[dest];

        return TraverseBetween(srcGalaxy, destGalaxy, 0);
    }

    private long TraverseBetween(Point currentPos, Point EndGoal, long DistanceCovered){
        if (currentPos == EndGoal){
            return DistanceCovered;
        }

        Point? up = GetAbove(currentPos);
        Point? down = GetBelow(currentPos);
        Point? left = GetLeft(currentPos);
        Point? right = GetRight(currentPos);

        List<Tuple<double, char, Point>> Distances = new List<Tuple<double,char, Point>>();

        if (up != null && currentPos.Y != EndGoal.Y){
            Distances.Add(new Tuple<double,char, Point>(up.DistanceToGoal(EndGoal,this),'v', up));
        }

        if (down != null && currentPos.Y != EndGoal.Y){
            Distances.Add(new Tuple<double,char, Point>(down.DistanceToGoal(EndGoal, this), 'v',down));
        }

        if (left != null && currentPos.X != EndGoal.X){
            Distances.Add(new Tuple<double,char, Point>(left.DistanceToGoal(EndGoal, this),'h',left));
        }

        if (right != null && currentPos.X != EndGoal.X){
            Distances.Add(new Tuple<double,char, Point>(right.DistanceToGoal(EndGoal, this), 'h', right));
        }

        var orderedDistances = Distances.OrderBy(x => x.Item1).ToList();
        var closest = orderedDistances[0];
        long newDistance = DistanceCovered;

        if (closest.Item2 == 'h'){
            newDistance += closest.Item3.Width;
        }
        else{
            newDistance += closest.Item3.Height;
        }
        
        return TraverseBetween(closest.Item3, EndGoal, newDistance);
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
    public int Width { get; set; } = 1;
    public int Height { get; set; } = 1;
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

    private int FindXDistance(Point goal, Universe universe){
        int distance = 0;
        int direction = 0; //-1, 1

        if (goal.X - X > 0){
            direction = 1;
        }
        else{
            direction = -1;
        }

        int currentX = X;
        while (currentX != goal.X){
            currentX = currentX + direction;

            distance += universe.Grid[Y][currentX].Width;
        }

        return distance;
    }

    private int FindYDistance(Point goal, Universe universe){
        int distance = 0;
        int direction = 0; //-1, 1

        if (goal.Y - Y > 0){
            direction = 1;
        }
        else{
            direction = -1;
        }

        int currentY = Y;
        while (currentY != goal.Y){
            currentY = currentY + direction;

            distance += universe.Grid[currentY][X].Height;
        }

        return distance;
    }

    internal double DistanceToGoal(Point goal, Universe universe)
    {
        int a = FindXDistance(goal, universe);
        int b = FindYDistance(goal, universe);

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

        universe.Expand(2);

        System.Console.WriteLine(universe.SumOfAllDistances());
        
        Universe universe2 = new Universe();

        universe2.Init(file);

        universe2.Expand(1000000);

        System.Console.WriteLine(universe2.SumOfAllDistances());
    }
}
