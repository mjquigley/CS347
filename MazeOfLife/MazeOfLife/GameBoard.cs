using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeOfLife {
	public class GameBoard {
		private int[,] board;
		public int[,] Board {
			get {
				return board;
			}
		}

		private Vector2 dimensions;
		public Vector2 Dimensions {
			get {
				return dimensions;
			}
		}

		private Vector2 goal;
		public Vector2 Goal {
			get {
				return goal;
			}
		}

		public Vector2 playerPosition {
			get {
				for (int i = 1; i < dimensions.X; i++) {
					for (int j = 1; j < dimensions.Y; j++) {
						if (board[i, j] == 2) {
							return new Vector2(i, j);
						}
					}
				}
				throw new PlayerNotFoundException();
			}
			set {
				if (isValidPlayerPosition(value)) {
					Vector2 currentPlayerPosition = playerPosition;
					board[value.X, value.Y] = 2;
					board[currentPlayerPosition.X, currentPlayerPosition.Y] = 0;
				}
			}
		}

		public GameBoard(int[,] board, Vector2 size, Vector2 goal) {
			this.board = board;
			this.dimensions = size;
			this.goal = goal;
		}
		public GameBoard(string filename) {
			try {
				loadBoard(filename);
			} catch (Exception e) {
				if (e is InvalidLevelObjectException || e is System.IO.FileNotFoundException) {
					Console.WriteLine(e.Message);
				} else {
					throw;
				}
			}
		}

		private void loadBoard(string filename) {
			string[] lines = System.IO.File.ReadAllLines(filename);
			int currentLine = 1;

			foreach (string line in lines) {
				if (currentLine == 1 || currentLine == 2) {
					string[] parts = line.Split(' ');
					if (currentLine == 1 && parts.Length == 2) {
						dimensions = new Vector2(Convert.ToInt32(parts[0]) + 1, Convert.ToInt32(parts[1]) + 1);
						board = new int[dimensions.X, dimensions.Y];
					} else if (currentLine == 2 && parts.Length == 2) {
						goal = new Vector2(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
					}
				} else {
					for (int i = 0; i < line.Length; i++) {
						if (line[i] == '0' || line[i] == '1' || line[i] == '2') {
							board[i + 1, currentLine - 2] = (int)char.GetNumericValue(line[i]);
						} else {
							if (line[i] != '\n') {
								throw new InvalidLevelObjectException("'" + line[i] + "' is not a valid level object!");
							}
						}
					}
				}
				currentLine++;
			}
		}

		public int GetCellAt(Vector2 position) {
			if (position.X < dimensions.X && position.X > 0 && position.Y < dimensions.Y && position.Y > 0) {
				return board[position.X, position.Y];
			} else {
				return -1;
			}
		}

		public Vector2 Left(Vector2 position) {
			return (new Vector2(position.X - 1, position.Y));
		}
		public Vector2 Right(Vector2 position) {
			return (new Vector2(position.X + 1, position.Y));
		}
		public Vector2 Up(Vector2 position) {
			return (new Vector2(position.X, position.Y - 1));
		}
		public Vector2 Down(Vector2 position) {
			return (new Vector2(position.X, position.Y + 1));
		}
		public Vector2 TopLeft(Vector2 position) {
			return (new Vector2(position.X - 1, position.Y - 1));
		}
		public Vector2 BottomLeft(Vector2 position) {
			return (new Vector2(position.X - 1, position.Y + 1));
		}
		public Vector2 TopRight(Vector2 position) {
			return (new Vector2(position.X + 1, position.Y - 1));
		}
		public Vector2 BottomRight(Vector2 position) {
			return (new Vector2(position.X + 1, position.Y + 1));
		}

		public List<Vector2> getAllDirections(Vector2 position) {
			List<Vector2> directions = new List<Vector2>();

			directions.Add(Up(position));
			directions.Add(Down(position));
			directions.Add(Left(position));
			directions.Add(Right(position));
			directions.Add(TopLeft(position));
			directions.Add(BottomLeft(position));
			directions.Add(TopRight(position));
			directions.Add(BottomRight(position));

			return directions;
		}

		private int getAdjacentCells(Vector2 position) {
			int cells = 0;
			List<Vector2> directions = getAllDirections(position);

			foreach (Vector2 d in directions) {
				if (GetCellAt(d) == 1) {
					cells++;
				}
			}
			return cells;
		}

		public bool isValidPlayerPosition(Vector2 position) {
			int adjacentCells = getAdjacentCells(position);
			return (adjacentCells == 2 || adjacentCells == 3);
		}

		public bool isGoal(Vector2 position) {
			return position.Equals(goal);
		}

		public void print() {
			printGrid(board, dimensions);
		}

		public void printGrid(int[,] grid, Vector2 size) {
			for (int i = 1; i < size.Y; i++) {
				for (int j = 1; j < size.X; j++) {
					Console.Write(board[j, i]);
				}
				Console.Write('\n');
			}
		}
	}
}
