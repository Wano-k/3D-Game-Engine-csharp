using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class DialogCondition : Form
    {
        private List<Control[]> HeroesSelectionItems = new List<Control[]>();
        private List<Control[]> HeroesConditionItems = new List<Control[]>();


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogCondition()
        {
            InitializeComponent();

            // Remove not visible pages
            tabControl1.TabPages.Remove(tabPageEnemies);
            tabControl1.TabPages.Remove(tabPageEvents);
            tabControl1.TabPages.Remove(tabPageOthers);

            // All tabs for enable
            HeroesSelectionItems.Add(new Control[] { comboBoxAllHeroes });
            HeroesSelectionItems.Add(new Control[] { comboBoxAtLeastHero });
            HeroesSelectionItems.Add(new Control[] { listViewHeroes });
            
        }

        // -------------------------------------------------------------------
        // Checks
        // -------------------------------------------------------------------

        public void CheckHeroesSelection(int index)
        {
            WANOK.CheckControls(HeroesSelectionItems, index);
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
