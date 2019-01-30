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

    public GameObject[,] initialiseMazeObject(int width, int height) {
        mazeObject = new GameObject[width, height];
        return mazeObject;
    }
}
