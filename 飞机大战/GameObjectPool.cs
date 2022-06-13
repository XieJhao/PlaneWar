using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战
{
    internal class GameObjectPool
    {
        private static GameObjectPool instance = null;
        private List<PlayerBullet> playerBulletPool = new List<PlayerBullet>();
        private List<EnemyBullet> enemyBulletPool = new List<EnemyBullet>();
        private List<EnemyPlane> enemyPlanePool = new List<EnemyPlane>();
        private List<Explosion> explosionPool = new List<Explosion>();
        public GameObjectPool()
        {

        }
        public static GameObjectPool GetInstance()
        {
            if(instance == null)
            {
                instance = new GameObjectPool();
            }
            return instance;
        }
        public void GameObjectPoolInit()
        {
            for(int i = 0;i < 60;i++)
            {
                //己方子弹生产
                PlayerBullet pBullet = new PlayerBullet();
                playerBulletPool.Add(pBullet);

                //敌机子弹生产
                EnemyBullet eBullet = new EnemyBullet();
                enemyBulletPool.Add(eBullet);

                //敌机生产
                EnemyPlane enemy = new EnemyPlane();
                enemyPlanePool.Add(enemy);
                
                Explosion exp = new Explosion();
                explosionPool.Add(exp);
                
            }
            
        }
        public GameObject GetGameObject(ObjectType type)
        {
            switch(type)
            {
                case ObjectType.Object_PlayerBullet:
                    {
                        PlayerBullet pBullet = playerBulletPool.First();
                        playerBulletPool.Remove(pBullet);
                        pBullet.Resetting();
                        return pBullet;
                    }
                case ObjectType.Object_Enemy:
                    {
                        EnemyPlane enemy = enemyPlanePool.First();
                        enemyPlanePool.Remove(enemy);
                        return enemy;
                    }
                case ObjectType.Object_EnemyBullet:
                    {
                        EnemyBullet eBullet = enemyBulletPool.First();
                        enemyBulletPool.Remove(eBullet);
                        eBullet.Resetting();
                        return eBullet;
                    }
                case ObjectType.Object_Explosion:
                    {
                        Explosion exp = explosionPool.First();
                        explosionPool.Remove(exp);
                        exp.Resetting();
                        return exp;
                    }
            }
            return null;
        }
        public bool ObjectIsEmpty(ObjectType type)
        {
            switch(type)
            {
                case ObjectType.Object_Enemy:
                    {
                        if (enemyPlanePool.Count() > 0)
                            return false;
                        break;
                    }
                case ObjectType.Object_PlayerBullet:
                    {
                        if (playerBulletPool.Count() > 0)
                            return false;
                        break;
                    }
                case ObjectType.Object_EnemyBullet:
                    {
                        if (enemyBulletPool.Count() > 0)
                            return false;
                        break;
                    }
                case ObjectType.Object_Explosion:
                    {
                        if (explosionPool.Count() > 0)
                            return false;
                        break;
                    }
            }
            return true;
        }
        public void RecoveryGameObject(GameObject _object)
        {
            switch(_object.GetObjectType())
            {
                case ObjectType.Object_Enemy:
                    enemyPlanePool.Add((EnemyPlane)_object);
                    break;
                case ObjectType.Object_PlayerBullet:
                    playerBulletPool.Add((PlayerBullet)_object);
                    break;
                case ObjectType.Object_EnemyBullet:
                    enemyBulletPool.Add((EnemyBullet)_object);
                    break;
                case ObjectType.Object_Explosion:
                    explosionPool.Add((Explosion)_object);
                    break;
            }
        }
    }
}
