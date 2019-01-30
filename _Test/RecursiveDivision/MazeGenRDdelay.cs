using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenRDdelay : MonoBehaviour {

    public GameObject cell;
    public Transform mazeParent;
    public static int xMax = 91;
    public static int yMax = 81;

    public static GameObject[,] cells;

    public static bool genFinished = false;
    private int corCreated = 0;
    private int corFinished = 0;

    void Start() {
        cells = new GameObject[xMax, yMax];
        genBoundary();
        StartCoroutine(genMaze(1, 1, yMax - 2, xMax - 2));      //startPoint,height,width
    }

    void Update() {
        if (!genFinished && corCreated == corFinished) {
            genFinished = true;
        }
    }

    private void genBoundary() {
        for (int i = 0; i < xMax; i++) {
            createCube(new Vector3(i, 0, 0), new Vector3(i, 0, 0));
            createCube(new Vector3(i, yMax - 1, 0), new Vector3(i, yMax - 1, 0));
        }
        for (int i = 0; i < yMax; i++) {
            createCube(new Vector3(0, i, 0), new Vector3(0, i, 0));
            createCube(new Vector3(xMax - 1, i, 0), new Vector3(xMax - 1, i, 0));
        }
    }

    private void breakWall(int x1, int y1, int x2, int y2) {
        int pos;
        if (x1.Equals(x2)) {
            pos = y1 + ((int)Random.Range(0, ((y2 - y1) / 2))) * 2;
            Destroy(cells[x1, pos]);
        } else if (y1 == y2) {
            pos = x1 + ((int)Random.Range(0, ((x2 - x1) / 2))) * 2;
            Destroy(cells[pos, y1]);
        } else {
            Debug.Log("Wrong");
        }
    }

    private IEnumerator genMaze(int x, int y, int height, int width) {
        int xPos, yPos;

        if (height <= 2 || width <= 2) {yield break;}

        corCreated++;

        xPos = x + ((int)Random.Range(0, (width / 2) - 1)) * 2 + 1;      //竖线
        for (int i = y; i < y + height; i++) {
            if (cells[xPos, i] == null) {
                //int randomDir = Random.Range(0, 2);
                //if(randomDir == 0) {
                //    createCube(new Vector3(xMax + 10, i, 0), new Vector3(xPos, i, 0));
                //} else {
                //    createCube(new Vector3(-10, i, 0), new Vector3(xPos, i, 0));
                //}
                createCube(new Vector3(xMax + 10, i, 0), new Vector3(xPos, i, 0));
                yield return new WaitForSeconds(0.01f);
            }
        }

        yPos = y + ((int)Random.Range(0, (height / 2) - 1)) * 2 + 1;       //横线
        for (int i = x; i < x + width; i++) {
            if (cells[i, yPos] == null) {
                //int randomDir = Random.Range(0, 2);
                //if (randomDir == 0) {
                //    createCube(new Vector3(i, yMax + 10, 0), new Vector3(i, yPos, 0));
                //} else {
                //    createCube(new Vector3(i, -10, 0), new Vector3(i, yPos, 0));
                //}
                createCube(new Vector3(i, yMax + 10, 0), new Vector3(i, yPos, 0));
                yield return new WaitForSeconds(0.01f);
            }
        }

        int closedWall = Random.Range(1, 4);
        switch (closedWall) {
            case 1:
                breakWall(xPos + 1, yPos, x + width - 1, yPos);     //right
                breakWall(xPos, yPos + 1, xPos, y + height - 1);     //up
                breakWall(x, yPos, xPos - 1, yPos);     //left
                break;
            case 2:
                breakWall(xPos, y, xPos, yPos - 1);       // buttom
                breakWall(xPos, yPos + 1, xPos, y + height - 1);     //up
                breakWall(xPos + 1, yPos, x + width - 1, yPos);     //right
                break;
            case 3:
                breakWall(x, yPos, xPos - 1, yPos);     //left
                breakWall(xPos + 1, yPos, x + width - 1, yPos);     //right
                breakWall(xPos, y, xPos, yPos - 1);       // buttom
                break;
            case 4:
                breakWall(xPos, y, xPos, yPos - 1);       // buttom
                breakWall(x, yPos, xPos - 1, yPos);     //left
                breakWall(xPos, yPos + 1, xPos, y + height - 1);     //up
                break;
            default:
                break;
        }

        StartCoroutine(genMaze(x, y, yPos - y, xPos - x));
        StartCoroutine(genMaze(x, yPos + 1, y + height - yPos - 1, xPos - x));
        StartCoroutine(genMaze(xPos + 1, y, yPos - y, x + width - xPos - 1));
        StartCoroutine(genMaze(xPos + 1, yPos + 1, y + height - yPos - 1, x + width - xPos - 1));

        corFinished++;
    }

    private void createCube(Vector3 genPos, Vector3 finalPos) {
        GameObject singleCell = Instantiate(cell, genPos, new Quaternion());
        singleCell.GetComponent<DelayCube>().setPosition(finalPos);
        singleCell.transform.parent = mazeParent;
        cells[(int)finalPos.x, (int)finalPos.y] = singleCell;
    }

    public static bool getGenFinished() {
        return genFinished;
    }

}
