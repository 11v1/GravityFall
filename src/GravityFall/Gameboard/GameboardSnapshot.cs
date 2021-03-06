using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall
{
    /// <summary>
    /// Serves to save gameboard dynamic objects states
    /// </summary>
    interface IGameboardSnapshot
    {

        /*************************************************************
         *  Properties
        /*************************************************************/

        /// <summary>
        /// Balls positions
        /// </summary>
        public IReadOnlyCollection<IGameboardObject> Balls { get; }


        /*************************************************************
         *  Methods
        /*************************************************************/

        /// <summary>
        /// Compares this instance values to other
        /// </summary>
        /// <remarks>
        /// Implementing our own Equals like method.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ValueEquals(IGameboardSnapshot other);
    }

    /// <summary>
    /// Abstract fabric for GameboardSnapshot
    /// </summary>
    interface IGameboardSnapshotFactory
    {
        IGameboardSnapshot CreateSnapshot(IEnumerable<IGameboardObject> balls);
    }

    /// <inheritdoc cref="IGameboardSnapshot"/>
    sealed class GameboardSnapshot : IGameboardSnapshot
    {

        /*************************************************************
         *  Ctors
        /*************************************************************/

        public GameboardSnapshot(IEnumerable<IGameboardObject> balls)
        {
            foreach (var ball in balls)
                _balls.Add((IGameboardObject)ball.Clone());
        }


        /*************************************************************
         *  Properties
        /*************************************************************/

        public IReadOnlyCollection<IGameboardObject> Balls => _balls.Select(p => (IGameboardObject)p.Clone()).ToList().AsReadOnly(); // Making internal items inaccessible. Returning objects copy
        private readonly List<IGameboardObject> _balls = new();


        /*************************************************************
         *  Methods
        /*************************************************************/

        public bool ValueEquals(IGameboardSnapshot other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var otherBalls = other.Balls;
            if (_balls.Count != otherBalls.Count)
                return false;

            foreach (var ball in _balls)
                if (otherBalls.FirstOrDefault(p => p.ValueEquals(ball)) == null)
                    return false;

            return true;
        }
    }
}
