using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public class TreeViewCommands : TreeView
    {
        protected List<EventCommand> CommandsSelected = null;
        private Timer CommandUnderscoreTimer = new Timer();
        private bool IsUnderScoreDisplayed = false;


        public TreeViewCommands()
        {


            // Events
            CommandUnderscoreTimer.Tick += CommandUnderscoreTimer_Tick;
            AfterSelect += CommandsView_AfterSelect;
        }

        private void CommandUnderscoreTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // -------------------------------------------------------------------
        // StopUnderscoreTimer
        // -------------------------------------------------------------------

        public void StopUnderscoreTimer()
        {
            CommandUnderscoreTimer.Stop();
            if (IsUnderScoreDisplayed) SelectedNode.Text = SelectedNode.Text.Substring(0, SelectedNode.Text.Length - 1);
            IsUnderScoreDisplayed = false;
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void CommandsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (((NTree<EventCommand>)SelectedNode.Tag).Data.Id == EventCommandKind.None)
            {
                CommandUnderscoreTimer.Start();
            }
            else
            {
                CommandUnderscoreTimer.Stop();
            }
        }

        private void CommandsView_KeyDown(object sender, KeyEventArgs e)
        {
            StopUnderscoreTimer();
            if (((NTree<EventCommand>)SelectedNode.Tag).Data.Id == EventCommandKind.None)
            {

                e.SuppressKeyPress = true;
            }
            CommandUnderscoreTimer.Start();
        }

        private void CommandUnderscoreTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsUnderScoreDisplayed) SelectedNode.Text += "_";
            else SelectedNode.Text = SelectedNode.Text.Substring(0, SelectedNode.Text.Length - 1);
        }
    }
}
