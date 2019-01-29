using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManRespawn : MonoBehaviour {

    public GameObject pacManPrefab;

    public GameObject respawnPacMan(Transform point) {
        return Instantiate(pacManPrefab, point.position, point.rotation);
    }

    private void Start() {}
}
