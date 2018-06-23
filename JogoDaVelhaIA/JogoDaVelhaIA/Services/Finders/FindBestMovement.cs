using JogoDaVelhaIA.Domain;
using System.Collections.Generic;
using System.Linq;

namespace JogoDaVelhaIA.Services.Finders
{
    public class FindBestMovement : FindBase
    {
        public FindBestMovement(FindBase next) : base(next) { }
        public FindBestMovement() : base(null) { }

        protected override int Find(ComputerBrain computerBrain, Layout currentGame)
        {
            var probabilityManager = new ProbabilityManager();

            var currentGameArray = currentGame.Positions.ToArray();

            var beforeEqualGames = computerBrain.Memory.Where(i => i.Positions.SequenceEqual(currentGame.Positions));

            var lostGames = beforeEqualGames.Where(i => !i.ComputerWin);
            var failMoves = GetNextTurnMoves(computerBrain, currentGame, lostGames);
            var lostPositions = GetPositionMoves(currentGameArray, failMoves);

            lostPositions.ForEach(position => probabilityManager.Increment(position, false));

            var winGames = beforeEqualGames.Where(i => i.ComputerWin);
            var correctMoves = GetNextTurnMoves(computerBrain, currentGame, winGames);
            var winPositions = GetPositionMoves(currentGameArray, correctMoves);

            winPositions.ForEach(position => probabilityManager.Increment(position, true));

            var findedPosition = probabilityManager.BestPosition(probabilityManager.ShouldMoveToWin());

            if (currentGame.CanMove(findedPosition)) return findedPosition;

            return AnyPosition(currentGame);
        }

        private int AnyPosition(Layout currentGame)
        {
            for (int i = 0; i < 9; i++)
            {
                if (currentGame.CanMove(i))
                    return i;
            }

            return -1;
        }

        private static List<Layout> GetNextTurnMoves(ComputerBrain computerBrain, Layout currentGame, IEnumerable<Layout> beforeGames)
        {
            List<Layout> failMoves = new List<Layout>(beforeGames.Count());

            foreach (var current in beforeGames)
            {
                var beforeGame = computerBrain.Memory.Where(i => i.GameId == current.GameId);

                var currentTurn = beforeGame.FirstOrDefault(i => i.Turn == currentGame.Turn + 1);

                if (currentTurn != null) failMoves.Add(currentTurn);
            }

            return failMoves;
        }

        private List<int> GetPositionMoves(char[] currentGameArray, IEnumerable<Layout> baseLayouts)
        {
            List<int> movePositions = new List<int>(baseLayouts.Count());

            foreach (var current in baseLayouts)
            {
                var array = current.Positions.ToArray();

                for (int i = 0; i < 9; i++)
                {
                    if (array[i] != currentGameArray[i])
                    {
                        movePositions.Add(i);
                        break;
                    }
                }
            }

            return movePositions;
        }
    }
}
