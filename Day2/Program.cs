using System;
using System.Runtime.CompilerServices;

namespace Day2{
	class Set{
		public int Red {get; set;}
		public int Green {get; set;}
		public int Blue {get; set;}

		public void ParseLine(string set){
			var cubes = set.Split(",");

			foreach (var cube in cubes){
				if (cube.Contains(" red")){
					Red = int.Parse(cube.Replace(" red", ""));
				}
				else if (cube.Contains(" green")){
					Green = int.Parse(cube.Replace(" green", ""));
				}
				else if (cube.Contains(" blue")){
					Blue = int.Parse(cube.Replace(" blue", ""));
				}
			}
		}

		public bool IsPossible(int r, int g, int b){
			return Red <= r && Green <= g && Blue <= b;
		}
	}

	class Game{
		List<Set> Sets = new List<Set>();

		//Min needed
		private int MinRed {get => Sets.Select(x => x.Red).Max();}
		private int MinGreen {get => Sets.Select(x => x.Green).Max();}
		private int MinBlue {get => Sets.Select(x => x.Blue).Max();}

		public void ParseLine(string line){
			//Trim the game part
			line = line.Split(':')[1];

			var sets = line.Split(";");

			foreach (var setStr in sets){
				Set newSet = new Set();

				newSet.ParseLine(setStr);

				Sets.Add(newSet);
			}
		}

		public bool IsPossible(int r, int g, int b){
			foreach (var set in Sets){
				if (!set.IsPossible(r, g, b)){
					return false;
				}
			}
			return true;
		}

		public int Power(){
			return MinRed * MinGreen * MinBlue;
		}
	}

	class Program
	{
		static List<Game> Games = new List<Game>();

		static void Main(string[] args)
		{
			var file = File.ReadLines("./input.txt");

			foreach (var line in file){
				Game newGame = new Game();

				newGame.ParseLine(line);

				Games.Add(newGame);
			}

			int sum = 0;
			int sumOfPower = 0;
			for	(int i = 0; i < Games.Count; i++){
				if (Games[i].IsPossible(12,13,14)){
					System.Console.WriteLine(i + 1);
					sum += (i + 1);
				}

				sumOfPower += Games[i].Power();
			}

			Console.WriteLine($"Sum of possible {sum}");
			Console.WriteLine($"Sum of power {sumOfPower}");
		}
	}
}