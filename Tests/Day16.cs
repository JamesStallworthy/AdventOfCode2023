using Day16;

namespace Day16;

public class Day16Tests{
	[Test]
	public void Test1(){
		Grid grid = new Grid(new string[] {
			".|...\\....",
			"|.-.\\.....",
			".....|-...",
			"........|.",
			"..........",
			".........\\",
			"..../.\\\\..",
			".-.-/..|..",
			".|....-|.\\",
			"..//.|...."});

		grid.Energise();

		Assert.That(grid.GetEnergisedMap(), Is.EqualTo("######....\n" +
			".#...#....\n" +
			".#...#####\n" +
			".#...##...\n" +
			".#...##...\n" +
			".#...##...\n" +
			".#..####..\n" +
			"########..\n" +
			".#######..\n" +
			".#...#.#..\n"
			));

		Assert.That(grid.GetEnergisedPoints(), Is.EqualTo(46));


		Assert.That(grid.BestEnergiseAnswer(), Is.EqualTo(51));

	}
}