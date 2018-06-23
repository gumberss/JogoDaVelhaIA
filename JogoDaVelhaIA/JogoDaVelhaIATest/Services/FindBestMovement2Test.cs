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
    public class FindBestMovement2Test
    {
        private FindBestMovement2 _findBestMovement;
        private Guid _currentGameId;
        private List<Layout> _memory;
        private ComputerBrain _computerBrain;

        public FindBestMovement2Test()
        {
            _findBestMovement = new FindBestMovement2();

            _currentGameId = Guid.NewGuid();

            _memory = new List<Layout>() {

                new Layout(new[] {  'X','O','\0',
                                    '\0','\0','\0',
                                    '\0','\0','\0',}, _currentGameId, 0,1),

                new Layout(new[] {  'X','O','\0',
                                    '\0','X','\0',
                                    '\0','\0','\0',}, _currentGameId, 1,4),

                new Layout(new[] {  'X','O','O',
                                    '\0','X','\0',
                                    '\0','\0','\0',}, _currentGameId, 2,2),

                new Layout(new[] {  'X','O','O',
                                    '\0','X','\0',
                                    '\0','\0','X',}, _currentGameId, 3, 8),
            };

            _computerBrain = new ComputerBrain(_memory);
        }

        [TestMethod]
        public void Deveria_retornar_melhor_posicao_com_base_no_historico()
        {
            var currentGame = new Layout(new[] {'X','O','\0',
                                                '\0','\0','\0',
                                                '\0','\0','\0'}, Guid.NewGuid(), 0, 1);

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
                                    '\0','\0','\0',}, newGameId, 0,1),

                new Layout(new[] {  'X','O','\0',
                                    'X','\0','\0',
                                    '\0','\0','\0',}, newGameId, 1,3),

                new Layout(new[] {  'X','O','O',
                                    'X','\0','\0',
                                    '\0','\0','\0',}, newGameId, 2,2),

                new Layout(new[] {  'X','O','O',
                                    'X','\0','\0',
                                    'X','\0','\0',}, newGameId, 3, 6),
            };

            _memory.AddRange(otherGame);
            _memory.AddRange(otherGame);

            var currentGame = new Layout(new[] {'X','O','\0',
                                                '\0','\0','\0',
                                                '\0','\0','\0'}, Guid.NewGuid(), 0, 1);

            var position = _findBestMovement.FindPosition(_computerBrain, currentGame);

            position.Should().Be(3, because: "Dois dos três jogos vencedores jogaram na posição 3 neste movimento");

        }
    }
}
