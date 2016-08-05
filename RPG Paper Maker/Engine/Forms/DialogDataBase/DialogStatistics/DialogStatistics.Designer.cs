namespace RPG_Paper_Maker
{
    partial class DialogStatistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxGraphicIcon = new RPG_Paper_Maker.Engine.CustomUserControls.TextBoxGraphic();
            this.PanelBar = new System.Windows.Forms.TableLayoutPanel();
            this.PictureBoxIcon = new RPG_Paper_Maker.InterpolationPictureBox();
            this.textBoxName = new RPG_Paper_Maker.Engine.TextBoxLang();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGameOver = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.PanelBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxIcon)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(583, 334);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.ok, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cancel, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(419, 302);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(161, 29);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(3, 3);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(74, 23);
            this.ok.TabIndex = 0;
            this.ok.Text = "Ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(83, 3);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(577, 293);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(571, 60);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.textBoxGraphicIcon, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.PanelBar, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(47, 33);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(521, 24);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // textBoxGraphicIcon
            // 
            this.textBoxGraphicIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGraphicIcon.Location = new System.Drawing.Point(260, 0);
            this.textBoxGraphicIcon.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxGraphicIcon.Name = "textBoxGraphicIcon";
            this.textBoxGraphicIcon.Size = new System.Drawing.Size(261, 24);
            this.textBoxGraphicIcon.TabIndex = 0;
            // 
            // PanelBar
            // 
            this.PanelBar.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.PanelBar.ColumnCount = 1;
            this.PanelBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelBar.Controls.Add(this.PictureBoxIcon, 0, 0);
            this.PanelBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBar.ForeColor = System.Drawing.Color.Gainsboro;
            this.PanelBar.Location = new System.Drawing.Point(3, 3);
            this.PanelBar.Name = "PanelBar";
            this.PanelBar.RowCount = 1;
            this.PanelBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelBar.Size = new System.Drawing.Size(254, 18);
            this.PanelBar.TabIndex = 2;
            // 
            // PictureBoxIcon
            // 
            this.PictureBoxIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PictureBoxIcon.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxIcon.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.PictureBoxIcon.Location = new System.Drawing.Point(115, 1);
            this.PictureBoxIcon.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBoxIcon.Name = "PictureBoxIcon";
            this.PictureBoxIcon.Size = new System.Drawing.Size(23, 16);
            this.PictureBoxIcon.TabIndex = 1;
            this.PictureBoxIcon.TabStop = false;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(47, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(521, 24);
            this.textBoxName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bar:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGameOver);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(571, 221);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageGameOver
            // 
            this.tabPageGameOver.Location = new System.Drawing.Point(4, 22);
            this.tabPageGameOver.Name = "tabPageGameOver";
            this.tabPageGameOver.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGameOver.Size = new System.Drawing.Size(563, 195);
            this.tabPageGameOver.TabIndex = 0;
            this.tabPageGameOver.Text = "Game over implication";
            this.tabPageGameOver.UseVisualStyleBackColor = true;
            // 
            // DialogStatistics
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(583, 334);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DialogStatistics";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistics";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.PanelBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxIcon)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private Engine.TextBoxLang textBoxName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Engine.CustomUserControls.TextBoxGraphic textBoxGraphicIcon;
        private System.Windows.Forms.TableLayoutPanel PanelBar;
        private InterpolationPictureBox PictureBoxIcon;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGameOver;
    }
}