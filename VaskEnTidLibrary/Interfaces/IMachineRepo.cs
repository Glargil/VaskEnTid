using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLibrary.Models.Machines;

namespace VaskEnTidLibrary.Interfaces
{
    public interface IMachineRepo
    {
        List<Machine> GetAllMachines();
        Machine GetMachineById(int id);
        void AddMachine(Machine machine);
        void UpdateMachine(Machine machine);
        void DeleteMachine(int machineId);

    }
}
