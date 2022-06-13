using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 飞机大战.Properties;

namespace 飞机大战
{
    public partial class GameOver : Form
    {
        public Bitmap BGImg { get; set; }
        private Button AgainBtn;
        
        public GameOver()
        {
            InitializeComponent();
            this.Size = new Size(512, 768);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            AgainBtn = new Button();
            this.BtnStyle(AgainBtn);
            AgainBtn.Location = new Point(171, 400);
            AgainBtn.Parent = this;

            AgainBtn.Click += AgainBtn_Click;

        }

        private void AgainBtn_Click(object sender, EventArgs e)
        {
            
            GameRuning.GRPtr.Show();

        }

        private void BtnStyle(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;//样式
            btn.ForeColor = Color.Transparent;//前景
            btn.BackColor = Color.Transparent;//去背景
            btn.FlatAppearance.BorderSize = 0;//去边线
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;//鼠标经过
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;//鼠标按下
            btn.BackgroundImage = Resources.game_again;

            btn.Size = Resources.game_again.Size;
        }

        private void GameOver_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameRuning.GRPtr.Close();
            Form1.winF.Close();
        }
    }
}
