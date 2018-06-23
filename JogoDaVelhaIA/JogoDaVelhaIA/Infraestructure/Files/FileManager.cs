using JogoDaVelhaIA.Domain.Interfaces.Infraestructure;
using System;
using System.IO;

namespace JogoDaVelhaIA.Infraestructure.Files
{
    public class FileManager : IDataManager
    {
        public String GetData(String fileName)
        {
            CreateFile(fileName);

            return File.ReadAllText(fileName);
        }

        public void SaveData(String fileName, String data)
        {
            CreateFile(fileName);

            File.WriteAllText(fileName, data);
        }

        private void CreateFile(string fileName)
        {
            if (!File.Exists(fileName))
                File.Create(fileName);
        }
    }
}
