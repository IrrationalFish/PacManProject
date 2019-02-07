using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour {

    public string itmeName;
    public float rotateSpeed = 1;

    private void FixedUpdate() {
        this.transform.Rotate(new Vector3(0,2,0)*rotateSpeed);
    }
    //protected abstract void TriggeredByPlayer(GameObject player);

    /*private void OnTriggerEnter(Collider other) {
        if (other.tag=="Player") {
            TriggeredByPlayer(other.gameObject);
            Destroy(gameObject);
        }
    }*/

}
