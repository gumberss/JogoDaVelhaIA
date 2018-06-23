using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Domain
{
    public class ComputerBrain
    {
        private List<Layout> _memory;

        public IEnumerable<Layout> Memory { get { return _memory; } }

        public ComputerBrain(IEnumerable<Layout> memory)
        {
            _memory = memory.ToList();
        }

        public ComputerBrain()
        {
            _memory = new List<Layout>();
        }

        public void AddNewGame(List<Layout> newGame)
        {
            _memory.AddRange(newGame);
        }

    }
}
