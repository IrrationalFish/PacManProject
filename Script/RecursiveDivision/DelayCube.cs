using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayCube : MonoBehaviour {

    public float moveSpeed = 1f;

    [SerializeField] private Vector3 pos;
    [SerializeField] private bool inPos = false;

    private static int noOfCubes = 0;
    private static int noOfInPosCubes = 0;

    void Start () {
        noOfCubes++;
	}
    private void OnDestroy() {
        noOfCubes--;
        if (inPos) noOfInPosCubes--;
    }

    void Update () {
        if (inPos) {
            return;
        }
        if(gameObject.transform.position == pos) {
            inPos = true;
            noOfInPosCubes++;
        }
		if(pos != null) {
            Vector3 p = Vector3.MoveTowards(transform.position, pos, moveSpeed);
            gameObject.transform.SetPositionAndRotation(p, new Quaternion());
        }
	}

    public void setPosition(Vector3 posPar) {pos = posPar;}

    public void setIsInPos(bool isInPosPar) {inPos = isInPosPar;}

    public static int getNoOfCubes() {return noOfCubes;}

    public static int getNoOfInPosCubes() { return noOfInPosCubes; }
}
