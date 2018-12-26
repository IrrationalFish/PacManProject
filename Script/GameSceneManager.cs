using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public GameObject mazePrefab;

	void Start () {
        Instantiate(mazePrefab);
        GetComponent<MazeGenPrim>().Generate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
