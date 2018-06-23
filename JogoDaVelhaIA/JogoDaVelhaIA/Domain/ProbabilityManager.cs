using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Domain
{
    public class ProbabilityManager
    {
        List<Probability> _victoryProbabilities;

        List<Probability> _lostProbabilities;

        public ProbabilityManager()
        {
            _victoryProbabilities = new List<Probability>();
            _lostProbabilities = new List<Probability>();

            for (int i = 0; i < 9; i++)
            {
                _victoryProbabilities.Add(new Probability(i));
                _lostProbabilities.Add(new Probability(i));
            }
        }

        public void Increment(int position, bool win)
        {
            var correctList = win ? _victoryProbabilities : _lostProbabilities;

            var currentProbability = correctList.First(i => i.Position == position);

            currentProbability.Increment();
        }

        public float GetPercent(int position, bool win)
        {
            var correctList = win ? _victoryProbabilities : _lostProbabilities;

            var currentProbability = correctList.FirstOrDefault(i => i.Position == position);

            return currentProbability?.Percent(TotalElements()) * correctList.Count ?? 0;
        }

        private int TotalElements()
        {
            return _victoryProbabilities.Sum(i => i.Quantity) + _lostProbabilities.Sum(i => i.Quantity);
        }

        public bool ShouldMoveToWin()
        {
            return _victoryProbabilities.Sum(i => i.Quantity) >= _lostProbabilities.Sum(i => i.Quantity);
        }

        public int BestPosition(bool moveToWin)
        {
            var correctList = moveToWin ? _victoryProbabilities : _lostProbabilities;

            var movements = correctList.OrderBy(i => i.Quantity);

            if (moveToWin)
                return movements.LastOrDefault()?.Position ?? 0;
            else
                return movements.FirstOrDefault()?.Position ?? 0;
        }

    }
}
