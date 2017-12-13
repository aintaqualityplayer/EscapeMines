using NUnit.Framework;
using TurtleEscapeMinesGame;

namespace TurtleEscapeMinesGameTests {
    [TestFixture]
    public class TurtleTest {
        private Turtle turtle;

        [SetUp]
        public void Setup() {
            string[] settings = {"0", "1", "N"};
            turtle = new Turtle(settings);
        }

        [TestCase]
        public void TestTurtleRotation() {
            turtle.RotateRight();
            turtle.RotateRight();
            Assert.AreEqual(Direction.South, turtle.FacingDirection);
        }


        [TestCase(Direction.North, 0, 0)]
        public void TestTurtleMove(Direction heading, int expectedX, int expectedY) {
            turtle.Move();
            var expectedPoint = new Coordinate(expectedX, expectedY);
            Assert.AreEqual(expectedPoint.X, turtle.TurtlePosition.X);
            Assert.AreEqual(expectedPoint.Y, turtle.TurtlePosition.Y);
        }

        [TestCase(Direction.North, 1, 0)]
        public void TestTurtleMoveAndRotation(Direction heading, int expectedX, int expectedY)
        {
            turtle.RotateRight();
            turtle.Move();
            turtle.RotateLeft();
            turtle.Move();

            var expectedPoint = new Coordinate(expectedX, expectedY);
            Assert.AreEqual(expectedPoint.X, turtle.TurtlePosition.X);
            Assert.AreEqual(expectedPoint.Y, turtle.TurtlePosition.Y);
        }

    }
}
