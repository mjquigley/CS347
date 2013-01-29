using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeOfLife;

namespace MazeOfLifeTests {
	[TestClass]
	public class MazeOfLifeTests {

		[TestMethod]
		public void TestIsValidPlayerPosition() {
			int[,] board1 = new int[,] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 1, 2, 1 }, { 0, 0, 0, 0 } };
			GameBoard gameBoard1 = new GameBoard(board1, new MazeOfLife.Vector2(4, 4), new Vector2(1, 1));

			int[,] board2 = new int[,] { { 0, 0, 0, 0 }, { 0, 1, 1, 1 }, { 0, 1, 2, 1 }, { 0, 1, 1, 1 } };
			GameBoard gameBoard2 = new GameBoard(board2, new MazeOfLife.Vector2(4, 4), new Vector2(1, 1));

			Assert.IsTrue(gameBoard1.isValidPlayerPosition(new Vector2(3, 2)));
			Assert.IsFalse(gameBoard2.isValidPlayerPosition(new Vector2(2, 2)));
		}

		[TestMethod]
		public void TestGetCellAt() {
			int[,] board = new int[5, 5];
			for (int i = 1; i < 5; i++) {
				for (int j = 1; j < 5; j++) {
					board[i, j] = (i + j) % 2;
				}
			}
			GameBoard gameBoard = new GameBoard(board, new MazeOfLife.Vector2(5, 5), new Vector2(1, 1));
			for (int i = 1; i < 5; i++) {
				for (int j = 1; j < 5; j++) {
					int cell = gameBoard.GetCellAt(new Vector2(i, j));
					if ((i + j) % 2 == 1) {
						Assert.IsTrue(cell == 1);
					} else {
						Assert.IsFalse(cell == 1);
					}
				}
			}
		}

		[TestMethod]
		public void TestIsGoal() {
			int[,] board = new int[5, 5];
			GameBoard gameBoard = new GameBoard(board, new MazeOfLife.Vector2(5, 5), new Vector2(1, 1));
			for (int i = 1; i < 5; i++) {
				for (int j = 1; j < 5; j++) {
					if (i == 1 && j == 1) {
						Assert.IsTrue(gameBoard.isGoal(new Vector2(i, j)));
					} else {
						Assert.IsFalse(gameBoard.isGoal(new Vector2(i, j)));
					}
				}
			}
		}
	}
}
