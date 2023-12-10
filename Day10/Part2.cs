using System.Data;
using System.IO.Pipes;
using System.Text;
using Day10;

//This is part2. Super duper hacky. Not proud...

public class ConnectedNodes: Node{
	public bool Connected {get; private set; } = false;
	public bool OutsideReachable {get; private set; } = false;
	public bool IsPipe {get => !(Pipe.PipeChar.Equals('.') || Pipe.PipeChar.Equals('C')); }

	public ConnectedNodes(char c, int x, int y): base(c, x, y){

    }

	public void Spread(MapPart2 map){
		if (this.IsPipe)
			return;
		if (this.OutsideReachable){
			return;
		}
		if (this.Connected){
			return;
		}

		OutsideReachable = true;

		//Spread is all directions
		TryGetNodeInDirection('N', map.Grid, out Node? NorthNode, false);
		(NorthNode as ConnectedNodes)?.Spread(map);
		TryGetNodeInDirection('S', map.Grid, out Node? SouthNode, false);
		(SouthNode as ConnectedNodes)?.Spread(map);
		TryGetNodeInDirection('W', map.Grid, out Node? WestNode, false);
		(WestNode as ConnectedNodes)?.Spread(map);
		TryGetNodeInDirection('E', map.Grid, out Node? EastNode, false);
		(EastNode as ConnectedNodes)?.Spread(map);
	}

	public void Connect(MapPart2 map){
		if (Pipe.PipeChar.Equals('J')){

		}

		TryGetNodeInDirection('N', map.Grid, out Node? NorthNode, true);
		if (NorthNode?.CanConnectFrom('S') ?? false){
			if (Y + 1 < map.Grid.Count){
				(map.Grid[Y + 1][X] as ConnectedNodes).Connected = true;
			}
		}
		TryGetNodeInDirection('S', map.Grid, out Node? SouthNode, true);
		if (SouthNode?.CanConnectFrom('N')?? false){
			if (Y > 0){
				(map.Grid[Y - 1][X] as ConnectedNodes).Connected = true;
			}
		}
		TryGetNodeInDirection('W', map.Grid, out Node? WestNode, true);
		if (WestNode?.CanConnectFrom('E') ?? false){
			if (X > 0){
				(map.Grid[Y][X - 1] as ConnectedNodes).Connected = true;
			}
		}
		TryGetNodeInDirection('E', map.Grid, out Node? EastNode, true);
		if (EastNode?.CanConnectFrom('W')?? false){
			if (X < map.Grid[0].Count){
				(map.Grid[Y][X + 1] as ConnectedNodes).Connected = true;
			}
		}
	}

}
public class ConnectionNode: ConnectedNodes{
    public ConnectionNode(int x, int y): base('C', x, y){

    }

    public override void Path(Map map, int distance, char inboundDirection)
    {
        TryGetNodeInDirection(InvertDirection(inboundDirection), map.Grid, out Node nextNode, false);

        nextNode.Path(map, distance, inboundDirection);
    }
}

public class MapPart2{
    public List<List<Node>> Grid {get; private set;} = new List<List<Node>>();

    int startX = -1;
    int startY = -1;

    public MapPart2(string[] rows)
    {
        int x = 0;
        int y = 0;
        foreach (var row in rows){
            x = 0;
            CreateConnectionRow(row.Length, y);
            y++;

            Grid.Add(new List<Node>());
            bool first = true;
            foreach (var c in row)
            {
                if (!first){
                    Grid.Last().Add(new ConnectionNode(x, y));
                    x++;
                }
                else{
                    first = false;
                }

                Grid.Last().Add(new ConnectedNodes(c, x, y));
                x++;
            }

            y ++;
        }
    }

    private void CreateConnectionRow(int width, int y){
        Grid.Add(new List<Node>());
        for (int i = 0; i < (width*2)-1; i += 2)
        {
            Grid.Last().Add(new ConnectionNode(i, y));
            Grid.Last().Add(new ConnectionNode(i+1, y));
        }

        Grid.Last().RemoveAt(Grid.Last().Count - 1);
    }

    public string StartPos(){
        return $"x:{startX}, y:{startY}";
    }

    public string MapOutput(){
        StringBuilder sb = new StringBuilder();
        foreach (var row in Grid)
        {
			List<string> rowOutput = new List<string>();
			foreach (var node in row)
			{
				if (node is ConnectionNode cn){
					if (cn.Connected){
						rowOutput.Add("#");
						continue;
					}
				}
				rowOutput.Add(node.Pipe.PipeChar.ToString());
			}
            sb.AppendLine(string.Join("", rowOutput));
        }

        return sb.ToString();
    }

	public string MapOutputNoConnections(){
        StringBuilder sb = new StringBuilder();
        foreach (var row in Grid)
        {
            if (row.Where(x => !(x is ConnectionNode)).Count() == 0)
                continue;

            sb.AppendLine(string.Join("", row.Where(x => !(x is ConnectionNode)).Select(x => x.Pipe.PipeChar)));
        }

        return sb.ToString();
    }

	public string OutsideConnection(){
        StringBuilder sb = new StringBuilder();
        foreach (var row in Grid)
        {
            if (row.Where(x => !(x is ConnectionNode)).Count() == 0)
                continue;

            List<string> rowOutput = new List<string>();
			foreach (var node in row)
			{
				if (node is ConnectionNode)
					continue;

				if (node is ConnectedNodes cn){
					if (!cn.Pipe.PipeChar.Equals('.')){
						rowOutput.Add(cn.Pipe.PipeChar.ToString());
					}
					else if (cn.OutsideReachable){
						rowOutput.Add("0");
					}
					else if (!cn.OutsideReachable){
						rowOutput.Add("1");
					}
				}
			}
			
            sb.AppendLine(string.Join("", rowOutput));
        }

        return sb.ToString();
    }

	public int FindReachable(){
		foreach (var node in GetOutsideNodes())
		{
			(node as ConnectedNodes).Spread(this);
		}

		return Grid.SelectMany(x => x).Where(x => !(x is ConnectionNode)).Where(x => !(x as ConnectedNodes).OutsideReachable && !(x as ConnectedNodes).IsPipe).Count();
	}

	private List<Node> GetOutsideNodes(){
		var top = Grid[1].Where(x => !(x is ConnectionNode));
		var bottom = Grid[Grid.Count - 1].Where(x => !(x is ConnectionNode));

		var left = Grid.Select(x => x.First()).Where(x => !(x is ConnectionNode));
		var right = Grid.Select(x => x.Last()).Where(x => !(x is ConnectionNode));

		return top.Concat(bottom).Concat(left).Concat(right).ToList();
	}

	private List<Node> AllPipes(){
		return Grid.SelectMany(x => x).Where(x => (x as ConnectedNodes).IsPipe).ToList();
	}

	public void ConnectPipes(){
		foreach (var pipeNode in AllPipes())
		{
			(pipeNode as ConnectedNodes).Connect(this);
		}

	}

}
