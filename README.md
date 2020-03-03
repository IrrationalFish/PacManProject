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
![Randomized Prim](https://github.com/NaughtyFishRei/PacManProject/raw/master/ScreenShots/RBwithMark.png)

