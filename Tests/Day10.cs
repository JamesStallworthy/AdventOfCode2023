namespace Day10;

public class Day10Tests{

	[Test]
	public void MapTest1(){
		List<string> mapInput = new List<string>(){
			".....",
			".S-7.",
			".|.|.",
			".L-J.",
			"....."
		};

		Map map = new Map(mapInput.ToArray());

		Assert.That(map.MapOutput(), Is.EqualTo(
			".....\n"+
			".S-7.\n"+
			".|.|.\n"+
			".L-J.\n"+
			".....\n"));

		Assert.That(map.StartPos(), Is.EqualTo("x:1, y:1"));

		map.CalculateMapDistances();

		Assert.That(map.DistanceOutput(), Is.EqualTo(
			".....\n"+
			".012.\n"+
			".1.3.\n"+
			".234.\n"+
			".....\n"));

		Assert.That(map.GetFurthestDistance(), Is.EqualTo(4));
	}
}