using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战
{
    internal abstract class Movething : GameObject
    {
        /// <summary>
        /// 移动检测
        /// </summary>
        public abstract void MoveCheck();
        /// <summary>
        /// 对象移动方法
        /// </summary>
        public abstract void Move();
        public virtual Size GetSize()
        {
            return bitmap.Size;
        }
    }
}
