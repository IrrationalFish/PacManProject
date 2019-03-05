using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserChildInstance : MonoBehaviour {

    public float moveSpeed;
    public float sleepTime = 8f;

    void FixedUpdate() {
        transform.Translate(Vector3.forward*moveSpeed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Ghost") {
            Debug.Log("Laser Hit Ghost");
            other.gameObject.GetComponent<Ghost>().Sleep(sleepTime);
        } else if(other.tag =="Wall") {
            Destroy(gameObject);
        }
    }
}
