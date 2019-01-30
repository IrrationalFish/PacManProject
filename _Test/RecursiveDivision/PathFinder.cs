using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    //public GameObject gameManager;
    //public Transform start;
    //public Transform end;
    public GameObject pathCube;
    public Transform pathParent;

    private bool[,] visited;
    private Stack<Vector3> path;
    private bool findPath = false;

	void Start () {
        path = new Stack<Vector3>();
        visited = new bool[MazeGenRDdelay.xMax, MazeGenRDdelay.yMax];
    }

    void Update() {
        //Debug.Log(DelayCube.getNoOfCubes() + "," + DelayCube.getNoOfInPosCubes());
        if (DelayCube.getNoOfCubes() == DelayCube.getNoOfInPosCubes() && !findPath) {
            getShortestPath(new Vector3(1, 1, 0), new Vector3(MazeGenRDdelay.xMax - 2, MazeGenRDdelay.yMax - 2, 0));
        }
    }

    private void getShortestPath(Vector3 start, Vector3 end) {

        Vector3 pos = start;
        path.Push(start);
        visited[(int)start.x, (int)start.y] = true;

        while (true) {
            pos = path.Peek();
            visited[(int)pos.x, (int)pos.y] = true;
            if (MazeGenRDdelay.cells[(int)pos.x, (int)pos.y + 1] == null && visited[(int)pos.x, (int)pos.y + 1] != true) {       //上方无障碍，且未访问过
                path.Push(new Vector3(pos.x, pos.y + 1, 0));
                continue;
            }
            if (MazeGenRDdelay.cells[(int)pos.x, (int)pos.y - 1] == null && visited[(int)pos.x, (int)pos.y - 1] != true) {       //下方无障碍，且未访问过
                path.Push(new Vector3(pos.x, pos.y - 1, 0));
                continue;
            }
            if (MazeGenRDdelay.cells[(int)pos.x + 1, (int)pos.y] == null && visited[(int)pos.x + 1, (int)pos.y] != true) {       //右方无障碍，且未访问过
                path.Push(new Vector3(pos.x + 1, pos.y, 0));
                continue;
            }
            if (MazeGenRDdelay.cells[(int)pos.x - 1, (int)pos.y] == null && visited[(int)pos.x - 1, (int)pos.y] != true) {       //左方无障碍，且未访问过
                path.Push(new Vector3(pos.x - 1, pos.y, 0));
                continue;
            }

            if (pos.Equals(end)) {break;}

            path.Pop();
        }
        findPath = true;

        int noOfSteps = path.Count;
        for(int i = 0; i<noOfSteps; i++) {
            Vector3 pathPos = path.Pop();
            GameObject singlePath = Instantiate(pathCube, pathPos, new Quaternion());
            singlePath.transform.parent = pathParent;

        }
    }
}
