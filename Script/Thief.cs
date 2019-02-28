using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Thief : Ghost {

    public Text stolenPacDotsText;
    public int stolenPacDots;
    public GameObject textCanvas;
    public Vector3 currentEnd;

    private new void Update() {
        textCanvas.transform.rotation=Quaternion.Euler(60, 0, 0);
        /*if (path.Count<=0) {
            Vector3 nextEnd = GetNextEnd();
            path=GetShortestPath(this.transform.position, nextEnd);
        }*/
        Vector3 playerFarrestCorner = GetNextEnd();
        if (playerFarrestCorner.Equals(currentEnd)) {
            if (path.Count<=0) {
                Vector3 nextEnd = GetNextEnd();
                path=GetShortestPath(this.transform.position, nextEnd);
            }
        } else {
            if (path.Count>0) {
                Vector3 nextCube= path.Pop();
                Vector3 nextEnd = GetNextEnd();
                currentEnd=nextEnd;
                path=GetShortestPath(nextCube, nextEnd);
                path.Push(nextCube);
            } else {
                Vector3 nextEnd = GetNextEnd();
                currentEnd=nextEnd;
                path=GetShortestPath(this.transform.position, nextEnd);
            }
        }
    }

    protected override Vector3 GetNextEnd() {
        float pacDistanceToLeftDownSqr = Vector3.SqrMagnitude(gmScript.GetPlayer().transform.position- new Vector3(1,0,1));
        float pacDistanceToLeftUpSqr = Vector3.SqrMagnitude(gmScript.GetPlayer().transform.position-new Vector3(1, 0, gmScript.mazeHeight-2));
        float pacDistanceToRightDownSqr = Vector3.SqrMagnitude(gmScript.GetPlayer().transform.position-new Vector3(gmScript.mazeWidth-2, 0, 1));
        float pacDistanceToRightUpSqr = Vector3.SqrMagnitude(gmScript.GetPlayer().transform.position-new Vector3(gmScript.mazeWidth-2, 0, gmScript.mazeHeight-2));
        List<float> list = new List<float> {
            pacDistanceToLeftDownSqr,
            pacDistanceToLeftUpSqr,
            pacDistanceToRightDownSqr,
            pacDistanceToRightUpSqr
        };
        list.Sort();
        Vector3 end;
        float maxDistance = list[3];
        if(maxDistance.Equals(pacDistanceToLeftDownSqr)) {
            end=new Vector3(1, 0, 1);
        }else if(maxDistance.Equals(pacDistanceToLeftUpSqr)) {
            end=new Vector3(1, 0, gmScript.mazeHeight-2);
        }else if (maxDistance.Equals(pacDistanceToRightUpSqr)) {
            end=new Vector3(gmScript.mazeWidth-2, 0, gmScript.mazeHeight-2);
        } else {
            end=new Vector3(gmScript.mazeWidth-2, 0, 1);
        }

        return end;
    }

    protected override void InitialiseGhost() {
        currentEnd=GetNextEnd();
        stolenPacDots=0;
        stolenPacDotsText.text=stolenPacDots.ToString();
    }

    private new void OnTriggerEnter(Collider other) {
        if (other.tag=="Player") {
            other.GetComponent<Player>().AddEnergy(stolenPacDots);
            Destroy(gameObject);
        } else if(other.tag =="PacDot") {
            PacDot pacDotScript = other.GetComponent<PacDot>();
            gmScript.PacDotIsEaten(pacDotScript.xPos, pacDotScript.zPos);
            stolenPacDots++;
            stolenPacDotsText.text=stolenPacDots.ToString();
        }
    }

}
