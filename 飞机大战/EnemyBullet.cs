using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战.Properties;

namespace 飞机大战
{
    internal class EnemyBullet : Bullet
    {
        public EnemyBullet() { }
        public EnemyBullet(Point _pos)
        {
            ObjectInit(_pos);
        }
        public override void ObjectInit(Point _pos)
        {
            objectType = ObjectType.Object_EnemyBullet;
            if(bitmap == null)
            {
                bitmap = new Bitmap(Resources.enemy_bullet);
            }
            pos.X = _pos.X - bitmap.Width / lessen / 2;
            pos.Y = _pos.Y;
        }

        public override void MoveCheck()
        {
            if (pos.Y > GameRuning.GetFormSize().Height)
            {
                IsDestroy = true;
            }

            Rectangle rect = GetRectangle();

            if(GameControl.GetInstance().IsCollidedPlayer(rect))
            {
                IsDestroy = true;
                GameControl.GetInstance().sound.PlayerBeHit();
                return;
            }
        }
        public override void Move()
        {
            pos.Y += Speed;
        }
    }
}
