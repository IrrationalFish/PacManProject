using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 0.1f;

    [SerializeField] private char nextMoveDir;

    void Start() {
        //enabled = false;
    }

    private void Update() {
        if (Input.GetKeyDown("w")) {
            nextMoveDir = 'w';
        }else if (Input.GetKeyDown("a")) {
            nextMoveDir = 'a';
        } else if (Input.GetKeyDown("s")) {
            nextMoveDir = 's';
        } else if (Input.GetKeyDown("d")) {
            nextMoveDir = 'd';
        }

        if(nextMoveDir == 'a') {
            transform.localEulerAngles = new Vector3(0, -90, 0);
        }else if(nextMoveDir == 's') {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        } else if (nextMoveDir == 'd') {
            transform.localEulerAngles = new Vector3(0, 90, 0);
        } else if (nextMoveDir == 'w') {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        RaycastHit hit1;
        bool hitornot = Physics.Linecast(gameObject.transform.position + new Vector3(0.5f, 0, 0), gameObject.transform.position + new Vector3(0.5f, 0, 0.5f));
        Debug.DrawLine(gameObject.transform.position + new Vector3(0.5f, 0, 0), gameObject.transform.position + new Vector3(0.5f, 0, 0.5f));
        Debug.Log(hitornot);
    }

    void FixedUpdate() {
        transform.Translate(Vector3.forward * moveSpeed);
    }
}
