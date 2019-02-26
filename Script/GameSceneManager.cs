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
    public int maxItemsNumber;
    public int totalPacDots = 0;

    public GameObject pacDotsParent;
    public GameObject pacDotPrefab;
    public GameObject pacManPrefab;

    public GameObject itemObjectButtonPrefab;
    public GameObject canvas;
    public Slider boostEnergySlider;
    public Text energyText;
    public Button buildMazeBtn;
    public Button getGrenadeBtn;
    public Button getWallBreakerBtn;
    public Button getLaserBtn;

    [SerializeField] private List<GameObject> itemObjectButtonList;
    [SerializeField] private GameObject maze;
    [SerializeField] private GameObject pacMan;
    private Maze mazeScript;
    private MazeGenerator mazeGenerator;

    public bool RD;
    public bool Prim;
    public bool RB;

    void Start() {
        Physics.IgnoreLayerCollision(8, 10);
        pacMan=RespawnPacMan(startPoint);
        virtualCamera1.Follow=pacMan.transform;
        InitialiseItemObjectButtons();
        BuildMaze();
        GeneratePacDot();
        buildMazeBtn.onClick.AddListener(delegate () { this.BuildMaze(); });
        getGrenadeBtn.onClick.AddListener(delegate () { pacMan.GetComponent<Player>().GetItem("Grenade"); });
        getWallBreakerBtn.onClick.AddListener(delegate () { pacMan.GetComponent<Player>().GetItem("WallBreaker"); });
        getLaserBtn.onClick.AddListener(delegate () { pacMan.GetComponent<Player>().GetItem("Laser"); });
    }

    void Update() {

    }

    public void PlayerGetItem(int index, string itemName) {
        itemObjectButtonList[index].GetComponent<ItemObjectButton>().SetItemObjectType(itemName);
    }

    public void PlayerUseItem(int index) {
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
            Debug.Log("Maze is built by RD");
        } else if (Prim) {
            mazeGenerator=GetComponent<MazeGenPrim>();
            maze=mazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            Debug.Log("Maze is built by Prim");
        } else if (RB) {
            mazeGenerator=GetComponent<MazeGenRB>();
            maze=mazeGenerator.GenerateMaze(mazeWidth, mazeHeight);
            Debug.Log("Maze is built by RB");
        } else {
            //do nothing
        }
        mazeScript=maze.GetComponent<Maze>();
    }

    private void GeneratePacDot() {
        for(int i = 1; i<mazeWidth-1; i++) {
            for(int j = 1; j<mazeHeight-1; j++) {
                if (!MazeCubeIsBlocked(i, j)) {
                    GameObject pacDot = Instantiate(pacDotPrefab, new Vector3(i, 0, j), Quaternion.Euler(45, 0, 45));
                    pacDot.transform.parent=pacDotsParent.transform;
                    totalPacDots++;
                }
            }
        }
    }

    private GameObject RespawnPacMan(Transform point) {
        GameObject pacman = Instantiate(pacManPrefab, point.position, point.rotation);
        pacman.GetComponent<Player>().maxItemsNumber=this.maxItemsNumber;
        return pacman;
    }

    public GameObject GetPlayer() {
        return pacMan;
    }

    public bool MazeCubeIsBlocked(int x, int z) {
        /*if (x<0||y<0||x>mazeWidth-1||y>mazeHeight-1) {
            return true;
        }*/
        if(mazeScript.mazeObjects[x,z] == null) {
            return false;
        } else {
            return true;
        }
    }
}
