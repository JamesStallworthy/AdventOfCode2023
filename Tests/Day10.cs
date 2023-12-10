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

	[Test]
	public void MapTest2(){
		List<string> mapInput = new List<string>(){
			".....",
			".S-7.",
			".|.|.",
			".L-J.",
			"....."
		};

		MapPart2 map = new MapPart2(mapInput.ToArray());

		Assert.That(map.MapOutput(), Is.EqualTo(
			"CCCCCCCCC\n" +
			".C.C.C.C.\n"+
			"CCCCCCCCC\n" +
			".CSC-C7C.\n"+
			"CCCCCCCCC\n" +
			".C|C.C|C.\n"+
			"CCCCCCCCC\n" +
			".CLC-CJC.\n"+
			"CCCCCCCCC\n" +
			".C.C.C.C.\n"));

		Assert.That(map.MapOutputNoConnections(), Is.EqualTo(
			".....\n"+
			".S-7.\n"+
			".|.|.\n"+
			".L-J.\n"+
			".....\n"));

		map.ConnectPipes();
		int result = map.FindReachable();

		Assert.That(map.OutsideConnection(), Is.EqualTo(
			"00000\n0S-70\n0|1|0\n0L-J0\n00000\n"
		));

		Assert.That(result, Is.EqualTo(1));
	}

	[Test]
	public void MapTest3Simple(){
		List<string> mapInput = new List<string>(){
			".....",
			".F-7.",
			".|.|.",
			".L-J.",
			".....",
		};

		MapPart2 map = new MapPart2(mapInput.ToArray());

		map.ConnectPipes();
		int result = map.FindReachable();

		Assert.That(map.OutsideConnection(), Is.EqualTo(
			"00000\n"+
			"0F-70\n"+
			"0|1|0\n"+
			"0L-J0\n"+
			"00000\n"
		));

		Assert.That(result, Is.EqualTo(1));
	}

	[Test]
	public void MapTest3Simple2(){
		List<string> mapInput = new List<string>(){
			"F-7.",
			"|.|.",
			"L-J.",
			"....",
		};

		MapPart2 map = new MapPart2(mapInput.ToArray());

		map.ConnectPipes();
		int result = map.FindReachable();

		Assert.That(map.OutsideConnection(), Is.EqualTo(
			"F-70\n"+
			"|1|0\n"+
			"L-J0\n"+
			"0000\n"
		));

		Assert.That(result, Is.EqualTo(1));
	}

	[Test]
	public void MapTest3Simple3(){
		List<string> mapInput = new List<string>(){
			"....",
			".F-7",
			".|.|",
			".L-J",
		};

		MapPart2 map = new MapPart2(mapInput.ToArray());

		map.ConnectPipes();
		int result = map.FindReachable();

		Assert.That(map.OutsideConnection(), Is.EqualTo(
			"0000\n"+
			"0F-7\n"+
			"0|1|\n"+
			"0L-J\n"
		));

		Assert.That(result, Is.EqualTo(1));
	}

	[Test]
	public void MapTest3Simple4(){
		List<string> mapInput = new List<string>(){
			".F-7",
			".|.|",
			".L-J",
			"....",
		};

		MapPart2 map = new MapPart2(mapInput.ToArray());

		map.ConnectPipes();
		int result = map.FindReachable();

		Assert.That(map.OutsideConnection(), Is.EqualTo(
			"0F-7\n"+
			"0|1|\n"+
			"0L-J\n"+
			"0000\n"
		));

		Assert.That(result, Is.EqualTo(1));
	}

	[Test]
	public void MapTest3Simple5(){
		List<string> mapInput = new List<string>(){
			"....",
			"F-7.",
			"|.|.",
			"L-J.",
		};

		MapPart2 map = new MapPart2(mapInput.ToArray());

		map.ConnectPipes();
		int result = map.FindReachable();

		Assert.That(map.OutsideConnection(), Is.EqualTo(
			"0000\n"+
			"F-70\n"+
			"|1|0\n"+
			"L-J0\n"
		));

		Assert.That(result, Is.EqualTo(1));
	}



	[Test]
	public void MapTest3(){
		List<string> mapInput = new List<string>(){
			"...........",
			".S-------7.",
			".|F-----7|.",
			".||.....||.",
			".||.....||.",
			".|L-7.F-J|.",
			".|..|.|..|.",
			".L--J.L--J.",
			"..........."
		};

		MapPart2 map = new MapPart2(mapInput.ToArray());

		Assert.That(map.MapOutput(), Is.EqualTo(
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".C.C.C.C.C.C.C.C.C.C.\n"+
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".CSC-C-C-C-C-C-C-C7C.\n"+
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".C|CFC-C-C-C-C-C7C|C.\n"+
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".C|C|C.C.C.C.C.C|C|C.\n"+
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".C|C|C.C.C.C.C.C|C|C.\n"+
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".C|CLC-C7C.CFC-CJC|C.\n"+
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".C|C.C.C|C.C|C.C.C|C.\n"+
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".CLC-C-CJC.CLC-C-CJC.\n"+
			"CCCCCCCCCCCCCCCCCCCCC\n"+
			".C.C.C.C.C.C.C.C.C.C.\n"
		));

		Assert.That(map.MapOutputNoConnections(), Is.EqualTo(
			"...........\n"+
			".S-------7.\n"+
			".|F-----7|.\n"+
			".||.....||.\n"+
			".||.....||.\n"+
			".|L-7.F-J|.\n"+
			".|..|.|..|.\n"+
			".L--J.L--J.\n"+
			"...........\n"
		));

		map.ConnectPipes();
		int result = map.FindReachable();

		Assert.That(map.OutsideConnection(), Is.EqualTo(
			"00000000000\n"+
			"0S-------70\n"+
			"0|F-----7|0\n"+
			"0||00000||0\n"+
			"0||00000||0\n"+
			"0|L-70F-J|0\n"+
			"0|11|0|11|0\n"+
			"0L--J0L--J0\n"+
			"00000000000\n"
		));

		Assert.That(result, Is.EqualTo(4));
	}

	[Test]
	public void MapTest4(){
		List<string> mapInput = new List<string>(){
			"..........",
			".S------7.",
			".|F----7|.",
			".||....||.",
			".||....||.",
			".|L-7F-J|.",
			".|..||..|.",
			".L--JL--J.",
			".........."
		};

		MapPart2 map = new MapPart2(mapInput.ToArray());

		Assert.That(map.MapOutputNoConnections(), Is.EqualTo(
			"..........\n"+
			".S------7.\n"+
			".|F----7|.\n"+
			".||....||.\n"+
			".||....||.\n"+
			".|L-7F-J|.\n"+
			".|..||..|.\n"+
			".L--JL--J.\n"+
			"..........\n"
		));

		map.ConnectPipes();
		int result = map.FindReachable();

		Assert.That(map.OutsideConnection(), Is.EqualTo(
			"0000000000\n"+
			"0S------70\n"+
			"0|F----7|0\n"+
			"0||0000||0\n"+
			"0||0000||0\n"+
			"0|L-7F-J|0\n"+
			"0|11||11|0\n"+
			"0L--JL--J0\n"+
			"0000000000\n"
		));

		Assert.That(result, Is.EqualTo(4));
	}
}