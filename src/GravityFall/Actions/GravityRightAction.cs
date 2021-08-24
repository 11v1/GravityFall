using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall.Actions
{
    class GravityRightAction : GravityAction
    {

        protected override IEnumerable<IGameboardObject> GetBallsOrderedFromGravitySide(IGameboard gameboard) =>
            gameboard.Balls.OrderByDescending(p => p.X);

        protected override IGameboardObject GetObjectUnderTheObject(IEnumerable<IGameboardObject> gameboardObjects, IGameboardObject @object) =>
            gameboardObjects.Where(p => p.Y == @object.Y).Where(p => p.X > @object.X).OrderBy(p => p.X).FirstOrDefault();

        protected override void FellBallToGravitySide(IGameboard gameboard, IGameboardObject ball, IGameboardObject surfaceBall) =>
            ball.X = surfaceBall == null ? gameboard.MaxX : surfaceBall.X - 1;
    }
}
