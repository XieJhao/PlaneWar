using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战.Properties;

namespace 飞机大战
{
    internal class Explosion : GameObject
    {
        private bool IsNeedDestroy { get; set; }
        private int playSpeed = 1;
        private int playCount = 0;
        private int index = 0;
       
        //private ObjectType ExpType { get; set; }
        private Bitmap[] enemyExp = null;
        
        public Explosion() { }
        public Explosion(Point _pos)
        {
            ObjectInit(_pos);
        }
        private Bitmap GetBitmap()
        {
            if (index > 5)
            {
                return enemyExp[5];
            }
            return enemyExp[index];
            
        }
        public override void DrawSelt()
        {
            painter.DrawImage(GetBitmap(), pos);
        }
        public override void Update()
        {
            playCount++;
            index = (playCount - 1) / playSpeed;

            if (index > 5)
            {
                IsNeedDestroy = true;
            }
            DrawSelt();
        }
        public override void ObjectInit(Point _pos)
        {
            ObjectInit(_pos,new Size());
        }
        public void ObjectInit(Point _pos,Size size)
        {
            if (enemyExp == null)
            {
                enemyExp = new Bitmap[]
                {
                    Resources.bomb4,
                    Resources.bomb5,
                    Resources.bomb3,
                    Resources.bomb21,
                    Resources.bomb1,
                    Resources.bomb0
                };
            }
            foreach (Bitmap bmp in enemyExp)
            {
                bmp.MakeTransparent(Color.White);
            }
            objectType = ObjectType.Object_Explosion;
            painter = GameControl.GetInstance().gameMenuG;
            if (!size.IsEmpty)
            {
                int x = _pos.X + size.Width / 2;
                int y = _pos.Y + size.Height / 2;
                pos.X = x - enemyExp[0].Width / 2;
                pos.Y = y - enemyExp[0].Height / 2;
            }
            else
            {
                pos = _pos;
            }
            
            index = 0;
            IsNeedDestroy = false;
        }
        public static Size GetSize()
        {
            return Resources.bomb5.Size;
        }
        public void Resetting()
        {
            IsNeedDestroy = false;
        }
        public bool GetIsNeedDestroy()
        {
            return IsNeedDestroy;
        }
        
    }
}
