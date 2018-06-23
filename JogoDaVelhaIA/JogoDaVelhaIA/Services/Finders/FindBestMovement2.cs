using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Services.WinnerModes;

namespace JogoDaVelhaIA.Services.Finders
{
    public class FindBestMovement2 : FindBase
    {
        public FindBestMovement2() : base(null) { }

        public FindBestMovement2(FindBase next) : base(next) { }

        protected override int Find(ComputerBrain computerBrain, Layout currentGame)
        {
            var afterWinners = computerBrain.Memory.Where(i => i.Turn >= currentGame.Turn && WinnerChecker.CheckAll(i));

            List<int?> possibleMovements = new List<int?>(afterWinners.Count());

            foreach (var afterWinner in afterWinners)
            {
                var nextTurn = computerBrain.Memory.FirstOrDefault(i => i.GameId == afterWinner.GameId && i.Turn == currentGame.Turn + 1);

                if (currentGame.CanMove(nextTurn.TurnPosition))
                {
                    possibleMovements.Add(nextTurn.TurnPosition);
                }
            }

            var bestMovement = possibleMovements.GroupBy(i => i).OrderByDescending(i => i.Count()).FirstOrDefault();

            return bestMovement?.First().Value ?? -1;
        }
    }
}
