using System.Drawing;
using System.Reflection.Metadata;
using System.Text;

namespace Day17;

//This is my dykstras attempt, however it needs more thinking as its not taking into account the three moves in one direction limitation.
//Should come back to this!

public class Point{
    public int R {get; private set;}
    public int C {get; private set;}

    public Point(int weight, int r, int c)
    {
        Weight = weight;
        R = r;
        C = c;
    }
    public long Weight {get; private set;}

    public long Distance {get; private set;} = -1;
    public bool Visited {get; set;} = false;

    private List<string> Directions = new List<string>();

    public void SetStart(){
        Directions.Add("");
        Distance = 0;
    }

    private void SetNewDirection(Point ParentPoint, char dir){
        Directions = new List<string>();
        string invalidRouteEnding = new string(dir, 3);
        foreach (var item in ParentPoint.Directions)
        {
            if (!item.EndsWith(invalidRouteEnding))
                Directions.Add(item + dir);
        }
    }

    private void AppendNewDirection(Point ParentPoint, char dir){
        string invalidRouteEnding = new string(dir, 3);
        foreach (var item in ParentPoint.Directions)
        {
            if (!item.EndsWith(invalidRouteEnding))
                Directions.Add(item + dir);
        }
    }

    private bool CheckRouteValid(Point parentPoint, char dir){
        string invalidRouteEnding = new string(dir, 3);
        foreach (var item in parentPoint.Directions)
        {
            if (!item.EndsWith(invalidRouteEnding)){
                return true;
            }
        }

        return false;
    }

    public void SetDistance(Point parentPoint, char dir){
        // if (C == 0 && R ==7){

        // }

        // if (!CheckRouteValid(parentPoint, dir)){
        //     return;
        // }

        long newDistance = parentPoint.Distance + this.Weight;
        // if (newDistance == this.Distance){
        //     AppendNewDirection(parentPoint, dir);
        // }
        // if (newDistance < this.Distance || this.Distance == -1)
        // {
        //     this.Distance = newDistance;
        //     SetNewDirection(parentPoint, dir);
        // }

        if (newDistance < this.Distance || this.Distance == -1){
            this.Distance = newDistance;
        }
    }
}

public class Grid{
    List<List<Point>> Points = new List<List<Point>>();

    public Grid(string[] rows)
    {
        int r = 0;
        int col = 0;
        foreach (var row in rows)
        {
            Points.Add(new List<Point>());

            foreach (var c in row)
            {
                Points.Last().Add(new Point(c - '0', r, col));
                col++;
            }
            col = 0;
            r++;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var row in Points)
        {
            foreach (var point in row)
            {
                sb.Append(point.Weight);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public string ToStringDistances()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var row in Points)
        {
            foreach (var point in row)
            {
                //sb.Append($"{point.Weight}:");
                //sb.AppendFormat("{0:00}|", point.Distance);
                sb.AppendFormat("{0:00}|", point.Distance);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    //PriorityQueue<Point, int> tentativePoints = new PriorityQueue<Point, int>();


    private int StartR = 0;
    private int StartC = 0;

    private int EndR;
    private int EndC;
    public void CalculateDistances(){
        EndR = Points.Count() - 1;
        EndC = Points[0].Count() - 1;

        Points[StartR][StartC].SetStart();
        CalculateDistancesImpl(Points[StartR][StartC]);
    }

    private void CalculateDistancesImpl(Point point){
        point.Visited = true;

        //if (point.R == EndR && point.C == EndC){
        //    return;
        //}

        CalcUpNeighbourTentativeDistance(point);
        CalcDownNeighbourTentativeDistance(point);
        CalcLeftNeighbourTentativeDistance(point);
        CalcRightNeighbourTentativeDistance(point);

        Point? nextPoint = FindLowestTentative();
        if (nextPoint == null)
            return;
        CalculateDistancesImpl(nextPoint);
    }

    private Point? FindLowestTentative(){
        return Points.SelectMany(x => x).Where(x => x.Visited == false && x.Distance != -1).OrderBy(x => x.Distance).FirstOrDefault();
    }

    private void CalcUpNeighbourTentativeDistance(Point point)
    {
        int r = point.R - 1;
        int c = point.C;

        if (r < 0)
        {
            return;
        }

        Point newPoint = Points[r][c];

        CalculateTentativeDistance(point, newPoint, 'u');
    }

    private static void CalculateTentativeDistance(Point point, Point newPoint, char dir)
    {
        if (newPoint.Visited)
        {
            return;
        }

        newPoint.SetDistance(point, dir);
    }

    private void CalcDownNeighbourTentativeDistance(Point point){
        int r = point.R + 1;
        int c = point.C;

        if (r >= Points.Count){
            return;
        }

        Point newPoint = Points[r][c];

        CalculateTentativeDistance(point, newPoint,'d');
    }

    private void CalcLeftNeighbourTentativeDistance(Point point){
        int r = point.R;
        int c = point.C - 1;

        if (c < 0){
            return;
        }

        Point newPoint = Points[r][c];

        CalculateTentativeDistance(point, newPoint, 'l');
    }
    private void CalcRightNeighbourTentativeDistance(Point point){
        int r = point.R;
        int c = point.C + 1;

        if (c >= Points[0].Count){
            return;
        }

        Point newPoint = Points[r][c];

        CalculateTentativeDistance(point, newPoint, 'r');
    }
}

class Program
{


    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
