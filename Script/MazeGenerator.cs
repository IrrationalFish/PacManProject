using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGenerator : MonoBehaviour {


	void Start () {
		
	}
	
	void Update () {
		
	}

    public abstract GameObject GenerateMazeParent(int width, int height);

    public abstract void Generate(int width, int height);

}
