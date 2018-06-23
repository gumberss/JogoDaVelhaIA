using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Domain.Interfaces.Services
{
    public interface IFindMovement
    {
        int Find(ComputerBrain computerBrain);
    }
}
