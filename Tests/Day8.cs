namespace Day8;

public class Day8Tests{

	[Test]
	public void Part1Example(){
		Dictionary<string, Node> Nodes = new Dictionary<string, Node>();

		string input =  "RL\r"+
						"\r"+
						"AAA = (BBB, CCC)\r"+
						"BBB = (DDD, EEE)\r"+
						"CCC = (ZZZ, GGG)\r"+
						"DDD = (DDD, DDD)\r"+
						"EEE = (EEE, EEE)\r"+
						"GGG = (GGG, GGG)\r"+
						"ZZZ = (ZZZ, ZZZ)";

		var splitInput = input.Split("\r");

		char[] instructions = splitInput[0].ToCharArray();

		NodeTree tree = new NodeTree(splitInput[2..]);

		Assert.That(tree.Part1(instructions), Is.EqualTo(2));
	}

	[Test]
	public void Part1Example2(){
		Dictionary<string, Node> Nodes = new Dictionary<string, Node>();

		string input =  "LLR\r"+
						"\r"+
						"AAA = (BBB, BBB)\r"+
						"BBB = (AAA, ZZZ)\r"+
						"ZZZ = (ZZZ, ZZZ)";

		var splitInput = input.Split("\r");

		char[] instructions = splitInput[0].ToCharArray();
		
		NodeTree tree = new NodeTree(splitInput[2..]);

		Assert.That(tree.Part1(instructions), Is.EqualTo(6));
	}

	[Test]
	public void Part2Example(){
		Dictionary<string, Node> Nodes = new Dictionary<string, Node>();

		string input =  "LR\r"+
						"\r"+
						"11A = (11B, XXX)\r"+
						"11B = (XXX, 11Z)\r"+
						"11Z = (11B, XXX)\r"+
						"22A = (22B, XXX)\r"+
						"22B = (22C, 22C)\r"+
						"22C = (22Z, 22Z)\r"+
						"22Z = (22B, 22B)\r"+
						"XXX = (XXX, XXX)";

		var splitInput = input.Split("\r");

		char[] instructions = splitInput[0].ToCharArray();
		
		NodeTree tree = new NodeTree(splitInput[2..]);

		Assert.That(tree.Part2(instructions), Is.EqualTo(6));
	}
}