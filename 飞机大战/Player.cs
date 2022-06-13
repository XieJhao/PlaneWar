using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 飞机大战.Properties;

namespace 飞机大战
{
    internal class Player : Plane
    {
        private int HP { get; set; }
        private Object _lock = new object();
        public Player(Point _pos)
        {
            HP = 100;
            objectType = ObjectType.Object_Player;
            Speed = 10;
            bitmap = new Bitmap(Resources.MyPlane1_fw);
            pos = _pos;
            
        }
        public override void PlaneShoot()
        {
            GameControl.GetInstance().sound.PlayPShoot();
            Point _pos = new Point();
            _pos.X = pos.X + bitmap.Width / 2;
            _pos.Y = pos.Y;
            GameControl.GetInstance().CreatePlayerBullet(_pos);
        }
        public override void Move()
        {

            foreach (Keys key in GameControl.GetInstance().GetKeyList())
            {
                switch (key)
                {
                    case Keys.W:
                        pos.Y -= Speed; break;
                    case Keys.S:
                        pos.Y += Speed; break;
                    case Keys.A:
                        pos.X -= Speed; break;
                    case Keys.D:
                        pos.X += Speed; break;
                    case Keys.Space:
                        PlaneShoot(); break;

                }
            }

        }
        public override void Update()
        {
            MoveCheck();
            Move(); 
            DrawSelt();
        }
        public override void MoveCheck()
        {
            //检测是否超出窗体
            if (pos.Y < 2)
            {
                pos.Y = 2;
            }
            if (pos.X < 2)
            {
                pos.X = 2;
                
            }
            if (pos.X > GameRuning.GetFormSize().Width - bitmap.Width)
            {
                pos.X = GameRuning.GetFormSize().Width - bitmap.Width;
                
            }
            if (pos.Y > GameRuning.GetFormSize().Height - bitmap.Height)
            {
                pos.Y = GameRuning.GetFormSize().Height - bitmap.Height;
            }
            //检测是否与敌机发射碰撞
            Rectangle rect = GetRectangle();
            EnemyPlane enemy = null;
            enemy = GameControl.GetInstance().IsCollidedEnemy(rect);
            if(enemy != null)
            {

                GameControl.GetInstance().Score++;
                HP--;
                GameControl.GetInstance().DestroyEnemy(enemy);
                GameControl.GetInstance().CreateExplosion(enemy.GetPos(),enemy.GetSize());
                return;
            }

        }
        public void PlaneBeHit()
        {
            HP--;
        }
        public int GetHP()
        {
            return HP;
        }
        
    }
}
