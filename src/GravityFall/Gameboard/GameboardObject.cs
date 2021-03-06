using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall
{
    /// <summary>
    /// Object that can be positioned on the gameboard
    /// </summary>
    interface IGameboardObject : ICloneable
    {

        /*************************************************************
         *  Properties
        /*************************************************************/

        /// <summary>
        /// X coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinates
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Object number
        /// </summary>
        public int Number { get; init; }


        /*************************************************************
         *  Methods
        /*************************************************************/

        /// <summary>
        /// Compares this instance values to other
        /// </summary>
        /// <remarks>
        /// Implementing our own Equals like method.
        /// Since <see cref="IGameboardObject"/> is mutable overriding <see cref="Object.Equals(object?)"/> is not recomended
        /// </remarks>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ValueEquals(IGameboardObject other);
    }

    interface IGameboardObjectFactory
    {
        IGameboardObject CreateGameboardObject(int number);
    }

    /// <inheritdoc cref="IGameboardSnapshot"/>
    sealed class GameboardObject : IGameboardObject
    {

        /*************************************************************
         *  Ctors
        /*************************************************************/

        public GameboardObject(int number)
        {
            Number = number;
        }

        public GameboardObject()
        { }


        /*************************************************************
         *  Properties
        /*************************************************************/

        public int X { get; set; }

        public int Y { get; set; }

        public int Number { get; init; }


        /*************************************************************
         *  Methods
        /*************************************************************/

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool ValueEquals(IGameboardObject other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return X == other.X && Y == other.Y && Number == other.Number;
        }
    }
}
