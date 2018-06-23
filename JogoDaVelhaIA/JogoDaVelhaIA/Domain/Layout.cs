using JogoDaVelhaIA.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Domain
{
    public class Layout
    {
        private Guid _gameId;

        private int _turn;

        private int _turnPosition;

        public char? _player1Icon;
        public char? _player2Icon;

        public bool ComputerWin { get; private set; }

        public char? Player1Icon { get { return _player1Icon; } }
        public char? Player2Icon { get { return _player2Icon; } }

        public Guid GameId { get { return _gameId; } }

        public int Turn { get { return _turn; } }

        public int TurnPosition { get { return _turnPosition; } }

        private char[] _positions;

        public IEnumerable<char> Positions
        {
            get { return _positions; }
        }

        public Layout(LayoutDTO dto)
        {

            ComputerWin = dto.ComputerWin;
            _gameId = dto.GameId;
            _positions = dto.Positions.ToArray();
            _turn = dto.Turn;
            _turnPosition = dto.TurnPosition;
        }

        public Layout(Layout layout)
        {

            ComputerWin = layout.ComputerWin;
            _gameId = layout.GameId;
            _positions = layout.Positions.ToArray();
            _turn = layout.Turn;
            _turnPosition = layout.TurnPosition;
        }

        public Layout(IEnumerable<char> positions, Guid gameId, int turn, int turnPosition) : this(positions)
        {
            _turn = turn;
            _gameId = gameId;
            _turnPosition = turnPosition;
        }

        public Layout(IEnumerable<char> positions)
        {
            _gameId = Guid.NewGuid();

            if (positions.Count() != 9)
                throw new ArgumentException("O array deve possuir 9 posições, que são as posições do jogo da velha");

            _positions = positions.ToArray();
        }

        public Layout()
        {
            _gameId = Guid.NewGuid();
            _positions = new char[] { '\0', '\0', '\0',
                                      '\0', '\0', '\0',
                                      '\0', '\0', '\0' };
        }

        public void Move(int position, char icon)
        {
            if (!IsValidPosition(position))
                throw new ArgumentException("Posição inválida");

            if (!IsEmptyPosition(position))
                throw new ArgumentException("Posição informada já preenchida");

            if (_player1Icon == null)
                _player1Icon = icon;
            else if (_player2Icon == null)
                _player2Icon = icon;

            _positions[position] = icon;
            _turnPosition = position;
            _turn++;
        }

        internal bool AllFilled()
        {
            return _positions.All(i => i != '\0');
        }

        private bool IsEmptyPosition(int position)
        {
            return _positions[position] == '\0';
        }

        private static bool IsValidPosition(int position)
        {
            return position <= 8 && position >= 0;
        }

        public bool CanMove(int position)
        {
            return IsValidPosition(position) && IsEmptyPosition(position);
        }

        public void Win(bool computerWin)
        {
            ComputerWin = computerWin;
        }
    }
}
