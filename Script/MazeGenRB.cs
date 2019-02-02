using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenRB : MazeGenerator {

    private Vector3 originPos = new Vector3(1, 0, 1);
    private bool[,] visitedList;
    private Vector3 current;
    private Vector3 next;
    private Stack<Vector3> path = new Stack<Vector3>();

    public override GameObject GenerateMaze(int width, int height) {
        GenerateMazeParent(width, height);
        GenMaze(width, height);
        return maze;
    }

    private void GenerateMazeParent(int width, int height) {
        maze = Instantiate(mazePrefab);
        mazeObjects = maze.GetComponent<Maze>().InitialiseMazeObject(width, height);  //这里的mazeobj是maze的引用
    }

    private void GenMaze(int width, int height) {
        InitialiseMazeBoard(width, height);
        path.Push(current);
        while(path.Count != 0) {
            Vector3[] adjs = GetNotVisitedAdjs(current,width,height);    //寻找未被访问的邻接格
            if (adjs.Length == 0) {
                current = path.Pop();               //如果该格子没有可访问的邻接格，则跳回上一个格子
                continue;
            }
            next = adjs[Random.Range(0, adjs.Length)];
            int x = (int)next.x;
            int y = (int)next.z;
            if (visitedList[x, y]) {
                current = path.Pop();       //如果该节点被访问过，则回到上一步继续寻找
            } else {            //否则将当前格压入栈，标记当前格为已访问，并且在迷宫地图上移除障碍物
                path.Push(next);
                visitedList[x, y] = true;
                BreakWall(x, y);
                BreakWall(((int)current.x + x) / 2, ((int)current.z + y) / 2);      //移除当前格与下一个之间的墙壁
                current = next;
            }
        }
    }

    private void InitialiseMazeBoard(int width, int height) {
        visitedList = new bool[width, height];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                GameObject singleCube = Instantiate(cube, new Vector3(i, 0, j), new Quaternion());      //cube里的y对应的是实际的场景的z
                mazeObjects[i, j] = singleCube;
                singleCube.GetComponent<Cube>().SetCoordinateAttributeAndParent(i, j, maze.transform);
                visitedList[i,j] = false;
            }
        }
        BreakWall((int)originPos.x, (int)originPos.z);
        visitedList[(int)originPos.x, (int)originPos.z] = true;
        current = originPos;
    }

    private Vector3[] GetNotVisitedAdjs(Vector3 node, int width, int height) {
        int[,] adj = { { 0, 2 }, { 0, -2 }, { 2, 0 }, { -2, 0 } };      //上下右左
        List<Vector3> list = new List<Vector3>();
        for (int i = 0; i < 4; i++) {
            int x = (int)node.x + adj[i,0];
            int y = (int)node.z + adj[i,1];
            if (x >= 1 && x < (width-1) && y >= 1 && y < (height-1)) {
                if (!visitedList[x, y]) {
                    list.Add(new Vector3(x, 0, y));
                }
            }
        }
        Vector3[] a = new Vector3[list.Count];
        for (int i = 0; i < list.Count; i++) {
            a[i] = list[i];
        }
        return a;
    }

    private void BreakWall(int x, int y) {
        Destroy(mazeObjects[x, y]);
        mazeObjects[x, y] = null;
    }
}
