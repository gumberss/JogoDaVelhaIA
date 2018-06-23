using FluentAssertions;
using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Services.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIATest.Services
{
    [TestClass]
    public class FindRandomMovementTest
    {
        private FindRandomMovement _findRandomMovement;

        public FindRandomMovementTest()
        {
            _findRandomMovement = new FindRandomMovement(null);
        }

        [TestMethod]
        public void Deveria_encontrar_posicao_aleatoria_quando_apenas_a_primeira_casa_estiver_disponivel()
        {
            var currentGame = new Layout(new[] { '\0', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', });

            var position = _findRandomMovement.FindPosition(null, currentGame);

            position.Should().Be(0);
        }


        [TestMethod]
        public void Deveria_encontrar_posicao_aleatoria_quando_apenas_a_ultima_casa_estiver_disponivel()
        {
            var currentGame = new Layout(new[] { 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '\0', });

            var position = _findRandomMovement.FindPosition(null, currentGame);

            position.Should().Be(8);
        }
    }
}
