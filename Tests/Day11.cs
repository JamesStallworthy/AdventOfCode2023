using Day11;

public class Day11Tests{
	[Test]
	public void Test1(){

		Universe universe = new Universe();

		universe.Init(new string[]{
			"...#......",
			".......#..",
			"#.........",
			"..........",
			"......#...",
			".#........",
			".........#",
			"..........",
			".......#..",
			"#...#....."});

		Assert.That(universe.Display(), Is.EqualTo(
			"...1......\n" +
			".......2..\n" +
			"3.........\n" +
			"..........\n" +
			"......4...\n" +
			".5........\n" +
			".........6\n" +
			"..........\n" +
			".......7..\n" +
			"8...9.....\n"));

			universe.Expand();
			Assert.That(universe.Display(), Is.EqualTo(
			"....1........\n" +
			".........2...\n" +
			"3............\n" +
			".............\n" +
			".............\n" +
			"........4....\n" +
			".5...........\n" +
			"............6\n" +
			".............\n" +
			".............\n" +
			".........7...\n" +
			"8....9.......\n"));

			Assert.That(universe.FindDistanceBetweenGalaxies(1, 2), Is.EqualTo(6));
			Assert.That(universe.FindDistanceBetweenGalaxies(1, 7), Is.EqualTo(15));
			Assert.That(universe.FindDistanceBetweenGalaxies(3, 6), Is.EqualTo(17));
			Assert.That(universe.FindDistanceBetweenGalaxies(8, 9), Is.EqualTo(5));

			Assert.That(universe.SumOfAllDistances(), Is.EqualTo(374));
	}
}