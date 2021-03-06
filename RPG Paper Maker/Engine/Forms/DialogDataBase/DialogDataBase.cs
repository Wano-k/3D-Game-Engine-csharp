﻿using System;
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
    public partial class DialogDataBase : Form
    {
        protected DialogDataBaseControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();

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
        // Constructor
        // -------------------------------------------------------------------

        public DialogDataBase()
        {
            InitializeComponent();
            Control = new DialogDataBaseControl();
            ViewModelBindingSource.DataSource = Control;

            // Heroes
            listBoxHeroes.InitializeListParameters(true, Control.Model.Heroes.HeroesList.Cast<SuperListItem>().ToList(), null, typeof(SystemHero), 1, SystemHero.MAX_HEROES);

            // Tilesets
            listBoxTilesets.GetListBox().SelectedIndexChanged += listBoxTilesets_SelectedIndexChanged;
            listBoxTilesets.InitializeListParameters(true, Control.Model.Tilesets.TilesetsList.Cast<SuperListItem>().ToList(), null, typeof(SystemTileset), 1, SystemTileset.MAX_TILESETS);
            listBoxTilesets.GetListBox().MouseDown += listBoxTilesets_SelectedIndexChanged;
            textBoxGraphic.GetTextBox().SelectedValueChanged += textBoxGraphic_SelectedValueChanged;
            collisionSettings.LoadTextures();
            listBoxAutotiles.GetButton().Text = "Choose autotiles";
            listBoxAutotiles.GetButton().Click += listBoxAutotiles_Click;
            listBoxRelief.GetButton().Text = "Choose reliefs";
            listBoxRelief.GetButton().Click += listBoxRelief_Click;

            // System
            ComboBoxResolution.SelectedIndex = Control.GetFullScreenIndex();
            toolTipSquareSize.SetToolTip(buttonSquareSize, "This option set the maps displaying, it is recommended to put multiple 8 numbers.\nNote that the pixel height addings are not modified.");
            textBoxLangGameName.InitializeParameters(Control.GameName);
            listBoxColors.InitializeListParameters(false, Control.Model.System.Colors.Cast<SuperListItem>().ToList(), typeof(DialogSystemColors), typeof(SystemColor), 1, SystemColor.MAX_COLORS);

            // Battle System
            listBoxElements.InitializeListParameters(false, Control.Model.BattleSystem.Elements.Cast<SuperListItem>().ToList(), typeof(DialogElement), typeof(SystemElement), 1, SystemElement.MAX_ELEMENTS);
            listBoxCommonStats.InitializeListParameters(false, Control.Model.BattleSystem.Statistics.Cast<SuperListItem>().ToList(), typeof(DialogStatistics), typeof(SystemStatistics), 1, SystemStatistics.MAX_STATISTICS);
            listBoxWeaponsKind.InitializeListParameters(true, Control.Model.BattleSystem.WeaponsKind.Cast<SuperListItem>().ToList(), null, typeof(SuperListItemName), 1, BattleSystemDatas.MAX_WEAPONS_KIND);
            listBoxArmorsKind.InitializeListParameters(true, Control.Model.BattleSystem.ArmorsKind.Cast<SuperListItem>().ToList(), null, typeof(SuperListItemName), 1, BattleSystemDatas.MAX_ARMORS_KIND);

            MouseWheel += new MouseEventHandler(form_MouseWheel);
            tabControl1.KeyDown += new KeyEventHandler(form_KeyDown);

            InitializeDataBindings();
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            numericWidth.DataBindings.Add("Value", ViewModelBindingSource, "ScreenWidth", true);
            numericHeight.DataBindings.Add("Value", ViewModelBindingSource, "ScreenHeight", true);
            numericSquareSize.DataBindings.Add("Value", ViewModelBindingSource, "SquareSize", true);
        }
        

        // -------------------------------------------------------------------
        // HEROES
        // -------------------------------------------------------------------

        #region Heroes

        

        #endregion

        // -------------------------------------------------------------------
        // TILESETS
        // -------------------------------------------------------------------

        #region Tilesets

        // -------------------------------------------------------------------
        // SetCommonTilesetList
        // -------------------------------------------------------------------

        public void SetCommonTilesetList(SystemTileset tileset)
        {
            listBoxAutotiles.InitializeListParameters(false, Control.Model.Tilesets, Control.Model.Tilesets.Autotiles.Cast<SuperListItem>().ToList(), tileset.Autotiles, typeof(DialogAddingAutotilesList), typeof(SystemAutotile), 1, SystemAutotile.MAX_AUTOTILES, Control.Model.Tilesets.GetAutotileById);
            listBoxRelief.InitializeListParameters(false, Control.Model.Tilesets, Control.Model.Tilesets.Reliefs.Cast<SuperListItem>().ToList(), tileset.Reliefs, typeof(DialogAddingReliefsList), typeof(SystemRelief), 1, SystemRelief.MAX_RELIEFS, Control.Model.Tilesets.GetReliefById);
        }

        // -------------------------------------------------------------------
        // listBoxTilesets_SelectedIndexChanged
        // -------------------------------------------------------------------

        public void listBoxTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {
            SystemTileset tileset = (SystemTileset)listBoxTilesets.GetListBox().SelectedItem;
            if (tileset != null)
            {
                textBoxTilesetName.Text = tileset.Name;
                textBoxGraphic.InitializeParameters(tileset.Graphic);
                collisionSettings.InitializeParameters(tileset.Collision, tileset.Graphic);
                SetCommonTilesetList(tileset);
            }
        }

        // -------------------------------------------------------------------
        // textBoxGraphic_SelectedValueChanged
        // -------------------------------------------------------------------

        public void textBoxGraphic_SelectedValueChanged(object sender, EventArgs e)
        {
            SystemTileset tileset = (SystemTileset)listBoxTilesets.GetListBox().SelectedItem;
            tileset.Graphic = textBoxGraphic.Graphic;
            collisionSettings.InitializeParameters(tileset.Collision, tileset.Graphic);
        }

        // -------------------------------------------------------------------
        // textBoxTilesetName_TextChanged
        // -------------------------------------------------------------------

        private void textBoxTilesetName_TextChanged(object sender, EventArgs e)
        {
            listBoxTilesets.SetName(textBoxTilesetName.Text);
        }

        // -------------------------------------------------------------------
        // listBoxAutotiles_Click
        // -------------------------------------------------------------------

        private void listBoxAutotiles_Click(object sender, EventArgs e)
        {
            SystemTileset tileset = (SystemTileset)listBoxTilesets.GetListBox().SelectedItem;
            DialogAddingAutotilesList dialog = new DialogAddingAutotilesList("Choose Autotile", Control.Model.Tilesets, tileset.Autotiles);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Control.Model.Tilesets.Autotiles = dialog.GetListAutotiles();
                tileset.Autotiles = dialog.GetListTileset();
                for (int i = 0; i < listBoxTilesets.GetListBox().Items.Count; i++)
                {
                    SystemTileset cpTileset = (SystemTileset)listBoxTilesets.GetListBox().Items[i];
                    List<int> list = new List<int>();
                    for (int j = 0; j < cpTileset.Autotiles.Count; j++)
                    {
                        list.Add(cpTileset.Autotiles[j]);
                    }
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[j] > Control.Model.Tilesets.Autotiles.Count) cpTileset.Autotiles.Remove(list[j]);
                    }
                }
                SetCommonTilesetList(tileset);
            }
        }

        // -------------------------------------------------------------------
        // listBoxRelief_Click
        // -------------------------------------------------------------------

        private void listBoxRelief_Click(object sender, EventArgs e)
        {
            SystemTileset tileset = (SystemTileset)listBoxTilesets.GetListBox().SelectedItem;
            DialogAddingReliefsList dialog = new DialogAddingReliefsList("Choose relief", Control.Model.Tilesets, tileset);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Control.Model.Tilesets.Reliefs = dialog.GetListReliefs();
                tileset.Reliefs = dialog.GetListTileset();
                tileset.ReliefTop = dialog.GetListReliefsTop();
                for (int i = 0; i < listBoxTilesets.GetListBox().Items.Count; i++)
                {
                    SystemTileset cpTileset = (SystemTileset)listBoxTilesets.GetListBox().Items[i];
                    List<int> list = new List<int>();
                    for (int j = 0; j < cpTileset.Reliefs.Count; j++)
                    {
                        list.Add(cpTileset.Reliefs[j]);
                    }
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[j] > Control.Model.Tilesets.Reliefs.Count) cpTileset.Reliefs.Remove(list[j]);
                    }
                }
                SetCommonTilesetList(tileset);
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // SYSTEM
        // -------------------------------------------------------------------

        #region System

        // -------------------------------------------------------------------
        // ComboBoxResolution_SelectedIndexChanged
        // -------------------------------------------------------------------

        public void ComboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control.SetFullScreen(ComboBoxResolution.SelectedIndex);
        }

        #endregion

        // -------------------------------------------------------------------
        // tabControl1_SelectedIndexChanged
        // -------------------------------------------------------------------

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageTilesets)
            {
                listBoxTilesets.GetListBox().Focus();
            }
            else if (tabControl1.SelectedTab == tabPageHeroes)
            {
                listBoxHeroes.GetListBox().Focus();
            }


        }

        // -------------------------------------------------------------------
        // form_MouseWheel
        // -------------------------------------------------------------------

        private void form_MouseWheel(object sender, MouseEventArgs e)
        {
            FocusList();
        }

        // -------------------------------------------------------------------
        // form_KeyDown
        // -------------------------------------------------------------------

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control) FocusList();
        }

        // -------------------------------------------------------------------
        // FocusList
        // -------------------------------------------------------------------

        public void FocusList()
        {
            if (tabControl1.SelectedTab == tabPageTilesets)
            {
                listBoxTilesets.GetListBox().Focus();
            }
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Control.Model.System.Colors = listBoxColors.ModelList.Cast<SystemColor>().ToList();
            Control.Model.BattleSystem.Elements = listBoxElements.ModelList.Cast<SystemElement>().ToList();
            Control.Model.BattleSystem.Statistics = listBoxCommonStats.ModelList.Cast<SystemStatistics>().ToList();
            Control.Model.BattleSystem.WeaponsKind = listBoxWeaponsKind.ModelList.Cast<SuperListItemName>().ToList();
            Control.Model.BattleSystem.ArmorsKind = listBoxArmorsKind.ModelList.Cast<SuperListItemName>().ToList();
            Control.Model.Tilesets.TilesetsList = listBoxTilesets.ModelList.Cast<SystemTileset>().ToList();
            Control.Save();
            Close();
        }      
    }
}
