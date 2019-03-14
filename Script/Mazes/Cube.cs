using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public int x;
    public int y;
    public int blockDir;
    public bool isBoundary;
    public Material boundaryMat;
    public GameObject boundaryCube;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCoordinateAttribute(int xPar, int yPar) {
        x = xPar;
        y = yPar;
        isBoundary=IsBoundary();
    }

    public void SetParent(Transform parentPar) {
        this.transform.parent = parentPar;
    }

    public void SetCoordinateAttributeAndParent(int xPar, int yPar, Transform parentPar) {
        this.x = xPar;
        this.y = yPar;
        this.transform.parent = parentPar;
        isBoundary=IsBoundary();
        if (isBoundary) {
            gameObject.GetComponent<MeshRenderer>().enabled=false;
            Instantiate(boundaryCube, this.gameObject.transform);
        }
    }

    private bool IsBoundary() {
        GameSceneManager gameManagerScript = GameObject.Find("GameManager").GetComponent<GameSceneManager>();
        int width = gameManagerScript.mazeWidth;
        int height = gameManagerScript.mazeHeight;
        if(x==0 || x==width-1 || y==0 ||y==height-1) {
            return true;
        } else {
            return false;
        }
    }
}
