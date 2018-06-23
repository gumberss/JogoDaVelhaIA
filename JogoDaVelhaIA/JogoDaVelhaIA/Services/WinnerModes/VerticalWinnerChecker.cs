using JogoDaVelhaIA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Services.WinnerModes
{
    public class VerticalWinnerChecker : WinnerChecker
    {
        public override bool Evaluate(Layout layout)
        {
            var positions = layout.Positions.ToArray();

            for (int i = 0; i < 3; i++)
            {
                if (positions[i] == '\0') continue;

                if (positions[i] == positions[i + 3] && positions[i] == positions[i + 6])
                    return true;
            }

            return false;
        }
    }
}
