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
        /// <summary>
        /// Balls positions
        /// </summary>
        public IReadOnlyCollection<IGameboardObject> Balls { get; }

        /// <summary>
        /// Compares this instance values to other
        /// </summary>
        /// <remarks>
        /// Implementing our own Equals like method.
        /// Since <see cref="IGameboardSnapshot"/> can be modified (to be certain technically it may be but it should not!) overriding <see cref="Object.Equals(object?)"/> is not recomended
        /// </remarks>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ValueEquals(IGameboardSnapshot other);
    }

    /// <inheritdoc cref="IGameboardSnapshot"/>
    sealed class GameboardSnapshot : IGameboardSnapshot
    {
        public GameboardSnapshot(IEnumerable<IGameboardObject> balls)
        {
            foreach (var ball in balls)
                _balls.Add((IGameboardObject)ball.Clone());
        }

        public IReadOnlyCollection<IGameboardObject> Balls => _balls.AsReadOnly();
        private readonly List<IGameboardObject> _balls = new();

        public bool ValueEquals(IGameboardSnapshot other)
        {
            if (other == null)
                throw new NullReferenceException(nameof(other));

            if (Balls.Count != other.Balls.Count)
                return false;

            foreach (var ball in Balls)
                if (other.Balls.FirstOrDefault(p => p.ValueEquals(ball)) == null)
                    return false;

            return true;
        }
    }
}
