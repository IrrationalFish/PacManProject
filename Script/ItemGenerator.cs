using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public int generateRadius;
    public int randomRadius;

    public bool wallBreakerUnlocked;
    public bool grenadeUnlocked;
    public bool laserUnlocked;
    public bool pelletUnlocked;
    public bool portalUnlocked;

    public GameObject wallBreakerObjectPrefab;
    public GameObject grenadeObjectPrefab;
    public GameObject laserObjectPrefab;
    public GameObject pelletObjectPrefab;
    public GameObject portalObjectPrefab;
    public GameObject testSphere;

    private GameSceneManager gmScript;

    void Start () {
        gmScript=gameObject.GetComponent<GameSceneManager>();
	}

    public List<GameObject> GenerateItemObejcts() {
        List<GameObject> itemObjectsList = new List<GameObject>();
        for (int i = gmScript.mazeWidth-2-generateRadius; i>=generateRadius+1; i=i-2*generateRadius-1) {
            for (int j = gmScript.mazeHeight-2-generateRadius; j>=generateRadius+1; j=j-2*generateRadius-1) {
                //itemObjectsList.Add(Instantiate(testSphere, new Vector3(i, 0, j), new Quaternion()));
                while (true) {
                    int xPos = Random.Range(i-randomRadius, i+randomRadius+1);
                    int zPos = Random.Range(j-randomRadius, j+randomRadius+1);
                    if (!gmScript.MazeCubeIsBlocked(xPos, zPos)) {
                        itemObjectsList.Add(Instantiate(testSphere,new Vector3(xPos,0,zPos),new Quaternion()));
                        break;
                    }
                }
            }
        }
        return itemObjectsList;
    }

    private List<GameObject> SetAvailableItemsList() {
        List<GameObject> availableItemsList = new List<GameObject>();
        if (wallBreakerUnlocked) {
            availableItemsList.Add(wallBreakerObjectPrefab);
        }
        if (grenadeUnlocked) {
            availableItemsList.Add(grenadeObjectPrefab);
        }
        if (laserUnlocked) {
            availableItemsList.Add(laserObjectPrefab);
        }
        if (pelletUnlocked) {
            availableItemsList.Add(pelletObjectPrefab);
        }
        if (portalUnlocked) {
            availableItemsList.Add(portalObjectPrefab);
        }
        return availableItemsList;
    }
}
