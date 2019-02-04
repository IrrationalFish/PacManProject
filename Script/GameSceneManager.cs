using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public CinemachineVirtualCamera virtualCamera1;
    public Transform startPoint;
    public int mazeWidth;           //长宽包含了边界的2格,一定是奇数
    public int mazeHeight;
    public Button btn;

    [SerializeField]private GameObject maze;
    [SerializeField]private GameObject pacMan;
    private MazeGenerator mazeGenerator;

    public bool Prim;
    public bool RD;
    public bool RB;

    void Start () {
        pacMan = this.GetComponent<PacManRespawn>().respawnPacMan(startPoint);
        virtualCamera1.Follow = pacMan.transform;
        if (Prim) {
            mazeGenerator = GetComponent<MazeGenPrim>();
            maze = mazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[0, 0] != null);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[1, 2] != null);
        } else if (RD) {
            mazeGenerator = GetComponent<MazeGenRD>();
            maze = mazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[0, 0] != null);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[1, 2] != null);
        }else if (RB) {
            mazeGenerator = GetComponent<MazeGenRB>();
            maze = mazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[0, 0] != null);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[1, 2] != null);
        }
        btn.onClick.AddListener(delegate () {mazeGenerator.RemoveDeadEnds(mazeWidth,mazeHeight);});
    }

    void Update () {
    }
}
