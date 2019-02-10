using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserChildInstance : MonoBehaviour {

    public float moveSpeed;

	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(Vector3.forward*moveSpeed);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Player"){
            return;
        } else if(other.tag =="Ghost") {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
