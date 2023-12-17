using Day17;

namespace Day17;

public class Day17Test{
	[Test]
	public void Test1(){
		Grid grid = new Grid(new string[]{
			"2413432311323",
			"3215453535623",
			"3255245654254",
			"3446585845452",
			"4546657867536",
			"1438598798454",
			"4457876987766",
			"3637877979653",
			"4654967986887",
			"4564679986453",
			"1224686865563",
			"2546548887735",
			"4322674655533"
		});

		Assert.That(grid.ToString(), Is.EqualTo("2413432311323\n"+
			"3215453535623\n"+
			"3255245654254\n"+
			"3446585845452\n"+
			"4546657867536\n"+
			"1438598798454\n"+
			"4457876987766\n"+
			"3637877979653\n"+
			"4654967986887\n"+
			"4564679986453\n"+
			"1224686865563\n"+
			"2546548887735\n"+
			"4322674655533\n"));

		grid.CalculateDistances();

		Assert.That(grid.ToStringDistances(), Is.EqualTo("2413432311323\n"+
			"3215453535623\n"+
			"3255245654254\n"+
			"3446585845452\n"+
			"4546657867536\n"+
			"1438598798454\n"+
			"4457876987766\n"+
			"3637877979653\n"+
			"4654967986887\n"+
			"4564679986453\n"+
			"1224686865563\n"+
			"2546548887735\n"+
			"4322674655533\n"));
	}

	[Test]
	public void Test2(){
		Grid grid = new Grid(new string[]{
			"24134",
			"32154",
			"32552",
			"34465",
			"45466",
		});
		grid.CalculateDistances();
	}

	[Test]
	public void Test3(){
		Grid grid = new Grid(new string[]{
			"11599",
			"99199",
			"99199",
			"99199",
			"99111",
		});

		grid.CalculateDistances();
	}

	[Test]
	public void Test4(){
		Grid grid = new Grid(new string[]{
			"2413432",
			"3215453",
		});

		grid.CalculateDistances();
	}

}