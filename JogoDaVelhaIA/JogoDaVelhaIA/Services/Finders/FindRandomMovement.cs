using System;
using JogoDaVelhaIA.Domain;

namespace JogoDaVelhaIA.Services.Finders
{
    public class FindRandomMovement : FindBase
    {
        private readonly int CENTER = 4;

        private readonly int TOPLEFT = 0;
        private readonly int TOPRIGHT = 2;
        private readonly int DOWNLEFT = 6;
        private readonly int DOWNRIGHT = 8;

        public FindRandomMovement(FindBase next) : base(next) { }

        protected override int Find(ComputerBrain computerBrain, Layout currentGame)
        {
            if (currentGame.CanMove(CENTER)) return CENTER;

            if (currentGame.CanMove(TOPLEFT)) return TOPLEFT;
            if (currentGame.CanMove(TOPRIGHT)) return TOPRIGHT;
            if (currentGame.CanMove(DOWNLEFT)) return DOWNLEFT;
            if (currentGame.CanMove(DOWNRIGHT)) return DOWNRIGHT;

            for (int i = 0; i < 9; i++)
            {
                if (currentGame.CanMove(i)) return i;
            }

            throw new InvalidOperationException("Eita! não é possivel mover para qualquer posição");
        }
    }
}
