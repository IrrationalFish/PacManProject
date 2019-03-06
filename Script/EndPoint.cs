using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag =="Player") {
            Debug.Log("Player Arrive End Point");
        }
    }
}
