namespace Day19;

public class Rule{
    enum Sign{
        MoreThan,
        LessThan,
        Nothing
    }
    private string Catergory {get; set;}
    private int Value {get; set;}
    private Sign Operation {get;set;}
    private string Output {get;set;}
    public Rule(string ruleInput)
    {
        string[] splitRule = ruleInput.Split(":");

        if (splitRule.Length == 1){
            Operation = Sign.Nothing;
            Output = ruleInput;
            Catergory = null;
            Value = -1;
            return;
        }

        string rule = splitRule[0];
        Output = splitRule[1];

        Catergory = rule[..1];
        string rawOperation = rule[1..2];
        if (rawOperation == ">"){
            Operation = Sign.MoreThan;
        }
        else{
            Operation = Sign.LessThan;
        }
        Value = int.Parse(rule[2..]);
    }

    public string Process(Part part){
        if (Operation == Sign.Nothing){
            return Output;
        }

        int partValue = part.Get(Catergory);

        if (Operation == Sign.MoreThan){
            if (partValue > Value){
                return Output;
            }
        }
        else if (Operation == Sign.LessThan){
            if (partValue < Value){
                return Output;
            }
        }

        return null;
    }
}

public class Workflow{
    List<Rule> Rules = new List<Rule>();

    //{a<2006:qkq,m>2090:A,rfg}
    public Workflow(string set)
    {
        set = set.Replace("{", "").Replace("}", "");

        foreach (var rule in set.Split(","))
        {
            Rules.Add(new Rule(rule));
        }
    }

    public string Process(Part part){
        foreach (var rule in Rules)
        {
            var output = rule.Process(part);
            if (output != null)
                return output;
        }

        return null;
    }
}

public class Sorter{
    Dictionary<string, Workflow> Workflows = new Dictionary<string, Workflow>();
    List<Part> Parts = new List<Part>();

    public List<Part> Accepted = new List<Part>();

    public Sorter(string workflows, string parts)
    {
        var splitWorkflows = workflows.Split(Environment.NewLine);

        foreach (var workflow in splitWorkflows)
        {
            var splitWorkflow = workflow.Split("{");

            Workflows.Add(splitWorkflow[0], new Workflow(splitWorkflow[1]));
        }

        var splitParts = parts.Split(Environment.NewLine);

        foreach (var part in splitParts)
        {
            Parts.Add(new Part(part));
        }
    }

    public void Sort(){
        foreach (var part in Parts)
        {
            ProcessPart(part);
        }
    }

    public void ProcessPart(Part part){
        Workflow current = Workflows["in"];
        while (true)
        {
            var output = current.Process(part);
            if (output == "A"){
                Accepted.Add(part);
                break;
            }
            else if (output == "R"){
                break;
            }
            
            current = Workflows[output];
        }
    }

    public long SumAccepted(){
        return Accepted.Select(x => x.Sum()).Sum();
    }
}

public class Part{
    Dictionary<string, int> Categories = new Dictionary<string, int>();
    public Part(string part){
        part = part.Replace("{", "").Replace("}", "");

        string[] categories = part.Split(",");

        foreach (var cat in categories)
        {
            var splitCat = cat.Split("=");

            Categories.Add(splitCat[0], int.Parse(splitCat[1]));
        }
    }

    public int Get(string catergory){
        return Categories[catergory];
    }

    public long Sum(){
        long sum = 0;
        foreach (var item in Categories)
        {
            sum += item.Value;
        }

        return sum;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var file = File.ReadAllText("./input.txt");

        var splitFile = file.Split($"{Environment.NewLine}{Environment.NewLine}");

        Sorter sorter = new Sorter(splitFile[0], splitFile[1]);

        sorter.Sort();

        System.Console.WriteLine(sorter.SumAccepted());
    }
}
