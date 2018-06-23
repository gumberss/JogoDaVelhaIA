using JogoDaVelhaIA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.DTOs
{
    public class LayoutDTO
    {
        public Guid GameId { get; set; }

        public Boolean ComputerWin { get; set; }

        public IEnumerable<char> Positions { get; set; }

        public int Turn { get; set; }

        public int TurnPosition { get; set; }

        public LayoutDTO()
        {

        }

        public LayoutDTO(Layout layout)
        {
            ComputerWin = layout.ComputerWin;
            GameId = layout.GameId;
            Positions = layout.Positions;
            Turn = layout.Turn;
            TurnPosition = layout.TurnPosition;
        }
    }

}
