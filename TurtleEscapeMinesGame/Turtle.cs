using System;

namespace TurtleEscapeMinesGame {
    public class Turtle {
        public Coordinate TurtlePosition { get; set; }
        public string FacingDirection { get; set; }

        public Turtle() {

        }

        public Turtle(Coordinate turtlePosition, string facingDirection) {
            TurtlePosition = turtlePosition;
            FacingDirection = facingDirection;
        }

        public Turtle(string[] gameSettings) {
            TurtlePosition = new Coordinate(Convert.ToInt16(gameSettings[0]), Convert.ToInt16(gameSettings[1]));
            FacingDirection = gameSettings[2];
        }

        public void RotateRight() {
            switch (FacingDirection) {
                case "N":
                    FacingDirection = "E";
                    break;
                case "E":
                    FacingDirection = "S";
                    break;
                case "S":
                    FacingDirection = "W";
                    break;
                case "W":
                    FacingDirection = "N";
                    break;
                default:
                    throw new Exception("Direction not recognised!");
            }
        }

        public void RotateLeft()
        {
            switch (FacingDirection)
            {
                case "N":
                    FacingDirection = "W";
                    break;
                case "E":
                    FacingDirection = "N";
                    break;
                case "S":
                    FacingDirection = "E";
                    break;
                case "W":
                    FacingDirection = "S";
                    break;
                default:
                    throw new Exception("Direction not recognised!");
            }
        }

        public void Move(string move) {
            if (move.Equals("R")) {
                RotateRight();
            } else if (move.Equals("L")) {
                RotateLeft();
            } else if (move.Equals("M")) {
                Move();
            }
        }

        public void Move() {
            switch (FacingDirection) {
                case "N":
                    TurtlePosition.Y -= 1;
                    break;
                case "E":
                    TurtlePosition.X += 1;
                    break;
                case "S":
                    TurtlePosition.Y += 1;
                    break;
                case "W":
                    TurtlePosition.X -= 1;
                    break;
                default:
                    throw new Exception("Invalid Move Exception. Turtle could not move");
            }
        }
    }


}
