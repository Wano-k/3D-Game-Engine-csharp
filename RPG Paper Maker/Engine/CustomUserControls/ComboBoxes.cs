using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    class ComboBoxComparaison : ComboBox
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public ComboBoxComparaison()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // -------------------------------------------------------------------
        // InitValues
        // -------------------------------------------------------------------

        public void InitValues()
        {
            Items.Add("= (Equal to)");
            Items.Add(">= (Equal to or greater than)");
            Items.Add("<= (Equal to or less than)");
            Items.Add("< (Less than)");
            Items.Add("> (Greater than)");
            Items.Add("!= (Not equal to)");
        }

        // -------------------------------------------------------------------
        // GetResult
        // -------------------------------------------------------------------

        public Comparaison GetResult()
        {
            return (Comparaison)SelectedIndex;
        }
    }

    class ComboBoxMeasure : ComboBox
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public ComboBoxMeasure()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // -------------------------------------------------------------------
        // InitValues
        // -------------------------------------------------------------------

        public void InitValues()
        {
            Items.Add("%");
            Items.Add("Unit");
        }

        // -------------------------------------------------------------------
        // GetResult
        // -------------------------------------------------------------------

        public Measure GetResult()
        {
            return (Measure)SelectedIndex;
        }
    }
}
