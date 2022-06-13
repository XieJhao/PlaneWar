using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战
{
    internal abstract class Plane : Movething
    {
        public abstract void PlaneShoot();
        public override void ObjectInit(Point _pos)
        {         
        }
        public override void DrawSelt()
        {
            painter.DrawImage(bitmap, pos);
        }
        public Point GetPos()
        {
            return pos;
        }
    }
    
}
