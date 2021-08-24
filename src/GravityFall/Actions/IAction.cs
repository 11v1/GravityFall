using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall.Actions
{
    interface IAction
    {
        /// <summary>
        /// Applies some action and modifies gameboard
        /// </summary>
        /// <param name="gameboard"></param>
        /// <returns></returns>
        public IEnumerable<(int HoleNumber, int BallNumber)> ApplyAction(IGameboard gameboard);
    }
}
