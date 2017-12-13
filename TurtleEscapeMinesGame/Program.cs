using System;
using System.IO;
using System.Linq;

namespace TurtleEscapeMinesGame {
    public class Program {

        public static void Main(string[] args) {
            string path = string.Empty;
            if (!args.Any()){
                Console.WriteLine("Please enter file path");
                
                path = Console.ReadLine();
            } else {
                path = args[0];
            }            
            // STEP 1: Read the file 
            if (!File.Exists(path)) {
                throw new Exception("Invalid file path");
            }
            var gameSettings = File.ReadAllLines(path);

            // STEP 2: Create the game with the settings provided in the file
            var game = Game.CreateGame(gameSettings);
            // STEP 3: Start the game
            game.StartGame();
            Console.ReadKey();
        }
    }
}
