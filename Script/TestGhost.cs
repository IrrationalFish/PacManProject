using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGhost : Ghost {

    private void Update() {
        if (path.Count<=0) {
            while (true) {
                int randomX = Random.Range(1, gmScript.mazeWidth-1);
                int randomZ = Random.Range(1, gmScript.mazeHeight-1);
                if (!gmScript.MazeCubeIsBlocked(randomX, randomZ)) {
                    path = GetShortestPath(new Vector3(randomX, 0, randomZ));
                    Instantiate(pathCube, new Vector3(randomX, 0, randomZ), new Quaternion());
                    break;
                }
            }
        }
    }


}
