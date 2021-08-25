using Aura.GravityFall.Actions;
using Ninject;
using Ninject.Extensions.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.GravityFall
{
    class Program
    {
        static void Main(/*string[] args*/)
        {
            try
            {
                // Configuring kernel
                IKernel kernel = new StandardKernel();
                kernel.Bind<IGameboardObject>().To<GameboardObject>();
                kernel.Bind<IGameboardSnapshot>().To<GameboardSnapshot>();
                kernel.Bind<IGameboard>().To<Gameboard>();
                kernel.Bind<ISolutionReceiver>().To<SolutionReceiver>();
                kernel.Bind<IGameboardObjectFactory>().ToFactory();
                kernel.Bind<IGameboardFactory>().ToFactory();
                kernel.Bind<IGameboardSnapshotFactory>().ToFactory();
                kernel.Bind<GravityBottomAction>().ToSelf().InSingletonScope();
                kernel.Bind<GravityLeftAction>().ToSelf().InSingletonScope();
                kernel.Bind<GravityRightAction>().ToSelf().InSingletonScope();
                kernel.Bind<GravityTopAction>().ToSelf().InSingletonScope();

                // Asking user for data
                Console.WriteLine(Resources.RequestXSize);
                int sizeX = int.Parse(Console.ReadLine());
                Console.WriteLine(Resources.RequestYSize);
                int sizeY = int.Parse(Console.ReadLine());
                Console.WriteLine(Resources.RequestHoles);
                List<IGameboardObject> holes = ParseCreateGameboardObject(kernel.Get<IGameboardObjectFactory>(), Console.ReadLine());
                Console.WriteLine(Resources.RequestBalls);
                List<IGameboardObject> balls = ParseCreateGameboardObject(kernel.Get<IGameboardObjectFactory>(), Console.ReadLine());

                // Searching for solution
                var gameboard = kernel.Get<IGameboardFactory>().CreateGameboard(sizeX, sizeY, holes, balls);
                var solutionReceiver = kernel.Get<ISolutionReceiver>();
                var solution = solutionReceiver.GetShortestSolution(gameboard, new List<IAction>()
                {
                    kernel.Get<GravityBottomAction>(),
                    kernel.Get<GravityLeftAction>(),
                    kernel.Get<GravityRightAction>(),
                    kernel.Get<GravityTopAction>(),
                });
                if (solution == null)
                {
                    WriteResultToConsole(Resources.NoSolution, ConsoleColor.Red);
                }
                else
                {
                    WriteResultToConsole(string.Join(" -> ", solution), ConsoleColor.Green);
                }
            }
            catch (Exception e)
            {
                WriteResultToConsole($"Error: {e.Message}", ConsoleColor.Red);
            }
        }

        private static List<IGameboardObject> ParseCreateGameboardObject(IGameboardObjectFactory factory, string str)
        {
            List<IGameboardObject> result = new();
            foreach (var item in str.Split(",", StringSplitOptions.RemoveEmptyEntries))
                result.Add(CreateGameboardObject(factory, item));
            return result;
        }

        private static IGameboardObject CreateGameboardObject(IGameboardObjectFactory factory, string str)
        {
            var value = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var result = factory.CreateGameboardObject(int.Parse(value[0]));
            result.X = int.Parse(value[1]);
            result.Y = int.Parse(value[2]);
            return result;
        }

        private static void WriteResultToConsole(string text, ConsoleColor foregrounfColor)
        {
            Console.ForegroundColor = foregrounfColor;
            Console.Write($"{Environment.NewLine}{text}{Environment.NewLine}");
            Console.ResetColor();
        }
    }

}
