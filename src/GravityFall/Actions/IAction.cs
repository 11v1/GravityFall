using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall.Actions
{
    /// <summary>
    /// Action that is performed over the gameboard
    /// </summary>
    interface IAction
    {
        /*************************************************************
         *  Methods
        /*************************************************************/

        /// <summary>
        /// Applies some action and modifies gameboard
        /// </summary>
        /// <param name="gameboard"></param>
        /// <returns>Hole-ball pairs of balls that have fallen into holes</returns>
        public IEnumerable<(int HoleNumber, int BallNumber)> ApplyAction(IGameboard gameboard);
    }
}
