using Day18;

namespace Day18;

public class Day18Tests{
	[Test]
	public void Test1(){
		Grid grid = new Grid();

		Instruction one = new Instruction("R 2");
		Instruction two = new Instruction("D 2");
		Instruction three = new Instruction("L 2");
		Instruction four = new Instruction("U 2");

		grid.PerformInstruction(one);
		grid.PerformInstruction(two);
		grid.PerformInstruction(three);
		grid.PerformInstruction(four);

		Assert.That(grid.ToString(), Is.EqualTo("###\n#.#\n###\n"));

		grid.Fill();

		Assert.That(grid.ToString(), Is.EqualTo("###\n###\n###\n"));

		Assert.That(grid.Sum(), Is.EqualTo(9));
	}

	[Test]
	public void Test1_1(){
		Grid grid = new Grid();

		Instruction one = new Instruction("L 2");
		Instruction two = new Instruction("D 2");
		Instruction three = new Instruction("R 2");
		Instruction four = new Instruction("U 2");

		grid.PerformInstruction(one);
		grid.PerformInstruction(two);
		grid.PerformInstruction(three);
		grid.PerformInstruction(four);

		Assert.That(grid.ToString(), Is.EqualTo("###\n#.#\n###\n"));

		grid.Fill();

		Assert.That(grid.ToString(), Is.EqualTo("###\n###\n###\n"));

		Assert.That(grid.Sum(), Is.EqualTo(9));
	}

	[Test]
	public void Test2(){
		Grid grid = new Grid();

		List<Instruction> instructions = new List<Instruction>(){
			new Instruction("R 6 (#70c710)"),
			new Instruction("D 5 (#0dc571)"),
			new Instruction("L 2 (#5713f0)"),
			new Instruction("D 2 (#d2c081)"),
			new Instruction("R 2 (#59c680)"),
			new Instruction("D 2 (#411b91)"),
			new Instruction("L 5 (#8ceee2)"),
			new Instruction("U 2 (#caa173)"),
			new Instruction("L 1 (#1b58a2)"),
			new Instruction("U 2 (#caa171)"),
			new Instruction("R 2 (#7807d2)"),
			new Instruction("U 3 (#a77fa3)"),
			new Instruction("L 2 (#015232)"),
			new Instruction("U 2 (#7a21e3)"),
		};

		foreach (var inst in instructions)
		{
			grid.PerformInstruction(inst);
		}

		grid.Fill();

		Assert.That(grid.ToString(), Is.EqualTo(
			"#######\n"+
			"#######\n"+
			"#######\n"+
			"..#####\n"+
			"..#####\n"+
			"#######\n"+
			"#####..\n"+
			"#######\n"+
			".######\n"+
			".######\n"
			));

		Assert.That(grid.Sum(), Is.EqualTo(62));
	}

	[Test]
	public void Test3(){
		Grid grid = new Grid();

		List<Instruction> instructions = new List<Instruction>(){
			new Instruction("R 6 (#70c710)"),
			new Instruction("U 5 (#0dc571)"),
			new Instruction("R 2 (#5713f0)"),
			new Instruction("D 10 (#d2c081)"),
			new Instruction("L 10 (#59c680)"),
			new Instruction("U 3 (#411b91)"),
			new Instruction("R 2 (#8ceee2)"),
			new Instruction("U 2 (#caa173)"),
		};

		foreach (var inst in instructions)
		{
			grid.PerformInstruction(inst);
		}

		grid.Fill();

		Assert.That(grid.Sum(), Is.EqualTo(77));
	}

	[Test]
	public void Test4(){
		Grid grid = new Grid();

		List<Instruction> instructions = new List<Instruction>(){
			new Instruction("R 3 (#70c710)"),
			new Instruction("D 1 (#70c710)"),
			new Instruction("R 2 (#70c710)"),
			new Instruction("U 1 (#70c710)"),
			new Instruction("R 1 (#70c710)"),
			new Instruction("D 5 (#0dc571)"),
			new Instruction("L 2 (#5713f0)"),
			new Instruction("D 2 (#d2c081)"),
			new Instruction("R 2 (#59c680)"),
			new Instruction("D 2 (#411b91)"),
			new Instruction("L 5 (#8ceee2)"),
			new Instruction("U 2 (#caa173)"),
			new Instruction("L 1 (#1b58a2)"),
			new Instruction("U 2 (#caa171)"),
			new Instruction("R 2 (#7807d2)"),
			new Instruction("U 3 (#a77fa3)"),
			new Instruction("L 2 (#015232)"),
			new Instruction("U 2 (#7a21e3)"),
		};

		foreach (var inst in instructions)
		{
			grid.PerformInstruction(inst);
		}

		grid.Fill();

		Assert.That(grid.ToString(), Is.EqualTo(
			"####.##\n"+
			"#######\n"+
			"#######\n"+
			"..#####\n"+
			"..#####\n"+
			"#######\n"+
			"#####..\n"+
			"#######\n"+
			".######\n"+
			".######\n"
			));

		Assert.That(grid.Sum(), Is.EqualTo(61));
	}

	[Test]
	public void Test5(){
		Grid grid = new Grid();

		List<Instruction> instructions = new List<Instruction>(){
			new Instruction("R 6 (#70c710)"),
			new Instruction("D 2 (#70c710)"),
			new Instruction("L 1 (#70c710)"),
			new Instruction("U 1 (#70c710)"),
			new Instruction("L 2 (#70c710)"),
			new Instruction("D 4 (#70c710)"),
			new Instruction("R 2 (#70c710)"),
			new Instruction("U 1 (#70c710)"),
			new Instruction("R 1 (#70c710)"),
			new Instruction("D 2 (#70c710)"),
			new Instruction("L 5 (#70c710)"),
			new Instruction("U 5 (#70c710)"),


		};

		foreach (var inst in instructions)
		{
			grid.PerformInstruction(inst);
		}

		grid.Fill();

		Assert.That(grid.ToString(), Is.EqualTo(
			"#######\n"+
			"#######\n"+
			"#######\n"+
			"..#####\n"+
			"..#####\n"+
			"#######\n"+
			"#####..\n"+
			"#######\n"+
			".######\n"+
			".######\n"
			));

		Assert.That(grid.Sum(), Is.EqualTo(62));
	}
}