using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGenerator : MonoBehaviour {

    public GameObject cube;
    public GameObject mazePrefab;

    protected GameObject[,] mazeObjects;       //最左下角是0,0
    protected GameObject maze;

    private bool deadEndShouldRemove = true;

    public abstract GameObject GenerateMaze(int width, int height);

    protected void GenerateMazeParent(int width, int height) {
        maze=Instantiate(mazePrefab);
        mazeObjects=maze.GetComponent<Maze>().InitialiseMazeObject(width, height);  //这里的mazeobj是maze的引用
    }

    protected void BreakWall(int x, int y) {
        if (x==0||y==0||x==maze.GetComponent<Maze>().width-1||y==maze.GetComponent<Maze>().height-1) {
            Debug.Log("Boundary Break Fail!");
            return;
        }
        Destroy(mazeObjects[x, y]);
        mazeObjects[x, y]=null;
    }

    public void RemoveDeadEnds(int width, int height) {
        //RemoveUpAndDownDeadEnds(width, height);
        //RemoveLeftAndRightDeadEnds(width, height);
        for (int i = 1; i<=width-2; i++) {          //[1,w-2],[1,w-2],[3,w-3],[2,w-4]   up,down,left,right
            for (int j = 1; j<=height-3; j++) {     //[2,h-4],[3,h-3],[1,h-2],[1,h-2]
                if (mazeObjects[i, j]==null) continue;
                if (mazeObjects[i-1, j]!=null&&mazeObjects[i-1, j+1]!=null&&mazeObjects[i+1, j]!=null&&mazeObjects[i+1, j+1]!=null) {
                    RemoveOneDeadEnd(i, j);
                } else if (mazeObjects[i-1, j]!=null&&mazeObjects[i-1, j-1]!=null&&mazeObjects[i+1, j]!=null&&mazeObjects[i+1, j-1]!=null) {
                    RemoveOneDeadEnd(i, j);
                } else if (mazeObjects[i, j+1]!=null&&mazeObjects[i-1, j+1]!=null&&mazeObjects[i, j-1]!=null&&mazeObjects[i-1, j-1]!=null) {
                    RemoveOneDeadEnd(i, j);
                } else if (mazeObjects[i, j+1]!=null&&mazeObjects[i, j-1]!=null&&mazeObjects[i+1, j+1]!=null&&mazeObjects[i+1, j-1]!=null) {
                    RemoveOneDeadEnd(i, j);
                }
            }
        }
    }

    public void BreakLongWalls(int width, int height) {
        for (int i = 2; i<=width-3; i++) {
            for (int j = 2; j<=height-3; j++) {
                if (mazeObjects[i, j]==null) continue;
                if(mazeObjects[i-1, j]!=null&&mazeObjects[i-2, j]!=null&&mazeObjects[i+1, j]!=null&&mazeObjects[i+2, j]!=null) {    //横超过5格
                    int randomX = Random.Range(-2, 3);          //由于可能随机到的上下有墙，所以一次清理不完，再次运行可再次清理
                    if (mazeObjects[i+randomX, j+1]==null && mazeObjects[i+randomX, j-1]==null &&mazeObjects[i+randomX-1, j]!=null&&mazeObjects[i+randomX+1, j]!=null) {
                        BreakWall(i+randomX, j);                                             //i+randomX在边界时，上下有墙，不通过前面的if，所以不会溢出
                    }
                }else if (mazeObjects[i, j-1]!=null&&mazeObjects[i, j-2]!=null&&mazeObjects[i, j+1]!=null&&mazeObjects[i, j+2]!=null) { //竖
                    int randomY = Random.Range(-2, 3);
                    if (mazeObjects[i+1, j+randomY]==null && mazeObjects[i-1, j+randomY]==null&&mazeObjects[i, j+randomY-1]!=null&&mazeObjects[i, j+randomY+1]!=null) {
                        BreakWall(i, j+randomY);
                    }
                }
            }
        }
    }

    private void RemoveOneDeadEnd(int x, int y) {
        if (deadEndShouldRemove) {
            BreakWall(x, y);
            deadEndShouldRemove=false;
        } else {
            deadEndShouldRemove=true;
        }
    }

    /*private void RemoveUpAndDownDeadEnds(int width, int height) {
        for (int i = 1; i<=width-2; i++) {          //[1,w-2],[1,w-2]
            for (int j = 2; j<=height-3; j++) {     //[2,h-4],[3,h-3]
                if (mazeObjects[i, j]==null) continue;
                if (mazeObjects[i-1, j]!=null&&mazeObjects[i-1, j+1]!=null&&mazeObjects[i+1, j]!=null&&mazeObjects[i+1, j+1]!=null) {
                    if (deadEndShouldRemove) {
                        BreakWall(i, j);
                        deadEndShouldRemove=false;
                    } else {
                        deadEndShouldRemove=true;
                    }
                } else if (mazeObjects[i-1, j]!=null&&mazeObjects[i-1, j-1]!=null&&mazeObjects[i+1, j]!=null&&mazeObjects[i+1, j-1]!=null) {
                    if (deadEndShouldRemove) {
                        BreakWall(i, j);
                        deadEndShouldRemove=false;
                    } else {
                        deadEndShouldRemove=true;
                    }
                }
            }
        }
    }
    private void RemoveLeftAndRightDeadEnds(int width, int height) {
        for (int i = 2; i<=width-3; i++) {          //[3,w-3],[2,w-4]
            for (int j = 1; j<=height-2; j++) {     //[1,h-2],[1,h-2]
                if (mazeObjects[i, j]!=null&&mazeObjects[i, j+1]!=null&&mazeObjects[i-1, j+1]!=null&&mazeObjects[i, j-1]!=null&&mazeObjects[i-1, j-1]!=null) {
                    if (deadEndShouldRemove) {
                        BreakWall(i, j);
                        deadEndShouldRemove=false;
                    } else {
                        deadEndShouldRemove=true;
                    }
                } else if (mazeObjects[i, j]!=null&&mazeObjects[i, j+1]!=null&&mazeObjects[i, j-1]!=null&&mazeObjects[i+1, j+1]!=null&&mazeObjects[i+1, j-1]!=null) {
                    if (deadEndShouldRemove) {
                        BreakWall(i, j);
                        deadEndShouldRemove=false;
                    } else {
                        deadEndShouldRemove=true;
                    }
                }
            }
        }
    }*/
}
