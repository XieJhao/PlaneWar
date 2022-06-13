using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战.Properties;

namespace 飞机大战
{
    /*enum Direction
    {
        left,
        right
    }*/
    internal class Boss : Plane
    {
        //private Bitmap BossImg;
        private bool IsBoss;
        private Point targetPos;
        private Point autoPos;
        double gradient;
        private int HP;
        private int initialHP;
        //Direction dir;
        public Boss(Point _pos)
        {
            ObjectInit(_pos);
        }
        public override void DrawSelt()
        {
            base.DrawSelt();

        }
        public override void Update()
        {
            MoveCheck();
            Move();
            PlaneShootTime();
            DrawSelt();
            DrawHitPoints();
        }
        public override void Move()
        {
            int x = pos.X - autoPos.X;
            int y = pos.Y - autoPos.Y;

            if (Math.Abs(x) < Speed && Math.Abs(y) < Speed)
            {
                autoPos = GetTargetPos();
            }
            if(pos.X < autoPos.X)
            {
                pos.X += Speed;
            }
            else if(pos.X > autoPos.X)
            {
                pos.X -= Speed;
            }
            if(pos.Y > autoPos.Y)
            {
                pos.Y -= Speed;
            }
            else if(pos.Y < autoPos.Y)
            {
                pos.Y += Speed;
            }
            /*if (pos.Y < 150)
            {
                pos.Y += Speed;
            }
            if (dir == Direction.right)
            {
                pos.X += Speed;
            }
            else if (dir == Direction.left)
            {
                pos.X -= Speed;
            }*/


        }
        public override void MoveCheck()
        {
            /*if (pos.X <= 0)
            {
                dir = Direction.right;
                return;
            }
            else if (pos.X >= GameRuning.GetFormSize().Width)
            {
                dir = Direction.left;
                return;
            }*/
            /*if(pos.Y < autoPos.Y)
            {
                dir = Direction.right;
            }
            if(pos.Y >= autoPos.Y)
            {
                dir = Direction.left;
            }*/
        }
        public override void PlaneShoot()
        {
            Point _pos = new Point();
            if(IsBoss == false)
            {
                _pos.X = pos.X + 51;
                _pos.Y = pos.Y + 107;
                GameControl.GetInstance().CreateEnemyBullet(_pos);
                _pos.X = pos.X + 133;
                GameControl.GetInstance().CreateEnemyBullet(_pos);
            }
            else
            {
                _pos.X = pos.X + 37;
                _pos.Y = pos.Y + 114;
                GameControl.GetInstance().CreateEnemyBullet(_pos);
                _pos.X = pos.X + 79;
                GameControl.GetInstance().CreateEnemyBullet(_pos);
                _pos.X = pos.X + 166;
                GameControl.GetInstance().CreateEnemyBullet(_pos);
                _pos.X = pos.X + 210;
                GameControl.GetInstance().CreateEnemyBullet(_pos);
            }
        }
        public override void ObjectInit(Point _pos)
        {
            if (GameControl.GetInstance().Score >= 50 && GameControl.GetInstance().Score < 70)
            {
                IsBoss = true;
                bitmap = new Bitmap(Resources.img_plane_enemyBoss);
                HP = 50;
            }
            else
            {
                IsBoss = false;
                bitmap = new Bitmap(Resources.img_plane_enemyElite);
                HP = 20;
            }
            initialHP = HP;
            pos = _pos;
            autoPos = pos;
            Speed = 3;
            //dir = Direction.left;
            base.ObjectInit(_pos);
        }
        private Point GetTargetPos()
        {
            Random rand = new Random();
            targetPos.X = rand.Next(0, GameRuning.GetFormSize().Width - bitmap.Width);
            targetPos.Y = rand.Next(0, 450);
            gradient = (1.0 * (targetPos.X - pos.X) / (1.0 * (targetPos.Y / pos.Y)));
            return targetPos;
        }
        protected void PlaneShootTime()
        {
            Random rand = new Random();
            if (rand.Next(20) == 0)
            {
                GameControl.GetInstance().sound.PlayEShoot();
                PlaneShoot();
            }
        }
        public void BossBeHit()
        {
            HP--;
        }
        public int GetHP()
        {
            return HP;
        }
        public bool GetIsBoss()
        {
            return IsBoss;
        }
        private void DrawHitPoints()
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush);
            int x = pos.X;
            int y = pos.Y + bitmap.Height;
            int HP_Width = bitmap.Width /initialHP * HP;

            painter.DrawRectangle(pen, x, y, bitmap.Width, 5);
            brush.Color = Color.Red;
            painter.FillRectangle(brush, x, y, HP_Width, 5);
        }
    }
}
