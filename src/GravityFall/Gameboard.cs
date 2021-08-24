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
        public uint SizeX { get; }

        /// <summary>
        /// Minimum X value bound
        /// </summary>
        public uint MinX { get; }

        /// <summary>
        /// Maximum X value bound
        /// </summary>
        public uint MaxX { get; }

        /// <summary>
        /// Cells number in Y direction
        /// </summary>
        public uint SizeY { get; }

        /// <summary>
        /// Minimum Y value bound
        /// </summary>
        public uint MinY { get; }

        /// <summary>
        /// Maximum Y value bound
        /// </summary>
        public uint MaxY { get; }

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
            SizeX = xSize;
            SizeY = ySize;
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

        public uint SizeX { get; }

        public uint MinX => 0;

        public uint MaxX => SizeX - 1;

        public uint SizeY { get; }

        public uint MinY => 0;

        public uint MaxY => SizeY - 1;

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
            var objectOutOfBounds = objects.FirstOrDefault(p => p.X < MinX || p.X > MaxX || p.Y < MinY || p.Y > MaxY);
            if (objectOutOfBounds != null)
                throw new ArgumentOutOfRangeException(string.Format(Resources.ExceptionObjectPositionOutOfBoard, objectOutOfBounds.X, objectOutOfBounds.Y, MinX, MaxX, MinY, MaxY));
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
