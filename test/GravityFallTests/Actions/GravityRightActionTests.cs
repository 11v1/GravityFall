using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aura.GravityFall.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall.Actions.Tests
{
    [TestClass()]
    public class GravityRightActionTests
    {

        private static List<IGameboardObject> TestBalls => new(_testBalls.Select(p => (IGameboardObject)p.Clone()));
        private static readonly List<IGameboardObject> _testBalls = new()
        {
            new GameboardObject() { Number = 1, X = 0, Y = 0 },
            new GameboardObject() { Number = 2, X = 1, Y = 1 },
            new GameboardObject() { Number = 3, X = 2, Y = 2 },
            new GameboardObject() { Number = 4, X = 3, Y = 3 },
            new GameboardObject() { Number = 5, X = 4, Y = 4 },
            new GameboardObject() { Number = 6, X = 5, Y = 5 },
            new GameboardObject() { Number = 7, X = 6, Y = 6 },
            new GameboardObject() { Number = 8, X = 7, Y = 7 },
            new GameboardObject() { Number = 9, X = 8, Y = 8 },
            new GameboardObject() { Number = 10, X = 9, Y = 9 },
            new GameboardObject() { Number = 11, X = 0, Y = 3 },
            new GameboardObject() { Number = 12, X = 1, Y = 4 },
            new GameboardObject() { Number = 13, X = 2, Y = 5 },
            new GameboardObject() { Number = 14, X = 3, Y = 6 },
            new GameboardObject() { Number = 15, X = 4, Y = 7 },
            new GameboardObject() { Number = 16, X = 5, Y = 8 },
            new GameboardObject() { Number = 17, X = 6, Y = 9 },
            new GameboardObject() { Number = 18, X = 3, Y = 0 },
            new GameboardObject() { Number = 19, X = 4, Y = 1 },
            new GameboardObject() { Number = 20, X = 5, Y = 2 },
            new GameboardObject() { Number = 21, X = 6, Y = 3 },
            new GameboardObject() { Number = 22, X = 7, Y = 4 },
            new GameboardObject() { Number = 23, X = 8, Y = 5 },
            new GameboardObject() { Number = 24, X = 9, Y = 6 },
            new GameboardObject() { Number = 25, X = 9, Y = 5 },
            new GameboardObject() { Number = 26, X = 1, Y = 3 },
            new GameboardObject() { Number = 27, X = 5, Y = 1 },
        };

        private static List<IGameboardObject> TestHoles => new(_testHoles.Select(p => (IGameboardObject)p.Clone()));
        private static readonly List<IGameboardObject> _testHoles = new()
        {
            new GameboardObject() { Number = 1, X = 0, Y = 1 },
            new GameboardObject() { Number = 2, X = 1, Y = 2 },
            new GameboardObject() { Number = 3, X = 2, Y = 3 },
            new GameboardObject() { Number = 4, X = 3, Y = 4 },
            new GameboardObject() { Number = 5, X = 4, Y = 5 },
            new GameboardObject() { Number = 6, X = 5, Y = 6 },
            new GameboardObject() { Number = 7, X = 6, Y = 7 },
            new GameboardObject() { Number = 8, X = 7, Y = 8 },
            new GameboardObject() { Number = 9, X = 8, Y = 9 },
            new GameboardObject() { Number = 10, X = 0, Y = 5 },
            new GameboardObject() { Number = 11, X = 1, Y = 6 },
            new GameboardObject() { Number = 12, X = 1, Y = 7 },
            new GameboardObject() { Number = 13, X = 0, Y = 7 },
            new GameboardObject() { Number = 14, X = 1, Y = 8 },
            new GameboardObject() { Number = 15, X = 2, Y = 9 },
            new GameboardObject() { Number = 16, X = 0, Y = 8 },
            new GameboardObject() { Number = 17, X = 1, Y = 9 },
            new GameboardObject() { Number = 18, X = 0, Y = 9 },
            new GameboardObject() { Number = 19, X = 6, Y = 0 },
            new GameboardObject() { Number = 20, X = 7, Y = 0 },
            new GameboardObject() { Number = 21, X = 8, Y = 1 },
            new GameboardObject() { Number = 22, X = 9, Y = 2 },
            new GameboardObject() { Number = 23, X = 8, Y = 0 },
            new GameboardObject() { Number = 24, X = 9, Y = 0 },
            new GameboardObject() { Number = 25, X = 9, Y = 1 },
            new GameboardObject() { Number = 26, X = 7, Y = 1 },
            new GameboardObject() { Number = 27, X = 8, Y = 2 },
        };

        private static void AssertBall(IGameboard gameboard, int number, int x, int y)
        {
            var ball = gameboard.Balls.FirstOrDefault(p => p.Number == number);
            Assert.IsNotNull(ball);
            Assert.AreEqual(x, ball.X);
            Assert.AreEqual(y, ball.Y);
        }

        [TestMethod()]
        public void ApplyActionTest()
        {
            // arrange
            Gameboard gameboard = new(10, 10, TestHoles, TestBalls);

            // act
            var result = gameboard.ApplyAction(new GravityRightAction());

            // assert
            // verifying balls that left
            Assert.AreEqual(12, gameboard.Balls.Count);
            AssertBall(gameboard, 21, 9, 3);
            AssertBall(gameboard, 4, 8, 3);
            AssertBall(gameboard, 22, 9, 4);
            AssertBall(gameboard, 5, 8, 4);
            AssertBall(gameboard, 25, 9, 5);
            AssertBall(gameboard, 23, 8, 5);
            AssertBall(gameboard, 6, 7, 5);
            AssertBall(gameboard, 24, 9, 6);
            AssertBall(gameboard, 7, 8, 6);
            AssertBall(gameboard, 8, 9, 7);
            AssertBall(gameboard, 9, 9, 8);
            AssertBall(gameboard, 10, 9, 9);

            // verifying balls that have fallen
            Assert.AreEqual(15, result.Count());
            Assert.IsNotNull(result.First(p => p.HoleNumber == 19 && p.BallNumber == 1));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 19 && p.BallNumber == 18));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 26 && p.BallNumber == 2));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 26 && p.BallNumber == 19));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 26 && p.BallNumber == 27));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 27 && p.BallNumber == 3));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 27 && p.BallNumber == 20));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 3 && p.BallNumber == 11));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 3 && p.BallNumber == 26));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 4 && p.BallNumber == 12));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 5 && p.BallNumber == 13));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 6 && p.BallNumber == 14));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 7 && p.BallNumber == 15));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 8 && p.BallNumber == 16));
            Assert.IsNotNull(result.First(p => p.HoleNumber == 9 && p.BallNumber == 17));
        }
    }
}