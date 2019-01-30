using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenRD : MonoBehaviour {

    public GameObject cube;
    public GameObject mazePrefab;

    private GameObject[,] mazeObject;       //最左下角是0,0
    private GameObject maze;

    void Start () {
        /*mazeObject = new GameObject[width,height];
        GenBoundary();
        GenMaze(1, 1, width-2, height-2);*/
    }

    public GameObject GenerateMazeParent(int width, int height) {
        maze = Instantiate(mazePrefab);
        mazeObject = maze.GetComponent<Maze>().InitialiseMazeObject(width, height);  //这里的mazeobj是maze的引用
        return maze;
    }

    public void Generate(int width, int height) {
        GenBoundary(width, height);
        GenMaze(1, 1, width - 2, height - 2);
    }

    private void GenBoundary(int width, int height) {
        for (int i = 0; i < width; i++) {
            GenerateSingleCube(i, 0);

            GenerateSingleCube(i, height - 1);
        }
        for (int i = 0; i < height; i++) {
            GenerateSingleCube(0, i);

            GenerateSingleCube(width-1, i);
        }
    }

    private void BreakWall(int x1, int y1, int x2, int y2) {
        int pos;
        if (x1.Equals(x2)) {
            pos = y1 + ((int)Random.Range(0, ((y2 - y1) / 2))) * 2;
            Destroy(mazeObject[x1,pos]); 
        }else if (y1 == y2) {
            pos = x1 + ((int)Random.Range(0, ((x2 - x1) / 2))) * 2;
            Destroy(mazeObject[pos, y1]);
        } else {
            Debug.Log("Wrong");
        }
    }

    private void GenMaze(int x, int y, int width, int height) {
        int xPos, yPos;

        if (height <= 2 || width <= 2) {
            return;
        }

        xPos = x + ((int)Random.Range(0, (width / 2) - 1)) * 2 + 1;      //竖线
        for(int i = y; i < y + height; i++) {
            if (mazeObject[xPos, i] == null) {
                GenerateSingleCube(xPos, i);
            }
        }

        yPos = y + ((int)Random.Range(0, (height / 2) - 1)) * 2 + 1;       //横线
        for (int i = x; i < x + width; i++) {
            if(mazeObject[i,yPos] == null) {
                GenerateSingleCube(i, yPos);
            }
        }

        int closedWall = Random.Range(1, 4);
        switch (closedWall) {
            case 1:
                BreakWall(xPos + 1, yPos, x + width - 1, yPos);     //right
                BreakWall(xPos, yPos+1, xPos, y + height - 1);     //up
                BreakWall(x, yPos, xPos - 1, yPos);     //left
                break;
            case 2:
                BreakWall(xPos, y, xPos, yPos-1);       // buttom
                BreakWall(xPos, yPos + 1, xPos, y + height - 1);     //up
                BreakWall(xPos + 1, yPos, x + width - 1, yPos);     //right
                break;
            case 3:
                BreakWall(x, yPos, xPos - 1, yPos);     //left
                BreakWall(xPos + 1, yPos, x + width - 1, yPos);     //right
                BreakWall(xPos, y, xPos, yPos - 1);       // buttom
                break;
            case 4:
                BreakWall(xPos, y, xPos, yPos - 1);       // buttom
                BreakWall(x, yPos, xPos - 1, yPos);     //left
                BreakWall(xPos, yPos + 1, xPos, y + height - 1);     //up
                break;
            default:
                break;
        }
        GenMaze(x, y, xPos - x, yPos - y);          //left-buttom
        GenMaze(x, yPos + 1, xPos - x, y + height - yPos - 1);      //left-up
        GenMaze(xPos + 1, y, x + width - xPos - 1, yPos - y);       //right-buttom
        GenMaze(xPos + 1, yPos + 1, x + width - xPos - 1, y + height - yPos - 1);       //right-up
    }

    private void GenerateSingleCube(int xPar, int yPar) {
        GameObject singleCell = Instantiate(cube, new Vector3(xPar, 0, yPar), new Quaternion());
        mazeObject[xPar, yPar] = singleCell;
        singleCell.GetComponent<Cube>().SetCoordinateAttributeAndParent(xPar, yPar, maze.transform);
    }
}
