using System.Reflection.Metadata;
using System.Text;

namespace Day13;

public class Pattern{
    StringBuilder FullPattern {get; set;}
    public Pattern(string pattern)
    {
        FullPattern = new StringBuilder(pattern);
    }

    public int Sum(){
        int sum = 0;
        int vert = VerticalPatterns();

        if (vert != - 1){
            sum += vert;
        }

        int hor = HorizontalPatterns();

        if (hor != -1){
            sum += hor * 100;
        }

        return sum;
    }

    public int Sum2(){
        int v = VerticalPatterns();
        int h = HorizontalPatterns();
        int nV = v;
        int nH = h;
        
        int fV = 0;
        int fH = 0;

        for (int i = 0; i < FullPattern.Length; i++)
        {
            if (FullPattern[i] == '\n'){
                continue;
            }
            else if (FullPattern[i] == '#'){
                FullPattern[i] = '.';
                nV = VerticalPatterns(v);
                nH = HorizontalPatterns(h);
                FullPattern[i] = '#';
            }
            else{
                FullPattern[i] = '#';
                nV = VerticalPatterns(v);
                nH = HorizontalPatterns(h);
                FullPattern[i] = '.';
            }

            if (nV != 0 || nH != 0){
                if (nV != v){
                    fV = nV;
                }
                if (nH != h){
                    fH = nH;
                }
            }
        }
        return fV + (fH * 100);
    }

    public int VerticalPatterns(int ignoreAnswer = -1)
    {
        List<string> rows = FullPattern.ToString().Split(Environment.NewLine).ToList();

        return IsSymmetrical(rows, ignoreAnswer);
    }

    public int HorizontalPatterns(int ignoreAnswer = -1)
    {
        List<string> rows = FullPattern.ToString().Split(Environment.NewLine).ToList();

        List<string> Columns = new List<string>(rows.Count);

        foreach (var row in rows)
        {
            int colIndex = 0;
            foreach (var c in row)
            {
                if (Columns.Count <= colIndex){
                    Columns.Add("");
                }

                Columns[colIndex] = Columns[colIndex] + c;

                colIndex ++;
            }
        }

        return IsSymmetrical(Columns, ignoreAnswer);
    }

    private int IsSymmetrical(List<string> rows,int ignoreAnswer = -1)
    {
        int rowWidth = rows[0].Count();

        for (int i = 1; i < rowWidth; i++)
        {
            bool allRowsSymmetrical = true;
            foreach (var row in rows)
            {
                if (!IsSymmetrical(i, row))
                {
                    allRowsSymmetrical = false;
                    break;
                }
            }

            if (allRowsSymmetrical)
            {
                if (i == ignoreAnswer){
                    continue;
                }
                return i;
            }
        }

        return 0;
    }

    private bool IsSymmetrical(int splitIndex, string value){
        string left = value.Substring(0, splitIndex);
        string right = value.Substring(splitIndex);

        left = string.Join("", left.Reverse());

        int maxSize = Math.Min(left.Length, right.Length);

        left = left.Substring(0, maxSize);
        right = right.Substring(0, maxSize);

        return left == right;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var patterns = File.ReadAllText("./input.txt").Split($"{Environment.NewLine}{Environment.NewLine}");

        long sum = 0;
        long sum2 = 0;
        foreach (var pattern in patterns)
        {
            Pattern pat = new Pattern(pattern);

            sum += pat.Sum();
            sum2 += pat.Sum2();
        }

        System.Console.WriteLine(sum);
        System.Console.WriteLine(sum2);
    }
}
