using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall
{
    interface IGameboardObject : ICloneable
    {
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
    }

    class GameboardObject : IGameboardObject
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Number { get; init; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
