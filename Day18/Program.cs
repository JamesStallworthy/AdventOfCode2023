using System.Diagnostics.Contracts;
using System.Drawing;
using System.Text;

namespace Day18;

public class Point{
    public bool Outside {get; set;} = false;

    public bool Dug {get; private set;} = false;

    public void Dig(){
        Dug = true;
    }

}

public class Instruction{
    public enum Directions{
        Up,
        Down,
        Left,
        Right
    }

    public Directions Direction {get; private set;}
    public long Steps {get; private set;}

    public Instruction(string instruction, bool part2)
    {
        var splitInstructions = instruction.Split(" ");

        var hex = splitInstructions.Last()[1..^1];

        hex = hex.Replace("#", "");

        string step = hex[..^1];

        string dir = hex[^1..];

        switch (dir)
        {
            case "0":
                dir = "R";
                break;
            case "1":
                dir = "D";
                break;
            case "2":
                dir = "L";
                break;
            case "3":
                dir = "U";
                break;
        }

        Parse($"{dir} {long.Parse(step, System.Globalization.NumberStyles.HexNumber)}");

    }

    public Instruction(string instruction)
    {
        Parse(instruction);
    }

    private void Parse(string instruction)
    {
        var splitInstructions = instruction.Split(" ");

        switch (splitInstructions[0])
        {
            case "U":
                Direction = Directions.Up;
                break;
            case "D":
                Direction = Directions.Down;
                break;
            case "L":
                Direction = Directions.Left;
                break;
            case "R":
                Direction = Directions.Right;
                break;
            default:
                throw new NotImplementedException();
        }

        Steps = int.Parse(splitInstructions[1]);
    }
}

public class Grid{
    public List<List<Point>> Points = new List<List<Point>>();

    public Grid()
    {
        Points.Add(new List<Point>());
        Points.Last().Add(new Point());
    }

    public int CurrentR = 0;
    public int CurrentC = 0;

    public bool Start = true;

    public void PerformInstruction(Instruction instruction){
        long steps = instruction.Steps;
        if (Start){
            DigCurrent();
            Start= false;
        }

        switch (instruction.Direction)
        {
            case Instruction.Directions.Up:
                Up(steps);
                break;
            case Instruction.Directions.Down:
                Down(steps);
                break;
            case Instruction.Directions.Left:
                Left(steps);
                break;
            case Instruction.Directions.Right:
                Right(steps);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void Up(long count){
        for (int i = 0; i < count; i++)
        {
            CurrentR = CurrentR - 1;        
            CurrentC = CurrentC;

            DigCurrent();
        }
    }

    private void Down(long count){
        for (int i = 0; i < count; i++)
        {
            CurrentR = CurrentR + 1;        
            CurrentC = CurrentC;

            DigCurrent();
        }
    }
    
    private void Left(long count){
        for (int i = 0; i < count; i++)
        {
            CurrentR = CurrentR;        
            CurrentC = CurrentC - 1;
        
            DigCurrent();   
        }
    }

    private void Right(long count){
        for (int i = 0; i < count; i++)
        {
            CurrentR = CurrentR;        
            CurrentC = CurrentC + 1;

            DigCurrent();   
        }
    }

    private void DigCurrent(){
        if (CurrentR < 0){
            AddNewRowAtStart();
        }
        if (CurrentR >= Points.Count){
            AddNewRowAtEnd();
        }

        if (CurrentC < 0){
            AddNewColumnAtStart();
        }
        if (CurrentC >= Points[0].Count){
            AddNewColumnAtEnd();
        }

        Points[CurrentR][CurrentC].Dig();
    }

    private void AddNewRowAtStart(){
        int width = Points[0].Count();

        Points.Insert(0, new List<Point>());

        for (int i = 0; i < width; i++)
        {
            Points[0].Add(new Point());
        }
        CurrentR = CurrentR + 1;
    }

    private void AddNewRowAtEnd(){
        int width = Points[0].Count();

        Points.Add(new List<Point>());

        for (int i = 0; i < width; i++)
        {
            Points.Last().Add(new Point());
        }
    }

    private void AddNewColumnAtStart(){
        foreach (var row in Points)
        {
            row.Insert(0,new Point());
        }
        CurrentC = CurrentC + 1;
    }

    private void AddNewColumnAtEnd(){
        foreach (var row in Points)
        {
            row.Add(new Point());
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var row in Points)
        {
            foreach (var point in row)
            {
                if (point.Dug)
                    sb.Append("#");
                else
                    sb.Append('.');
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public void Fill(){
        CheckTopOutside();
        CheckBottomOutside();
        CheckRightOutside();
        CheckLeftOutside();

        foreach (var point in Points.SelectMany(x => x).Where(x => !x.Outside && !x.Dug))
        {
            point.Dig();
        }
    }

    private void CheckTopOutside(){
        for (int c = 0; c < Points[0].Count; c++)
        {
            FillImpl(0, c);
        }
    }

    private void CheckBottomOutside(){
        for (int c = 0; c < Points[0].Count; c++)
        {
            FillImpl(Points.Count - 1, c);
        }
    }

    private void CheckLeftOutside(){
        for (int r = 0; r < Points.Count; r++)
        {
            FillImpl(r, 0);
        }
    }

    
    private void CheckRightOutside(){
        for (int r = 0; r < Points.Count; r++)
        {
            FillImpl(r, Points[0].Count - 1);
        }
    }

    private void FillImpl(int R, int C){
        Point point = Points[R][C];
        if (point.Outside || point.Dug)
            return;

        point.Outside = true;

        int uR = R - 1;
        int dR = R + 1;
        int lC = C - 1;
        int rC = C + 1;

        if (uR >= 0){
            FillImpl(uR, C);
        }

        if (dR < Points.Count){
            FillImpl(dR, C);
        }

        if (lC >= 0){
            FillImpl(R, lC);
        }

        if (rC < Points[0].Count){
            FillImpl(R, rC);
        }
    }

    public long Sum(){
        return Points.SelectMany(x => x).Where(x => x.Dug).Count();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllLines("./input.txt");

        List<Instruction> instructions = new List<Instruction>();

        foreach (var row in file)
        {
            instructions.Add(new Instruction(row));
        }

        Grid grid = new Grid();

        foreach (var instr in instructions)
        {
            grid.PerformInstruction(instr);
        }

        grid.Fill();

        System.Console.WriteLine($"Part 1: {grid.Sum()}");

        //Part2
        instructions = new List<Instruction>();
        foreach (var row in file)
        {
            instructions.Add(new Instruction(row, true));
        }

        GridPart2 grid2 = new GridPart2();

        foreach (var instr in instructions)
        {
            grid2.AddInstruction(instr);
        }

        grid2.ShiftPositive();

        System.Console.WriteLine($"Part 2: {grid2.Area()}");
    }
}
