using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战
{
    
    internal abstract class GameObject
    {
        protected ObjectType objectType;
        protected Point pos;
        protected Bitmap bitmap = null;
        protected int Speed;
        protected Graphics painter;
        public GameObject()
        {
            this.painter = GameControl.GetInstance().gameMenuG;
            bitmap = null;
        }
        /// <summary>
        /// 返回该对象的矩形
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            Rectangle rect = new Rectangle(pos.X, pos.Y, bitmap.Width, bitmap.Height);
            return rect;
        }
        /// <summary>
        /// 返回对象类型
        /// </summary>
        /// <returns></returns>
        public ObjectType GetObjectType()
        {
            return objectType;
        }
        
        /// <summary>
        /// 绘制该对象图片
        /// </summary>
        public abstract void DrawSelt();
        /// <summary>
        /// 更新该对象图片状态
        /// </summary>
        public abstract void Update();
        
        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <param name="_pos"></param>
        public abstract void ObjectInit(Point _pos);
    }
}
