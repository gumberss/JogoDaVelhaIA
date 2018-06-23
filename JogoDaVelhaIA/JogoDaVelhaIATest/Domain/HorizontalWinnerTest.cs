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
    public class HorizontalWinnerTest
    {
        private Layout _layout;
        private HorizontalWinnerChecker _horizontalWinner;

        public HorizontalWinnerTest()
        {
            _horizontalWinner = new HorizontalWinnerChecker();
        }

        [TestMethod]
        public void Deveria_avaliar_como_vitoria_quando_hover_vitoria_na_primeira_linha()
        {
            _layout = new Layout(new[] { 'X', 'X', 'X',
                                        '\0', '\0', '\0',
                                        '\0', '\0', '\0', });

            var result = _horizontalWinner.Evaluate(_layout);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Deveria_avaliar_como_vitoria_quando_hover_vitoria_na_segunda_linha()
        {
            _layout = new Layout(new[] { '\0', '\0', '\0',
                                         'X', 'X', 'X',
                                        '\0', '\0', '\0', });

            var result = _horizontalWinner.Evaluate(_layout);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Deveria_avaliar_como_vitoria_quando_hover_vitoria_na_terceira_linha()
        {
            _layout = new Layout(new[] { '\0', '\0', '\0',
                                         '\0', '\0', '\0',
                                         'X', 'X', 'X',});

            var result = _horizontalWinner.Evaluate(_layout);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Nao_deveria_avaliar_como_vitoria_quando_nao_exitir_vitoria_na_horizontal()
        {
            _layout = new Layout(new[] { '\0', '\0', '\0',
                                         '\0', 'X', '\0',
                                         'X', '\0', 'X',});

            var result = _horizontalWinner.Evaluate(_layout);

            result.Should().BeFalse();
        }
    }
}
