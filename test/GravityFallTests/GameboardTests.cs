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
    public class GameboardTests
    {

        #region ValidHolesTests

        [TestMethod()]
        public void GameboardValidHolesTest1()
        {
            // arrange

            // act

            // assert
            Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>(), new List<IGameboardObject>());
        }

        [TestMethod()]
        public void GameboardValidHolesTest2()
        {
            // arrange

            // act

            // assert
            Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 }
            }, new List<IGameboardObject>());
        }

        [TestMethod()]
        public void GameboardValidHolesTest3()
        {
            // arrange

            // act

            // assert
            Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 0, Y = 1 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 }
            }, new List<IGameboardObject>());
        }

        [TestMethod()]
        public void GameboardNotValidHolesDuplicateNumberTest()
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>()
                {
                    new GameboardObject() { Number = 1, X = 0, Y = 1 },
                    new GameboardObject() { Number = 1, X = 1, Y = 1 }
                }, new List<IGameboardObject>());
            });
        }

        [TestMethod()]
        public void GameboardNotValidHolesDuplicatePositionTest()
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>()
                {
                    new GameboardObject() { Number = 1, X = 1, Y = 1 },
                    new GameboardObject() { Number = 2, X = 1, Y = 1 }
                }, new List<IGameboardObject>());
            });
        }

        [TestMethod()]
        public void GameboardNotValidHolesOutOfBoardTest1()
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>()
                {
                    new GameboardObject() { Number = 1, X = 1, Y = 1 },
                    new GameboardObject() { Number = 2, X = 2, Y = 1 }
                }, new List<IGameboardObject>());
            });
        }

        [TestMethod()]
        public void GameboardNotValidHolesOutOfBoardTest2()
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>()
                {
                    new GameboardObject() { Number = 1, X = 1, Y = 2 },
                    new GameboardObject() { Number = 2, X = 1, Y = 1 }
                }, new List<IGameboardObject>());
            });
        }

        #endregion

        #region ValidBallsTests

        [TestMethod()]
        public void GameboardValidBallsTest1()
        {
            // arrange

            // act

            // assert
            Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>(), new List<IGameboardObject>());
        }

        [TestMethod()]
        public void GameboardValidBallsTest2()
        {
            // arrange

            // act

            // assert
            Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>(), new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 }
            });
        }

        [TestMethod()]
        public void GameboardValidBallsTest3()
        {
            // arrange

            // act

            // assert
            Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>(), new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 0, Y = 1 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 }
            });
        }

        [TestMethod()]
        public void GameboardNotValidBallsDuplicateNumberTest()
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>(), new List<IGameboardObject>()
                {
                    new GameboardObject() { Number = 1, X = 0, Y = 1 },
                    new GameboardObject() { Number = 1, X = 1, Y = 1 }
                });
            });
        }

        [TestMethod()]
        public void GameboardNotValidBallsDuplicatePositionTest()
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>(), new List<IGameboardObject>()
                {
                    new GameboardObject() { Number = 1, X = 1, Y = 1 },
                    new GameboardObject() { Number = 2, X = 1, Y = 1 }
                });
            });
        }

        [TestMethod()]
        public void GameboardNotValidBallsOutOfBoardTest1()
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>(), new List<IGameboardObject>()
                {
                    new GameboardObject() { Number = 1, X = 1, Y = 1 },
                    new GameboardObject() { Number = 2, X = 2, Y = 1 }
                });
            });
        }

        [TestMethod()]
        public void GameboardNotValidBallsOutOfBoardTest2()
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Gameboard gameboard = new Gameboard(2, 2, new List<IGameboardObject>(), new List<IGameboardObject>()
                {
                    new GameboardObject() { Number = 1, X = 1, Y = 2 },
                    new GameboardObject() { Number = 2, X = 1, Y = 1 }
                });
            });
        }

        #endregion

        [TestMethod()]
        public void SaveSnapshotTest()
        {
            // arrange
            Gameboard gameboard = new Gameboard(10, 10, new List<IGameboardObject>(), new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 2 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 }
            });

            // act
            var snapshot = gameboard.SaveSnapshot();

            // assert
            CollectionAssert.AreEqual(new List<IGameboardObject>(gameboard.Balls), new List<IGameboardObject>(snapshot.Balls));
        }

        [TestMethod()]
        public void SaveSnapshotIsImmutableTest()
        {
            // arrange
            Gameboard gameboard = new Gameboard(10, 10, new List<IGameboardObject>(), new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 2 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 }
            });

            // act
            var snapshot = gameboard.SaveSnapshot();
            gameboard.SetBallPosition(1, 5, 5);

            // assert
            CollectionAssert.AreNotEqual(new List<IGameboardObject>(gameboard.Balls), new List<IGameboardObject>(snapshot.Balls));
        }

        //[TestMethod()]
        //public void LoadTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void SetBallPositionTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void RemoveBallTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void ApplyActionTest()
        //{
        //    Assert.Fail();
        //}
    }
}