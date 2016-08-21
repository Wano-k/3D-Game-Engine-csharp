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

        public DialogAddingAutotilesList(string text, TilesetsDatas model, List<int> superListTileset) : base(text, model, superListTileset, typeof(SystemAutotile))
        {
            // Collision settings
            collisionSettings = new CollisionSettings();
            collisionSettings.Dock = DockStyle.Fill;
            PanelOther.Controls.Add(collisionSettings);
            collisionSettings.LoadTextures();

            listBoxComplete.GetListBox().SelectedIndexChanged += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.InitializeListParameters(true, WANOK.GetSuperListItem(model.Autotiles.Cast<SuperListItem>().ToList()), null, Type, 1, SystemAutotile.MAX_AUTOTILES);
            List<SuperListItem> modelTileset = new List<SuperListItem>();
            for (int i = 0; i < superListTileset.Count; i++)
            {
                modelTileset.Add(model.GetAutotileById(superListTileset[i]));
            }

            listBoxTileset.InitializeListParameters(true, modelTileset, null, Type, 0, 0, false, false);

            textBoxGraphic.GetTextBox().SelectedValueChanged += textBoxGraphic_SelectedValueChanged;
            listBoxComplete.GetListBox().MouseDown += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.GetButton().Click += listBoxComplete_Click;
        }

        // -------------------------------------------------------------------
        // GetListAutotiles
        // -------------------------------------------------------------------

        public List<SystemAutotile> GetListAutotiles()
        {
            return listBoxComplete.GetListBox().Items.Cast<SystemAutotile>().ToList();
        }

        // -------------------------------------------------------------------
        // listBoxComplete_SelectedIndexChanged
        // -------------------------------------------------------------------

        public void listBoxComplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            SystemAutotile autotile = (SystemAutotile)listBoxComplete.GetListBox().SelectedItem;
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
            SystemAutotile autotile = (SystemAutotile)listBoxComplete.GetListBox().SelectedItem;
            autotile.Graphic = textBoxGraphic.Graphic;
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
