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

    public bool Prim;
    public bool RD;

    void Start () {
        pacMan = this.GetComponent<PacManRespawn>().respawnPacMan(startPoint);
        virtualCamera1.Follow = pacMan.transform;
        if (Prim) {
            maze = this.GetComponent<MazeGenPrim>().GenerateMazeParent(mazeWidth, mazeHeight);
            GetComponent<MazeGenPrim>().Generate(mazeWidth, mazeHeight);
        }else if (RD) {
            maze = this.GetComponent<MazeGenRD>().GenerateMazeParent(mazeWidth, mazeHeight);
            GetComponent<MazeGenRD>().Generate(mazeWidth, mazeHeight);
        }
    }

    void Update () {
    }
}
