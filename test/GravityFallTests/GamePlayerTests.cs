using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aura.GravityFall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aura.GravityFall.Actions;

namespace Aura.GravityFall.Tests
{
    [TestClass()]
    public class GamePlayerTests
    {
        [TestMethod()]
        public void GameNoHolesTest()
        {
            // arrange
            Gameboard gameboard = new(10, 10, new List<GameboardObject>()
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
            Gameboard gameboard = new(10, 10, new List<GameboardObject>()
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

        //[TestMethod()]
        //public void GetShortestSolutionTest()
        //{
        //    Assert.Fail();
        //}
    }
}