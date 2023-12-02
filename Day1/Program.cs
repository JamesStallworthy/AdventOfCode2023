Dictionary<string, char> Map = new Dictionary<string, char>(){
 	{"one", '1'},
 	{"two", '2'},
 	{"three", '3'},
 	{"four", '4'},
 	{"five", '5'},
 	{"six", '6'},
 	{"seven", '7'},
 	{"eight", '8'},
 	{"nine", '9'},
};

var file = File.ReadLines("./input.txt");

var sum = 0;
foreach (var line in file){
	List<int> numbersInLine = new List<int>();
	int index = 0;
	foreach (char c in line){
		if (char.IsNumber(c)){
			numbersInLine.Add(c - '0');
		}
		else{
			foreach(var strNum in Map){
				if (line.Substring(index).StartsWith(strNum.Key)){
					numbersInLine.Add(strNum.Value - '0');
				}
			}
		}

		index ++;
	}

	if (numbersInLine.Any()){
		sum += int.Parse($"{numbersInLine.First()}{numbersInLine.Last()}");
	}
}

Console.WriteLine($"Answer: {sum}");