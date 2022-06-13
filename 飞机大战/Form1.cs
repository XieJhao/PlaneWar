using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 飞机大战.Properties;

namespace 飞机大战
{
    public partial class Form1 : Form
    {
        public static Form1 winF;
        private  Graphics winG;
        private  Button startBtn;
        private GameRuning gameRuning;
        
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            winF = this;
            
            
            this.Size = new Size(512, 768);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            


            gameRuning = new GameRuning();

            winG = this.CreateGraphics();
            
            startBtn = new Button();
            this.BtnStyle(startBtn);

            startBtn.Location = new Point(171, 400);
            startBtn.Parent = this;

            this.startBtn.Click += new EventHandler(this.startBtn_Click);

            this.BackgroundImage = Resources.img_bg_logo;

            /*Region rion = new Region(new Rectangle(0, 0, 500, 500));
            Region = rion;*/
            //GraphicsPath path = new GraphicsPath(FillMode.)
            
        }
        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //BGtimer2.Abort();
        }

        private void BtnStyle(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;//样式
            btn.ForeColor = Color.Transparent;//前景
            btn.BackColor = Color.Transparent;//去背景
            btn.FlatAppearance.BorderSize = 0;//去边线
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;//鼠标经过
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;//鼠标按下
            btn.BackgroundImage = Resources.startBtn_fw;
            
            btn.Size = Resources.startBtn_fw.Size;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            gameRuning.Location = this.Location;
            gameRuning.StartPosition = FormStartPosition.Manual;
            this.Hide();
            gameRuning.GameStart();
            gameRuning.Show();

        }
    }
}
