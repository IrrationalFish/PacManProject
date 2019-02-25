using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Ghost {

    public int detectDistance;

    protected override Vector3 GetNextEnd() {
        return new Vector3(1, 0, 1);
    }

    protected override void InitialiseGhost() {
        //do nothing
    }

    private new void FixedUpdate() {
        if (path.Count>0) {
            MoveToPosition(path.Peek());
        }
    }

    private new void Update() {
        Vector3 pos = this.transform.position;
        bool xPositionReady = Mathf.Abs(pos.x-Mathf.RoundToInt(pos.x))<delta;
        bool zPositionReady = Mathf.Abs(pos.z-Mathf.RoundToInt(pos.z))<delta;
        if (xPositionReady&&zPositionReady) {
            DrawLine();
            int layerMask = 1<<8|1<<13;   //只和8player,13wall反应
            RaycastHit hitInfo;
            if (Physics.Linecast(pos, pos+new Vector3(0, 0, detectDistance), out hitInfo, layerMask)) {     //up
                if (hitInfo.collider.tag=="Player") {
                    path=new Stack<Vector3>();
                    Debug.Log("up: "+Mathf.RoundToInt(hitInfo.distance+0.1f));
                    int distance = Mathf.RoundToInt(hitInfo.distance+0.1f);
                    Debug.Log(hitInfo.collider.transform.forward);
                    SetPath(new Vector3(0, 0, 1), distance, hitInfo.collider.transform.forward);
                }
            }
            if (Physics.Linecast(pos, pos-new Vector3(0, 0, detectDistance), out hitInfo, layerMask)) {     //down
                if (hitInfo.collider.tag=="Player") {
                    path=new Stack<Vector3>();
                    Debug.Log("down: "+Mathf.RoundToInt(hitInfo.distance+0.1f));
                    int distance = Mathf.RoundToInt(hitInfo.distance+0.1f);
                    SetPath(new Vector3(0, 0, -1), distance, hitInfo.collider.transform.forward);
                }
            }
            if (Physics.Linecast(pos, pos-new Vector3(detectDistance, 0, 0), out hitInfo, layerMask)) {     //left
                if (hitInfo.collider.tag=="Player") {
                    path=new Stack<Vector3>();
                    Debug.Log("left: "+Mathf.RoundToInt(hitInfo.distance+0.1f));
                    int distance = Mathf.RoundToInt(hitInfo.distance+0.1f);
                    Debug.Log(hitInfo.collider.transform.forward);
                    SetPath(new Vector3(-1, 0, 0), distance, hitInfo.collider.transform.forward);
                }
            }
            if (Physics.Linecast(pos, pos+new Vector3(detectDistance, 0, 0), out hitInfo, layerMask)) {     //right
                if (hitInfo.collider.tag=="Player") {
                    path=new Stack<Vector3>();
                    Debug.Log("right: "+Mathf.RoundToInt(hitInfo.distance+0.1f));
                    int distance = Mathf.RoundToInt(hitInfo.distance+0.1f);
                    Debug.Log(hitInfo.collider.transform.forward);
                    SetPath(new Vector3(1, 0, 0), distance, hitInfo.collider.transform.forward);
                }
            }
        }
    }

    private void SetPath(Vector3 dir, int cubeDistance, Vector3 nextDir) {
        Debug.Log("Next: "+nextDir);
        Stack<Vector3> tempPath = new Stack<Vector3>();
        Vector3 currentPos = new Vector3(Mathf.RoundToInt(this.transform.position.x), 0, Mathf.RoundToInt(this.transform.position.z));
        Vector3 turnPoint = currentPos+dir*cubeDistance;
        Debug.Log("curr: "+currentPos+",turn: "+turnPoint);
        for (int i = 0; i<cubeDistance; i++) {
            tempPath.Push(currentPos+dir*(i+1));
        }
        int turnCount = 1;
        while (true) {
            Vector3 newPoint = turnPoint+nextDir*turnCount;
            int x = Mathf.RoundToInt(newPoint.x);
            int z = Mathf.RoundToInt(newPoint.z);
            if (gmScript.MazeCubeIsBlocked(x,z)) {
                Debug.Log("New Point: "+newPoint+ " IsBloacked: "+gmScript.MazeCubeIsBlocked(x, z));
                break;
            }
            tempPath.Push(newPoint);
            turnCount++;
        }
        while (tempPath.Count>0) {
            path.Push(tempPath.Pop());
        }
    }

    private void DrawLine() {
        Vector3 pos = this.transform.position;
        Debug.DrawLine(pos, pos+new Vector3(0, 0, detectDistance), Color.red);      //up
        Debug.DrawLine(pos, pos-new Vector3(0, 0, detectDistance), Color.red);      //down
        Debug.DrawLine(pos, pos-new Vector3(detectDistance, 0, 0), Color.red);   //left
        Debug.DrawLine(pos, pos+new Vector3(detectDistance, 0, 0), Color.red); //right
    }
}
