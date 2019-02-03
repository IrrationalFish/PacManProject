using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGenerator : MonoBehaviour {

    public GameObject cube;
    public GameObject mazePrefab;

    protected GameObject[,] mazeObjects;       //最左下角是0,0
    protected GameObject maze;

    void Start () {
	}
	
	void Update () {	
	}

    protected void GenerateMazeParent(int width, int height) {
        maze = Instantiate(mazePrefab);
        mazeObjects = maze.GetComponent<Maze>().InitialiseMazeObject(width, height);  //这里的mazeobj是maze的引用
    }

    protected void BreakWall(int x, int y) {
        Destroy(mazeObjects[x, y]);
        mazeObjects[x, y] = null;
    }

    public abstract GameObject GenerateMaze(int width, int height);
}
