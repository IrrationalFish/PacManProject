using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGenerator : MonoBehaviour {

    public int randomRadius;

    public GameObject blinkyPrefab;
    public GameObject ambusherPrefab;
    public GameObject chaserPrefab;
    public GameObject thiefPrefab;
    public GameObject testSphere;

    private GameSceneManager gmScript;
    private List<GameObject> availableGhostList;

    void Start () {
        gmScript=gameObject.GetComponent<GameSceneManager>();
}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<GameObject> GenerateGhosts() {
        List<GameObject> ghostList = new List<GameObject>();
        List<Vector3> generationAreaCenterList = gameObject.GetComponent<ItemGenerator>().generationAreaCenterList;
        availableGhostList=SetAvailableGhostList();
        foreach (Vector3 pos in generationAreaCenterList) {
            if(pos.Equals(new Vector3(5, 0, 5))) {
                continue;
            }
            //ghostList.Add(Instantiate(blinkyPrefab, pos, new Quaternion()));
            if (availableGhostList.Count<=0) {
                return ghostList;
            }
            while (true) {
                int centerX = Mathf.RoundToInt(pos.x);
                int centerZ = Mathf.RoundToInt(pos.z);
                int xPos = Random.Range(centerX-randomRadius, centerX+randomRadius+1);
                int zPos = Random.Range(centerZ-randomRadius, centerZ+randomRadius+1);
                if (!gmScript.MazeCubeIsBlocked(xPos, zPos)) {
                    ghostList.Add(GenerateARandomGhost(xPos, zPos));
                    break;
                }
            }
        }
        return ghostList;
    }

    private GameObject GenerateARandomGhost(int xPos, int zPos) {
        int randomIndex = Random.Range(0, availableGhostList.Count);
        GameObject ghost = Instantiate(availableGhostList[randomIndex], new Vector3(xPos, 0, zPos), new Quaternion());
        if (availableGhostList[randomIndex]==thiefPrefab) {
            availableGhostList.RemoveAt(randomIndex);
        }
        return ghost;
    }

    private List<GameObject> SetAvailableGhostList() {
        List<GameObject> ghostList = new List<GameObject>();
        ghostList.Add(blinkyPrefab);
        ghostList.Add(ambusherPrefab);
        ghostList.Add(chaserPrefab);
        ghostList.Add(thiefPrefab);
        return ghostList;
    }

    public void GenerateStaticBlinky(int x, int z) {
        /*availableGhostList=new List<GameObject>();
        availableGhostList.Add(blinkyPrefab);
        List<GameObject> ghostList = new List<GameObject>();
        ghostList=GenerateGhosts();
        foreach (GameObject ghost in ghostList) {
            ghost.GetComponent<Ghost>().moveSpeed=0f;
        }
        return ghostList;*/
        GameObject staticBlinky = Instantiate(blinkyPrefab, new Vector3(x, 0, z), new Quaternion());
        staticBlinky.GetComponent<Ghost>().moveSpeed=0f;
        gmScript.GetGhostList().Add(staticBlinky);
    }

    public void GenerateBlinky(int x, int z) {
        GameObject staticBlinky = Instantiate(blinkyPrefab, new Vector3(x, 0, z), new Quaternion());
        staticBlinky.GetComponent<Ghost>().moveSpeed=0.05f;
        gmScript.GetGhostList().Add(staticBlinky);
    }
}
