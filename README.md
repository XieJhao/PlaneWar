# 飞机大战

基于 C# WinForm 窗体开发的飞机大战小游戏，主要通过键盘控制飞机移动和子弹发射 #开发工具
Visual Studio 2022

# 主要功能

1. 分两个窗体
   - 窗体 1：点击开始游戏按钮进入窗体 2
   - 窗体 2：进行游戏
2. 键盘控制
   - WASD 键控制飞机移动
   - 空格键发射子弹
   - 碰撞检测：当玩家子弹或玩家飞机碰撞到敌机时敌机发生爆炸

# 游戏规则

- 界面上有两个矩形条，分别表示生命值和得分；
- 被敌机子弹击中则减少 1 点生命值，若我方子弹击中敌机则增加 1 分；
- 当生命值减到 0 或得分加满时，游戏结束。

# 项目目录结构

## 飞机大战主要作用文件

1. Resources 存放图片、音效文件
2. EnemyBullet.cs 敌方子弹类
   - 继承于 Bullet 类
   - 主要实现子弹飞行移动和子弹移动检测
3. PlayerBullet.cs 我方飞机子弹类
   - 继承于 Bullet 类
   - 主要实现与敌机子弹类类似
4. EnemyPlane.cs 敌方飞机类
   - 继承于 Plane 类
   - 主要实现敌机的飞行移动、移动检测碰撞检测、敌机发射与发射时间间隔
5. Explosion.cs 爆炸效果管理类
   - 继承于要游戏基类 GameObject
   - 爆炸效果播放绘制，设有爆炸大小接口和判断是否移除爆炸效果接口
6. SoundMananger.cs 声音管理类
   - 不继承任何类
   - 主要用 mci 命令方式调用 Window 系统 winmm.dll 文件进行声音播放
   - 比起 SoundPlayer 类的优点：通过多线程可以同时播放多种声音，也能播放 mp3 文件
7. Form1.cs 游戏初始主界面
   - 主要有一个进入游戏按钮
8. GameRuning.cs 游戏进行主界面

   - 绘制游戏背景移动画面，整合 GameControl 类绘制的换面形成全部游戏画面，

9. Player.cs 我方飞机类
   - 继承于 Plane 类
   - 主要实现飞机移动碰撞检测，键盘操控飞机移动和发射
   - 实现飞机被击中扣血接口和返回飞机当前血量接口
10. GameControl.cs 游戏管理类
    - 单例类
    - 管理游戏各种元素的生成和移除、以及各种元素的画面绘制
    - 控制游戏进行以及游戏结束
11. Program.cs 程序入口
12. Resource.resx 资源文件
    - 主要在此文件添加游戏个元素图片资源
