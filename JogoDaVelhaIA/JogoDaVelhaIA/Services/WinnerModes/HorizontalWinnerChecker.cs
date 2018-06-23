using JogoDaVelhaIA.Domain;
using System.Linq;

namespace JogoDaVelhaIA.Services.WinnerModes
{
    public class HorizontalWinnerChecker : WinnerChecker
    {
        public override bool Evaluate(Layout layout)
        {
            var positions = layout.Positions.ToArray();

            for (int i = 0; i < 9; i += 3)
            {
                if (positions[i] == '\0') continue;

                if (positions[i] == positions[i + 1] && positions[i] == positions[i + 2])
                    return true;
            }

            return false;
        }
    }
}
