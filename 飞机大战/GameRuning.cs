using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 飞机大战.Properties;

namespace 飞机大战
{
    public partial class GameRuning : Form
    {
        //private PictureBox Picture1;
        //private Graphics gamePic;
        private System.Timers.Timer BGtimer;
        int BGImg1 = 0;
        public static Graphics bmpG;
        private Graphics formG;
        private Bitmap tempBmp;
        public static GameRuning GRPtr;
        private GameOver gameOver;
        //private int over = 0;

        public GameRuning()
        {
            InitializeComponent();
            this.Size = new Size(512, 768);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            GRPtr = this;

            gameOver = new GameOver();
            formG = this.CreateGraphics();

            /*Picture1 = new PictureBox();

            Picture1.Size = Resources.img_bg_level_1.Size;
            Picture1.Location = new Point(0, 0);
            this.Controls.Add(Picture1);*/
            
            tempBmp = new Bitmap(512, 768);
            bmpG = Graphics.FromImage(tempBmp);

        }
        public void GameStart()
        {
            //游戏初始化
            GameControl.GetInstance().GameInit();
            BGtimer = new System.Timers.Timer();
            BGtimer.Interval = 1000 / 60;
            BGtimer.Elapsed += new System.Timers.ElapsedEventHandler(BGtimer_Tick);
            BGtimer.Start();
        }
        private void GameRuning_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameControl.GetInstance().sound.Exit();

            Form1.winF.Close();
            gameOver.Close();
        }


        private Bitmap DrawGameMenu()
        {

            int x = 2;
            bmpG.Clear(Color.Transparent);
            bmpG.DrawImage(Resources.img_bg_level_1, new Rectangle(0, BGImg1, 512, 768)
                , new Rectangle(0, 0, 512, 768), GraphicsUnit.Pixel);
            bmpG.DrawImage(Resources.img_bg_level_1, new Rectangle(0, BGImg1 - 768, 512, 768)
                , new Rectangle(0, 0, 512, 768), GraphicsUnit.Pixel);

            if (BGImg1 >= 768)
            {
                BGImg1 = 0;
            }
            BGImg1 += x;
            bmpG.DrawImage(GameControl.GetInstance().GetGameMenu(),0,0);
            Bitmap bitmap = new Bitmap(tempBmp);
            //Picture1.Image = bitmap;
            formG.DrawImage(bitmap, 0, 0);

            return bitmap;
        }
        private void BGtimer_Tick(object stnder, System.Timers.ElapsedEventArgs e)
        {
            
            Bitmap GameOverBGImg = this.DrawGameMenu();
            /*if (GameControl.GetInstance().GameOver() && over == 0)
            {
                gameOver.Location = this.Location;
                gameOver.StartPosition = FormStartPosition.Manual;
                this.Hide();
                gameOver.Show();
                gameOver.BGImg = GameOverBGImg;
                over++;

            }*/
        }

        private void GameRuning_KeyDown(object sender, KeyEventArgs e)
        {
            if(GameControl.GetInstance().GetState() == GameState.Runing)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                    case Keys.S:
                    case Keys.A:
                    case Keys.D:
                    case Keys.Space:
                        if (!GameControl.GetInstance().GetKeyList().Contains(e.KeyCode))
                            GameControl.GetInstance().GetKeyList().Add(e.KeyCode);
                        break;
                }
            }
            else { }
        }

        private void GameRuning_KeyUp(object sender, KeyEventArgs e)
        {

            if (GameControl.GetInstance().GetKeyList().Contains(e.KeyCode))
            {
                GameControl.GetInstance().GetKeyList().Remove(e.KeyCode);
            }
            
        }
        public static Size GetFormSize()
        {
            return GRPtr.Size;
        }

        private void GameRuning_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
