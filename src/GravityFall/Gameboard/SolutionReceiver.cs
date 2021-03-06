using Aura.GravityFall.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.GravityFall
{
    /// <summary>
    /// Plays the game
    /// </summary>
    interface ISolutionReceiver
    {

        /*************************************************************
         *  Methods
        /*************************************************************/

        /// <summary>
        /// Performs set of actions to get shortest actions sequence to archive game goal
        /// </summary>
        /// <param name="actions"></param>
        /// <returns>List of actions to reach solution, null if solution does not exist</returns>
        public IList<IAction> GetShortestSolution(IGameboard gameboard, IEnumerable<IAction> actions);
    }

    /// <inheritdoc cref="ISolutionReceiver"/>
    class SolutionReceiver : ISolutionReceiver
    {

        /*************************************************************
         *  Types
        /*************************************************************/

        /// <summary>
        /// Item to build actions tree
        /// </summary>
        private class ActionTreeItem
        {
            /// <summary>
            /// Reference to a parent action. Null if it is root item
            /// </summary>
            public ActionTreeItem Parent { get; init; }

            /// <summary>
            /// Action
            /// </summary>
            public IAction Action { get; init; }

            /// <summary>
            /// Gameboard state on which action is executed
            /// </summary>
            public IGameboardSnapshot GameboardSnapshot { get; init; }
        }


        /*************************************************************
         *  Ctors
        /*************************************************************/


        /*************************************************************
         *  Fields
        /*************************************************************/


        /*************************************************************
         *  Methods
        /*************************************************************/

        public IList<IAction> GetShortestSolution(IGameboard gameboard, IEnumerable<IAction> actions)
        {
            if (gameboard.Holes.Count == 0)
                throw new InvalidOperationException(Resources.ExceptionNoHoles);
            if (gameboard.Balls.Count == 0)
                throw new InvalidOperationException(Resources.ExceptionNoBalls);

            List<ActionTreeItem> currentLevelActions = new();
            IGameboardSnapshot gameboardSnapshot = gameboard.SaveSnapshot();
            foreach (var action in actions)
                currentLevelActions.Add(new ActionTreeItem() { Action = action, GameboardSnapshot = gameboardSnapshot });
            return DoGetShortestSolution(gameboard, actions, currentLevelActions);
        }

        /// <summary>
        /// Performs recursive actions in search of shortest solution
        /// </summary>
        /// <param name="actions"></param>
        /// <param name="currentLevelActions"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0042:Deconstruct variable declaration", Justification = "<Pending>")]
        private IList<IAction> DoGetShortestSolution(IGameboard gameboard, IEnumerable<IAction> actions, IList<ActionTreeItem> currentLevelActions)
        {
            // No current level actions. It means that there is no possible solution
            if (currentLevelActions.Count == 0)
                return null;
            // Actions for next tree level iteration
            List<ActionTreeItem> nextLevelActions = new();
            // Iterating current level actions
            for (int i = 0; i < currentLevelActions.Count; i++)
            {
                // Loading balls positions
                gameboard.LoadShapshot(currentLevelActions[i].GameboardSnapshot, true);
                // Executing action
                var ballsInholes = gameboard.ApplyAction(currentLevelActions[i].Action);
                // Checking balls that have fallen to holes
                bool deadend = false;
                foreach (var item in ballsInholes)
                {
                    // Ball should fall only into the holes with the same number. Otherwise this direction is the losing one
                    if (item.HoleNumber != item.BallNumber)
                    {
                        deadend = true;
                        break;
                    }
                }
                if (deadend)
                {
                    // Removing current level action
                    continue;
                }
                // If no balls left, than we have won
                if (gameboard.Balls.Count == 0)
                    return GetActionsFromRoot(currentLevelActions[i]);
                // Checking for state looping. If there is same balls position in some previous tree level, then this is deadend
                IGameboardSnapshot gameboardSnapshot = gameboard.SaveSnapshot();
                if (CheckForActionsLoop(gameboardSnapshot, currentLevelActions[i]))
                {
                    // Removing current level action
                    continue;
                }
                // Not won nor lost, preparing next tree level actions
                foreach (var action in actions)
                {
                    // Skipping current action type for next level
                    if (action == currentLevelActions[i].Action)
                        continue;
                    nextLevelActions.Add(new ActionTreeItem()
                    {
                        Parent = currentLevelActions[i],
                        Action = action,
                        GameboardSnapshot = gameboardSnapshot,
                    });
                }
            }
            // Executing next tree level actions
            return DoGetShortestSolution(gameboard, actions, nextLevelActions);
        }

        /// <summary>
        /// Returns list of actions from root to current action
        /// </summary>
        /// <param name="actionTreeItem"></param>
        /// <returns></returns>
        private static IList<IAction> GetActionsFromRoot(ActionTreeItem actionTreeItem)
        {
            static void DoGetActionsFromParent(IList<IAction> actions, ActionTreeItem actionTreeItem)
            {
                if (actionTreeItem == null)
                    return;
                actions.Add(actionTreeItem.Action);
                DoGetActionsFromParent(actions, actionTreeItem.Parent);
            }

            List<IAction> actions = new();
            DoGetActionsFromParent(actions, actionTreeItem);
            actions.Reverse();
            return actions;
        }

        /// <summary>
        /// Checks previous tree steps for the equality to the current snapshot state
        /// </summary>
        /// <param name="currentSnapshot"></param>
        /// <param name="actionTreeItem"></param>
        /// <returns></returns>
        private static bool CheckForActionsLoop(IGameboardSnapshot currentSnapshot, ActionTreeItem actionTreeItem)
        {
            while (actionTreeItem != null)
            {
                if (currentSnapshot.ValueEquals(actionTreeItem.GameboardSnapshot))
                    return true;
                actionTreeItem = actionTreeItem.Parent;
            }
            return false;
        }
    }
}
