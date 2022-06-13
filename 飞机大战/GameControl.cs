using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 飞机大战.Properties;

namespace 飞机大战
{
    enum ObjectType
    {
        Object_Player,
        Object_Enemy,
        Object_PlayerBullet,
        Object_EnemyBullet,
        Object_Explosion
    }
    enum GameState
    {
        Runing,
        Over
    }
    class GameControl
    {
        private static GameControl gameControl;
        //容器
        private List<Keys> KeyList = new List<Keys>();
        private List<PlayerBullet> playerBulletList = new List<PlayerBullet>();
        private List<EnemyPlane> enemyPlaneList = new List<EnemyPlane>();
        private List<EnemyBullet> enemyBulletList = new List<EnemyBullet>();
        private List<Explosion> exceptionList = new List<Explosion>();
        private List<Player> playerList = new List<Player>();
        private List<Boss> bossList = new List<Boss>();

        public SoundMananger sound = new SoundMananger();
        public  Graphics  gameMenuG;
        private Bitmap gameMenu;
        private Player player;
        private System.Timers.Timer enemyCreateTimer;
        public int Score { get;set; }
        private GameState gameState;
        
        public GameControl()
        {
            gameMenu = new Bitmap(512,768);
            enemyCreateTimer = new System.Timers.Timer();
            enemyCreateTimer.Interval = 1000;
            enemyCreateTimer.Elapsed += EnemyCreateTimer_Elapsed;
            enemyCreateTimer.Start();
            
            gameMenuG = Graphics.FromImage(gameMenu);
            Score = 0;
            gameState = GameState.Runing;
        }

        private void EnemyCreateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CreateEnemyPlane();
        }

        public static GameControl GetInstance()
        {
            if (gameControl == null)
            {
                gameControl = new GameControl();
            }
            return gameControl;
        }
        public void GameInit()
        {
            //游戏资源初始化
            GameObjectPool.GetInstance().GameObjectPoolInit();
            sound.BGMusic();
            GameResourceLoad();
        }
        public void GameResourceLoad()
        {
            CreatePlayer();
        }
        public Bitmap GetGameMenu()
        {
            gameMenuG.Clear(Color.Transparent);
            GameMenuUpdate();
            SolidBrush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush);
            gameMenuG.DrawImage(Resources.imgHead, 10, 10);
            brush.Color = Color.Red;
            Font font = new Font("微软雅黑", 14);
            gameMenuG.DrawString("HP:",font,brush,80,12);
            gameMenuG.DrawRectangle(pen, 140, 15, 101, 20);
            gameMenuG.FillRectangle(brush, 141, 16, player.GetHP()-1, 18);
            brush.Color = Color.SkyBlue;
            gameMenuG.DrawString("Score:", font, brush, 80, 42);
            gameMenuG.DrawRectangle(pen, 140, 45, 101, 20);
            if (Score > 100)
                Score = 100;
            gameMenuG.FillRectangle(brush, 141, 46,Score,18);
            
            return gameMenu;
        }
        public void GameMenuUpdate()
        {
            
            DestroyEnemyBullet();
            DestroyPlayerBullet();
            RemoveExplosion();
            
            foreach(Player MyPlane in playerList)
            {
                MyPlane.Update();
            }
            foreach(PlayerBullet bullet in playerBulletList)
            {
                bullet.Update();
            }
            foreach(EnemyBullet bullet1 in enemyBulletList)
            {
                bullet1.Update();
            }
            foreach(EnemyPlane plane in enemyPlaneList)
            {
                plane.Update();
            }
            foreach(Boss boss in bossList)
            {
                boss.Update();
            }
            foreach(Explosion explosion in exceptionList)
            {
                explosion.Update();
            }
            BossOrEliteBorn();
            GameOver();
        }
        /// <summary>
        /// 生成己方飞机
        /// </summary>
        public void CreatePlayer()
        {
            player = new Player(new Point(300,400));
            playerList.Add(player);
        }
        
        public void CreateEnemyPlane()
        {
            Random rand = new Random();
            Point _pos = new Point();
            _pos.X = rand.Next(0, GameRuning.GetFormSize().Width - 100);
            _pos.Y = -100;
            EnemyPlane enemy = (EnemyPlane)GameObjectPool.GetInstance().GetGameObject(ObjectType.Object_Enemy);
            enemy.ObjectInit(_pos);
            enemyPlaneList.Add(enemy);
            
        }
        /// <summary>
        /// 生成敌机子弹
        /// </summary>
        /// <param name="_pos">敌机坐标</param>
        public void CreateEnemyBullet(Point _pos)
        {
            EnemyBullet eBullet = (EnemyBullet)GameObjectPool.GetInstance().GetGameObject(ObjectType.Object_EnemyBullet);
            eBullet.ObjectInit(_pos);
            enemyBulletList.Add(eBullet);
        }
        /// <summary>
        /// 生成我方子弹
        /// </summary>
        /// <param name="_pos">我方飞机坐标</param>
        public void CreatePlayerBullet(Point _pos)
        {
            PlayerBullet bullet = (PlayerBullet)GameObjectPool.GetInstance().GetGameObject(ObjectType.Object_PlayerBullet);
            bullet.ObjectInit(_pos);
            playerBulletList.Add(bullet);
        }
        public void CreateExplosion(Point _pos,Size size)
        {
            Explosion exp = (Explosion)GameObjectPool.GetInstance().GetGameObject(ObjectType.Object_Explosion);
            exp.ObjectInit(_pos,size);
            exceptionList.Add(exp);

        }
        /// <summary>
        /// 生产Boss和精英怪
        /// </summary>
        /// <param name="_pos"></param>
        public void CreateBossOrElite(Point _pos)
        {
            Boss boss = new Boss(_pos);
            bossList.Add(boss);
        }
        /// <summary>
        /// 若与敌机发生碰撞则返回敌机对象，否则返回null；
        /// </summary>
        /// <param name="_rect"></param>
        /// <returns></returns>
        public EnemyPlane IsCollidedEnemy(Rectangle _rect)
        {
            foreach(EnemyPlane enemy in enemyPlaneList)
            {
                if(enemy.GetRectangle().IntersectsWith(_rect))
                {
                    return enemy;
                }
            }
            return null;
        }
        public Boss IsCollidedBoss(Rectangle _rect)
        {
            foreach(Boss boss in bossList)
            {
                if(boss.GetRectangle().IntersectsWith(_rect))
                {
                    boss.BossBeHit();
                    return boss;
                }
            }
            return null;
        }
        /// <summary>
        /// 若与己方飞机发射碰撞则返回true,否则返回false;
        /// </summary>
        /// <param name="_rect"></param>
        /// <returns></returns>
        public bool IsCollidedPlayer(Rectangle _rect)
        {
            if(player.GetRectangle().IntersectsWith(_rect))
            {
                player.PlaneBeHit();
                return true;
            }
            return false;
        }
        public void BossOrEliteBorn()
        {
            Random rand = new Random();
            Point _pos = new Point(rand.Next(0, 512), -150);
            if(bossList.Count() == 0 && (Score == 10||Score==40||Score==60))
            {
                CreateBossOrElite(_pos);
            }

        }
        /// <summary>
        /// 销毁敌机
        /// </summary>
        /// <param name="enemy"></param>
        public void DestroyEnemy(EnemyPlane enemy)
        {
            if (enemyPlaneList.Contains(enemy))
            {
                sound.PlayBoom();
                GameObjectPool.GetInstance().RecoveryGameObject(enemy);
                enemyPlaneList.Remove(enemy);
            }

        }
        /// <summary>
        /// 销毁敌机子弹
        /// </summary>
        public void DestroyEnemyBullet()
        {
            foreach(EnemyBullet enemyBullet in enemyBulletList)
            {
                if(enemyBullet.GetIsDestroy())
                {
                    GameObjectPool.GetInstance().RecoveryGameObject(enemyBullet);
                    enemyBulletList.Remove(enemyBullet);
                    return;
                }
            }
        }
        /// <summary>
        /// 销毁己方子弹
        /// </summary>
        public void DestroyPlayerBullet()
        {
            foreach(PlayerBullet playerBullet in playerBulletList)
            {
                if(playerBullet.GetIsDestroy())
                {
                    GameObjectPool.GetInstance().RecoveryGameObject(playerBullet);
                    playerBulletList.Remove(playerBullet);
                    return;
                }
            }
        }
        public void DestroyPlayer()
        {
            if (player.GetHP() == 0)
            {
                playerList.Remove(player);

                CreateExplosion(player.GetPos(),player.GetSize());
                sound.PlayBoom();
            }
        }
        public void DestroyBoss(Boss boss)
        {
            if(boss.GetHP() == 0)
            {
                bossList.Remove(boss);
                Point _pos = new Point();
                _pos.X = boss.GetPos().X - (Explosion.GetSize().Width - boss.GetSize().Width);
                _pos.Y = boss.GetPos().Y - (Explosion.GetSize().Height - boss.GetSize().Height);
                CreateExplosion(_pos,boss.GetSize());
                sound.PlayBoom();
                if(boss.GetIsBoss())
                {
                    Score = 100;
                }
                Score += 5;
            }
        }
        /// <summary>
        /// 移除爆炸效果
        /// </summary>
        public void RemoveExplosion()
        {
            foreach(Explosion exp in exceptionList)
            {
                if(exp.GetIsNeedDestroy())
                {
                    GameObjectPool.GetInstance().RecoveryGameObject(exp);
                    exceptionList.Remove(exp);
                    return;
                }
            }
            
        }
        /// <summary>
        /// 返回按键容器
        /// </summary>
        /// <returns></returns>
        public List<Keys> GetKeyList()
        {
            return KeyList;
        }
        public GameState GetState()
        {
            return gameState;
        }
        public bool GameOver()
        {
            if(player.GetHP() <= 0 || Score >= 100)
            {
                DestroyPlayer();
                gameState = GameState.Over;
                gameMenuG.DrawImage(Resources.gameover, 66, 300);
                enemyCreateTimer.Stop();
                sound.Exit();

                return true;
            }
            return false;
        }
    }
}
