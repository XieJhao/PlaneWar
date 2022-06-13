using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using 飞机大战.Properties;
using System.Threading;

namespace 飞机大战
{
    internal class SoundMananger
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(
            string command,//MCI命令字符串
            string returnString,//存放反馈信息的缓冲区
            int returnSize,//缓冲区的长度
            IntPtr hwndCallback//回调窗口的句柄，一般为NULL
            );//若成功则返回0，否则返回错误码。


        private Thread SoundThread;
        
        public SoundMananger()
        {
            
        }
        /// <summary>
        /// 播放己方飞机发射声音
        /// </summary>
        public void PlayPShoot()
        {
            this.Play("../../Resources/weapon_player.wav", 1);
        }
        /// <summary>
        /// 播放敌机发射声音
        /// </summary>
        public void PlayEShoot()
        {
            Play("../../Resources/weapon_enemy.wav", 1);
        }
        /// <summary>
        /// 播放敌机爆炸声音
        /// </summary>
        public void PlayBoom()
        {
            Play("../../Resources/BOMB2.WAV", 1);
        }
        /// <summary>
        /// 游戏背景音乐
        /// </summary>
        public void BGMusic()
        {
            Play("../../Resources/BGMusic.mp3",0);
        }
        /// <summary>
        /// 我方飞机受攻击声音
        /// </summary>
        public void PlayerBeHit()
        {
            Play("../../Resources/playerBeHit.wav",1);
        }
        /// <summary>
        /// 按指定次数播放
        /// </summary>
        /// <param name="file"></param>
        private void PlayWait(string file)
        {
            /*
             * open device_name type device_type alias device_alias  打开设备
             * device_name　 　　　要使用的设备名，通常是文件名。
             * type device_type　　设备类型，例如mpegvideo或waveaudio，可省略。
             * alias device_alias　设备别名，指定后可在其他命令中代替设备名。
             */
            mciSendString(string.Format("open \"{0}\" type mpegvideo alias media", file), null, 0, IntPtr.Zero);

            /*
             * play device_alias from pos1 to pos2 wait repeat  开始设备播放
             * 若省略from则从当前磁道开始播放。
             * 若省略to则播放到结束。
             * 若指明wait则等到播放完毕命令才返回。即指明wait会产生线程阻塞，直到播放完毕
             * 若指明repeat则会不停的重复播放。
             * 若同时指明wait和repeat则命令不会返回，本线程产生堵塞，通常会引起程序失去响应。
             */
            mciSendString("play media wait", null, 0, IntPtr.Zero);

            /*
             * close　　　 关闭设备
             */
            mciSendString("close media", null, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 循环播放
        /// </summary>
        /// <param name="file"></param>
        private void PlayRepeat(string file)
        {
            mciSendString(string.Format("open \"{0}\" type mpegvideo alias media", file), null, 0, IntPtr.Zero);
            mciSendString("play media repeat", null, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 播放音频文件
        /// </summary>
        /// <param name="file">音频文件路径</param>
        /// <param name="times">播放次数，0：循环播放 大于0：按指定次数播放</param>
        private void Play(string file, int times)
        {
            //用线程主要是为了解决在播放的时候指定wait时产生线程阻塞,从而导致界面假死的现象
            SoundThread = new Thread(() =>
            {
                if (times == 0)
                {
                    PlayRepeat(file);
                }
                else if (times > 0)
                {
                    for (int i = 0; i < times; i++)
                    {
                        PlayWait(file);
                    }
                }
            });

            //线程必须为单线程
            SoundThread.SetApartmentState(ApartmentState.STA);
            SoundThread.IsBackground = true;
            SoundThread.Start();
        }
        /// <summary>
        /// 结束播放的线程
        /// </summary>
        public void Exit()
        {
            if (SoundThread != null)
            {
                try
                {
                    SoundThread.Abort();
                }
                catch { }
                SoundThread = null;
            }
        }
    }
}
