using Day12;

namespace Day12;

public class Day12Tests{
	[Test]
	public void Test1(){
		Row row = new Row("#.#.### 1,1,3");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(1));
	}

	[Test]
	public void Test2(){
		Row row = new Row("#...### 1,1,3");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(0));
	}

	[Test]
	public void Test3(){
		Row row = new Row("#.?.### 1,1,3");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(1));
	}

	[Test]
	public void Test4(){
		Row row = new Row("##.. 1,1");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(0));
	}

	[Test]
	public void Part1Examples(){
		Row	row = new Row("???.### 1,1,3");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(1));

		row = new Row(".??..??...?##. 1,1,3");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(4));

		row = new Row("?#?#?#?#?#?#?#? 1,3,1,6");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(1));

		row = new Row("????.#...#... 4,1,1");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(1));

		row = new Row("????.######..#####. 1,6,5");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(4));

		row = new Row("?###???????? 3,2,1 1,6,5");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(10));
	}

	[Test]
	public void Part2Examples(){
		Row	row = new Row("???.###????.###????.###????.###????.### 1,1,3,1,1,3,1,1,3,1,1,3,1,1,3");

		Assert.That(row.FindValidCombinations(), Is.EqualTo(1));

		
	}
}