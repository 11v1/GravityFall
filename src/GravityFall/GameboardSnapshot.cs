using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall
{

    /// <summary>
    /// Serves to same gameboard dynamic objects
    /// </summary>
    interface IGameboardSnapshot
    {
        /// <summary>
        /// Balls positions
        /// </summary>
        public IReadOnlyCollection<IGameboardObject> Balls { get; }
    }

    /// <inheritdoc cref="IGameboardSnapshot"/>
    class GameboardSnapshot : IGameboardSnapshot
    {
        public GameboardSnapshot(IEnumerable<IGameboardObject> balls)
        {
            foreach (var ball in balls)
                _balls.Add((IGameboardObject)ball.Clone());
        }

        public IReadOnlyCollection<IGameboardObject> Balls => _balls.AsReadOnly();
        private readonly List<IGameboardObject> _balls = new();
    }
}
