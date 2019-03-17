using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambusher : Ghost {

    public int awakeRadius;
    public int predictRadius;

    private GameObject player;

    protected override void InitialiseGhost() {
        player=gmScript.GetPlayer();
    }

    protected override Vector3 GetNextEnd() {
        List<Vector3> potentialPoints = new List<Vector3>();
        int maxPotentialValue = 0;
        int currentPotentialValue = 0;
        int playerXPos = Mathf.RoundToInt(player.transform.position.x);
        int playerZPos = Mathf.RoundToInt(player.transform.position.z);
        int xDistance = Mathf.Abs(playerXPos-Mathf.RoundToInt(transform.position.x));
        int zDistance = Mathf.Abs(playerZPos-Mathf.RoundToInt(transform.position.z));
        //Debug.Log("XDIS: "+xDistance+", zDis: "+zDistance);
        if (xDistance>awakeRadius ||zDistance>awakeRadius) {        //不在awake范围内
            return new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
        } else {
            for(int i= playerXPos-predictRadius; i<=playerXPos +predictRadius; i++) {
                for(int j = playerZPos-predictRadius; j<=playerZPos+predictRadius; j++) {
                    bool isVerticalLine = (i==playerXPos-predictRadius)||(i==playerXPos+predictRadius);
                    bool isHorizontalLine = (j==playerZPos-predictRadius)||(j==playerZPos+predictRadius);
                    bool isBoundary = isVerticalLine||isHorizontalLine;
                    if (isBoundary && gmScript.PointIsInsideMaze(i,j)&& !gmScript.MazeCubeIsBlocked(i, j)) {
                        Stack<Vector3> playerPath = GetShortestPath(new Vector3(playerXPos, 0, playerZPos), new Vector3(i, 0, j));
                        foreach( Vector3 point in playerPath) {
                            if (gmScript.HasPacDotInPoint(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.z))) {
                                currentPotentialValue++;
                            }
                        }
                        if (currentPotentialValue>maxPotentialValue) {
                            potentialPoints=new List<Vector3>();
                            maxPotentialValue=currentPotentialValue;
                            potentialPoints.Add(new Vector3(i, 0, j));
                        }else if (currentPotentialValue==maxPotentialValue) {
                            potentialPoints.Add(new Vector3(i, 0, j));
                        }
                        currentPotentialValue=0;
                    }
                }
            }
            int randomNumber = Random.Range(0, potentialPoints.Count);
            Vector3 nextEnd = potentialPoints[randomNumber];
            //Instantiate(pathCube, nextEnd, new Quaternion());
            return nextEnd;
        }
        
    }

}
