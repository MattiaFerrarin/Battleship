using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class StartPage : Form
    {
        public StartPage()
        {
            InitializeComponent();

            foreach(Control ctrl in btnsFLP.Controls)
            {
                ctrl.MaximumSize = new Size(btnsFLP.Width - 10, btnsFLP.Width / 4);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btn_newGame_Click(object sender, EventArgs e)
        {
            NewGamePage newGamePage = new NewGamePage();
            newGamePage.Show();
        }

        private void StartPage_Paint(object sender, PaintEventArgs e)
        {
            if (BackgroundImage != null)
            {
                var g = e.Graphics;
                g.Clear(BackColor);

                int imgW = BackgroundImage.Width;
                int imgH = BackgroundImage.Height;
                int ctlW = this.Width;
                int ctlH = this.Height;

                float scale = Math.Max((float)ctlW / imgW, (float)ctlH / imgH);

                int drawW = (int)(imgW * scale);
                int drawH = (int)(imgH * scale);

                int x = (ctlW - drawW) / 2;
                int y = (ctlH - drawH) / 2;

                g.DrawImage(BackgroundImage, new Rectangle(x, y, drawW, drawH));
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        private void StartPage_Resize(object sender, EventArgs e)
        {
            int capWidth = 210;
            int margin = 23;

            int availableWidth = this.ClientSize.Width - margin * 2;

            if(btnsFLP.Width != availableWidth)
            {
                if (availableWidth < capWidth)
                    btnsFLP.Width = availableWidth;
                else
                    btnsFLP.Width = capWidth;

                foreach (Control ctrl in btnsFLP.Controls)
                {
                    ctrl.MaximumSize = new Size(btnsFLP.Width - 10, btnsFLP.Width / 4);
                    ctrl.Size = new Size(btnsFLP.Width - 10, btnsFLP.Width / 4);
                    ctrl.Font = new Font(ctrl.Font.FontFamily, btnsFLP.Width / 13.33f);
                }
            }

            float scale = this.ClientSize.Width / 20f;

            if (scale < 20f) scale = 20f;
            if (scale > 60f) scale = 60f;
            if(lbl_title.Font.Size != scale)
                lbl_title.Font = new Font(lbl_title.Font.FontFamily, scale, FontStyle.Bold);

            btnsFLP.Location = new Point(margin, lbl_title.Location.Y + lbl_title.Size.Height + 2);
        }
    }
}
