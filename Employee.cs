using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workstation_Simulation
{
    public class Employee
    {
        public int ID;
        public string Name;
        public string Level;

        public override string ToString()
        {
            return Name + "/" + Level;
        }
    }
}
