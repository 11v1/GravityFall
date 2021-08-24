using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall
{
    interface IGameboardObject
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public uint X { get; set; }

        /// <summary>
        /// Y coordinates
        /// </summary>
        public uint Y { get; set; }

        /// <summary>
        /// Object number
        /// </summary>
        public uint Number { get; init; }
    }

    struct GameboardObject : IGameboardObject, IEquatable<GameboardObject>
    {
        public uint X { get; set; }

        public uint Y { get; set; }

        public uint Number { get; init; }

        public override bool Equals(object obj)
        {
            return obj is GameboardObject @object && Equals(@object);
        }

        public bool Equals(GameboardObject other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Number == other.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Number);
        }

        public static bool operator ==(GameboardObject left, GameboardObject right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GameboardObject left, GameboardObject right)
        {
            return !(left == right);
        }
    }
}
