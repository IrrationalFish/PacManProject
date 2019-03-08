using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

    private GameSceneManager gmScript;

	void Start () {
        if (gmScript==null) {
            gmScript=GameObject.Find("GameManager").GetComponent<GameSceneManager>();

        }
    }
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag =="Player") {
            Debug.Log("Player Arrive End Point");
            gmScript.PacManArriveEndPoint();
        }
    }
}
