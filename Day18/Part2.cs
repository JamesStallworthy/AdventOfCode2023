using System.Security.Cryptography.X509Certificates;

namespace Day18;

//https://www.reddit.com/r/adventofcode/comments/18l0qtr/comment/kdvrqv8/?utm_source=share&utm_medium=web2x&context=3
//Thank you u/DakilPl for a great explaination
public class Vertex{
	public long X {get; set;}
	public long Y {get; set;}
}

public class GridPart2{
	public List<Vertex> Vertices = new List<Vertex>();

	public GridPart2()
	{
	}

	public void AddInstruction(Instruction instruction){
		if (Vertices.Count == 0){
			Vertices.Add(new Vertex(){X = 0, Y = 0});
		}

		long X = Vertices.Last().X;
		long Y = Vertices.Last().Y;
		switch (instruction.Direction)
		{
			case Instruction.Directions.Up:
				Y = Y - instruction.Steps;
				break;
			case Instruction.Directions.Down:
				Y = Y + instruction.Steps;
				break;
			case Instruction.Directions.Left:
				X = X - instruction.Steps;
				break;
			case Instruction.Directions.Right:
				X = X + instruction.Steps;
				break;
		}

		Vertices.Add(new Vertex(){
			X = X,
			Y = Y
		});
	}

	public double Area(){
		double A = Shoelace();
		long B = Perimiter();

		double finalArea = A + B/2 + 1;

		return finalArea;
	}

	public long Perimiter(){
		long permi = 0;

		for (int i = 0; i < Vertices.Count; i++)
		{
			Vertex v1 = Vertices[i];
			Vertex v2;
			if (i + 1 < Vertices.Count){
				v2 = Vertices[i + 1];
			}
			else{
				v2 = Vertices[0];
			}

			permi += Math.Abs((v1.X - v2.X) + (v1.Y - v2.Y));
		}

		return permi;
	}

    public void ShiftPositive()
    {
        long lowestX = Vertices.Select(x => x.X).OrderBy(x => x).First();
        long lowestY = Vertices.Select(x => x.Y).OrderBy(x => x).First();

		long shiftX = Math.Abs(lowestX);
		long shiftY = Math.Abs(lowestY);

		foreach (var vertex in Vertices)
		{
			vertex.X = vertex.X + shiftX;
			vertex.Y = vertex.Y + shiftY;
		}
    }

    public double Shoelace(){
		double AA = 0;

		for (int i = 0; i < Vertices.Count; i++)
		{
			Vertex v1 = Vertices[i];
			Vertex v2;
			if (i + 1 < Vertices.Count){
				v2 = Vertices[i + 1];
			}
			else{
				v2 = Vertices[0];
			}

			AA += (v1.X * v2.Y) - (v1.Y * v2.X);
		}

		return Math.Abs(AA/2);
	}
}