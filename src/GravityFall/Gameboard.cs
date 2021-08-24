using Aura.GravityFall.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall
{
    interface IGameboard
    {
        /// <summary>
        /// Cells number in X direction
        /// </summary>
        public uint XSize { get; }

        /// <summary>
        /// Cells number in Y direction
        /// </summary>
        public uint YSize { get; }

        /// <summary>
        /// Holes positioned on the gameboard
        /// </summary>
        public IReadOnlyCollection<IGameboardObject> Holes { get; }

        /// <summary>
        /// Balls positioned on the gameboard
        /// </summary>
        public IReadOnlyCollection<IGameboardObject> Balls { get; }

        /// <summary>
        /// Saves gameboard state
        /// </summary>
        /// <returns></returns>
        public IGameboardSnapshot SaveSnapshot();

        /// <summary>
        /// Loads gameboard state
        /// </summary>
        /// <returns></returns>
        public void LoadShapshot(IGameboardSnapshot snapshot);

        /// <summary>
        /// Modifies ball's position
        /// </summary>
        /// <param name="number"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetBallPosition(uint number, uint x, uint y);

        /// <summary>
        /// Removes ball from gameboard
        /// </summary>
        /// <param name="number"></param>
        public void RemoveBall(uint number);

        /// <summary>
        /// Applies action to gameboard.
        /// Action can (or should) modify gameboard
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IEnumerable<(uint HoleNumber, uint BallNumber)> ApplyAction(IAction action);
    }

    class Gameboard : IGameboard
    {

        /*************************************************************
         *  Ctors
        /*************************************************************/

        public Gameboard(uint xSize, uint ySize, IEnumerable<IGameboardObject> holes, IEnumerable<IGameboardObject> balls)
        {
            // Setting gameboard size
            XSize = xSize;
            YSize = ySize;
            // Positioning holes
            ValidateObjects(holes);
            foreach (var h in holes)
                _holes.Add(h.Number, h);
            // Positioning balls
            ValidateObjects(balls);
            foreach (var b in balls)
                _balls.Add(b.Number, b);
        }


        /*************************************************************
         *  Properties
        /*************************************************************/

        public uint XSize { get; }

        public uint YSize { get; }

        public IReadOnlyCollection<IGameboardObject> Holes => _holes.Values;
        private readonly Dictionary<uint, IGameboardObject> _holes = new();

        public IReadOnlyCollection<IGameboardObject> Balls => _balls.Values;
        private readonly Dictionary<uint, IGameboardObject> _balls = new();


        /*************************************************************
         *  Methods
        /*************************************************************/

        public IGameboardSnapshot SaveSnapshot()
        {
            return new GameboardSnapshot(Balls);
        }

        public void LoadShapshot(IGameboardSnapshot snapshot)
        {
            ValidateObjects(snapshot.Balls);
            _balls.Clear();
            foreach (var b in snapshot.Balls)
                _balls.Add(b.Number, b);
        }

        public void SetBallPosition(uint number, uint x, uint y)
        {
            if (!_balls.TryGetValue(number, out var ball))
                throw new ArgumentException(nameof(number));
            ball.X = x;
            ball.Y = y;
            _balls[number] = ball;
        }

        public void RemoveBall(uint number)
        {
            if (!_balls.ContainsKey(number))
                throw new ArgumentException(nameof(number));
            _balls.Remove(number);
        }

        public IEnumerable<(uint HoleNumber, uint BallNumber)> ApplyAction(IAction action) =>
            action.ApplyAction(this);

        /// <summary>
        /// Validates objects to gameboard
        /// </summary>
        /// <param name="objects"></param>
        private void ValidateObjects(IEnumerable<IGameboardObject> objects)
        {
            // Checking if object x and y coordinate do not exceeds gameboard size
            var objectOutOfBounds = objects.FirstOrDefault(p => p.X >= XSize || p.Y >= YSize);
            if (objectOutOfBounds != null)
                throw new ArgumentOutOfRangeException(string.Format(Resources.ExceptionObjectPositionOutOfBoard, objectOutOfBounds.X, objectOutOfBounds.Y, 0, XSize - 1, 0, YSize - 1));
            // Checking if object numbers are unique
            if (objects.Select(p => p.Number).Distinct().Count() != objects.Count())
                throw new ArgumentException(Resources.ExceptionObjectNumberDuplicate);
            // Checking if objects have unique positions (not place on each other)
            var points = objects.Select(p => (p.X, p.Y));
            if (points.Distinct().Count() != points.Count())
                throw new ArgumentException(Resources.ExceptionObjectPositionDuplicate);
        }
    }
}
