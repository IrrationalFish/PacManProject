using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public int x;
    public int y;
    public int blockDir;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCoordinateAttribute(int xPar, int yPar) {
        x = xPar;
        y = yPar;
    }

    public void SetParent(Transform parentPar) {
        this.transform.parent = parentPar;
    }

    public void SetCoordinateAttributeAndParent(int xPar, int yPar, Transform parentPar) {
        this.x = xPar;
        this.y = yPar;
        this.transform.parent = parentPar;
    }
}
