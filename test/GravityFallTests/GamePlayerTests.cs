using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aura.GravityFall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aura.GravityFall.Actions;
using Ninject;
using Ninject.Extensions.Factory;

namespace Aura.GravityFall.Tests
{
    [TestClass()]
    public class GamePlayerTests
    {
        [TestMethod()]
        public void GameNoHolesTest()
        {
            // arrange
            Gameboard gameboard = new(null, 10, 10, new List<GameboardObject>()
            {

            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 }
            });

            // act
            GamePlayer gamePlayer = new(gameboard);

            // assert
            Assert.ThrowsException<InvalidOperationException>(() => gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            }));
        }

        [TestMethod()]
        public void GameNoBallsTest()
        {
            // arrange
            Gameboard gameboard = new(null, 10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 }
            }, new List<GameboardObject>()
            {

            });

            // act
            GamePlayer gamePlayer = new(gameboard);

            // assert
            Assert.ThrowsException<InvalidOperationException>(() => gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            }));
        }

        [TestMethod()]
        public void GameBall1ToHole1Test()
        {
            // arrange
            IKernel kernel = new StandardKernel();
            kernel.Bind<IGameboardFactory>().ToFactory();
            kernel.Bind<IGameboard>().To<Gameboard>();
            kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();


            Gameboard gameboard = (Gameboard)kernel.Get<IGameboardFactory>().CreateGameboard(10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 }
            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 2 }
            });

            // act
            GamePlayer gamePlayer = new(gameboard);
            var solution = gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            });

            // assert
            Assert.IsNotNull(solution);
            Assert.AreEqual(1, solution.Count);
            Assert.AreEqual(Resources.GravityTop, solution[0].Name);
        }

        [TestMethod()]
        public void GameHoleNotAccessibleTest()
        {
            // arrange
            IKernel kernel = new StandardKernel();
            kernel.Bind<IGameboardFactory>().ToFactory();
            kernel.Bind<IGameboard>().To<Gameboard>();
            kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();


            Gameboard gameboard = (Gameboard)kernel.Get<IGameboardFactory>().CreateGameboard(10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 }
            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 2, Y = 2 }
            });

            // act
            GamePlayer gamePlayer = new(gameboard);
            var solution = gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            });

            // assert
            Assert.IsNull(solution);
        }

        [TestMethod()]
        public void GameBalls2ToHoles2Steps1Test1()
        {
            // arrange
            IKernel kernel = new StandardKernel();
            kernel.Bind<IGameboardFactory>().ToFactory();
            kernel.Bind<IGameboard>().To<Gameboard>();
            kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();

            Gameboard gameboard = (Gameboard)kernel.Get<IGameboardFactory>().CreateGameboard(10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 },
                new GameboardObject() { Number = 2, X = 2, Y = 2 },
            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 2, Y = 1 },
                new GameboardObject() { Number = 2, X = 3, Y = 2 },
            });

            // act
            GamePlayer gamePlayer = new(gameboard);
            var solution = gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            });

            // assert
            Assert.IsNotNull(solution);
            Assert.AreEqual(1, solution.Count);
            Assert.AreEqual(Resources.GravityLeft, solution[0].Name);
        }

        [TestMethod()]
        public void GameBalls2ToHoles2Steps1Test2()
        {
            // arrange
            IKernel kernel = new StandardKernel();
            kernel.Bind<IGameboardFactory>().ToFactory();
            kernel.Bind<IGameboard>().To<Gameboard>();
            kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();

            Gameboard gameboard = (Gameboard)kernel.Get<IGameboardFactory>().CreateGameboard(10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 8, Y = 0 },
                new GameboardObject() { Number = 2, X = 9, Y = 0 },
            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 8, Y = 9 },
                new GameboardObject() { Number = 2, X = 9, Y = 9 },
            });

            // act
            GamePlayer gamePlayer = new(gameboard);
            var solution = gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            });

            // assert
            Assert.IsNotNull(solution);
            Assert.AreEqual(1, solution.Count);
            Assert.AreEqual(Resources.GravityTop, solution[0].Name);
        }

        [TestMethod()]
        public void GameBalls2ToHoles2Steps2Test1()
        {
            // arrange
            IKernel kernel = new StandardKernel();
            kernel.Bind<IGameboardFactory>().ToFactory();
            kernel.Bind<IGameboard>().To<Gameboard>();
            kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();

            Gameboard gameboard = (Gameboard)kernel.Get<IGameboardFactory>().CreateGameboard(10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 8, Y = 9 },
                new GameboardObject() { Number = 2, X = 9, Y = 9 },
            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 },
                new GameboardObject() { Number = 2, X = 2, Y = 1 },
            });

            // act
            GamePlayer gamePlayer = new(gameboard);
            var solution = gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            });

            // assert
            Assert.IsNotNull(solution);
            Assert.AreEqual(2, solution.Count);
            Assert.AreEqual(Resources.GravityRight, solution[0].Name);
            Assert.AreEqual(Resources.GravityBottom, solution[1].Name);
        }

        [TestMethod()]
        public void GameBalls2ToHoles2Steps2Test2()
        {
            // arrange
            IKernel kernel = new StandardKernel();
            kernel.Bind<IGameboardFactory>().ToFactory();
            kernel.Bind<IGameboard>().To<Gameboard>();
            kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();

            Gameboard gameboard = (Gameboard)kernel.Get<IGameboardFactory>().CreateGameboard(10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 0, Y = 1 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 },
            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 8, Y = 9 },
                new GameboardObject() { Number = 2, X = 9, Y = 9 },
            });

            // act
            GamePlayer gamePlayer = new(gameboard);
            var solution = gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            });

            // assert
            Assert.IsNotNull(solution);
            Assert.AreEqual(2, solution.Count);
            Assert.AreEqual(Resources.GravityLeft, solution[0].Name);
            Assert.AreEqual(Resources.GravityTop, solution[1].Name);
        }

        [TestMethod()]
        public void GameBalls2ToHoles2NotAccessibleTest1()
        {
            // arrange
            IKernel kernel = new StandardKernel();
            kernel.Bind<IGameboardFactory>().ToFactory();
            kernel.Bind<IGameboard>().To<Gameboard>();
            kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();

            Gameboard gameboard = (Gameboard)kernel.Get<IGameboardFactory>().CreateGameboard(10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 1, Y = 1 },
                new GameboardObject() { Number = 2, X = 2, Y = 1 },
            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 8, Y = 9 },
                new GameboardObject() { Number = 2, X = 9, Y = 9 },
            });

            // act
            GamePlayer gamePlayer = new(gameboard);
            var solution = gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            });

            // assert
            Assert.IsNull(solution);
        }

        [TestMethod()]
        public void GameBalls4ToHoles2NotAccessibleTest1()
        {
            // arrange
            IKernel kernel = new StandardKernel();
            kernel.Bind<IGameboardFactory>().ToFactory();
            kernel.Bind<IGameboard>().To<Gameboard>();
            kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();

            Gameboard gameboard = (Gameboard)kernel.Get<IGameboardFactory>().CreateGameboard(10, 10, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 0, Y = 1 },
                new GameboardObject() { Number = 2, X = 1, Y = 1 },
            }, new List<GameboardObject>()
            {
                new GameboardObject() { Number = 1, X = 6, Y = 9 },
                new GameboardObject() { Number = 2, X = 7, Y = 9 },
                new GameboardObject() { Number = 3, X = 8, Y = 9 },
                new GameboardObject() { Number = 4, X = 9, Y = 9 },
            });

            // act
            GamePlayer gamePlayer = new(gameboard);
            var solution = gamePlayer.GetShortestSolution(new List<IAction>()
            {
                new GravityBottomAction(),
                new GravityTopAction(),
                new GravityLeftAction(),
                new GravityRightAction(),
            });

            // assert
            Assert.IsNull(solution);
        }

    }
}