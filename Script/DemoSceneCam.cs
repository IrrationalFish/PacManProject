using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSceneCam : MonoBehaviour {

    public float moveSpeed;

    private Camera freeCamera;

    void Start() {
        freeCamera=gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(new Vector3(0, moveSpeed, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(new Vector3(0, -moveSpeed, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(new Vector3(-moveSpeed, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(new Vector3(moveSpeed, 0, 0));
        }
        if(Input.GetAxis("Mouse ScrollWheel")<0) {
            freeCamera.orthographicSize++;
        }
        if (Input.GetAxis("Mouse ScrollWheel")>0) {
            freeCamera.orthographicSize--;
        }
    }
}
