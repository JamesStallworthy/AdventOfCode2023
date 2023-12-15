using Day15;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Day15;

public class Day15Tests{
	[Test]
	public void Test1(){
		Word word = new Word("HASH");

		Assert.That(word.Hash(), Is.EqualTo(52));
	}

	[Test]
	public void Test2(){
		Lense lense = new Lense("rn=1");

		Assert.That(new Word(lense.LenseIdentifier).Hash(), Is.EqualTo(0));
		Assert.That(lense.LenseIdentifier, Is.EqualTo("rn"));
	}


	[Test]
	public void Test3(){
		List<string> RawLenses = new List<string>(){
			"rn=1","cm-","qp=3","cm=2","qp-","pc=4","ot=9","ab=5","pc-","pc=6","ot=7"
		};

		List<Box> Boxes = new List<Box>();
		for (int i = 0; i < 4; i++)
		{
			Boxes.Add(new Box());
		}

		foreach (var item in RawLenses)
		{
			Lense lense = new Lense(item);

			if (lense.Action == '='){
				Boxes[new Word(lense.LenseIdentifier).Hash()].Add(lense);
			}
			else{
				Boxes[new Word(lense.LenseIdentifier).Hash()].Remove(lense);
			}
		}

		Assert.That(Day15.Program.Part2Sum(Boxes), Is.EqualTo(145));


	}
}