using Microsoft.VisualStudio.TestTools.UnitTesting;
using VaskEnTidLibrary.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLibrary.Models.Machines;
using System.Diagnostics;

namespace VaskEnTidLibrary.Repos.Tests
{
    [TestClass()]
    public class MachineRepoTests
    {

        [TestMethod()]
        public void GetAllMachinesTest()
        {
            //Arrange
            MachineRepo repo = new MachineRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
            List<Machine> expectedMachines = new List<Machine>
            {
                new Washer(1),
                new Washer(2),
                new Washer(3),
                new Dryer(4),
                new Dryer(5),
                new Dryer(6),
                new Roller(7),
                new Roller(8),
                new Roller(9),
            };
          
            //Act
            List<Machine> actMachines = repo.GetAllMachines();

            //Assert
            for(int i = 0; i < expectedMachines.Count; i++)
            {
                Assert.AreEqual(expectedMachines[i].MachineID, actMachines[i].MachineID);
                Assert.AreEqual(expectedMachines[i].Type, actMachines[i].Type);
            }
        }

        [TestMethod()]
        public void GetMachineByIdTest()
        {
            //Arrange
            MachineRepo repo = new MachineRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
            List<Machine> allMachines = repo.GetAllMachines();
            Machine expectedMachine = new Washer(1);

            //Act
            Machine actMachine = repo.GetMachineById(1);

            //Assert
            Assert.AreEqual(expectedMachine.MachineID, actMachine.MachineID);
        }

    }
}