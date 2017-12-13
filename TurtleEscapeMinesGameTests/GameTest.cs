using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TurtleEscapeMinesGame;

namespace TurtleEscapeMinesGameTests {
    [TestFixture]
    public class GameTest {
        private Game game;

        [SetUp]
        public void SetUp() {
            game = new Game();
            game.BoardColumns = 5;
            game.BoardRows = 4;
            game.Exit = new Coordinate(4, 2);
            game.Mines.AddRange(new List<Coordinate> {
                                                         new Coordinate(1, 1),
                                                         new Coordinate(1, 3),
                                                         new Coordinate(3, 3)
                                                     });
            game.TurtleGameSettings = new[] {"0", "1", "N"};
            game.Turtle = new Turtle(game.TurtleGameSettings);
            game.Moves.AddRange(new List<string> {"M R M M M M R M M", "R M M M"});
        }

        [TestCase]
        public void TestStartGame() {
            using (StringWriter sw = new StringWriter()) {
                Console.SetOut(sw);
                game.StartGame();
                string expectedConsoleOutput =
                    $"Sequence 1: Success!{Environment.NewLine}Sequence 2: Mine hit!{Environment.NewLine}";
                Assert.AreEqual(expectedConsoleOutput, sw.ToString());
            }
        }

        [TestCase]
        public void TestIfTurtleHitsMine() {
            game.Moves = new List<string> {"R M"};
            using (StringWriter sw = new StringWriter()) {
                Console.SetOut(sw);
                game.StartGame();
                string expectedConsoleOutput =
                    $"Sequence 1: Mine hit!{Environment.NewLine}";
                Assert.AreEqual(expectedConsoleOutput, sw.ToString());
            }
        }
    }
}
