using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour {

    public float rotateSpeed;
    public int xPos;
    public int zPos;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 45, 0) * rotateSpeed, Space.World);
	}

    public void SetXAndZPos(int xPar, int zPar) {
        xPos=xPar;
        zPos=zPar;
    }
}
