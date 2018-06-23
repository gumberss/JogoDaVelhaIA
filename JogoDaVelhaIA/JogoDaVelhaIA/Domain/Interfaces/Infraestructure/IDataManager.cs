using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Domain.Interfaces.Infraestructure
{
    public interface IDataManager
    {
        String GetData(String fileName);

        void SaveData(String fileName, String data);
    }
}
