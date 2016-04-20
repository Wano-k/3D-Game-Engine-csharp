namespace RPG_Paper_Maker
{
    partial class DialogDemoTip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogDemoTip));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CheckBoxShow = new System.Windows.Forms.CheckBox();
            this.ButtonLater = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RPG_Paper_Maker.Properties.Resources.kate;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 190);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // CheckBoxShow
            // 
            this.CheckBoxShow.AutoSize = true;
            this.CheckBoxShow.Location = new System.Drawing.Point(156, 215);
            this.CheckBoxShow.Name = "CheckBoxShow";
            this.CheckBoxShow.Size = new System.Drawing.Size(178, 17);
            this.CheckBoxShow.TabIndex = 2;
            this.CheckBoxShow.Text = "Never show that message again";
            this.CheckBoxShow.UseVisualStyleBackColor = true;
            // 
            // ButtonLater
            // 
            this.ButtonLater.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(169)))), ((int)(((byte)(97)))));
            this.ButtonLater.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLater.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonLater.Location = new System.Drawing.Point(353, 208);
            this.ButtonLater.Name = "ButtonLater";
            this.ButtonLater.Size = new System.Drawing.Size(81, 23);
            this.ButtonLater.TabIndex = 3;
            this.ButtonLater.Text = "See later";
            this.ButtonLater.UseVisualStyleBackColor = false;
            // 
            // ButtonOk
            // 
            this.ButtonOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(164)))), ((int)(((byte)(142)))));
            this.ButtonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonOk.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonOk.Location = new System.Drawing.Point(440, 208);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(134, 23);
            this.ButtonOk.TabIndex = 4;
            this.ButtonOk.Text = "Start Guide Demo!";
            this.ButtonOk.UseVisualStyleBackColor = false;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(13, 14);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(386, 163);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.ZoomFactor = 1.1F;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::RPG_Paper_Maker.Properties.Resources.tuto1;
            this.pictureBox2.Location = new System.Drawing.Point(286, 98);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(113, 79);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Location = new System.Drawing.Point(156, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 190);
            this.panel1.TabIndex = 6;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::RPG_Paper_Maker.Properties.Resources.small_kate;
            this.pictureBox3.Location = new System.Drawing.Point(70, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 31);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // DialogDemoTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(584, 238);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.ButtonLater);
            this.Controls.Add(this.CheckBoxShow);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogDemoTip";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Welcome to RPG Paper Maker!";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox CheckBoxShow;
        private System.Windows.Forms.Button ButtonLater;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}