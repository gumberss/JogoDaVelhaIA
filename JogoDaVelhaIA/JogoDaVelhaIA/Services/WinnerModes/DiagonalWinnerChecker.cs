using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JogoDaVelhaIA.Domain;

namespace JogoDaVelhaIA.Services.WinnerModes
{
    public class DiagonalWinnerChecker : WinnerChecker
    {
        public override bool Evaluate(Layout layout)
        {
            var positions = layout.Positions.ToArray();

            if (positions[4] == '\0') return false;

            return (positions[0] == positions[4] && positions[0] == positions[8])
                || (positions[2] == positions[4] && positions[2] == positions[6]);
        }
    }
}
