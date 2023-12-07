using System.Text;
using Day7;
namespace Day7;
public class Day7Tests
{
    [SetUp]
    public void Setup()
    {
    }

	[Test]
    public void SortingPart1()
    {
		var rawTest = 
			"32T3K 765\r\n"+
			"T55J5 684\r\n"+
			"KK677 28\r\n"+
			"KTJJT 220\r\n"+
			"QQQJA 483";

		List<Hand> hands = new List<Hand>();
        foreach (var line in rawTest.Split("\r\n"))
        {
            hands.Add(new Hand(line, false));
        }

        hands.Sort(new CustomHandComparer());

        StringBuilder sb = new StringBuilder();
        foreach (var hand in hands)
        {
            sb.AppendLine(hand.ToString());
        }

		Assert.That(sb.ToString(), Is.EqualTo("32T3K\nKK677\nT55J5\nQQQJA\nKTJJT\n"));
	}

	[Test]
    public void SortingPart2()
    {
		Hand.CardMap['J'] = -1;
		var rawTest = 
			"32T3K 765\r\n"+
			"T55J5 684\r\n"+
			"KK677 28\r\n"+
			"KTJJT 220\r\n"+
			"QQQJA 483";

		List<Hand> hands = new List<Hand>();
        foreach (var line in rawTest.Split("\r\n"))
        {
            hands.Add(new Hand(line, true));
        }

        hands.Sort(new CustomHandComparer());

        StringBuilder sb = new StringBuilder();
        foreach (var hand in hands)
        {
            sb.AppendLine(hand.ToString());
        }

		Assert.That(sb.ToString(), Is.EqualTo("32T3K\nKTJJT\nKK677\nT55J5\nQQQJA\n"));
	}

    [Test]
    public void HandType()
    {
        var hand = new Hand("AAAAA 10");
        Assert.That(hand.Type, Is.EqualTo(0));

        hand = new Hand("AA8AA 10");
		Assert.That(hand.Type, Is.EqualTo(1));

        hand = new Hand("23332 10");
		Assert.That(hand.Type, Is.EqualTo(2));

        hand = new Hand("TTT98 10");
        Assert.That(hand.Type, Is.EqualTo(3));

        hand = new Hand("23432 10");
        Assert.That(hand.Type, Is.EqualTo(4));

        hand = new Hand("A23A4 10");
        Assert.That(hand.Type, Is.EqualTo(5));

        hand = new Hand("23456 10");
        Assert.That(hand.Type, Is.EqualTo(6));
    }
}