using Day19;

namespace Day19;

public class Day19Tests{
	[Test]
	public void RuleTest(){
		Rule rule = new Rule("a<2006:qkq");

		Part part = new Part("{x=787,m=2655,a=1222,s=2876}");

		Assert.That(rule.Process(part), Is.EqualTo("qkq"));


		Part part2 = new Part("{x=787,m=2655,a=4222,s=2876}");

		Assert.That(rule.Process(part2), Is.EqualTo(null));

		Rule rule2 = new Rule("a>2006:qkq");

		Part part3 = new Part("{x=787,m=2655,a=1222,s=2876}");

		Assert.That(rule2.Process(part3), Is.EqualTo(null));

		Part part4 = new Part("{x=787,m=2655,a=4222,s=2876}");

		Assert.That(rule2.Process(part4), Is.EqualTo("qkq"));
	}

	[Test]
	public void RuleTest2(){
		Rule rule = new Rule("rfg");

		Part part = new Part("{x=787,m=2655,a=1222,s=2876}");

		Assert.That(rule.Process(part), Is.EqualTo("rfg"));
	}

	[Test]
	public void Test1(){
		Sorter sorter = new Sorter("px{a<2006:qkq,m>2090:A,rfg}\n" +
			"pv{a>1716:R,A}\n" +
			"lnx{m>1548:A,A}\n" +
			"rfg{s<537:gd,x>2440:R,A}\n" +
			"qs{s>3448:A,lnx}\n" +
			"qkq{x<1416:A,crn}\n" +
			"crn{x>2662:A,R}\n" +
			"in{s<1351:px,qqz}\n" +
			"qqz{s>2770:qs,m<1801:hdj,R}\n" +
			"gd{a>3333:R,R}\n" +
			"hdj{m>838:A,pv}",


			"{x=787,m=2655,a=1222,s=2876}\n"+
			"{x=1679,m=44,a=2067,s=496}\n"+
			"{x=2036,m=264,a=79,s=2244}\n"+
			"{x=2461,m=1339,a=466,s=291}\n"+
			"{x=2127,m=1623,a=2188,s=1013}"
			);

		sorter.Sort();

		Assert.That(sorter.SumAccepted(), Is.EqualTo(19114));
	}

}