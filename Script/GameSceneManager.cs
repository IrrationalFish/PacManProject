using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public CinemachineVirtualCamera virtualCamera1;
    public Transform startPoint;

    [SerializeField]private GameObject mazeParent;
    [SerializeField]private GameObject pacMan;

    void Start () {
        pacMan = this.GetComponent<PacManRespawn>().respawnPacMan(startPoint);
        virtualCamera1.Follow = pacMan.transform;
        mazeParent = this.GetComponent<MazeGenPrim>().generateMazeParent();
        GetComponent<MazeGenPrim>().Generate();
	}
	
	void Update () {
		
	}
}
