using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall.Actions
{
    class GravityBottomAction : GravityAction
    {

        protected override IEnumerable<IGameboardObject> GetBallsOrderedFromGravitySide(IGameboard gameboard) =>
            gameboard.Balls.OrderByDescending(p => p.Y);

        protected override IGameboardObject GetObjectUnderTheObject(IEnumerable<IGameboardObject> gameboardObjects, IGameboardObject @object) =>
            gameboardObjects.Where(p => p.X == @object.X).Where(p => p.Y > @object.Y).OrderBy(p => p.X).FirstOrDefault();

        protected override void FellBallToGravitySide(IGameboard gameboard, IGameboardObject ball, IGameboardObject surfaceBall) =>
            gameboard.SetBallPosition(ball.Number, ball.X, surfaceBall == null ? 0 : surfaceBall.Y - 1);
    }
}
