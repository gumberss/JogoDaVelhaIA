using FluentAssertions;
using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Services.WinnerModes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JogoDaVelhaIATest.Domain
{
    [TestClass]
    public class VerticalWinnerTest
    {
        private Layout _layout;
        private VerticalWinnerChecker _verticalWinner;

        public VerticalWinnerTest()
        {
            _verticalWinner = new VerticalWinnerChecker();
        }

        [TestMethod]
        public void Deveria_avaliar_como_vitoria_quando_hover_vitoria_na_primeira_coluna()
        {
            _layout = new Layout(new[] { 'X', '\0', '\0',
                                         'X', '\0', '\0',
                                         'X', '\0', '\0', });

            var result = _verticalWinner.Evaluate(_layout);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Deveria_avaliar_como_vitoria_quando_hover_vitoria_na_segunda_coluna()
        {
            _layout = new Layout(new[] { '\0', 'X', '\0',
                                         '\0', 'X', '\0',
                                         '\0', 'X', '\0', });
            var result = _verticalWinner.Evaluate(_layout);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Deveria_avaliar_como_vitoria_quando_hover_vitoria_na_terceira_coluna()
        {
            _layout = new Layout(new[] { '\0', '\0', 'X',
                                         '\0', '\0', 'X',
                                         '\0', '\0', 'X',});

            var result = _verticalWinner.Evaluate(_layout);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Nao_deveria_avaliar_como_vitoria_quando_nao_exitir_vitoria_na_vertical()
        {
            _layout = new Layout(new[] { '\0', 'X', '\0',
                                         '\0', 'X', '\0',
                                         'X', '\0', '\0',});

            var result = _verticalWinner.Evaluate(_layout);

            result.Should().BeFalse();
        }
    }
}
