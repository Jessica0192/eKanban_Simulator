/*
* FILE          : Workstation.cs
* PROJECT       : SENG3070 - Project Kanban
* PROGRAMMER    : Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-24
* DESCRIPTION:
*    This file contains Workstation class for the structure of the workstation
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workstation_Simulation
{
    public class WorkStation
    {
        public int ID;
        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
