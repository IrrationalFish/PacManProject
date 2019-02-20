using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost {

    public int leftAndRightDistance;
    public int upAndDownDistance;
    public Vector3 startPosition;

    protected override void InitialiseGhost() {
        startPosition=this.transform.position;
    }

    protected override Vector3 GetNextEnd() {
        while (true) {
            int randomX = Random.Range((int)startPosition.x-leftAndRightDistance, (int)startPosition.x+leftAndRightDistance+1);
            int randomZ = Random.Range((int)startPosition.z-upAndDownDistance, (int)startPosition.z+upAndDownDistance+1);
            if (randomX<1||randomX>gmScript.mazeWidth-2||randomZ<1||randomZ>gmScript.mazeHeight-2) {
                continue;
            }
            if (!gmScript.MazeCubeIsBlocked(randomX, randomZ)) {
                Instantiate(pathCube, new Vector3(randomX, 0, randomZ), new Quaternion());
                return new Vector3(randomX, 0, randomZ);
            }
        }
        
    }

    
}
