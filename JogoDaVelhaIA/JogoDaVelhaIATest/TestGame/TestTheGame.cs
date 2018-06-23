using JogoDaVelhaIA.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIATest.TestGame
{
    [TestClass]
    public class TestTheGame
    {

        [TestMethod]
        public void Deveria_criar_inteligencia()
        {
            using (TicTacToe ticTacToe = new TicTacToe('O', 'X'))
            {
                ticTacToe.AutomaticPlay();
            }
        }

    }
}
