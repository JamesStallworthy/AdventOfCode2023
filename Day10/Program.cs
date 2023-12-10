using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Day10;

public class Pipe{
    public Pipe(char pipeChar)
    {
        PipeChar = pipeChar;
        CreatePipeMapping();
    }

    public char PipeChar {get; private set;}
    public Dictionary<char, char> PipeMapping {get; private set; } = new Dictionary<char, char>();

    private void CreatePipeMapping(){
        PipeMapping = new Dictionary<char, char>();

        switch (PipeChar)
        {
            case '|':
                PipeMapping.Add('N', 'S');
                PipeMapping.Add('S', 'N');
                break;
            case '-':
                PipeMapping.Add('E', 'W');
                PipeMapping.Add('W', 'E');
                break;
            case 'L':
                PipeMapping.Add('N', 'E');
                PipeMapping.Add('E', 'N');
                break;
            case 'J':
                PipeMapping.Add('N', 'W');
                PipeMapping.Add('W', 'N');
                break;
            case '7':
                PipeMapping.Add('S', 'W');
                PipeMapping.Add('W', 'S');
                break;
            case 'F':
                PipeMapping.Add('S', 'E');
                PipeMapping.Add('E', 'S');
                break;
            case 'C':
            case 'S':
                PipeMapping.Add('N', 'S');
                PipeMapping.Add('S', 'N');
                PipeMapping.Add('E', 'W');
                PipeMapping.Add('W', 'E');
                break;
            case '.':
            case ' ':
                break;
            default:
                throw new Exception("Invalid pipe has been parsed");
        }
    }

    public bool CanConnectFrom(char direction){
        return PipeMapping.ContainsKey(direction);
    }

    public char OutputDirection(char inputDirection){
        return PipeMapping[inputDirection];
    }
}

public class StartNode: Node{
    public StartNode(char pipeChar, int x, int y): base(pipeChar, x, y){

    }

    public Dictionary<char,Node> FindConnectedPipes(Map map){
        Dictionary<char,Node> Connection = new Dictionary<char,Node>();
        //try each direction
        if (TryGetNodeInDirection('N', map.Grid, out Node? NorthernNode, false)){
            if (NorthernNode.CanConnectFrom('S'))
                Connection.Add('S',NorthernNode);
        }
        if (TryGetNodeInDirection('S', map.Grid, out Node? SouthernNode, false)){
            if (SouthernNode.CanConnectFrom('N'))
                Connection.Add('N',SouthernNode);
        }
        if (TryGetNodeInDirection('E', map.Grid, out Node? EasternNode, false)){
            if (EasternNode.CanConnectFrom('W'))
                Connection.Add('W',EasternNode);
        }
        if (TryGetNodeInDirection('W', map.Grid, out Node? WesternNode, false)){
            if (WesternNode.CanConnectFrom('E'))
                Connection.Add('E',WesternNode);
        }

        if (Connection.Count != 2){
            throw new Exception("Invalid start node");
        }

        return Connection;
    }
}

public class Node{
    public Node(char pipeChar, int x, int y)
    {
        Pipe = new Pipe(pipeChar);
        this.X = x;
        this.Y = y;
    }

    public int X {get; private set;}
    public int Y {get; private set;}

    public Pipe Pipe {get; set;}
    public int Distance {get; set;} = -1;

    public bool CanConnectFrom(char direction){
        return Pipe.CanConnectFrom(direction);
    }

    public bool TryGetNodeInDirection(char direction,List<List<Node>> Grid, out Node? NodeInDirection, bool skipConnectionNode){
        NodeInDirection = null;
        int offset = 1;
        if (skipConnectionNode){
            offset = 2;
        }

        switch (direction)
        {
            case 'N':
                if (Y >= offset){
                    NodeInDirection = Grid[Y - offset][X];
                }
                break;
            case 'S':
                if (Y < Grid.Count - offset){
                    NodeInDirection = Grid[Y + offset][X];
                }
                break;
            case 'W':
                if (X >= offset){
                    NodeInDirection = Grid[Y][X - offset];
                }
                break;
            case 'E':
                if (X < Grid[0].Count - offset){
                    NodeInDirection = Grid[Y][X + offset];
                }
                break;
        }
        if (NodeInDirection == null)
            return false;

        if (skipConnectionNode)
            return NodeInDirection.Pipe.PipeChar != '.';

        return NodeInDirection != null;
    }

    public virtual void Path(Map map, int distance, char inboundDirection){
        distance = distance + 1;

        if (this.Distance != -1 && this.Distance <= distance){
            return;
        }

        this.Distance = distance;

        Node nextNode;

        char outputDirection = Pipe.OutputDirection(inboundDirection);
        if (!TryGetNodeInDirection(outputDirection, map.Grid, out nextNode, false)){
            throw new Exception($"Unable to Path from X:{X}, Y:{Y}");
        }

        if (nextNode is StartNode){
            return;
        }

        nextNode.Path(map, distance, InvertDirection(outputDirection));
    }

    protected char InvertDirection(char dir){
        if (dir == 'N'){
            return 'S';
        }
        if (dir == 'S'){
            return 'N';
        }
        if (dir == 'E'){
            return 'W';
        }
        if (dir == 'W'){
            return 'E';
        }

        throw new Exception();
    }
}

public class Map{
    public List<List<Node>> Grid {get; private set;} = new List<List<Node>>();

    int startX = -1;
    int startY = -1;

    public Map(string[] rows)
    {
        int x = 0;
        int y = 0;
        foreach (var row in rows){
            x = 0;
            Grid.Add(new List<Node>());
            foreach (var c in row)
            {
                if (c == 'S'){
                    startX = x;
                    startY = y;
                    Grid.Last().Add(new StartNode(c, x, y));
                }
                else{
                    Grid.Last().Add(new Node(c, x, y));
                }
                x++;
            }

            y ++;
        }
    }

    public string StartPos(){
        return $"x:{startX}, y:{startY}";
    }

    public string MapOutput(){
        StringBuilder sb = new StringBuilder();
        foreach (var row in Grid)
        {
            sb.AppendLine(string.Join("", row.Select(x => x.Pipe.PipeChar)));
        }

        return sb.ToString();
    }

    public string DistanceOutput(){
        StringBuilder sb = new StringBuilder();
        foreach (var row in Grid)
        {
            List<string> rowOutput = new List<string>();
            foreach (var node in row)
            {
                if (node.Pipe.PipeChar == '.'){
                    rowOutput.Add(".");
                }
                else if (node.Distance == -1){
                    rowOutput.Add("X");
                }
                else{
                    rowOutput.Add(node.Distance.ToString());
                }
            }

            sb.AppendLine(string.Join("", rowOutput));
        }

        return sb.ToString();
    }

    public void CalculateMapDistances(){
        StartNode startNode = Grid[startY][startX] as StartNode;

        startNode.Distance = 0;

        Dictionary<char, Node> StartingNodeAndDirection = startNode.FindConnectedPipes(this);

        foreach (var nodeAndDir in StartingNodeAndDirection)
        {
            Node node = nodeAndDir.Value;
            char dir = nodeAndDir.Key;

            node.Path(this, 0, dir);
        }
    }

    public int GetFurthestDistance(){
        return Grid.Select(x => x.Select(y => y.Distance).Max()).Max();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllLines("./input.txt");
        Map map = new Map(file);

        map.CalculateMapDistances();

        System.Console.WriteLine($"Part1 Solution is {map.GetFurthestDistance()}");

        MapPart2 map2 = new MapPart2(file);
        map2.ConnectPipes();
        System.Console.WriteLine($"Part2 Solution is {map2.FindReachable()}");
    }
}
