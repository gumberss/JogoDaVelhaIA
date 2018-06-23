using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Domain
{
    public class Probability
    {
        public Probability(int position)
        {
            Position = position;

            Quantity = 0;
        }

        public int Position { get; private set; }

        public int Quantity { get; private set; }

        public void Increment()
        {
            Quantity++;
        }

        public float Percent(int totalElements)
        {
            return Quantity / totalElements;
        }
    }
}
