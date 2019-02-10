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
    public GameObject pacManPrefab;
    public int maxItemsNumber;
    public GameObject itemObjectButtonPrefab;
    public GameObject canvas;
    public Button buildMazeBtn;

    [SerializeField] private List<GameObject> itemObjectButtonList;
    [SerializeField] private GameObject maze;
    [SerializeField] private GameObject pacMan;
    private MazeGenerator mazeGenerator;

    public bool RD;
    public bool Prim;
    public bool RB;

    void Start() {
        pacMan=RespawnPacMan(startPoint);
        virtualCamera1.Follow=pacMan.transform;
        InitialiseItemObjectButtons();
        BuildMaze();
        buildMazeBtn.onClick.AddListener(delegate () { this.BuildMaze(); });
    }

    void Update() {
    }

    public void PlayerGetItem(int index, string itemName) {
        itemObjectButtonList[index].GetComponent<ItemObjectButton>().SetItemObjectType(itemName);
    }

    public void PlayerUsrItem(int index) {
        itemObjectButtonList[index].GetComponent<ItemObjectButton>().ItemISUsed();
    }

    public void GmBreakWall(int x, int y) {
        if (x==0||y==0||x==maze.GetComponent<Maze>().width-1||y==maze.GetComponent<Maze>().height-1) {
            Debug.Log("Boundary GMBreak Fail!");
            return;
        }
        GameObject[,] mazeArray = maze.GetComponent<Maze>().mazeObjects;
        Destroy(mazeArray[x, y]);
        mazeArray[x, y]=null;
    }

    private void InitialiseItemObjectButtons() {
        for (int i = 0; i<maxItemsNumber; i++) {
            GameObject button = Instantiate(itemObjectButtonPrefab, canvas.transform);
            button.transform.SetPositionAndRotation(new Vector3(100+150*i,50,0), new Quaternion());
            itemObjectButtonList.Add(button);
        }
    }

    private void BuildMaze() {
        if (maze!=null) {
            Destroy(maze);
        }
        if (RD) {
            mazeGenerator=GetComponent<MazeGenRD>();
            maze=mazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[0, 0]!=null);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[1, 2]!=null);
            Debug.Log("Maze is built by RD");
        } else if (Prim) {
            mazeGenerator=GetComponent<MazeGenPrim>();
            maze=mazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[0, 0]!=null);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[1, 2]!=null);
            Debug.Log("Maze is built by Prim");
        } else if (RB) {
            mazeGenerator=GetComponent<MazeGenRB>();
            maze=mazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[0, 0]!=null);
            Debug.Log(maze.GetComponent<Maze>().mazeObjects[1, 2]!=null);
            Debug.Log("Maze is built by RB");
        } else {
            //do nothing
        }
    }

    private GameObject RespawnPacMan(Transform point) {
        GameObject pacman = Instantiate(pacManPrefab, point.position, point.rotation);
        pacman.GetComponent<Player>().maxItemsNumber=this.maxItemsNumber;
        return pacman;
    }
}
