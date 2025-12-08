namespace Battleship
{
    partial class NewGamePage
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
            this.txb_player1 = new System.Windows.Forms.TextBox();
            this.txb_player2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_newPvP = new System.Windows.Forms.Button();
            this.btn_newPvPRush = new System.Windows.Forms.Button();
            this.btn_newPvC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txb_player1
            // 
            this.txb_player1.Location = new System.Drawing.Point(12, 403);
            this.txb_player1.MaxLength = 64;
            this.txb_player1.Name = "txb_player1";
            this.txb_player1.Size = new System.Drawing.Size(100, 20);
            this.txb_player1.TabIndex = 4;
            this.txb_player1.Tag = "Player 1";
            this.txb_player1.Text = "Player 1";
            this.txb_player1.Leave += new System.EventHandler(this.txb_player_Leave);
            // 
            // txb_player2
            // 
            this.txb_player2.Location = new System.Drawing.Point(12, 429);
            this.txb_player2.MaxLength = 64;
            this.txb_player2.Name = "txb_player2";
            this.txb_player2.Size = new System.Drawing.Size(100, 20);
            this.txb_player2.TabIndex = 5;
            this.txb_player2.Tag = "Player 2";
            this.txb_player2.Text = "Player 2";
            this.txb_player2.Leave += new System.EventHandler(this.txb_player_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Player Names";
            // 
            // btn_newPvP
            // 
            this.btn_newPvP.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newPvP.Location = new System.Drawing.Point(40, 39);
            this.btn_newPvP.Name = "btn_newPvP";
            this.btn_newPvP.Size = new System.Drawing.Size(300, 60);
            this.btn_newPvP.TabIndex = 1;
            this.btn_newPvP.Text = "New PvP Game";
            this.btn_newPvP.UseVisualStyleBackColor = true;
            this.btn_newPvP.Click += new System.EventHandler(this.btn_newPvP_Click);
            // 
            // btn_newPvPRush
            // 
            this.btn_newPvPRush.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newPvPRush.Location = new System.Drawing.Point(40, 105);
            this.btn_newPvPRush.Name = "btn_newPvPRush";
            this.btn_newPvPRush.Size = new System.Drawing.Size(300, 60);
            this.btn_newPvPRush.TabIndex = 2;
            this.btn_newPvPRush.Text = "New PvP Rush Game";
            this.btn_newPvPRush.UseVisualStyleBackColor = true;
            this.btn_newPvPRush.Click += new System.EventHandler(this.btn_newPvPRush_Click);
            // 
            // btn_newPvC
            // 
            this.btn_newPvC.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newPvC.Location = new System.Drawing.Point(40, 171);
            this.btn_newPvC.Name = "btn_newPvC";
            this.btn_newPvC.Size = new System.Drawing.Size(300, 60);
            this.btn_newPvC.TabIndex = 3;
            this.btn_newPvC.Text = "New PvC Game";
            this.btn_newPvC.UseVisualStyleBackColor = true;
            this.btn_newPvC.Click += new System.EventHandler(this.btn_newPvC_Click);
            // 
            // NewGamePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.btn_newPvC);
            this.Controls.Add(this.btn_newPvPRush);
            this.Controls.Add(this.btn_newPvP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_player2);
            this.Controls.Add(this.txb_player1);
            this.Name = "NewGamePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_player1;
        private System.Windows.Forms.TextBox txb_player2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_newPvP;
        private System.Windows.Forms.Button btn_newPvPRush;
        private System.Windows.Forms.Button btn_newPvC;
    }
}