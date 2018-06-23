using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Services.Finders
{
    public abstract class FindBase 
    {
        private FindBase _next;

        public FindBase(FindBase next)
        {
            _next = next;
        }

        public int FindPosition(ComputerBrain computerBrain, Layout currentGame)
        {
            var findedPosision = Find(computerBrain, currentGame);

            if (findedPosision >= 0 || _next == null) return findedPosision;

            return _next.FindPosition(computerBrain, currentGame);
        }

        protected abstract int Find(ComputerBrain computerBrain, Layout currentGame);
    }
}
