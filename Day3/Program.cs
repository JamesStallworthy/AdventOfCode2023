using System;
using System.Data;

//This is super hacked together but works...

namespace Day3;
class Program
{
	static List<List<char>> DataSet = new List<List<char>>();

    class Gear{
        public int r {get; set;}
        public int y {get; set;}

        public List<int> AdjacentNumbers = new List<int>();

        public int GetRatio(List<List<char>> DataSet){
            GetNumbers(DataSet);

            if (AdjacentNumbers.Count == 2){
                System.Console.WriteLine($"{AdjacentNumbers[0]} * {AdjacentNumbers[1]}");
                return AdjacentNumbers[0] * AdjacentNumbers[1];
            }

            return 0;
        }

        private void GetNumbers(List<List<char>> DataSet){
            int startPos = y - 1;
            if (startPos == -1){
                startPos = 0;
            }

            int endPos = y + 1;
            if (endPos > DataSet[r].Count){
                endPos = DataSet[r].Count - 1;
            }

            //Check current row
            CheckRow(DataSet[r], startPos, endPos);

            //Check row above
            if (r != 0)
                CheckRow(DataSet[r-1], startPos, endPos);

            //Check row below
            if (r + 1 < DataSet.Count)
                CheckRow(DataSet[r+1], startPos, endPos);
        }

        private void CheckRow(List<char> row, int s, int e){
            for (int i = s; i <= e; i++)
            {
                if (char.IsNumber(row[i])){
                    i = FindFullNumber(row, i); //Continue searching from the end of the last number
                }
            }
        }

        private int FindFullNumber(List<char> row, int pos){
            //Work back from the found numbers position to find the beginning of it, then work forward to find it all.
            int startPos = pos;
            for (int i = pos; i >= 0; i--)
            {
                if (char.IsNumber(row[i])){
                    startPos = i;
                }
                else{
                    break;
                }
            }

            string numStr = "";
            //Now we know the start pos, lets build the number up!
            int endOfNumPos = -1;
            for (int i = startPos; i < row.Count; i++)
            {
                if (char.IsNumber(row[i])){
                    numStr += row[i];
                    endOfNumPos = i;
                }
                else{
                    break;
                }
            }

            AdjacentNumbers.Add(int.Parse(numStr));
            return endOfNumPos;
        }
    }

    class EngineNumber{

        public int num {get => int.Parse(numStr);}
        public string numStr {get; set;} = "";
        public int r {get; set;}
        public int start {get; set;}
        public int end {get; set;}

        private bool IsSymbol(char c){
            if (c == '.'){
                return false;
            }

            return !char.IsNumber(c);
        }

        private bool IsAdjacentBefore(List<char> row){
            if (start > 0){
                return IsSymbol(row[start-1]);
            }
            return false;
        }

        private bool IsAdjacentAfter(List<char> row){
            if (end + 1 < row.Count){
                return IsSymbol(row[end+1]);
            }
            return false;
        }

        private bool IsAdjacentBetween(List<char> row){
            int startPos;
            int endPos;

            if (start == 0){
                startPos = 0;
            }
            else{
                startPos = start - 1;
            }

            if (end == row.Count - 1){
                endPos = end;
            }
            else{
                endPos = end + 1;
            }

            for (int i = startPos; i <= endPos; i++)
            {
                if (IsSymbol(row[i]))
                    return true;
            }

            return false;
        }

        public bool IsAdjacent(List<List<char>> dataSet){
            if (IsAdjacentBefore(dataSet[r]))
                return true;

            if (IsAdjacentAfter(dataSet[r]))
                return true;

            if (r != 0)
                if (IsAdjacentBetween(dataSet[r - 1])) //Checks the row above
                    return true;

            if (r + 1 < dataSet.Count)
                if (IsAdjacentBetween(dataSet[r + 1])) //Checks the row above
                    return true;

            return false;
        }
    }

    static List<EngineNumber> EngineNumbers = new List<EngineNumber>();
    static List<Gear> Gears = new List<Gear>();

    static void Main(string[] args)
    {
        LoadDataStructure();

        for (int i = 0; i < DataSet.Count; i++)
        {
            ProcessRow(i);
        }

        int sum = 0;
        foreach (var engineNumber in EngineNumbers)
        {
            if (engineNumber.IsAdjacent(DataSet)){
                sum += engineNumber.num;
            }
        }

        int sumPart2 = 0;
        foreach (var gear in Gears)
        {
            sumPart2 += gear.GetRatio(DataSet);
        }

        System.Console.WriteLine($"sum: {sum}");
        System.Console.WriteLine($"sum2: {sumPart2}");
    }

    static void ProcessRow(int r){
        //Find the numbers and record there positions
        EngineNumber? engineNumber = null;
        for (int i = 0; i < DataSet[r].Count; i++)
        {
            if (DataSet[r][i] == '*'){
                Gears.Add(new Gear(){
                    r = r,
                    y = i
                });
            }

            if (char.IsNumber(DataSet[r][i])){
                if (engineNumber == null){
                    engineNumber = new EngineNumber();
                    engineNumber.start = i;
                    engineNumber.r = r;
                }

                engineNumber.numStr += DataSet[r][i];

                if (i == DataSet[r].Count - 1){
                    engineNumber.end = i;
                    EngineNumbers.Add(engineNumber);
                    engineNumber = null;
                }
            }
            else{
                if (engineNumber != null){
                    engineNumber.end = i - 1;
                    EngineNumbers.Add(engineNumber);
                    engineNumber = null;
                }
            }
        }

    }

    static void LoadDataStructure()
    {
        var file = File.ReadAllLines("./input.txt");

        foreach (var line in file)
        {
            DataSet.Add(new List<char>());
            foreach (char c in line)
            {
                DataSet.Last().Add(c);
            }
        }
    }
}
