using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战.Properties;

namespace 飞机大战
{
    internal class PlayerBullet : Bullet
    {
        public PlayerBullet() { }
        public PlayerBullet(Point _pos)
        {
            ObjectInit(_pos);
        }
        public override void Update()
        {
            base.Update();
        }
        public override void ObjectInit(Point _pos)
        {
            objectType = ObjectType.Object_PlayerBullet;
            if(bitmap == null)
            {
                bitmap = new Bitmap(Resources.player_bullet);
            }
            
            pos.X = _pos.X - bitmap.Width / lessen / 2;
            pos.Y = _pos.Y;
        }
        public override void MoveCheck()
        {
                   
            //判断子弹是否超出窗体边界
            if (pos.Y < 0)
            {
                IsDestroy = true;
            }


            //判断是否发生碰撞
            Rectangle rect = GetRectangle();

            EnemyPlane enemy = null;
            enemy = GameControl.GetInstance().IsCollidedEnemy(rect);

            if(enemy != null)
            {
                IsDestroy = true;
                GameControl.GetInstance().DestroyEnemy(enemy);
                GameControl.GetInstance().CreateExplosion(enemy.GetPos(),enemy.GetSize());
                GameControl.GetInstance().Score++;
                return;
            }

            Boss boss = null;
            boss = GameControl.GetInstance().IsCollidedBoss(rect);
            if(boss != null)
            {
                IsDestroy = true;
                GameControl.GetInstance().DestroyBoss(boss);
                return;
            }

        }
        public override void Move()
        {
            pos.Y -= Speed;
        }
    
    }
}
