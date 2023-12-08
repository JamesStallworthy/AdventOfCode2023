namespace Day8;

public class NodeTree{
    Dictionary<string,Node> Nodes = new Dictionary<string,Node>();

    public NodeTree(string[] input)
    {
        for (int i = 0; i < input.Length; i++)
		{
			string[] splitLine = input[i].Split(" = ");
			string Id = splitLine[0];

			string left = splitLine[1].Split(", ")[0].Replace("(", "");
			string right = splitLine[1].Split(", ")[1].Replace(")", "");
			Nodes.Add(Id, new Node(Id, left, right));
		}
    }

    public int Part1(char[] instructions){
        Node currentNode = Nodes["AAA"];
		int instructionId = 0;
		int numberOfSteps = 0;

		while(currentNode.ID != "ZZZ"){
			numberOfSteps ++;
			if (instructionId >= instructions.Length){
				instructionId = 0;
			}

			char currentInstruction = instructions[instructionId];

			if (currentInstruction == 'L'){
				currentNode = Nodes[currentNode.Left];
			}
			else{
				currentNode = Nodes[currentNode.Right];
			}

			instructionId ++;
		}

        return numberOfSteps;
    }

    //Brute force sadly was not the solution today... Had to look for some help. LCM is a interesting bit of maths!
    //HyperNeutrino made a great video on how part 2 is solved. Here is my messy solution in c#
    //https://www.youtube.com/watch?v=_nnxLcrwO_U
    public long Part2(char[] instructions){
        List<Node> StartNodes = Nodes.Where(x => x.Value.ID.EndsWith("A")).Select(x => x.Value).ToList();

        List<int> cycles = new List<int>();
        foreach (var node in StartNodes)
        {
            int cycle = FindCycle(node, instructions);
            cycles.Add(cycle);
        }

        System.Console.WriteLine(string.Join(",", cycles));

        return lcm(cycles.OrderBy(x => x).Select(x => (long)x).ToList());
    }

    private long lcm(List<long> numbers){
        long currentLcm = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            currentLcm = lcm(currentLcm, numbers[i]);
        }

        return currentLcm;
    }

    private long lcm(long a, long b){
        return (a*b)/gcd(a, b);
    }

    private long gcd(long a, long b){
        return b == 0 ? a : gcd(b, a % b);
    }

    private int FindCycle(Node startNode, char[] instructions){
        Node currentNode = startNode;
		int instructionId = 0;
		int numberOfSteps = 0;

		while(!currentNode.ID.EndsWith("Z")){
			numberOfSteps ++;
			if (instructionId >= instructions.Length){
				instructionId = 0;
			}

			char currentInstruction = instructions[instructionId];

			if (currentInstruction == 'L'){
				currentNode = Nodes[currentNode.Left];
			}
			else{
				currentNode = Nodes[currentNode.Right];
			}

			instructionId ++;
		}

        return numberOfSteps;
    }
}

public class Node{
    public Node(string Id, string l, string r)
    {
        ID = Id;
        Left = l;
        Right = r;
    }

    public string ID {get; set;} 

    public string Left {get; set;}
    public string Right {get; set;}
}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllLines("./input.txt");

        char[] instructions = file[0].ToCharArray();

        NodeTree nodeTree = new NodeTree(file[2..]);

        System.Console.WriteLine($"Part1 Answer: {nodeTree.Part1(instructions)}");
        System.Console.WriteLine($"Part2 Answer: {nodeTree.Part2(instructions)}");
    }
}
