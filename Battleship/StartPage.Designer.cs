namespace Battleship
{
    partial class StartPage
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_title = new System.Windows.Forms.Label();
            this.btnsFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_newGame = new System.Windows.Forms.Button();
            this.btn_settings = new System.Windows.Forms.Button();
            this.btnsFLP.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.AccessibleDescription = "Title of the game";
            this.lbl_title.AccessibleName = "Battleship";
            this.lbl_title.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.lbl_title.AutoSize = true;
            this.lbl_title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.Bisque;
            this.lbl_title.Location = new System.Drawing.Point(13, 13);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(244, 55);
            this.lbl_title.TabIndex = 1;
            this.lbl_title.Text = "Battleship";
            // 
            // btnsFLP
            // 
            this.btnsFLP.BackColor = System.Drawing.Color.Transparent;
            this.btnsFLP.Controls.Add(this.btn_newGame);
            this.btnsFLP.Location = new System.Drawing.Point(23, 72);
            this.btnsFLP.Name = "btnsFLP";
            this.btnsFLP.Size = new System.Drawing.Size(210, 371);
            this.btnsFLP.TabIndex = 2;
            // 
            // btn_newGame
            // 
            this.btn_newGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newGame.Location = new System.Drawing.Point(3, 3);
            this.btn_newGame.Name = "btn_newGame";
            this.btn_newGame.Size = new System.Drawing.Size(160, 40);
            this.btn_newGame.TabIndex = 0;
            this.btn_newGame.Text = "New Game";
            this.btn_newGame.UseVisualStyleBackColor = true;
            this.btn_newGame.Click += new System.EventHandler(this.btn_newGame_Click);
            // 
            // btn_settings
            // 
            this.btn_settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_settings.BackColor = System.Drawing.SystemColors.Control;
            this.btn_settings.BackgroundImage = global::Battleship.Properties.Resources.setting;
            this.btn_settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_settings.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_settings.Location = new System.Drawing.Point(932, 509);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(40, 40);
            this.btn_settings.TabIndex = 99;
            this.btn_settings.UseVisualStyleBackColor = false;
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Battleship.Properties.Resources.BattleshipBackgroundImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.btnsFLP);
            this.Controls.Add(this.lbl_title);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "StartPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battleship";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StartPage_Paint);
            this.Resize += new System.EventHandler(this.StartPage_Resize);
            this.btnsFLP.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_settings;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.FlowLayoutPanel btnsFLP;
        private System.Windows.Forms.Button btn_newGame;
    }
}

