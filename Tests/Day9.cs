using Day9;

namespace Day8;

public class Day9Tests{
	[Test]
	public void Part1Example1(){
		Sequence Sequence = new Sequence(new int[]{0,3,6,9,12,15});

		Assert.That(Sequence.DebugOutput()
			, Is.EqualTo("0 3 6 9 12 15\n"
						+ "3 3 3 3 3\n"+
						   "0 0 0 0\n"));

		Sequence.IncrementSequence();

		Assert.That(Sequence.DebugOutput()
			, Is.EqualTo("0 3 6 9 12 15 18\n"
						+ "3 3 3 3 3 3\n"+
						   "0 0 0 0 0\n"));

		Assert.That(string.Join(" ", Sequence.GetSequence()), Is.EqualTo("0 3 6 9 12 15 18"));

		Sequence = new Sequence(new int[]{1,3,6,10,15,21});

		Sequence.IncrementSequence();

		Assert.That(string.Join(" ", Sequence.GetSequence()), Is.EqualTo("1 3 6 10 15 21 28"));

		Sequence = new Sequence(new int[]{10,13,16,21,30,45});

		Sequence.IncrementSequence();

		Assert.That(string.Join(" ", Sequence.GetSequence()), Is.EqualTo("10 13 16 21 30 45 68"));
	}
}