﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerMovement moveScript;

	void Start () {
        moveScript = GetComponent<PlayerMovement>();
	}
	
	void Update () {

	}
}
