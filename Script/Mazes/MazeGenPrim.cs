using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenPrim : MazeGenerator {

    private Vector3 originPos;
    private List<GameObject> wallsList = new List<GameObject>();

    override public GameObject GenerateMaze(int width, int height) {
        GenerateMazeParent(width, height);
        GenMaze(width, height);
        RemoveDeadEnds(width, height);
        BreakLongWalls(width, height);
        return maze;
    }

    private void GenMaze(int width, int height) {
        originPos = new Vector3(1, 0, 1);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                GameObject singleCube = Instantiate(cube, new Vector3(i, 0, j), new Quaternion());      //cube里的y对应的是实际的场景的z
                singleCube.GetComponent<Cube>().x = i;
                singleCube.GetComponent<Cube>().y = j;
                mazeObjects[i, j] = singleCube;                                 //0代表墙，1代表可通过    //这里改一下
                singleCube.transform.parent = maze.transform;
            }
        }
        int xPos = (int)originPos.x;
        int yPos = (int)originPos.z;
        BreakWall(xPos, yPos);
        //BreakWall(1, height - 2);
        if (xPos > 1) {
            wallsList.Add(mazeObjects[xPos - 1, yPos]);
            mazeObjects[xPos - 1, yPos].GetComponent<Cube>().blockDir = 0;//把左边墙进表
        }
        if (yPos < height - 2) {
            wallsList.Add(mazeObjects[xPos, yPos + 1]);//把上边墙进表
            mazeObjects[xPos, yPos + 1].GetComponent<Cube>().blockDir = 1;
        }
        if (xPos < width - 2) {
            wallsList.Add(mazeObjects[xPos + 1, yPos]);//把右边墙进表
            mazeObjects[xPos + 1, yPos].GetComponent<Cube>().blockDir = 2;
        }
        if (yPos > 1) {
            wallsList.Add(mazeObjects[xPos, yPos - 1]);//把下边墙进表
            mazeObjects[xPos, yPos - 1].GetComponent<Cube>().blockDir = 3;
        }

        while (wallsList.Count > 0) {
            int randomNumber = Random.Range(0, wallsList.Count);
            switch (wallsList[randomNumber].GetComponent<Cube>().blockDir) {
                case 0:             //左边
                    xPos = wallsList[randomNumber].GetComponent<Cube>().x - 1;      //左边的左边作为目标格
                    yPos = wallsList[randomNumber].GetComponent<Cube>().y;
                    break;
                case 1:             //上边
                    xPos = wallsList[randomNumber].GetComponent<Cube>().x;
                    yPos = wallsList[randomNumber].GetComponent<Cube>().y + 1;
                    break;
                case 2:             //右边
                    xPos = wallsList[randomNumber].GetComponent<Cube>().x + 1;
                    yPos = wallsList[randomNumber].GetComponent<Cube>().y;
                    break;
                case 3:             //下边
                    xPos = wallsList[randomNumber].GetComponent<Cube>().x;
                    yPos = wallsList[randomNumber].GetComponent<Cube>().y - 1;
                    break;
                default:
                    Debug.Log("Dir Error");
                    break;
            }

            if (mazeObjects[xPos, yPos] != null) {
                BreakWall(xPos, yPos);              //把“左边的左边”打通
                BreakWall(wallsList[randomNumber].GetComponent<Cube>().x, wallsList[randomNumber].GetComponent<Cube>().y);      //把“左边”打通
                if (xPos > 1 && mazeObjects[xPos - 1, yPos] != null && mazeObjects[xPos - 2, yPos] != null) {    //左边的邻墙
                    mazeObjects[xPos - 1, yPos].GetComponent<Cube>().blockDir = 0;
                    wallsList.Add(mazeObjects[xPos - 1, yPos]);
                }
                if (yPos < height - 2 && mazeObjects[xPos, yPos + 1] != null && mazeObjects[xPos, yPos + 2] != null) {    //上边的邻墙
                    mazeObjects[xPos, yPos + 1].GetComponent<Cube>().blockDir = 1;
                    wallsList.Add(mazeObjects[xPos, yPos + 1]);
                }
                if (xPos < width - 2 && mazeObjects[xPos + 1, yPos] != null && mazeObjects[xPos + 2, yPos] != null) {    //右边的邻墙
                    mazeObjects[xPos + 1, yPos].GetComponent<Cube>().blockDir = 2;
                    wallsList.Add(mazeObjects[xPos + 1, yPos]);
                }
                if (yPos > 1 && mazeObjects[xPos, yPos - 1] != null && mazeObjects[xPos, yPos - 2] != null) {    //下边的邻墙
                    mazeObjects[xPos, yPos - 1].GetComponent<Cube>().blockDir = 3;
                    wallsList.Add(mazeObjects[xPos, yPos - 1]);
                }
            }

            wallsList.RemoveAt(randomNumber);

        }
    }

}
