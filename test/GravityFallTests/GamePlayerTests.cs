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
        public void GameTest1()
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
        }
    }
}