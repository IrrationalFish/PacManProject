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
        mazeObject = maze.GetComponent<Maze>().initialiseMazeObject(width, height);  //这里的mazeobj是maze的引用
        return maze;
    }

    public void Generate(int width, int height) {
        mazeObject = new GameObject[width, height];
        GenBoundary(width, height);
        GenMaze(1, 1, width - 2, height - 2);
    }

        private void GenBoundary(int width, int height) {
        GameObject singleCell;
        for (int i = 0; i < width; i++) {
            singleCell = Instantiate(cube, new Vector3(i, 0, 0), new Quaternion());
            mazeObject[i, 0] = singleCell;
            singleCell.GetComponent<Cube>().SetCoordinateAttributeAndParent(i, 0, maze.transform);

            singleCell = Instantiate(cube, new Vector3(i, 0, height-1), new Quaternion());
            mazeObject[i, height-1] = singleCell;
            singleCell.GetComponent<Cube>().SetCoordinateAttributeAndParent(i, height - 1, maze.transform);
        }
        for (int i = 0; i < height; i++) {
            singleCell = Instantiate(cube, new Vector3(0, 0, i), new Quaternion());
            mazeObject[0, i] = singleCell;
            singleCell.GetComponent<Cube>().SetCoordinateAttributeAndParent(0, i, maze.transform);

            singleCell = Instantiate(cube, new Vector3(width-1, 0, i), new Quaternion());
            mazeObject[width-1, i] = singleCell;
            singleCell.GetComponent<Cube>().SetCoordinateAttributeAndParent(width-1, i, maze.transform);
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
                GameObject singleCell = Instantiate(cube, new Vector3(xPos, 0, i), new Quaternion());
                mazeObject[xPos, i] = singleCell;
                singleCell.GetComponent<Cube>().SetCoordinateAttributeAndParent(xPos, i, maze.transform);
            }
        }

        yPos = y + ((int)Random.Range(0, (height / 2) - 1)) * 2 + 1;       //横线
        for (int i = x; i < x + width; i++) {
            if(mazeObject[i,yPos] == null) {
                GameObject singleCell = Instantiate(cube, new Vector3(i, 0, yPos), new Quaternion());
                mazeObject[i, yPos] = singleCell;
                singleCell.GetComponent<Cube>().SetCoordinateAttributeAndParent(i, yPos, maze.transform);
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
