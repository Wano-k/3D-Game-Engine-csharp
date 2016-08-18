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
        private List<RadioButton> SwitchesVariablesSelectionRadios = new List<RadioButton>();
        private List<Control[]> SwitchesVariablesSelectionItems = new List<Control[]>();
        private List<RadioButton> HeroesSelectionRadios = new List<RadioButton>();
        private List<RadioButton> HeroesConditionRadios = new List<RadioButton>();
        private List<Control[]> HeroesSelectionItems = new List<Control[]>();
        private List<Control[]> HeroesConditionItems = new List<Control[]>();
        public ListBox[] ListBoxesCanceling, ListBoxes;


        public List<object> Result = null;

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

        public DialogCondition(List<object> condition)
        {
            InitializeComponent();

            // ListBoxCancelling
            ListBoxesCanceling = new ListBox[] { constantVariableHeroes.GetListBox() };
            ListBoxes = new ListBox[] {  };

            // All tabs for enable
            SwitchesVariablesSelectionRadios.Add(radioButtonSwitch);
            SwitchesVariablesSelectionRadios.Add(radioButtonSelfSwitch);
            SwitchesVariablesSelectionRadios.Add(radioButtonVariable);
            SwitchesVariablesSelectionItems.Add(new Control[] { textBoxVariablesSwitch, label1, comboBoxSwitchOnOff });
            SwitchesVariablesSelectionItems.Add(new Control[] { comboBoxSelfSwitch, label2, comboBoxSelfSwitchOnOff });
            SwitchesVariablesSelectionItems.Add(new Control[] { textBoxVariablesVariable, label3, comboBoxComparaisonVariable, constantVariableVariable });
            HeroesSelectionRadios.Add(radioButtonHeroesAll);
            HeroesSelectionRadios.Add(radioButtonHeroesAtLeast);
            HeroesSelectionRadios.Add(radioButtonHeroes);
            HeroesConditionRadios.Add(radioButtonHeroesNamed);
            HeroesConditionRadios.Add(radioButtonHeroesIn);
            HeroesConditionRadios.Add(radioButtonHeroesSkill);
            HeroesConditionRadios.Add(radioButtonHeroesEquiped);
            HeroesConditionRadios.Add(radioButtonHeroesEffect);
            HeroesConditionRadios.Add(radioButtonHeroesStat);
            HeroesSelectionItems.Add(new Control[] { comboBoxAllHeroes });
            HeroesSelectionItems.Add(new Control[] { comboBoxAtLeastHero });
            HeroesSelectionItems.Add(new Control[] { listViewHeroes });
            HeroesConditionItems.Add(new Control[] { textBoxHeroesNamed });
            HeroesConditionItems.Add(new Control[] { comboBoxHeroesIn });
            HeroesConditionItems.Add(new Control[] { comboBoxHeroesSkill });
            HeroesConditionItems.Add(new Control[] { comboBoxHeroesEquiped });
            HeroesConditionItems.Add(new Control[] { comboBoxHeroesEffect });
            HeroesConditionItems.Add(new Control[] { comboBoxHeroesStat, comboBoxComparaisonHeroesStat, constantVariableHeroes, comboBoxMeasureHeroes });

            // Heroes list
            listViewHeroes.Columns[0].Width = listViewHeroes.Size.Width - 4;
            for (int i = 0; i < WANOK.Game.Heroes.HeroesList.Count; i++)
            {
                listViewHeroes.Items.Add(WANOK.GetStringList(WANOK.Game.Heroes.HeroesList[i].Id, WANOK.Game.Heroes.HeroesList[i].Name));
            }

            // Default Selection
            comboBoxSwitchOnOff.SelectedIndex = 0;
            comboBoxSelfSwitchOnOff.SelectedIndex = 0;
            comboBoxComparaisonVariable.InitValues();
            comboBoxComparaisonVariable.SelectedIndex = 0;
            radioButtonHeroesNamed.Checked = true;
            comboBoxAllHeroes.SelectedIndex = 0;
            comboBoxAtLeastHero.SelectedIndex = 0;
            comboBoxHeroesIn.SelectedIndex = 0;
            comboBoxComparaisonHeroesStat.InitValues();
            comboBoxComparaisonHeroesStat.SelectedIndex = 0;
            comboBoxComparaisonVariable.InitValues();
            comboBoxComparaisonVariable.SelectedIndex = 0;


            // Current selection
            MakeCurrentSelection(condition);

            // Remove not visible pages
            tabControl1.TabPages.Remove(tabPageEnemies);
            tabControl1.TabPages.Remove(tabPageEvents);
            tabControl1.TabPages.Remove(tabPageOthers);

            // Unselect list
            UnselectAllLists();
        }

        // -------------------------------------------------------------------
        // UnselectAllCancelingLists
        // -------------------------------------------------------------------

        public void UnselectAllCancelingLists()
        {
            for (int i = 0; i < ListBoxesCanceling.Length; i++)
            {
                ListBoxesCanceling[i].ClearSelected();
            }
        }

        // -------------------------------------------------------------------
        // UnselectAllLists
        // -------------------------------------------------------------------

        public void UnselectAllLists()
        {
            UnselectAllCancelingLists();
            for (int i = 0; i < ListBoxes.Length; i++)
            {
                ListBoxes[i].SelectedIndex = 0;
            }
        }

        // -------------------------------------------------------------------
        // Checks
        // -------------------------------------------------------------------

        public void CheckSwitchesVariablesSelection(int index)
        {
            WANOK.CheckControls(SwitchesVariablesSelectionItems, index);
            UnCheckSwitchesVariablesSelection();
        }

        public void UnCheckSwitchesVariablesSelection()
        {
            WANOK.UnCheckRadios(HeroesSelectionRadios);
            WANOK.UnableRadios(HeroesConditionRadios);
            WANOK.UnCheckAllControls(HeroesSelectionItems);
            WANOK.UnCheckAllControls(HeroesConditionItems);
        }

        public void CheckHeroesSelection(int index)
        {
            WANOK.CheckControls(HeroesSelectionItems, index);
            UnCheckHeroesSelection();
        }

        public void CheckHeroesCondition(int index)
        {
            WANOK.CheckControls(HeroesConditionItems, index);
        }

        public void UnCheckHeroesSelection()
        {
            WANOK.UnCheckRadios(SwitchesVariablesSelectionRadios);
            WANOK.UnCheckAllControls(SwitchesVariablesSelectionItems);
            WANOK.UnableRadios(HeroesConditionRadios, true);
            int index = 0;
            for (int i = 0; i < HeroesConditionRadios.Count; i++)
            {
                if (HeroesConditionRadios[i].Checked) break;
                index++;
            }
            CheckHeroesCondition(index);
        }

        // -------------------------------------------------------------------
        // MakeCurrentSelection
        // -------------------------------------------------------------------

        public void MakeCurrentSelection(List<object> condition)
        {
            tabControl1.SelectedIndex = (int)condition[0];
            int selection = (int)condition[1];

            switch (tabControl1.SelectedIndex)
            {
                // Switches & varaibles
                case 0:
                    if (selection == 0)
                    {
                        radioButtonSwitch.Checked = true;
                        comboBoxSwitchOnOff.SelectedIndex = (int)condition[3];
                    }
                    else if (selection == 1)
                    {
                        radioButtonSelfSwitch.Checked = true;
                        comboBoxSelfSwitch.SelectedIndex = (int)condition[2];
                        comboBoxSelfSwitchOnOff.SelectedIndex = (int)condition[3];
                    }
                    else if (selection == 3)
                    {
                        radioButtonVariable.Checked = true;
                    }
                    break;
                // Heroes
                case 1:
                    if (selection == 0) {
                        radioButtonHeroesAll.Checked = true;
                        comboBoxAllHeroes.SelectedIndex = (int)condition[2];
                    }
                    else if (selection == 1)
                    {
                        radioButtonHeroesAtLeast.Checked = true;
                        comboBoxAtLeastHero.SelectedIndex = (int)condition[2];
                    }
                    else if (selection == 3)
                    {
                        radioButtonHeroes.Checked = true;
                    }
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        // -------------------------------------------------------------------
        // GetCurrentSelection
        // -------------------------------------------------------------------

        public void GetCurrentSelection()
        {
            Result = new List<object>();
            for (int i = 0; i < SwitchesVariablesSelectionRadios.Count; i++)
            {
                if (SwitchesVariablesSelectionRadios[i].Checked)
                {
                    Result.Add(0);
                    Result.Add(i);
                    switch (i)
                    {
                        case 0:
                            Result.Add(1);
                            Result.Add(comboBoxSwitchOnOff.SelectedIndex);
                            return;
                        case 1:
                            Result.Add(comboBoxSelfSwitch.SelectedIndex);
                            Result.Add(comboBoxSelfSwitchOnOff.SelectedIndex);
                            return;
                        case 2:
                            
                            return;
                    }
                }
            }
            
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            GetCurrentSelection();
            Close();
        }

        // SWITCHES & VARIABLES

        private void radioButtonSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckSwitchesVariablesSelection(0);
        }

        private void radioButtonSelfSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckSwitchesVariablesSelection(1);
        }

        private void radioButtonVariable_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckSwitchesVariablesSelection(2);
        }

        // HEROES 

        private void radioButtonHeroesAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesSelection(0);
        }

        private void radioButtonHeroesAtLeast_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesSelection(1);
        }

        private void radioButtonHeroes_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesSelection(2);
        }

        private void radioButtonHeroesNamed_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesCondition(0);
        }

        private void radioButtonHeroesIn_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesCondition(1);
        }

        private void radioButtonHeroesSkill_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesCondition(2);
        }

        private void radioButtonHeroesEquiped_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesCondition(3);
        }

        private void radioButtonHeroesEffect_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesCondition(4);
        }

        private void radioButtonHeroesStat_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) CheckHeroesCondition(5);
        }
    }
}
