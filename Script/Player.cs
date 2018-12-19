using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerMovement moveScript;

	void Start () {
        moveScript = GetComponent<PlayerMovement>();
        moveScript.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(moveScript.enabled == false) {
            if (Input.GetKeyDown("w")) {
                moveScript.setNextMoveDir('w');
                moveScript.enabled = true;
            } else if (Input.GetKeyDown("a")) {
                moveScript.setNextMoveDir('a');
                moveScript.enabled = true;
            } else if (Input.GetKeyDown("s")) {
                moveScript.setNextMoveDir('s');
                moveScript.enabled = true;
            } else if (Input.GetKeyDown("d")) {
                moveScript.setNextMoveDir('d');
                moveScript.enabled = true;
            }
        }
	}
}
