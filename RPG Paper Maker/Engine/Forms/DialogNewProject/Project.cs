using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker.Dialog
{
    public class Project
    {
        public string ProjectName { get; set; }
        public string DirPath { get; set; }

        public Project(string p, string d)
        {
            ProjectName = p;
            DirPath = d;
        }
    }
}
