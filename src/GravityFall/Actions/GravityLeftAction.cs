using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall.Actions
{
    class GravityLeftAction : GravityAction
    {

        protected override IEnumerable<IGameboardObject> GetBallsOrderedFromGravitySide(IGameboard gameboard) =>
            gameboard.Balls.OrderBy(p => p.X);

        protected override IGameboardObject GetObjectUnderTheObject(IEnumerable<IGameboardObject> gameboardObjects, IGameboardObject @object) =>
            gameboardObjects.Where(p => p.Y == @object.Y).Where(p => p.X < @object.X).OrderBy(p => p.X).LastOrDefault();

        protected override void FellBallToGravitySide(IGameboard gameboard, IGameboardObject ball, IGameboardObject surfaceBall) =>
            gameboard.SetBallPosition(ball.Number, surfaceBall == null ? 0 : surfaceBall.X + 1, ball.Y);
    }
}
