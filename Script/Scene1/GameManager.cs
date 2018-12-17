using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject dotPrefab;
    public GameObject parentOfDots;
    public GameObject ghostSpawnArea;

	void Start () {
        Bounds bound = ghostSpawnArea.GetComponent<BoxCollider2D>().bounds;
        for (int i = 2; i <= 27; i++) {
            for (int j = 2; j <= 30; j++) {
                Debug.Log(bound.extents + "" + bound.center);
                if(bound.Contains(new Vector3(i, j, 0))) {
                    continue;
                }
                GameObject dot = Instantiate(dotPrefab, parentOfDots.transform);
                dot.transform.SetPositionAndRotation(new Vector3(i, j, 0), dot.transform.rotation);
            }
        }
    }
	
	void Update () {
		
	}
}
