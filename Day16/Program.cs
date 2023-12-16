using System.Numerics;
using System.Text;

namespace Day16;

public abstract class Point{
    public bool Energised {get; private set;}
    public void EnergisePoint(){
        Energised = true;
    }

    public void Reset(){
        Energised = false;
        VistedFrom = new HashSet<Grid.Direction>();
    }

    public HashSet<Grid.Direction> VistedFrom = new HashSet<Grid.Direction>();

    public abstract List<Grid.Direction> GetOutbound(Grid.Direction inboundDirection);
}

public class EmptySpace: Point{
    public override List<Grid.Direction> GetOutbound(Grid.Direction inboundDirection)
    {
        return new List<Grid.Direction>(){inboundDirection};
    }

    public override string ToString()
    {
        return ".";
    }
}

public class Splitter: Point{
    public override List<Grid.Direction> GetOutbound(Grid.Direction inboundDirection)
    {
        if (inboundDirection == Grid.Direction.Left || inboundDirection == Grid.Direction.Right){
            return new List<Grid.Direction>(){
                Grid.Direction.Up,
                Grid.Direction.Down
            };
        }

        return new List<Grid.Direction>(){inboundDirection};
    }

    public override string ToString()
    {
        return "|";
    }
}

public class HSplitter: Splitter{
    public override string ToString()
    {
        return "-";
    }
    public override List<Grid.Direction> GetOutbound(Grid.Direction inboundDirection)
    {
        if (inboundDirection == Grid.Direction.Up || inboundDirection == Grid.Direction.Down){
            return new List<Grid.Direction>(){
                Grid.Direction.Left,
                Grid.Direction.Right
            };
        }

        return new List<Grid.Direction>(){inboundDirection};
    }
}

public abstract class Mirror: Point{

}

public class BackSlashMirror: Mirror{
    private Dictionary<Grid.Direction, Grid.Direction> Map = new Dictionary<Grid.Direction, Grid.Direction>(){
        {Grid.Direction.Up, Grid.Direction.Left},
        {Grid.Direction.Down, Grid.Direction.Right},
        {Grid.Direction.Left, Grid.Direction.Up},
        {Grid.Direction.Right, Grid.Direction.Down},
    };

    public override List<Grid.Direction> GetOutbound(Grid.Direction inboundDirection)
    {
        return new List<Grid.Direction>(){
            Map[inboundDirection]
        };
    }

    public override string ToString()
    {
        return "\\";
    }
}

public class ForwardSlashMirror: Mirror{
    private Dictionary<Grid.Direction, Grid.Direction> Map = new Dictionary<Grid.Direction, Grid.Direction>(){
        {Grid.Direction.Up, Grid.Direction.Right},
        {Grid.Direction.Down, Grid.Direction.Left},
        {Grid.Direction.Left, Grid.Direction.Down},
        {Grid.Direction.Right, Grid.Direction.Up},
    };

    public override List<Grid.Direction> GetOutbound(Grid.Direction inboundDirection)
    {
        return new List<Grid.Direction>(){
            Map[inboundDirection]
        };
    }

    public override string ToString()
    {
        return "//";
    }
}

public class Grid{

    public enum Direction{
        Up,
        Down,
        Left,
        Right
    }

    public int StartX = 0;
    public int StartY = 0;

    public Grid(string[] rows)
    {
        foreach (var row in rows)
        {
            Points.Add(new List<Point>());
            foreach (var point in row)
            {
                switch (point)
                {
                    case '/':
                        Points.Last().Add(new ForwardSlashMirror());
                        break;
                    case '\\':
                        Points.Last().Add(new BackSlashMirror());
                        break;
                    case '.':
                        Points.Last().Add(new EmptySpace());
                        break;
                    case '|':
                        Points.Last().Add(new Splitter());
                        break;
                    case '-':
                        Points.Last().Add(new HSplitter());
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }

    List<List<Point>> Points = new List<List<Point>>();

    private void ResetEnergised(){
        foreach (var row in Points)
        {
            foreach (var point in row)
            {
                point.Reset();
            }
        }
    }

    public long BestEnergiseAnswer(){
        ResetEnergised();
        long bestSum = -1;
        //Top
        for (int i = 0; i < Points[0].Count; i++)
        {
            EnergiseImpl(0, i, Direction.Down);
            long result = GetEnergisedPoints();

            if (result > bestSum){
                bestSum = result;
            }
            ResetEnergised();
        }
        //Bottom
        for (int i = 0; i < Points[0].Count; i++)
        {
            EnergiseImpl(Points.Count - 1, i, Direction.Up);
            long result = GetEnergisedPoints();

            if (result > bestSum){
                bestSum = result;
            }
            ResetEnergised();
        }
        //Left
        for (int i = 0; i < Points.Count; i++)
        {
            EnergiseImpl(i, 0, Direction.Right);
            long result = GetEnergisedPoints();

            if (result > bestSum){
                bestSum = result;
            }
            ResetEnergised();
        }
        //Right
        for (int i = 0; i < Points.Count; i++)
        {
            EnergiseImpl(i, Points[0].Count - 1, Direction.Left);
            long result = GetEnergisedPoints();

            if (result > bestSum){
                bestSum = result;
            }
            ResetEnergised();
        }

        return bestSum;
    }

    public void Energise(){
        EnergiseImpl(StartX, StartY, Direction.Right);
    }

    private void EnergiseImpl(int r, int c, Direction inboundDir){
        if (r < 0 || r >= Points.Count){
            return;
        }

        if (c < 0 || c >= Points[0].Count){
            return;
        }

        Point currentPoint = Points[r][c];

        if (currentPoint.VistedFrom.Contains(inboundDir))
            return;
        else
            currentPoint.VistedFrom.Add(inboundDir);

        currentPoint.EnergisePoint();

        List<Direction> newDirections = currentPoint.GetOutbound(inboundDir);

        foreach (var dir in newDirections)
        {
            int newR;
            int newC;

            switch (dir)
            {
                case Direction.Up:
                    newR = r - 1;
                    newC = c;
                    break;
                case Direction.Down:
                    newR = r + 1;
                    newC = c;
                    break;
                case Direction.Left:
                    newR = r;
                    newC = c - 1;
                    break;
                case Direction.Right:
                    newR = r;
                    newC = c + 1;
                    break;
                default:
                    throw new NotImplementedException();
            }

            EnergiseImpl(newR, newC, dir);
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var row in Points)
        {
            foreach (var point in row)
            {
                sb.Append(point.ToString());
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public long GetEnergisedPoints(){
        return Points.SelectMany(x => x).Where(x => x.Energised).Count();
    }

    public string GetEnergisedMap()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var row in Points)
        {
            foreach (var point in row)
            {
                if (point.Energised){
                    sb.Append('#');
                }
                else{
                    sb.Append('.');
                }
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllLines("./input.txt");

        Grid grid = new Grid(file);

        grid.Energise();

        System.Console.WriteLine($"Sum Part1 {grid.GetEnergisedPoints()}");
        System.Console.WriteLine($"Sum Part2 {grid.BestEnergiseAnswer()}");
    }
}
