using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour {

    public float moveSpeed;
    public List<Vector3> targetsList;
    public float delta = 0.001f;

    protected void Start () {

    }

    protected void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("Move Up!");
            targetsList.Add(transform.position+new Vector3(0, 0, 1));
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Debug.Log("Move Down!");
            targetsList.Add(transform.position+new Vector3(0, 0, -1));
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Debug.Log("Move Left!");
            targetsList.Add(transform.position+new Vector3(-1, 0, 0));
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Debug.Log("Move Right!");
            targetsList.Add(transform.position+new Vector3(1, 0, 0));
        }
    }

    protected void FixedUpdate () {
        if(targetsList.Count>0) {
            MoveToPosition(targetsList[0]);
        }
    }

    protected void MoveToPosition(Vector3 position) {
        transform.LookAt(position);
        bool xPositionReady = Mathf.Abs(this.transform.position.x-position.x)<delta;
        bool zPositionReady = Mathf.Abs(this.transform.position.z-position.z)<delta;
        if (xPositionReady && zPositionReady) {          //到到指定位置
            targetsList.RemoveAt(0);
            this.transform.position=position;
            //transform.position=new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
            Debug.Log("Arrive!");
        } else {
            transform.Translate(Vector3.forward*moveSpeed);
            Debug.Log("Not Arrive!");
        }
        
    }
}
