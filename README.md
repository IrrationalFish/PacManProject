---
title: Example content
---
aaa
![开始菜单](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/1.mainmenu.PNG)

这是一个独立完成的PacMan游戏项目，基于原版游戏的基本操作和概念，添加了随机迷宫生成，道具系统，商店系统以及4种基于不同AI脚本的敌人（Ghost）。

# 玩家PacMan
使用WASD控制移动，SPACE消耗能量条（左下角）加速，收集散落在地图中的道具后，数字键使用
![游戏画面1](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/8.game.PNG)

# 随机迷宫生成
利用三种不同的随机迷宫算法（Recursive Division, Randomized Prim, Recursive backtracker）生成不同风格，不同尺寸的迷宫
## Recursive Division
主要由平行和垂直的短墙组成，难度最低
![Recursive Division](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/RD.PNG)
## Randomized Prim
有更多的折线和死路，难度稍高
![Randomized Prim](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/Prim.PNG)
## Recursive backtracker
折线较多，且存在较多Z型和‘凹’型结构，这种结构只有在入口出口间没有分岔，在有敌人的情况下是较难处理的结构
![Recursive backtracker](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/RBwithMark.png)

# 商店和道具系统
在迷宫中收集足够点数后，可在商店解锁道具，解锁的道具会出现在迷宫中，玩家可以收集使用
## 商店
共有5种道具可解锁，下面三个是道具栏上限，能量上限和购买生命值
![Shop](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/ui3.PNG)
## Wall-Breaker
可以击杀一个Ghost或者打碎一面迷宫内的墙
![Wall-Breaker](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/wallbreaker.PNG)
## Grenade
可以扔过墙，也可以直线向前扔出，可以击杀一名Ghost
![Grenade](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/grenade.PNG)
## Laser
向前发射镭射光，被镭射光击中的Ghost会被瘫痪15秒
![Laser](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/laser.PNG)
## Energy Pellet
无视能量上限立即为PacMan提供100点能量
![Energy Pellet](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/pellet.PNG)
## Portal
每个迷宫最多出现一次，可使用两次来放置一对传送门，可利用传送门在迷宫中移动
![Portal](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/portal.PNG)

# 敌人（Ghosts）
实现了4种不同敌人，每个敌人有不同的行为逻辑
![Ghosts](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/ghosts.PNG)
# Blinky（红色）
在一定范围内随机移动
# Ambusher（黄色）
根据玩家之前的移动路径，更倾向于在玩家还未到达的区域活动
# Chaser（紫色）
平时不移动，当视线追踪到玩家后会以较快速度追赶玩家，玩家需要通过多次转弯甩开
# Thief（绿色）
不会进攻，只会远离玩家，移动时会收集路径上的点数，玩家可以通过碰撞杀死它并夺回点数
