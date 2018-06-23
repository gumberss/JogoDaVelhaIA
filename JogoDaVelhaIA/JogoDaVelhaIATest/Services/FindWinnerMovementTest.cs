using FluentAssertions;
using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Services.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JogoDaVelhaIATest.Services
{
    [TestClass]
    public class FindWinnerMovementTest
    {
        private FindWinnerMovement _findWinnerMovement;
        private ComputerBrain _computerBrain;

        public FindWinnerMovementTest()
        {
            _findWinnerMovement = new FindWinnerMovement();
        }

        [TestMethod]
        public void Deveria_retornar_posicao_para_vitoria_quando_computador_encontrar_jogada_igual_a_atual_com_posicao_de_vitoria_livre()
        {
            var currentGame = new Layout();
            currentGame.Move(0, 'x');
            currentGame.Move(1, 'o');
            currentGame.Move(8, 'x');
            currentGame.Move(2, 'o');
            
            var newPosition = _findWinnerMovement.FindPosition(_computerBrain, currentGame);

            newPosition.Should().Be(4);
        }

        [TestMethod]
        public void Deveria_retornar_posicao_para_vitoria_quando_livre_para_vencer_e_para_bloquear_vitoria()
        {
            var currentGame = new Layout();
            currentGame.Move(4, 'x');
            currentGame.Move(1, 'o');
            currentGame.Move(5, 'x');
            currentGame.Move(2, 'o');

            var newPosition = _findWinnerMovement.FindPosition(_computerBrain, currentGame);

            newPosition.Should().Be(3);
        }

        [TestMethod]
        public void Deveria_retornar_posicao_para_impedir_a_vitoria_do_adversario_quando_impossibilitado_de_vencer_mas_adversario_nao()
        {
            var currentGame = new Layout();
            currentGame.Move(4, 'x');
            currentGame.Move(1, 'o');
            currentGame.Move(6, 'x');
            currentGame.Move(2, 'o');

            var newPosition = _findWinnerMovement.FindPosition(_computerBrain, currentGame);

            newPosition.Should().Be(0);
        }
    }
}
