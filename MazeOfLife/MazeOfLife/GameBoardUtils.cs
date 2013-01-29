using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeOfLife {
	public struct Vector2 {
		public int X;
		public int Y;

		public Vector2(int x, int y) {
			this.X = x;
			this.Y = y;
		}

		public bool Equals(Vector2 other) {
			return (this.X == other.X && this.Y == other.Y);
		}

		public override string ToString() {
			return "<" + X + ", " + Y + ">";
		}
	}

	public class GameBoardUtils {
		public static class Search {
			public static List<Vector2> BFTS(GameBoard gameBoard) {
				Vector2 playerPosition;
				bool[,] visited = new bool[gameBoard.Dimensions.X, gameBoard.Dimensions.Y];
				Vector2[,] previousPosition = new Vector2[gameBoard.Dimensions.X, gameBoard.Dimensions.Y];
				try {
					playerPosition = gameBoard.playerPosition;
				} catch (PlayerNotFoundException) {
					Console.WriteLine("Invalid game board, player not found!");
					return null;
				}
				if (gameBoard.isGoal(playerPosition)) {
					List<Vector2> pathToGoal = new List<Vector2>();
					pathToGoal.Add(playerPosition);
					return pathToGoal;
				}
				Queue<Vector2> moveQueue = new Queue<Vector2>();
				moveQueue.Enqueue(playerPosition);
				visited[playerPosition.X, playerPosition.Y] = true;

				while (moveQueue.Count > 0) {
					Vector2 move = moveQueue.Dequeue();
					foreach (Vector2 newPosition in gameBoard.getAllDirections(move)) {
						if (gameBoard.isValidPlayerPosition(newPosition) && !visited[newPosition.X, newPosition.Y]) {
							visited[newPosition.X, newPosition.Y] = true;
							previousPosition[newPosition.X, newPosition.Y] = move;

							if (gameBoard.isGoal(newPosition)) {
								//when goal is found retrace path from goal to start
								List<Vector2> pathToGoal = new List<Vector2>();
								Vector2 lastPosition = newPosition;
								while (lastPosition.Equals(playerPosition) == false) {
									pathToGoal.Add(lastPosition);
									lastPosition = previousPosition[lastPosition.X, lastPosition.Y];
								}
								pathToGoal.Add(lastPosition);
								pathToGoal.Reverse();
								return pathToGoal;
							} else {
								moveQueue.Enqueue(newPosition);
							}
						}
					}
				}
				Console.WriteLine("No solution could be found!");
				return null;
			}
		}

		public static void outputPath(List<Vector2> path, GameBoard gameBoard) {
			foreach (Vector2 move in path) {
				Console.WriteLine(move.ToString());
			}
		}

		public static void outputStatesFromPath(List<Vector2> path, GameBoard gameBoard) {
			GameBoard board = gameBoard;
			foreach (Vector2 move in path) {
				board = new GameBoard(board.Board, board.Dimensions, board.Goal);
				board.playerPosition = move;
				gameBoard.printGrid(board.Board, gameBoard.Dimensions);
				Console.WriteLine();
			}
		}
	}

	[Serializable]
	class PlayerNotFoundException : Exception {
		public PlayerNotFoundException() {
		}
		public PlayerNotFoundException(string msg)
			: base(msg) {
		}
	}
	[Serializable]
	class InvalidLevelObjectException : Exception {
		public InvalidLevelObjectException() {
		}
		public InvalidLevelObjectException(string msg)
			: base(msg) {
		}
	}
}
