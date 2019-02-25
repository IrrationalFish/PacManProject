using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 0.1f;

    public char nextMoveDir;
    public bool startMovement;
    public float turningLineDistance;

    void Start() {
        startMovement=false;
    }

    private void Update() {
        if (Input.GetKeyDown("w")) {
            nextMoveDir='w';
            startMovement=true;
        } else if (Input.GetKeyDown("a")) {
            nextMoveDir='a';
            startMovement=true;
        } else if (Input.GetKeyDown("s")) {
            nextMoveDir='s';
            startMovement=true;
        } else if (Input.GetKeyDown("d")) {
            nextMoveDir='d';
            startMovement=true;
        }

        if (nextMoveDir=='a'&&Valid('a')) {
            transform.localEulerAngles=new Vector3(0, -90, 0);
        } else if (nextMoveDir=='s'&&Valid('s')) {
            transform.localEulerAngles=new Vector3(0, 180, 0);
        } else if (nextMoveDir=='d'&&Valid('d')) {
            transform.localEulerAngles=new Vector3(0, 90, 0);
        } else if (nextMoveDir=='w'&&Valid('w')) {
            transform.localEulerAngles=new Vector3(0, 0, 0);
        }

        /*if (nextMoveDir == 'a') {
            transform.localEulerAngles = new Vector3(0, -90, 0);
        } else if (nextMoveDir == 's') {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        } else if (nextMoveDir == 'd') {
            transform.localEulerAngles = new Vector3(0, 90, 0);
        } else if (nextMoveDir == 'w') {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }*/
    }

    void FixedUpdate() {
        if (startMovement==false) {
            return;
        }
        transform.Translate(Vector3.forward*moveSpeed);
    }

    bool Valid(char dir) {
        Vector3 dirVector = new Vector3(0, 0, 0);
        int layerMask=1<<9 | 1<<12;
        layerMask=~layerMask;       //不和9tem,12ghost反应
        bool lineOneHit;
        bool lineTwoHit;
        if (dir=='a') {         //想往左
            dirVector=new Vector3(-1, 0, 0);
            Debug.DrawLine(gameObject.transform.position+new Vector3(0, 0, -turningLineDistance), gameObject.transform.position+dirVector+new Vector3(0, 0, -turningLineDistance), Color.red);
            Debug.DrawLine(gameObject.transform.position+new Vector3(0, 0, turningLineDistance), gameObject.transform.position+dirVector+new Vector3(0, 0, turningLineDistance), Color.red);
            lineOneHit=Physics.Linecast(gameObject.transform.position+new Vector3(0, 0, -turningLineDistance), gameObject.transform.position+dirVector+new Vector3(0, 0, -turningLineDistance), layerMask);
            lineTwoHit=Physics.Linecast(gameObject.transform.position+new Vector3(0, 0, turningLineDistance), gameObject.transform.position+dirVector+new Vector3(0, 0, turningLineDistance), layerMask);
        } else if(dir=='s') {      //想往下
            dirVector=new Vector3(0, 0, -1);
            Debug.DrawLine(gameObject.transform.position+new Vector3(-turningLineDistance, 0, 0), gameObject.transform.position+dirVector+new Vector3(-turningLineDistance, 0, 0), Color.red);
            Debug.DrawLine(gameObject.transform.position+new Vector3(turningLineDistance, 0, 0), gameObject.transform.position+dirVector+new Vector3(turningLineDistance, 0, 0), Color.red);
            lineOneHit=Physics.Linecast(gameObject.transform.position+new Vector3(-turningLineDistance, 0, 0), gameObject.transform.position+dirVector+new Vector3(-turningLineDistance, 0, 0), layerMask);
            lineTwoHit=Physics.Linecast(gameObject.transform.position+new Vector3(turningLineDistance, 0, 0), gameObject.transform.position+dirVector+new Vector3(turningLineDistance, 0, 0), layerMask);
        } else if (dir=='d') {
            dirVector=new Vector3(1, 0, 0);
            Debug.DrawLine(gameObject.transform.position+new Vector3(0, 0, -turningLineDistance), gameObject.transform.position+dirVector+new Vector3(0, 0, -turningLineDistance), Color.red);
            Debug.DrawLine(gameObject.transform.position+new Vector3(0, 0, turningLineDistance), gameObject.transform.position+dirVector+new Vector3(0, 0, turningLineDistance), Color.red);
            lineOneHit=Physics.Linecast(gameObject.transform.position+new Vector3(0, 0, -turningLineDistance), gameObject.transform.position+dirVector+new Vector3(0, 0, -turningLineDistance), layerMask);
            lineTwoHit=Physics.Linecast(gameObject.transform.position+new Vector3(0, 0, turningLineDistance), gameObject.transform.position+dirVector+new Vector3(0, 0, turningLineDistance), layerMask);

        } else if (dir=='w') {
            dirVector=new Vector3(0, 0, 1);
            Debug.DrawLine(gameObject.transform.position+new Vector3(-turningLineDistance, 0, 0), gameObject.transform.position+dirVector+new Vector3(-turningLineDistance, 0, 0), Color.red);
            Debug.DrawLine(gameObject.transform.position+new Vector3(turningLineDistance, 0, 0), gameObject.transform.position+dirVector+new Vector3(turningLineDistance, 0, 0), Color.red);
            lineOneHit=Physics.Linecast(gameObject.transform.position+new Vector3(-turningLineDistance, 0, 0), gameObject.transform.position+dirVector+new Vector3(-turningLineDistance, 0, 0), layerMask);
            lineTwoHit=Physics.Linecast(gameObject.transform.position+new Vector3(turningLineDistance, 0, 0), gameObject.transform.position+dirVector+new Vector3(turningLineDistance, 0, 0), layerMask);
        } else {
            return false;
        }

        if (!lineOneHit&&!lineTwoHit) {
            return true;       //都没遇到障碍，可通过
        } else {
            return false;       //遇到障碍，不可通过
        }
    }
}
