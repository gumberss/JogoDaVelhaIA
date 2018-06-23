using JogoDaVelhaIA.Domain;
using System.Collections.Generic;

namespace JogoDaVelhaIA.Services.WinnerModes
{
    public abstract class WinnerChecker
    {
        private static List<WinnerChecker> _winnerCheckers;

        public abstract bool Evaluate(Layout layout);

        public static bool CheckAll(Layout layout)
        {
            if(_winnerCheckers == null)
            {
                _winnerCheckers = new List<WinnerChecker>
                {
                    new HorizontalWinnerChecker(),
                    new VerticalWinnerChecker(),
                    new DiagonalWinnerChecker()
                };
            }

            foreach (var current in _winnerCheckers)
            {
                if (current.Evaluate(layout)) return true;
            }

            return false;
        }

    }
}
