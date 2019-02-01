using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGenerator : MonoBehaviour {

    public GameObject cube;
    public GameObject mazePrefab;

    protected GameObject[,] mazeObject;       //最左下角是0,0
    protected GameObject maze;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public abstract GameObject GenerateMaze(int width, int height);

    //public abstract void GenerateMazeParent(int width, int height);

}
