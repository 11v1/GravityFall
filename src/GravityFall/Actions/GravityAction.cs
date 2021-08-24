﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall.Actions
{

    abstract class GravityAction : IAction
    {

        public IEnumerable<(uint HoleNumber, uint BallNumber)> ApplyAction(IGameboard gameboard)
        {
            List<(uint HoleNumber, uint BallNumber)> result = new();
            var balls = GetBallsOrderedFromGravitySide(gameboard);
            foreach (var ball in balls)
            {
                var hole = GetHoleUnderTheBall(gameboard, ball);
                if (hole != null)
                {
                    // There is hole under the ball. Ball fells in it and annihilates
                    result.Add((hole.Number, ball.Number));
                    gameboard.RemoveBall(ball.Number);
                }
                else
                {
                    // There is no hole. Ball fells to the surface (or to the ball, lying on the surface)
                    var surfaceBall = GetBallUnderTheBall(gameboard, ball);
                    FellBallToGravitySide(gameboard, ball, surfaceBall);
                }
            }
            return result;
        }

        /// <summary>
        /// Returns balls in orders from gravity side (closes to furthest)
        /// </summary>
        /// <param name="gameboard"></param>
        /// <returns></returns>
        protected abstract IEnumerable<IGameboardObject> GetBallsOrderedFromGravitySide(IGameboard gameboard);

        /// <summary>
        /// Gets the closes hole under the ball
        /// </summary>
        /// <param name="gameboard"></param>
        /// <param name="ball"></param>
        /// <returns></returns>
        private IGameboardObject GetHoleUnderTheBall(IGameboard gameboard, IGameboardObject ball) =>
            GetObjectUnderTheObject(gameboard.Holes, ball);

        /// <summary>
        /// Returns the highest ball in a line. If no ball beneath, then nothing returned/>
        /// </summary>
        /// <param name="gameboard"></param>
        /// <param name="ball"></param>
        /// <returns></returns>
        private IGameboardObject GetBallUnderTheBall(IGameboard gameboard, IGameboardObject ball) =>
            GetObjectUnderTheObject(gameboard.Balls, ball);

        /// <summary>
        /// Get object under the object
        /// </summary>
        /// <param name="gameboardObjects"></param>
        /// <param name="object"></param>
        /// <returns></returns>
        protected abstract IGameboardObject GetObjectUnderTheObject(IEnumerable<IGameboardObject> gameboardObjects, IGameboardObject @object);

        /// <summary>
        /// Drops ball to the gravity side
        /// </summary>
        /// <param name="gameboard"></param>
        /// <param name="ball"></param>
        /// <param name="highestBall"></param>
        protected abstract void FellBallToGravitySide(IGameboard gameboard, IGameboardObject ball, IGameboardObject highestBall);
    }
}
