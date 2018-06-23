using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Services.Finders;
using JogoDaVelhaIA.Services.WinnerModes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogoDaVelhaIA.Services
{
    public class TicTacToe : IDisposable
    {
        private List<Layout> _currentGameHistory;

        private Layout _currentGame;
        private readonly char _playerIcon;
        private readonly char _computerIcon;
        private readonly ComputerPhysicalBrain _computerPhysicalBrain;
        private readonly ComputerBrain _computerBrain;
        private FindBestMovement _inteligence;

        public TicTacToe(char playerIcon, char computerIcon)
        {
            _playerIcon = playerIcon;
            _computerIcon = computerIcon;

            _computerPhysicalBrain = new ComputerPhysicalBrain();

            _computerBrain = _computerPhysicalBrain.LoadMemory();

            CreateInteligence();

            Reset();
        }

        public void Reset()
        {
            _currentGameHistory = new List<Layout>();
            _currentGame = new Layout();
        }

        public bool PlayerTurn(int position)
        {
            _currentGame.Move(position, _playerIcon);

            _currentGameHistory.Add(_currentGame);

            var playerWin = CheckWin(false);

            if (playerWin) return true;

            var computerWin = ComputerTurn();

            return computerWin;
        }

        public bool ComputerTurn()
        {
            int bestComputerPosition = _inteligence.FindPosition(_computerBrain, _currentGame);

            _currentGame.Move(bestComputerPosition, _computerIcon);

            _currentGameHistory.Add(_currentGame);

            return CheckWin(true);
        }



        private bool CheckWin(bool computerTurn)
        {
            if (Win())
            {
                Finish(computerTurn);

                Reset();

                return true;
            }

            if (_currentGame.AllFilled())
            {
                Finish(false);

                Reset();
                throw new ExecutionEngineException("Velha");
            }

            return false;
        }

        private void Finish(bool computerWin)
        {
            _currentGameHistory.ForEach(i => i.Win(computerWin));
            _computerBrain.AddNewGame(_currentGameHistory);
            
        }

        private bool Win()
        {
            return WinnerChecker.CheckAll(_currentGame);
        }

        private void CreateInteligence()
        {
            _inteligence = new FindBestMovement();
        }

        public void AutomaticPlay()
        {
            for (int i = 0; i < 1000; i++)        
            {
                try
                {
                    for (int j = 0; j < 9; j++)
                    {
                        int position = FindPosition();

                        _currentGame.Move(position, _playerIcon);

                        _currentGameHistory.Add(new Layout(_currentGame));

                        if (CheckWin(false)) break;

                        _currentGame.Move(_inteligence.FindPosition(_computerBrain, _currentGame), _computerIcon);

                        _currentGameHistory.Add(new Layout(_currentGame));

                        if (CheckWin(true)) break;
                    }
                }
                catch (ExecutionEngineException ex)
                {
                    //velha
                }
            }
        }

        private int FindPosition(int oldPosition = -1)
        {
            int position = -1;

            if (oldPosition == -1)
            {
                Random r = new Random();

                position = r.Next(0, 10);
            }
            else
            {
                if (oldPosition == 9)
                    position = 0;
                else
                    position = ++oldPosition;
            }

            if (!_currentGame.CanMove(position))
                return FindPosition(position);

            return position;
        }

        public void Dispose()
        {
            _computerPhysicalBrain.SaveMemory(_computerBrain);
        }
    }
}
