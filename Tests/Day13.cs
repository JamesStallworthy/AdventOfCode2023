using Day13;

namespace Day13;

public class Day13Tests{

	[Test]
	public void Test1(){
		Pattern pat = new Pattern(
			"#.##..##.\n"+
			"..#.##.#.\n"+
			"##......#\n"+
			"##......#\n"+
			"..#.##.#.\n"+
			"..##..##.\n"+
			"#.#.##.#.");

		Assert.That(pat.VerticalPatterns(), Is.EqualTo(5));
	}

	[Test]
	public void Test2(){
		Pattern pat = new Pattern(
		"#...##..#\n" +
		"#....#..#\n" +
		"..##..###\n" +
		"#####.##.\n" +
		"#####.##.\n" +
		"..##..###\n" +
		"#....#..#");
		
		Assert.That(pat.HorizontalPatterns(), Is.EqualTo(4));
	}

	[Test]
	public void Test3(){
		Pattern pat = new Pattern(
			"#.##..##.\n"+
			"..#.##.#.\n"+
			"##......#\n"+
			"##......#\n"+
			"..#.##.#.\n"+
			"..##..##.\n"+
			"#.#.##.#.");

		Assert.That(pat.VerticalPatterns(), Is.EqualTo(5));

		Pattern pat2 = new Pattern(
			"#...##..#\n" +
			"#....#..#\n" +
			"..##..###\n" +
			"#####.##.\n" +
			"#####.##.\n" +
			"..##..###\n" +
			"#....#..#");
		
		Assert.That(pat2.HorizontalPatterns(), Is.EqualTo(4));

		Assert.That(pat.Sum() + pat2.Sum(), Is.EqualTo(405));
	}

	[Test]
	public void Test4(){
		Pattern pat = new Pattern(
			".##...#..##\n"+
			".....##...#\n"+
			"#..#..###..\n"+
			"#..#####..#\n"+
			"####.##.##.\n"+
			"####.##.##.\n"+
			"#..#####..#\n"+
			"#..##.###..\n"+
			".....##...#\n"+
			".##...#..##\n"+
			".##..#.##.#\n"+
			"........###\n"+
			".....#...#.\n"+
			"....#.#.##.\n"+
			"#..##..###.\n"+
			"#..###..#.#\n"+
			"#..#...#.##");

		Assert.That(pat.VerticalPatterns(), Is.EqualTo(2));
		Assert.That(pat.Sum(), Is.EqualTo(2));
	}

	[Test]
	public void Test5(){
		Pattern pat = new Pattern(
			"#.##..##.\n"+
			"..#.##.#.\n"+
			"##......#\n"+
			"##......#\n"+
			"..#.##.#.\n"+
			"..##..##.\n"+
			"#.#.##.#.");

		Assert.That(pat.Sum2(), Is.EqualTo(300));

		Pattern pat2 = new Pattern(
			"#...##..#\n" +
			"#....#..#\n" +
			"..##..###\n" +
			"#####.##.\n" +
			"#####.##.\n" +
			"..##..###\n" +
			"#....#..#");
			
		Assert.That(pat2.Sum2(), Is.EqualTo(100));

		Assert.That(pat.Sum2() + pat2.Sum2(), Is.EqualTo(400));
	}
}