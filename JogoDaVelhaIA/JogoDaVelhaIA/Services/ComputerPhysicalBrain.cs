using JogoDaVelhaIA.Domain;
using JogoDaVelhaIA.Domain.Interfaces.Infraestructure;
using JogoDaVelhaIA.DTOs;
using JogoDaVelhaIA.Infraestructure.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelhaIA.Services
{
    public class ComputerPhysicalBrain
    {
        private IDataManager _dataManager;

        public ComputerPhysicalBrain()
        {
            _dataManager = new FileManager();
        }

        public void SaveMemory(ComputerBrain brain)
        {
            var layoutDTOs = brain.Memory.Select(layout => new LayoutDTO(layout));

            var memoryData = JsonConvert.SerializeObject(layoutDTOs);

            _dataManager.SaveData(GetMemoryPath(), memoryData);
        }

        public ComputerBrain LoadMemory()
        {
            var memoryData = _dataManager.GetData(GetMemoryPath());

            if (String.IsNullOrEmpty(memoryData))
            {
                return new ComputerBrain();
            }

            var memory = JsonConvert.DeserializeObject<List<LayoutDTO>>(memoryData);

            return new ComputerBrain(memory.Select(dto => new Layout(dto)));
        }

        private String GetMemoryPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            return Path.Combine(currentDirectory, "ComputerMemory.txt");
        }
    }
}
