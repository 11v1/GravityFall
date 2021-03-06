using Aura.GravityFall.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall
{
    /// <summary>
    /// Two-dimensional board tha contains objects
    /// </summary>
    interface IGameboard
    {

        /*************************************************************
         *  Properties
        /*************************************************************/

        /// <summary>
        /// Cells number in X direction
        /// </summary>
        public int SizeX { get; }

        /// <summary>
        /// Minimum X value bound
        /// </summary>
        public int MinX { get; }

        /// <summary>
        /// Maximum X value bound
        /// </summary>
        public int MaxX { get; }

        /// <summary>
        /// Cells number in Y direction
        /// </summary>
        public int SizeY { get; }

        /// <summary>
        /// Minimum Y value bound
        /// </summary>
        public int MinY { get; }

        /// <summary>
        /// Maximum Y value bound
        /// </summary>
        public int MaxY { get; }

        /// <summary>
        /// Holes positioned on the gameboard
        /// </summary>
        public IReadOnlyCollection<IGameboardObject> Holes { get; }

        /// <summary>
        /// Balls positioned on the gameboard
        /// </summary>
        public IReadOnlyCollection<IGameboardObject> Balls { get; }


        /*************************************************************
         *  Methods
        /*************************************************************/

        /// <summary>
        /// Saves gameboard state
        /// </summary>
        /// <returns></returns>
        public IGameboardSnapshot SaveSnapshot();

        /// <summary>
        /// Loads gameboard state
        /// </summary>
        /// <param name="skipValidation">Flag may be set to true when there is no reason to think that data may be incorrect
        /// (e.g. when you totally sure that snapshot was made from this gameboard object instance</param>
        /// <returns></returns>
        public void LoadShapshot(IGameboardSnapshot snapshot, bool skipValidation = false);

        /// <summary>
        /// Removes ball from gameboard
        /// </summary>
        /// <param name="number"></param>
        public void RemoveBall(int number);

        /// <summary>
        /// Applies action to gameboard.
        /// Action can (or should) modify gameboard
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Returns list of hole-ball pairs for balls that have fallen in holes</returns>
        public IEnumerable<(int HoleNumber, int BallNumber)> ApplyAction(IAction action);
    }

    /// <summary>
    /// Abstract fabric for Gameboard creation
    /// </summary>
    interface IGameboardFactory
    {
        IGameboard CreateGameboard(int sizeX, int sizeY, IEnumerable<IGameboardObject> holes, IEnumerable<IGameboardObject> balls);
    }

    /// <inheritdoc cref="IGameboard"/>
    class Gameboard : IGameboard
    {

        /*************************************************************
         *  Ctors
        /*************************************************************/

        public Gameboard(IGameboardSnapshotFactory factory, int sizeX, int sizeY, IEnumerable<IGameboardObject> holes, IEnumerable<IGameboardObject> balls)
        {
            // Validating values
            if (sizeX < 1)
                throw new ArgumentOutOfRangeException(nameof(sizeX));
            if (sizeY < 1)
                throw new ArgumentOutOfRangeException(nameof(sizeY));
            // Initializing
            _factory = factory;
            // Setting gameboard size
            SizeX = sizeX;
            SizeY = sizeY;
            // Validating objects
            ValidateObjects(holes);
            ValidateObjects(balls);
            ValidateObjectsPosition(holes.Concat(balls));
            // Positioning holes
            foreach (var h in holes)
                _holes.Add(h.Number, h);
            // Positioning balls
            foreach (var b in balls)
                _balls.Add(b.Number, b);
        }


        /*************************************************************
         *  Fields
        /*************************************************************/

        private readonly IGameboardSnapshotFactory _factory;


        /*************************************************************
         *  Properties
        /*************************************************************/

        public int SizeX { get; }

        public int MinX => 0;

        public int MaxX => SizeX - 1;

        public int SizeY { get; }

        public int MinY => 0;

        public int MaxY => SizeY - 1;

        public IReadOnlyCollection<IGameboardObject> Holes => _holes.Values;
        private readonly Dictionary<int, IGameboardObject> _holes = new();

        public IReadOnlyCollection<IGameboardObject> Balls => _balls.Values;
        private readonly Dictionary<int, IGameboardObject> _balls = new();


        /*************************************************************
         *  Methods
        /*************************************************************/

        public IGameboardSnapshot SaveSnapshot()
        {
            return _factory.CreateSnapshot(Balls);
        }

        public void LoadShapshot(IGameboardSnapshot snapshot, bool skipValidation = false)
        {
            var snapshotBalls = snapshot.Balls;
            if (!skipValidation)
            {
                ValidateObjects(snapshotBalls);
                ValidateObjectsPosition(Holes.Concat(snapshotBalls));
            }
            _balls.Clear();
            foreach (var b in snapshotBalls)
                _balls.Add(b.Number, b);
        }

        public void RemoveBall(int number)
        {
            if (!_balls.ContainsKey(number))
                throw new ArgumentException(string.Format(Resources.ExceptionBallNotFound, number));
            _balls.Remove(number);
        }

        public IEnumerable<(int HoleNumber, int BallNumber)> ApplyAction(IAction action) =>
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
        }

        /// <summary>
        /// Validates that object positions are unique (not overlapping)
        /// </summary>
        /// <param name="objects"></param>
        private static void ValidateObjectsPosition(IEnumerable<IGameboardObject> objects)
        {
            // Checking if objects have unique positions (not place on each other)
            var points = objects.Select(p => (p.X, p.Y));
            if (points.Distinct().Count() != points.Count())
                throw new ArgumentException(Resources.ExceptionObjectPositionDuplicate);
        }
    }
}
