using Day14;

namespace Day14;

public class Day14Tests{
	[Test]
	public void Test1(){
		Dish dish = new Dish(new List<string>(){
			"O....#....",
			"O.OO#....#",
			".....##...",
			"OO.#O....O",
			".O.....O#.",
			"O.#..O.#.#",
			"..O..#O..O",
			".......O..",
			"#....###..",
			"#OO..#...."
		});

		Assert.That(dish.OutputGrid(), Is.EqualTo("O....#....\n" +
			"O.OO#....#\n" +
			".....##...\n" +
			"OO.#O....O\n" +
			".O.....O#.\n" +
			"O.#..O.#.#\n" +
			"..O..#O..O\n" +
			".......O..\n" +
			"#....###..\n" +
			"#OO..#....\n"));

		dish.TiltNorth();

		Assert.That(dish.OutputGrid(), Is.EqualTo(
			"OOOO.#.O..\n"+
			"OO..#....#\n"+
			"OO..O##..O\n"+
			"O..#.OO...\n"+
			"........#.\n"+
			"..#....#.#\n"+
			"..O..#.O.O\n"+
			"..O.......\n"+
			"#....###..\n"+
			"#....#....\n"));

		Assert.That(dish.Sum1, Is.EqualTo(136));
	}

	[Test]
	public void Test2(){
		Dish dish = new Dish(new List<string>(){
			"...#....",
			"....O...",
			"........",
		});

		dish.TiltNorth();

		Assert.That(dish.OutputGrid(), Is.EqualTo(
			"...#O...\n"+
			"........\n"+
			"........\n"));

		dish.TiltWest();

		Assert.That(dish.OutputGrid(), Is.EqualTo(
			"...#O...\n"+
			"........\n"+
			"........\n"));

		dish.TiltSouth();

		Assert.That(dish.OutputGrid(), Is.EqualTo(
			"...#....\n"+
			"........\n"+
			"....O...\n"));

		dish.TiltEast();

		Assert.That(dish.OutputGrid(), Is.EqualTo(
			"...#....\n"+
			"........\n"+
			".......O\n"));
	}

	[Test]
	public void Test3(){
		Dish dish = new Dish(new List<string>(){
			"O....#....",
			"O.OO#....#",
			".....##...",
			"OO.#O....O",
			".O.....O#.",
			"O.#..O.#.#",
			"..O..#O..O",
			".......O..",
			"#....###..",
			"#OO..#...."
		});


		dish.Cycle();

		Assert.That(dish.OutputGrid(), Is.EqualTo(
			".....#....\n"+
			"....#...O#\n"+
			"...OO##...\n"+
			".OO#......\n"+
			".....OOO#.\n"+
			".O#...O#.#\n"+
			"....O#....\n"+
			"......OOOO\n"+
			"#...O###..\n"+
			"#..OO#....\n"));
	}
}