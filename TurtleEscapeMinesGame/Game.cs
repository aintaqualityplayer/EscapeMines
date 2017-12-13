using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleEscapeMinesGame {

    /// Game class holds everything related to this Turtle Escape Mines Game
    public class Game {
        public int BoardColumns { get; set; }
        public int BoardRows { get; set; }
        public List<Coordinate> Mines { get; set; }
        public Coordinate Exit { get; set; }
        public Turtle Turtle { get; set; }
        public List<string> Moves { get; set; }
        public string[] TurtleGameSettings { get; set; }

        private const char SPACE_SEPARATOR = ' ';
        private const char COMMA_SEPARATOR = ',';

        public Game() {
            Mines = new List<Coordinate>();
            Moves = new List<string>();
        }

        public static Game CreateGame(string[] settings) {
            var game = new Game();

            // STEP 1: Get Board settings 
            var boardSettings = settings[0].Split(SPACE_SEPARATOR);
            game.BoardColumns = Convert.ToInt16(boardSettings[0]);
            game.BoardRows = Convert.ToInt16(boardSettings[1]);

            // STEP 2: From line2 in file, get mine points
            var mineCoordinates = settings[1].Split(SPACE_SEPARATOR).ToList();
            foreach (var mine in mineCoordinates) {
                var minePoint = mine.Split(COMMA_SEPARATOR);
                game.Mines.Add(new Coordinate(Convert.ToInt16(minePoint[0]), Convert.ToInt16(minePoint[1])));
            }

            // STEP 3: Read the exitPoint from file @ Line 3
            var exitPoint = settings[2].Split(SPACE_SEPARATOR);
            game.Exit = new Coordinate(Convert.ToInt16(exitPoint[0]), Convert.ToInt16(exitPoint[1]));

            // STEP 4: Get the Starting point of the turtle
            game.TurtleGameSettings = settings[3].Split(SPACE_SEPARATOR);
            game.Turtle = new Turtle(game.TurtleGameSettings);

            // STEP 5: Read moves from line 5 to the end of file
            foreach (var move in settings.Skip(4)) {
                game.Moves.Add(move);
            }

            return game;
        }

        public void Validate() {
            if (BoardRows < 0 || BoardColumns < 0)
                throw new Exception("Invalid board settings");
            if (Turtle.TurtlePosition.X > BoardColumns || Turtle.TurtlePosition.X < 0)
                throw new Exception("Invalid Turtle starting position (X coordiante): " + Turtle.TurtlePosition.X);
            if (Turtle.TurtlePosition.Y > BoardRows || Turtle.TurtlePosition.Y < 0)
                throw new Exception("Invalid Turtle starting position (Y coordiante): " + Turtle.TurtlePosition.Y);
            if (Exit.X > BoardColumns || Exit.X < 0)
                throw new Exception("Invalid exit position (X coordiante): " + Exit.X);
            if (Exit.Y > BoardRows || Exit.Y < 0)
                throw new Exception("Invalid exit position (Y coordiante): " + Exit.Y);
        }

        public void StartGame() {
            // Validate if the game settings provided in the file are valid
            Validate();

            // Set Board and mines and exit points on the matrix board
            var board = SetBoardWithMinesAndExitPoints();

            // Start the turtle move sequences
            int sequenceNumber = 0;

            foreach (var move in Moves) {
                var sequences = move.Split(SPACE_SEPARATOR);
                // Reset the Turtle position
                Turtle = new Turtle(TurtleGameSettings);
                bool isSuccess = false;
                sequenceNumber++;
                foreach (var sequence in sequences) {
                    Turtle.Move(sequence);

                    var turtleCurrentPoint = board[Turtle.TurtlePosition.X, Turtle.TurtlePosition.Y];
                    if (turtleCurrentPoint == (int) PointType.Safe) {
                        continue;
                    }

                    if (turtleCurrentPoint == (int) PointType.Mine) {
                        Console.WriteLine($"Sequence {sequenceNumber}: Mine hit!");
                        break;
                    }

                    if (turtleCurrentPoint == (int) PointType.Exit) {
                        isSuccess = true;
                        Console.WriteLine($"Sequence {sequenceNumber}: Success!");
                        break;
                    }
                }

                //if we finished all moves and didn't win and didn't hit a mine then we are still in danger!!
                if (!isSuccess && board[Turtle.TurtlePosition.X, Turtle.TurtlePosition.Y] ==
                    (int) PointType.Safe) {
                    Console.WriteLine($"Sequence {sequenceNumber}: Still in danger!");
                }
            }
        }

        public int[,] SetBoardWithMinesAndExitPoints() {
            int[,] matrixBoard = new int[BoardColumns, BoardRows];
            matrixBoard[Exit.X, Exit.Y] = (int) PointType.Exit;
            foreach (var mine in Mines) {
                try {
                    matrixBoard[mine.X, mine.Y] = (int) PointType.Mine;
                } catch (Exception e) {
                    Console.WriteLine($"Invalid x {mine.X} and y {mine.Y} coordinates to create a mine", e);
                }
            }
            return matrixBoard;
        }
    }

    public enum PointType {
        Exit = 9,
        Mine = 8,
        Safe = 0
    }
}
