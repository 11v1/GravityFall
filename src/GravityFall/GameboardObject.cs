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

    class GameboardObject : IGameboardObject
    {
        public uint X { get; set; }

        public uint Y { get; set; }

        public uint Number { get; init; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
