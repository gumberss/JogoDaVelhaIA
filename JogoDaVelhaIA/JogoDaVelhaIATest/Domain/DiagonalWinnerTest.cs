using FluentAssertions;
using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Services.WinnerModes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIATest.Domain
{
    [TestClass]
    public class DiagonalWinnerTest
    {
        private Layout _layout;
        private DiagonalWinnerChecker _diagonalWinner;

        public DiagonalWinnerTest()
        {
            _diagonalWinner = new DiagonalWinnerChecker();
        }

        [TestMethod]
        public void Deveria_avaliar_como_vitoria_quando_hover_vitoria_primeira_diagonal()
        {
            _layout = new Layout(new[] { 'X', '\0', '\0',
                                         '\0', 'X', '\0',
                                         '\0', '\0', 'X',});

            var result = _diagonalWinner.Evaluate(_layout);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Deveria_avaliar_como_vitoria_quando_hover_vitoria_segunda_diagonal()
        {
            _layout = new Layout(new[] { '\0', '\0', 'X',
                                         '\0', 'X', '\0',
                                         'X', '\0', '\0',});

            var result = _diagonalWinner.Evaluate(_layout);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Nao_deveria_avaliar_como_vitoria_quando_hover_diagonal_com_vitoria()
        {
            _layout = new Layout(new[] { 'X', '\0', '\0',
                                         '\0', '\0', '\0',
                                         '\0', '\0', 'X',});

            var result = _diagonalWinner.Evaluate(_layout);

            result.Should().BeFalse();
        }
    }
}
