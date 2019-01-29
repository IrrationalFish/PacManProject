using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenPrim : MonoBehaviour {

    public int width = 11;
    public int height = 11;
    public GameObject cube;
    public GameObject mazeParentPrefab;

    private Transform mazeParentTrans;
    private GameObject[,] mazeObject;
    private Vector3 originPos;
    private List<GameObject> wallsList = new List<GameObject>();

    void Start() {
        /*mazeObject = new GameObject[width, height];     //网上代码：先y后x，（1,2）代表第一行第二列(2,1)
        originPos = new Vector3(1, 1, 0);
        for(int i=0; i < width; i++) {
            for(int j =0; j< height; j++) {
                GameObject singleCube = Instantiate(cube, new Vector3(i, 0, j), new Quaternion());      //cube里的y对应的是实际的场景的z
                singleCube.GetComponent<Cube>().x = i;
                singleCube.GetComponent<Cube>().y = j;
                mazeObject[i, j] = singleCube;                                 //0代表墙，1代表可通过
                singleCube.transform.parent = mazeParent;
            }
        }
        int xPos = (int)originPos.x;
        int yPos = (int)originPos.y;
        breakWall(xPos, yPos);
        //breakWall(1, height - 2);
        if (xPos > 1) {
            wallsList.Add(mazeObject[xPos - 1, yPos]);
            mazeObject[xPos - 1, yPos].GetComponent<Cube>().blockDir = 0;//把左边墙进表
        }     
        if (yPos < height - 2) {
            wallsList.Add(mazeObject[xPos, yPos + 1]);//把上边墙进表
            mazeObject[xPos, yPos + 1].GetComponent<Cube>().blockDir = 1;
        }     
        if (xPos < width - 2) {
            wallsList.Add(mazeObject[xPos + 1, yPos]);//把右边墙进表
            mazeObject[xPos + 1, yPos].GetComponent<Cube>().blockDir = 2;
        }     
        if (yPos > 1) {
            wallsList.Add(mazeObject[xPos, yPos - 1]);//把下边墙进表
            mazeObject[xPos, yPos - 1].GetComponent<Cube>().blockDir = 3;
        }     
        //Debug.Log("s");

        while(wallsList.Count > 0) {
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

            if(mazeObject[xPos,yPos] != null) {
                breakWall(xPos, yPos);              //把“左边的左边”打通
                breakWall(wallsList[randomNumber].GetComponent<Cube>().x, wallsList[randomNumber].GetComponent<Cube>().y);      //把“左边”打通
                if(xPos>1 && mazeObject[xPos-1,yPos] != null && mazeObject[xPos - 2, yPos] !=null) {    //左边的邻墙
                    mazeObject[xPos - 1, yPos].GetComponent<Cube>().blockDir = 0;
                    wallsList.Add(mazeObject[xPos - 1, yPos]);
                }
                if (yPos < height - 2 && mazeObject[xPos, yPos + 1] != null && mazeObject[xPos, yPos + 2] != null) {    //上边的邻墙
                    mazeObject[xPos, yPos + 1].GetComponent<Cube>().blockDir = 1;
                    wallsList.Add(mazeObject[xPos, yPos + 1]);
                }
                if (xPos < width - 2 && mazeObject[xPos + 1, yPos] != null && mazeObject[xPos + 2, yPos] != null) {    //右边的邻墙
                    mazeObject[xPos + 1, yPos].GetComponent<Cube>().blockDir = 2;
                    wallsList.Add(mazeObject[xPos + 1, yPos]);
                }
                if (yPos > 1 && mazeObject[xPos, yPos - 1] != null && mazeObject[xPos, yPos - 2] != null) {    //下边的邻墙
                    mazeObject[xPos, yPos - 1].GetComponent<Cube>().blockDir = 3;
                    wallsList.Add(mazeObject[xPos, yPos - 1]);
                }
            }

            wallsList.RemoveAt(randomNumber);

        }*/
    }

    public GameObject generateMazeParent() {
        GameObject maze = Instantiate(mazeParentPrefab);
        mazeParentTrans = maze.transform;
        return maze;
    }

    public void Generate() {
        mazeObject = new GameObject[width, height];     //网上代码：先y后x，（1,2）代表第一行第二列(2,1)
        originPos = new Vector3(1, 1, 0);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                GameObject singleCube = Instantiate(cube, new Vector3(i, 0, j), new Quaternion());      //cube里的y对应的是实际的场景的z
                singleCube.GetComponent<Cube>().x = i;
                singleCube.GetComponent<Cube>().y = j;
                mazeObject[i, j] = singleCube;                                 //0代表墙，1代表可通过
                singleCube.transform.parent = mazeParentTrans;
            }
        }
        int xPos = (int)originPos.x;
        int yPos = (int)originPos.y;
        breakWall(xPos, yPos);
        //breakWall(1, height - 2);
        if (xPos > 1) {
            wallsList.Add(mazeObject[xPos - 1, yPos]);
            mazeObject[xPos - 1, yPos].GetComponent<Cube>().blockDir = 0;//把左边墙进表
        }
        if (yPos < height - 2) {
            wallsList.Add(mazeObject[xPos, yPos + 1]);//把上边墙进表
            mazeObject[xPos, yPos + 1].GetComponent<Cube>().blockDir = 1;
        }
        if (xPos < width - 2) {
            wallsList.Add(mazeObject[xPos + 1, yPos]);//把右边墙进表
            mazeObject[xPos + 1, yPos].GetComponent<Cube>().blockDir = 2;
        }
        if (yPos > 1) {
            wallsList.Add(mazeObject[xPos, yPos - 1]);//把下边墙进表
            mazeObject[xPos, yPos - 1].GetComponent<Cube>().blockDir = 3;
        }
        //Debug.Log("s");

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

            if (mazeObject[xPos, yPos] != null) {
                breakWall(xPos, yPos);              //把“左边的左边”打通
                breakWall(wallsList[randomNumber].GetComponent<Cube>().x, wallsList[randomNumber].GetComponent<Cube>().y);      //把“左边”打通
                if (xPos > 1 && mazeObject[xPos - 1, yPos] != null && mazeObject[xPos - 2, yPos] != null) {    //左边的邻墙
                    mazeObject[xPos - 1, yPos].GetComponent<Cube>().blockDir = 0;
                    wallsList.Add(mazeObject[xPos - 1, yPos]);
                }
                if (yPos < height - 2 && mazeObject[xPos, yPos + 1] != null && mazeObject[xPos, yPos + 2] != null) {    //上边的邻墙
                    mazeObject[xPos, yPos + 1].GetComponent<Cube>().blockDir = 1;
                    wallsList.Add(mazeObject[xPos, yPos + 1]);
                }
                if (xPos < width - 2 && mazeObject[xPos + 1, yPos] != null && mazeObject[xPos + 2, yPos] != null) {    //右边的邻墙
                    mazeObject[xPos + 1, yPos].GetComponent<Cube>().blockDir = 2;
                    wallsList.Add(mazeObject[xPos + 1, yPos]);
                }
                if (yPos > 1 && mazeObject[xPos, yPos - 1] != null && mazeObject[xPos, yPos - 2] != null) {    //下边的邻墙
                    mazeObject[xPos, yPos - 1].GetComponent<Cube>().blockDir = 3;
                    wallsList.Add(mazeObject[xPos, yPos - 1]);
                }
            }

            wallsList.RemoveAt(randomNumber);

        }
    }

    private void breakWall(int x, int y) {
        Destroy(mazeObject[x, y]);
        mazeObject[x, y] = null;
    }
}
