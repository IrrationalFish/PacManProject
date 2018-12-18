using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    // Use this for initialization
    void Start() {
        GetComponent<MazeGenPrim>().Generate();
    }

    // Update is called once per frame
    void Update() {

    }
}
