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
    public class GameboardObjectTests
    {
        [TestMethod()]
        public void ValueEqualsTest1()
        {
            // arrange
            GameboardObject object1 = new()
            {
                Number = 1,
                X = 1,
                Y = 1,
            };
            GameboardObject object2 = new()
            {
                Number = 1,
                X = 1,
                Y = 1,
            };

            // act

            // assert
            Assert.IsTrue(object1.ValueEquals(object2));
        }

        [TestMethod()]
        public void ValueEqualsTest2()
        {
            // arrange
            GameboardObject object1 = new()
            {
                Number = 2,
                X = 1,
                Y = 1,
            };
            GameboardObject object2 = new()
            {
                Number = 1,
                X = 1,
                Y = 1,
            };

            // act

            // assert
            Assert.IsFalse(object1.ValueEquals(object2));
        }

        [TestMethod()]
        public void ValueEqualsTest3()
        {
            // arrange
            GameboardObject object1 = new()
            {
                Number = 1,
                X = 2,
                Y = 1,
            };
            GameboardObject object2 = new()
            {
                Number = 1,
                X = 1,
                Y = 1,
            };

            // act

            // assert
            Assert.IsFalse(object1.ValueEquals(object2));
        }

        [TestMethod()]
        public void ValueEqualsTest4()
        {
            // arrange
            GameboardObject object1 = new()
            {
                Number = 1,
                X = 1,
                Y = 2,
            };
            GameboardObject object2 = new()
            {
                Number = 1,
                X = 1,
                Y = 1,
            };

            // act

            // assert
            Assert.IsFalse(object1.ValueEquals(object2));
        }

        [TestMethod()]
        public void ValueEqualsTest5()
        {
            // arrange
            GameboardObject object1 = new()
            {
                Number = 2,
                X = 1,
                Y = 1,
            };
            GameboardObject object2 = null;

            // act

            // assert
            Assert.ThrowsException<ArgumentNullException>(() => object1.ValueEquals(object2));
        }


        [TestMethod()]
        public void CloneTest()
        {
            // arrange
            GameboardObject object1 = new()
            {
                Number = 2,
                X = 1,
                Y = 1,
            };
            GameboardObject object2 = (GameboardObject)object1.Clone();

            // act

            // assert
            Assert.AreNotEqual(object1, object2);
            Assert.IsTrue(object1.ValueEquals(object2));
        }


    }
}