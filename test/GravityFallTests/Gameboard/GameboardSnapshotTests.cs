using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aura.GravityFall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall.Tests
{
    [TestClass()]
    public class GameboardSnapshotTests
    {
        [TestMethod()]
        public void ValueEqualsTest1()
        {
            // arrange
            List<GameboardObject> balls = new()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 },
                new GameboardObject() { Number = 2, X = 2, Y = 2 },
                new GameboardObject() { Number = 3, X = 3, Y = 3 },
            };

            // act
            GameboardSnapshot snapshot1 = new(balls);
            GameboardSnapshot snapshot2 = new(balls);

            // assert
            Assert.IsTrue(snapshot1.ValueEquals(snapshot2));
        }

        [TestMethod()]
        public void ValueEqualsTest2()
        {
            // arrange
            List<GameboardObject> balls = new()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 },
                new GameboardObject() { Number = 2, X = 2, Y = 2 },
                new GameboardObject() { Number = 3, X = 3, Y = 3 },
            };

            // act
            GameboardSnapshot snapshot1 = new(balls);
            balls[0].X = 10;
            GameboardSnapshot snapshot2 = new(balls);

            // assert
            Assert.IsFalse(snapshot1.ValueEquals(snapshot2));
        }

        [TestMethod()]
        public void ValueEqualsTest3()
        {
            // arrange
            List<GameboardObject> balls = new()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 },
                new GameboardObject() { Number = 2, X = 2, Y = 2 },
                new GameboardObject() { Number = 3, X = 3, Y = 3 },
            };

            // act
            GameboardSnapshot snapshot1 = new(balls);
            balls.RemoveAt(0);
            GameboardSnapshot snapshot2 = new(balls);

            // assert
            Assert.IsFalse(snapshot1.ValueEquals(snapshot2));
        }

        [TestMethod()]
        public void ValueEqualsTest4()
        {
            // arrange
            List<GameboardObject> balls = new()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 },
                new GameboardObject() { Number = 2, X = 2, Y = 2 },
                new GameboardObject() { Number = 3, X = 3, Y = 3 },
            };

            // act
            GameboardSnapshot snapshot1 = new(balls);
            GameboardSnapshot snapshot2 = null;

            // assert
            Assert.ThrowsException<ArgumentNullException>(() => snapshot1.ValueEquals(snapshot2));
        }
    }
}