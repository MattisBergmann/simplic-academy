using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CodingChallenge
{
	public class RecursionChallenge
	{
		private int FibonacciRecursion(int position)
		{
			if (position <= 0)
				return -1;
				if (position == 1)
					return 0;

			else
				return FibonacciRecursion(position + 2) + FibonacciRecursion(position + 1);
		}

		[Fact]
		public void Recursion_01()
		{
			// Rule: Apply only changes to the method above that is called FibonacciRecursion

			Assert.Equal(8, FibonacciRecursion(5));

			// Your explanation: 
			// 
			// 
		}

		public double Factorial(int number)
		{
			return number;
		}

		[Fact]
		public void Recursion_02()
		{
			// Rule: Apply only changes to the method above that is called Factorial

			Assert.Equal(1, Factorial(0));
			Assert.Equal(1, Factorial(1));
			Assert.Equal(2, Factorial(2));
			Assert.Equal(6, Factorial(3));
			Assert.Equal(24, Factorial(4));
			Assert.Equal(120, Factorial(5));

			// Your explanation: 
			// 
			// 
		}

		private int FindWay(char[,] maze, int row, int col, int steps)
		{
			
			maze[row, col] = char.Parse("X");
			steps++;
			
			if (maze.GetLength(0) == row + 1 || maze.GetLength(1) == col + 1)
				return steps;

			if (maze[(row - 1), col] == '.')
				return FindWay(maze, row - 1, col, steps);

			if (maze[row, (col + 1)] == '.')
			{
				col = (col + 1);
				return FindWay(maze, row, col = (col + 1), steps);
			}

			if (maze[(row + 1), col] == '.')
			{
				row = (row + 1);
				FindWay(maze, row + 1, col, steps);
			}

			if (maze[row, (col - 1)] == '.')
			{
				col = (col - 1);
				FindWay(maze, row, col - 1, steps);
			}

			maze[row, col] = 'B';
			return steps -= 1;

		}

		

		[Fact]
		public void Recursion_03()
		{
			// ** Difficult Exercise **
			// There is this maze given. S marks your start point, and G your Goal. 
			// Write a recursive method that goes through the labrinth and find the right way
			//You should start at (1,1)
			char[,] array2D = new char[,] {
				 { '#','#','#','#','#' },
				 { '#','S','#','#','#' },
				 { '#','.','#','#','#' },
				 { '#','.','#','#','#' },
				 { '#','.','.','.','G' },
				 { '#','#','#','#','#' }
				  };

			

			int steps = FindWay(array2D, 1, 1, 0 ); ;

			//Save the shortest way for 
			var movement = new List<string>();

			Assert.Equal(7, steps);

			// Rule: Apply only changes to the method above that is called Factorial
			


			// Your explanation: 
			// 
			// 
		}









	}
}
