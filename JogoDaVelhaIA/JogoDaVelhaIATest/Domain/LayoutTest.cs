using FluentAssertions;
using JogoDaVelhaIA.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIATest.Domain
{
    [TestClass]
    public class LayoutTest
    {
        private readonly char[] _positions;
        private Layout _layout;

        public LayoutTest()
        {
            _positions = new char[9] {'\0','\0','\0',
                                      '\0','\0','\0',
                                      '\0','\0','\0' };
            _layout = new Layout(_positions);
        }

        [TestMethod]
        public void Deveria_lancar_excecao_quando_posicao_informada_for_maior_do_que_o_tamanho_do_campo_do_jogo()
        {
            int position = 9;

            Action action = () => _layout.Move(position, 'X');

            action.Should().ThrowExactly<ArgumentException>().And.Message.Should().Be("Posição inválida");
        }

        [TestMethod]
        public void Deveria_lancar_excecao_quando_posicao_informada_for_menor_do_que_o_tamanho_do_campo_do_jogo()
        {
            int position = -1;

            Action action = () => _layout.Move(position, 'X');

            action.Should().ThrowExactly<ArgumentException>().And.Message.Should().Be("Posição inválida");
        }

        [TestMethod]
        public void Nao_deveria_lancar_excecao_quando_posicao_informada_for_o_limite_inferior_do_tamanho_do_campo_do_jogo()
        {
            int position = 0;

            Action action = () => _layout.Move(position, 'X');

            action.Should().NotThrow<ArgumentException>();
        }

        [TestMethod]
        public void Nao_deveria_lancar_excecao_quando_posicao_informada_for_o_limite_superior_do_tamanho_do_campo_do_jogo()
        {
            int position = 8;

            Action action = () => _layout.Move(position, 'X');

            action.Should().NotThrow<ArgumentException>();
        }

        [TestMethod]
        public void Deveria_lancar_excecao_quando_efetuar_jogada_em_uma_posicao_ja_preenchida()
        {
            _layout = new Layout(new char[] { 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X' });

            int position = 8;

            Action action = () => _layout.Move(position, 'X');

            action.Should().ThrowExactly<ArgumentException>().And.Message.Should().Be("Posição informada já preenchida");
        }

        [TestMethod]
        public void Deveria_preencher_posicao_quando_jogada_valida()
        {
            _layout = new Layout(new char[] { 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '\0' });

            int position = 8;

            _layout.Move(position, 'X');
            
            _layout.Positions.Last().Should().Be('X');
        }
    }
}
