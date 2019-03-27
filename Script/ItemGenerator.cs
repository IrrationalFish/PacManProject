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

    public List<Vector3> generationAreaCenterList;

    private GameSceneManager gmScript;
    private List<GameObject> availableItemList;
    public int pelletCount = 0;

    void Start () {
        gmScript=gameObject.GetComponent<GameSceneManager>();
	}

    public List<GameObject> GenerateItemObejcts() {
        pelletCount=0;
        List<GameObject> itemObjectsList = new List<GameObject>();
        availableItemList= SetAvailableItemsList();
        SetGenerationCenterList();
        foreach(Vector3 pos in generationAreaCenterList) {
            //itemObjectsList.Add(Instantiate(testSphere, pos, new Quaternion()));
            if (availableItemList.Count<=0) {
                return itemObjectsList;
            }
            while (true) {
                int centerX = Mathf.RoundToInt(pos.x);
                int centerZ = Mathf.RoundToInt(pos.z);
                int xPos = Random.Range(centerX-randomRadius, centerX+randomRadius+1);
                int zPos = Random.Range(centerZ-randomRadius, centerZ+randomRadius+1);
                if (!gmScript.MazeCubeIsBlocked(xPos, zPos)) {
                    itemObjectsList.Add(GenerateARandomItem(xPos,zPos));
                    break;
                }
            }
        }
        return itemObjectsList;
    }

    private void SetGenerationCenterList() {
        generationAreaCenterList=new List<Vector3>();
        for (int i = gmScript.mazeWidth-2-generateRadius; i>=generateRadius+1; i=i-2*generateRadius-1) {
            for (int j = gmScript.mazeHeight-2-generateRadius; j>=generateRadius+1; j=j-2*generateRadius-1) {
                generationAreaCenterList.Add(new Vector3(i, 0, j));
            }
        }
    }

    private GameObject GenerateARandomItem(int xPos, int zPos) {
        int randomIndex = Random.Range(0, availableItemList.Count);
        GameObject item = Instantiate(availableItemList[randomIndex], new Vector3(xPos, 0, zPos), new Quaternion());
        if(availableItemList[randomIndex] ==portalObjectPrefab) {
            availableItemList.RemoveAt(randomIndex);
        } else if(availableItemList[randomIndex]==pelletObjectPrefab) {
            pelletCount++;
            if(pelletCount >generationAreaCenterList.Count/4) {
                availableItemList.RemoveAt(randomIndex);
                Debug.Log("Remove pellet");
            }
        } else {
            //do nothing
        }
        return item;
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

    public void GenerateGrenadeObject(int xPos, int zPos) {
        GameObject grenade = Instantiate(grenadeObjectPrefab, new Vector3(xPos, 0, zPos), new Quaternion());
        gmScript.GetItemObjectsList().Add(grenade);
    }

    public void GenerateWallBreakerObject(int xPos, int zPos) {
        GameObject wallBreaker = Instantiate(wallBreakerObjectPrefab, new Vector3(xPos, 0, zPos), new Quaternion());
        gmScript.GetItemObjectsList().Add(wallBreaker);
    }
}
