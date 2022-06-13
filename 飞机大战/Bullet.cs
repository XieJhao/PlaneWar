using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战
{
    internal abstract class Bullet : Movething
    {
        protected bool IsDestroy { get; set; }
        protected int lessen { get; }//图片缩小倍数
        public Bullet()
        {
            IsDestroy = false;
            this.Speed = 10;
            lessen = 4;
        }

        public override void DrawSelt()
        {
            painter.DrawImage(bitmap, pos.X, pos.Y, bitmap.Width / lessen, bitmap.Height / lessen);
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            DrawSelt();
        }
        /// <summary>
        /// 重置子弹状态
        /// </summary>
        public void Resetting()
        {
            IsDestroy = false;
        }
        public bool GetIsDestroy()
        {
            return IsDestroy;
        }
        public override Size GetSize()
        {
            Size size = new Size();
            size.Height = bitmap.Height / lessen;
            size.Width = bitmap.Width / lessen;
            return size;
        }
    }
}
