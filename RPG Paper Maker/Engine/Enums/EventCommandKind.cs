using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public enum EventCommandKind
    {
        None,
        ShowMessage,
        ShowChoices,
        InputNumber,
        ChangeWindowOptions,
        ChangeSwitches,
        Conditions
    }
}
