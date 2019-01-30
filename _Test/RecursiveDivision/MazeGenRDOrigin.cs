using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenRDOrigin : MonoBehaviour {

    public GameObject cell;
    public int xMax = 11;
    public int yMax = 11;

    private GameObject[,] cells;

    void Start() {
        cells = new GameObject[xMax, xMax];
        GenBoundary();
        GenMaze(1, 1, xMax - 2, xMax - 2);
    }

    void Update() {

    }

    private void GenBoundary() {
        GameObject singleCell;
        for (int i = 0; i < xMax; i++) {
            singleCell = Instantiate(cell, new Vector3(i, 0, 0), new Quaternion());
            cells[i, 0] = singleCell;
            singleCell = Instantiate(cell, new Vector3(i, yMax - 1, 0), new Quaternion());
            //Debug.Log(i + "," + (yMax - 1));
            cells[i, yMax - 1] = singleCell;
        }
        for (int i = 0; i < yMax; i++) {
            singleCell = Instantiate(cell, new Vector3(0, i, 0), new Quaternion());
            cells[0, i] = singleCell;
            singleCell = Instantiate(cell, new Vector3(xMax - 1, i, 0), new Quaternion());
            cells[xMax - 1, i] = singleCell;
        }
    }

    private void BreakWall(int x1, int y1, int x2, int y2) {
        int pos;
        //Debug.Log(x1 + "," + y1 + "," + x2 + "," + y2);
        if (x1.Equals(x2)) {
            pos = y1 + ((int)Random.Range(0, ((y2 - y1) / 2))) * 2;
            //Debug.Log(x1 + "," + pos);
            Destroy(cells[x1, pos]);
        } else if (y1 == y2) {
            pos = x1 + ((int)Random.Range(0, ((x2 - x1) / 2))) * 2;
            //Debug.Log(pos + "," + y1);
            Destroy(cells[pos, y1]);
        } else {
            Debug.Log("Wrong");
        }
    }

    private void GenMaze(int x, int y, int height, int width) {
        //Debug.Log("gen" + x + "," + y + "," + height + "," + width + ",");
        int xPos, yPos;

        if (height <= 2 || width <= 2) {
            return;
        }

        xPos = x + ((int)Random.Range(0, (width / 2) - 1)) * 2 + 1;      //竖线
        for (int i = y; i < y + height; i++) {
            //Debug.Log(xPos + "," + i);
            if (cells[xPos, i] == null) {
                GameObject singleCell = Instantiate(cell, new Vector3(xPos, i, 0), new Quaternion());
                cells[xPos, i] = singleCell;
            }

        }

        yPos = y + ((int)Random.Range(0, (height / 2) - 1)) * 2 + 1;       //横线
        for (int i = x; i < x + width; i++) {
            if (cells[i, yPos] == null) {
                GameObject singleCell = Instantiate(cell, new Vector3(i, yPos, 0), new Quaternion());
                cells[i, yPos] = singleCell;
            }
        }

        int closedWall = Random.Range(1, 4);
        switch (closedWall) {
            case 1:
                BreakWall(xPos + 1, yPos, x + width - 1, yPos);     //right
                BreakWall(xPos, yPos + 1, xPos, y + height - 1);     //up
                BreakWall(x, yPos, xPos - 1, yPos);     //left
                break;
            case 2:
                BreakWall(xPos, y, xPos, yPos - 1);       // buttom
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

        /*Debug.Log("xPos: " + xPos + ",yPos: " + yPos);
        Debug.Log("lb:" + x + "," + y + "," + (yPos - y) + "," + (xPos - x));
        Debug.Log("ru: " + (xPos + 1) + "," + (yPos + 1) + "," + (y + height - yPos - 1) + "," + (x + width - xPos - 1));*/
        GenMaze(x, y, yPos - y, xPos - x);          //left-buttom
        GenMaze(x, yPos + 1, y + height - yPos - 1, xPos - x);      //left-up
        GenMaze(xPos + 1, y, yPos - y, x + width - xPos - 1);       //right-buttom
        GenMaze(xPos + 1, yPos + 1, y + height - yPos - 1, x + width - xPos - 1);       //right-up
    }
}
