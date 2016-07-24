using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    class DialogAddingReliefsList : DialogAddingSpecialList
    {
        InterpolationPictureBox PictureBox = new InterpolationPictureBox();
        Panel PicturePanel = new Panel();

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogAddingReliefsList(string text, SystemDatas model, List<int> superListTileset) : base(text, model, superListTileset, typeof(SystemRelief))
        {
            listBoxComplete.InitializeListParameters(new ListBox[] { }, model.Reliefs.Cast<SuperListItem>().ToList(), null, Type, 1, SystemRelief.MAX_RELIEFS);
            for (int i = 0; i < superListTileset.Count; i++)
            {
                listBoxTileset.Items.Add(model.GetReliefById(superListTileset[i]));
            }

            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.InterpolationMode = InterpolationMode.NearestNeighbor;
            PicturePanel.AutoScroll = true;
            PicturePanel.Controls.Add(PictureBox);
            PanelOther.Controls.Add(PicturePanel);

            textBoxGraphic.GetTextBox().SelectedValueChanged += textBoxGraphic_SelectedValueChanged;
            listBoxComplete.GetListBox().SelectedIndexChanged += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.GetButton().Click += listBoxComplete_Click;

            UnselectAllLists();
        }

        // -------------------------------------------------------------------
        // GetListReliefs
        // -------------------------------------------------------------------

        public List<SystemRelief> GetListReliefs()
        {
            return listBoxComplete.GetListBox().Items.Cast<SystemRelief>().ToList();
        }

        // -------------------------------------------------------------------
        // listBoxComplete_SelectedIndexChanged
        // -------------------------------------------------------------------

        public void listBoxComplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            SystemRelief relief = (SystemRelief)listBoxComplete.GetListBox().SelectedItem;
            if (relief != null)
            {
                textBoxName.Text = relief.Name;
                textBoxGraphic.InitializeParameters(relief.Graphic);
                PictureBox.LoadTexture(relief.Graphic);
            }
        }

        // -------------------------------------------------------------------
        // textBoxGraphic_SelectedValueChanged
        // -------------------------------------------------------------------

        public void textBoxGraphic_SelectedValueChanged(object sender, EventArgs e)
        {
            SystemRelief relief = (SystemRelief)listBoxComplete.GetListBox().SelectedItem;
            PictureBox.LoadTexture(relief.Graphic);
        }

        // -------------------------------------------------------------------
        // listBoxComplete_Click
        // -------------------------------------------------------------------

        private void listBoxComplete_Click(object sender, EventArgs e)
        {
            
        }
    }
}
