using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Domain.Interfaces.Services;
using JogoDaVelhaIA.Services.WinnerModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Services.Finders
{
    public class FindWinnerMovement : FindBase
    {

        public FindWinnerMovement() : base(null)
        {

        }

        public FindWinnerMovement(FindBase next) : base(next)
        {
        }

        protected override int Find(ComputerBrain computerBrain, Layout currentGame)
        {
            var currentGameArray = currentGame.Positions.ToArray();

            var rivalIcon = currentGameArray[currentGame.TurnPosition];

            var computerIcon = currentGame.Player1Icon == rivalIcon ? currentGame.Player2Icon : currentGame.Player1Icon;

            if (computerIcon == null) return -1;

            var computerWinPosition = Win(currentGame, computerIcon.Value);

            if (computerWinPosition != -1) return computerWinPosition;

            var rivalWillWinPosition = Win(currentGame, rivalIcon);

            return rivalWillWinPosition;
        }

        private int Win(Layout currentGame, char icon)
        {
            for (int i = 0; i < 9; i++)
            {
                if (currentGame.CanMove(i))
                {
                    var testLayout = new Layout(currentGame.Positions);

                    testLayout.Move(i, icon);

                    var win = WinnerChecker.CheckAll(testLayout);

                    if (win) return i;
                }
            }

            return - 1;
        }
    }
}
