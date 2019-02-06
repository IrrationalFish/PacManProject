using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 0.1f;

    public char nextMoveDir;
    public bool startMovement;

    void Start() {
        startMovement = false;
    }

    private void Update() {
        if (Input.GetKeyDown("w")) {
            nextMoveDir = 'w';
            startMovement = true;
        } else if (Input.GetKeyDown("a")) {
            nextMoveDir = 'a';
            startMovement = true;
        } else if (Input.GetKeyDown("s")) {
            nextMoveDir = 's';
            startMovement = true;
        } else if (Input.GetKeyDown("d")) {
            nextMoveDir = 'd';
            startMovement = true;
        }

        if (nextMoveDir == 'a' && Valid('a')) {
            transform.localEulerAngles = new Vector3(0, -90, 0);
        } else if (nextMoveDir == 's' && Valid('s')) {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        } else if (nextMoveDir == 'd' && Valid('d')) {
            transform.localEulerAngles = new Vector3(0, 90, 0);
        } else if (nextMoveDir == 'w' && Valid('w')) {
            transform.localEulerAngles = new Vector3(0, 0, 0);
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
        if (startMovement == false) {
            return;
        }
        transform.Translate(Vector3.forward * moveSpeed);
    }

    bool Valid(char dir) {
        RaycastHit hit;
        Vector3 dirVector = new Vector3(0, 0, 0);
        if (dir == 'a') {
            dirVector = new Vector3(-1, 0, 0);
        } else if (dir == 's') {
            dirVector = new Vector3(0, 0, -1);
        } else if (dir == 'd') {
            dirVector = new Vector3(1, 0, 0);
        } else if (dir == 'w') {
            dirVector = new Vector3(0, 0, 1);
        }
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + dirVector, Color.red);
        if (Physics.Linecast(gameObject.transform.position, gameObject.transform.position + dirVector, out hit, 9)) {
            return false;       //在想要走向的方向上有障碍，无效
        } else {
            return true;        //无障碍，有效
        }
    }

}
