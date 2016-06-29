using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    class DialogAddingAutotilesList : DialogAddingSpecialList
    {
        CollisionSettings collisionSettings;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogAddingAutotilesList(string text, SystemDatas model, List<int> superListTileset) : base(text, model, superListTileset, typeof(Autotile), 1, Autotile.MAX_AUTOTILES)
        {
            // Collision settings
            collisionSettings = new CollisionSettings();
            collisionSettings.Dock = DockStyle.Fill;
            PanelOther.Controls.Add(collisionSettings);
            collisionSettings.LoadTextures();

            textBoxGraphic.GetTextBox().SelectedValueChanged += textBoxGraphic_SelectedValueChanged;
            listBoxComplete.GetListBox().SelectedIndexChanged += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.GetButton().Click += listBoxComplete_Click;

            UnselectAllLists();
        }

        // -------------------------------------------------------------------
        // GetListAutotiles
        // -------------------------------------------------------------------

        public List<Autotile> GetListAutotiles()
        {
            return listBoxComplete.GetListBox().Items.Cast<Autotile>().ToList();
        }

        // -------------------------------------------------------------------
        // listBoxComplete_SelectedIndexChanged
        // -------------------------------------------------------------------

        public void listBoxComplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            Autotile autotile = (Autotile)listBoxComplete.GetListBox().SelectedItem;
            if (autotile != null)
            {
                textBoxName.Text = autotile.Name;
                textBoxGraphic.InitializeParameters(autotile.Graphic);
                collisionSettings.InitializeParameters(autotile.Collision, autotile.Graphic);
            }
        }

        // -------------------------------------------------------------------
        // textBoxGraphic_SelectedValueChanged
        // -------------------------------------------------------------------

        public void textBoxGraphic_SelectedValueChanged(object sender, EventArgs e)
        {
            Autotile autotile = (Autotile)listBoxComplete.GetListBox().SelectedItem;
            collisionSettings.InitializeParameters(autotile.Collision, autotile.Graphic);
        }

        // -------------------------------------------------------------------
        // listBoxComplete_Click
        // -------------------------------------------------------------------

        private void listBoxComplete_Click(object sender, EventArgs e)
        {
            
        }
    }
}
