using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public CinemachineVirtualCamera virtualCamera1;
    public Transform startPoint;
    public int mazeWidth;           //长宽包含了边界的一格,一定是奇数
    public int mazeHeight;

    [SerializeField]private GameObject maze;
    [SerializeField]private GameObject pacMan;

    void Start () {
        pacMan = this.GetComponent<PacManRespawn>().respawnPacMan(startPoint);
        virtualCamera1.Follow = pacMan.transform;
        maze = this.GetComponent<MazeGenPrim>().GenerateMazeParent(mazeWidth, mazeHeight);
        GetComponent<MazeGenPrim>().Generate(mazeWidth, mazeHeight);
	}
	
	void Update () {
		
	}
}
