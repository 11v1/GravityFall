using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aura.GravityFall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Aura.GravityFall.Actions;

namespace Aura.GravityFall.Tests
{
    [TestClass()]
    public class GameboardTests
    {

        private class GameObjectComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                IGameboardObject xx = (IGameboardObject)x;
                IGameboardObject yy = (IGameboardObject)y;
                if (xx.Number == yy.Number && xx.X == yy.X && xx.Y == yy.Y)
                    return 0;
                // in other case we should determine which is predecessor but it's doesn't matter in our case
                return -1;
            }
        }


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

        #region SnapshotTests

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
            Assert.AreEqual(gameboard.Balls.Count, snapshot.Balls.Count);
            CollectionAssert.AreEqual(new List<IGameboardObject>(gameboard.Balls), new List<IGameboardObject>(snapshot.Balls), new GameObjectComparer());
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
            gameboard.Balls.First().X = 5;

            // assert
            CollectionAssert.AreNotEqual(new List<IGameboardObject>(gameboard.Balls), new List<IGameboardObject>(snapshot.Balls), new GameObjectComparer());
        }

        [TestMethod()]
        public void LoadSnapshotTest()
        {
            // arrange
            Gameboard gameboard = new Gameboard(10, 10, new List<IGameboardObject>(), new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 2 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 }
            });

            // act
            var snapshot = gameboard.SaveSnapshot();
            gameboard.Balls.First().X = 5;
            gameboard.LoadShapshot(snapshot);

            // assert
            CollectionAssert.AreEqual(new List<IGameboardObject>(gameboard.Balls), new List<IGameboardObject>(snapshot.Balls), new GameObjectComparer());
        }

        #endregion

        #region RemoveBallTests

        [TestMethod()]
        public void RemoveBallTest()
        {
            // arrange
            Gameboard gameboard = new Gameboard(10, 10, new List<IGameboardObject>(), new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 2 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 }
            });

            // act
            gameboard.RemoveBall(1);

            // assert
            Assert.AreEqual(1, gameboard.Balls.Count);
        }

        [TestMethod()]
        public void RemoveBallUnknownIdTest()
        {
            // arrange
            Gameboard gameboard = new Gameboard(10, 10, new List<IGameboardObject>(), new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 2 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 }
            });

            // act

            // assert
            Assert.ThrowsException<ArgumentException>(() => gameboard.RemoveBall(3));
        }

        #endregion

        #region ApplyActionTests

        private class TestModifyPointsAction : IAction
        {
            public IEnumerable<(int HoleNumber, int BallNumber)> ApplyAction(IGameboard gameboard)
            {
                var ball = gameboard.Balls.First(p => p.Number == 1);
                ball.X = 5;

                ball = gameboard.Balls.First(p => p.Number == 2);
                ball.Y = 7;

                return null;
            }
        }


        [TestMethod()]
        public void ApplyActionModifyPointsTest()
        {
            // arrange
            Gameboard gameboard = new Gameboard(10, 10, new List<IGameboardObject>(), new List<IGameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 2 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 }
            });

            // act
            var result = gameboard.ApplyAction(new TestModifyPointsAction());

            // assert
            Assert.IsNull(result);
            Assert.AreEqual(5, gameboard.Balls.First(p => p.Number == 1).X);
            Assert.AreEqual(7, gameboard.Balls.First(p => p.Number == 2).Y);
        }

        #endregion

        //[TestMethod()]
        //public void SetBallPositionTest()
        //{
        //    Assert.Fail();
        //}




    }
}