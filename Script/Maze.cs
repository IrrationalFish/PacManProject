using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public int width;
    public int height;
    public GameObject[,] mazeObject;

	void Start () {
		
	}
	
	void Update () {

    }

    public GameObject[,] InitialiseMazeObject(int widthPar, int heightPar) {
        mazeObject = new GameObject[widthPar, heightPar];
        width = widthPar;
        height = heightPar;
        return mazeObject;
    }
}
