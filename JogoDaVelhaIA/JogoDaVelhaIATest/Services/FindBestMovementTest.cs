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
    public class FindBestMovementTest
    {
        private FindBestMovement _findBestMovement;
        private Guid _currentGameId;
        private List<Layout> _memory;
        private ComputerBrain _computerBrain;

        public FindBestMovementTest()
        {
            _findBestMovement = new FindBestMovement(null);

            _currentGameId = Guid.NewGuid();

            _memory = new List<Layout>() {

                new Layout(new[] {  'X','O','\0',
                                    '\0','\0','\0',
                                    '\0','\0','\0',}, _currentGameId, 2,1),

                new Layout(new[] {  'X','O','\0',
                                    '\0','X','\0',
                                    '\0','\0','\0',}, _currentGameId, 3,4),

                new Layout(new[] {  'X','O','O',
                                    '\0','X','\0',
                                    '\0','\0','\0',}, _currentGameId, 4,2),

                new Layout(new[] {  'X','O','O',
                                    '\0','X','\0',
                                    '\0','\0','X',}, _currentGameId, 5, 8),
            };

            _memory.ForEach(i => i.Win(true));

            _computerBrain = new ComputerBrain(_memory);
        }

        [TestMethod]
        public void Deveria_retornar_melhor_posicao_com_base_no_historico()
        {
            var currentGame = new Layout();

            currentGame.Move(0, 'X');
            currentGame.Move(1, 'O');

            var position = _findBestMovement.FindPosition(_computerBrain, currentGame);

            position.Should().Be(4);

        }

        [TestMethod]
        public void Deveria_retornar_posicao_com_maior_probabilidade_de_vitoria_com_base_no_historico_quando_dois_movimentos_possiveis()
        {
            var newGameId = Guid.NewGuid();

            var otherGame = new List<Layout>() {

                new Layout(new[] {  'X','O','\0',
                                    '\0','\0','\0',
                                    '\0','\0','\0',}, newGameId, 2,1),

                new Layout(new[] {  'X','O','\0',
                                    'X','\0','\0',
                                    '\0','\0','\0',}, newGameId, 3,3),

                new Layout(new[] {  'X','O','O',
                                    'X','\0','\0',
                                    '\0','\0','\0',}, newGameId, 4,2),

                new Layout(new[] {  'X','O','O',
                                    'X','\0','\0',
                                    'X','\0','\0',}, newGameId, 5, 6),
            };
            otherGame.ForEach(i => i.Win(true));

            _memory.AddRange(otherGame);
            _memory.AddRange(otherGame);

            var currentGame = new Layout();

            currentGame.Move(0, 'X');
            currentGame.Move(1, 'O');

            var position = _findBestMovement.FindPosition(_computerBrain, currentGame);

            position.Should().Be(3, because: "Dois dos três jogos vencedores jogaram na posição 3 neste movimento");
        }

        [TestMethod]
        public void Deveria_retornar_posicao_com_base_em_falhas_quando_probabilidade_de_falha_maior_que_de_vitoria()
        {
            var newGameId = Guid.NewGuid();

            var otherGame = new List<Layout>() {

                new Layout(new[] {  'X','O','\0',
                                    '\0','\0','\0',
                                    '\0','\0','\0',}, newGameId, 2,1),

                new Layout(new[] {  'X','O','\0',
                                    '\0','X','\0',
                                    '\0','\0','\0',}, newGameId, 3,4),

                new Layout(new[] {  'X','O','O',
                                    '\0','X','\0',
                                    '\0','\0','\0',}, newGameId, 4,2),

                new Layout(new[] {  'X','O','O',
                                    '\0','X','\0',
                                    'X','\0','\0',}, newGameId, 5, 6),
            };

            otherGame.ForEach(i => i.Win(false));

            _memory.AddRange(otherGame);
            _memory.AddRange(otherGame);

            var currentGame = new Layout();

            currentGame.Move(0, 'X');
            currentGame.Move(1, 'O');

            var position = _findBestMovement.FindPosition(_computerBrain, currentGame);

            position.Should().NotBe(4, because: "Pois falhou quando colocou nesse");
        }
    }
}
