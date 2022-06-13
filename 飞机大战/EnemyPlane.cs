using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战.Properties;

namespace 飞机大战
{
    internal class EnemyPlane : Plane
    {
        public EnemyPlane() { }
        public EnemyPlane(Point _pos)
        {
            ObjectInit(_pos);
        }
        public override void ObjectInit(Point _pos)
        {
            objectType = ObjectType.Object_Enemy;
            if(bitmap == null)
            {
                Random rand = new Random();
                int planeType = rand.Next(0, 3);
                if(planeType == 0)
                {
                    bitmap = new Bitmap(Resources.enemy_plane);
                }
                else if(planeType == 1)
                {
                    bitmap = new Bitmap(Resources.img_plane_enemy2);
                }
                else if(planeType == 2)
                {
                    bitmap = new Bitmap(Resources.img_plane_enemy3);
                }
            }
            Speed = 3;
            pos = _pos;
            base.ObjectInit(_pos);

        }
        public override void PlaneShoot()
        {
            Point _pos = new Point();
            _pos.X = pos.X + bitmap.Width / 2;
            _pos.Y = pos.Y + bitmap.Height;
            GameControl.GetInstance().CreateEnemyBullet(_pos);
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            DrawSelt();
            PlaneShootTime();
        }
        public override void Move()
        {
            pos.Y += Speed;
        }
        public override void MoveCheck()
        {
            if(pos.Y > GameRuning.GetFormSize().Height )
            {
                GameControl.GetInstance().DestroyEnemy(this);
                return;
            }
        }
        protected void PlaneShootTime()
        {
            Random rand = new Random();
            if(rand.Next(20) == 0)
            {
                GameControl.GetInstance().sound.PlayEShoot();
                PlaneShoot();
            }
        }
    }
}
