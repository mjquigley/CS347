using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeOfLife {
	class Program {
		static void Main(string[] args) {
			if (args.Length > 0) {
				GameBoard gameBoard = new GameBoard(args[0]);
				List<Vector2> path = GameBoardUtils.Search.BFTS(gameBoard);
				Console.WriteLine("Maze - Goal("+gameBoard.Goal.X+","+gameBoard.Goal.Y+"): ");
				gameBoard.print();
				Console.WriteLine("\nPath:");
				GameBoardUtils.outputPath(path, gameBoard);
				Console.WriteLine("\nPath cost: " + (path.Count - 1));
				Console.WriteLine("States: \n");
				GameBoardUtils.outputStatesFromPath(path, gameBoard);
				Console.WriteLine("\nPress any key to close");
				Console.ReadKey();	//wait for user input to exit
			}
		}
	}
}
