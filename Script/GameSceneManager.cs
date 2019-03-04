using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public CinemachineVirtualCamera virtualCamera1;
    public int mazeWidth;           //长宽包含了边界的2格,一定是奇数
    public int mazeHeight;
    public int maxItemsNumber;
    public int maxPacManLives = 3;
    public int currentPacManLives;
    public int totalPacDotsInCurrentStage = 0;

    public GameObject pacDotsParentPrefab;
    public GameObject pacDotPrefab;
    public GameObject pacManPrefab;

    public GameObject itemObjectButtonPrefab;
    public GameObject canvas;
    public Slider boostEnergySlider;
    public Text energyText;
    public Image livesIconThree;
    public Image livesIconTwo;
    public Image livesIconOne;

    public Button buildMazeBtn;
    public Button getGrenadeBtn;
    public Button getWallBreakerBtn;
    public Button getLaserBtn;

    [SerializeField] private List<GameObject> itemObjectButtonList;
    [SerializeField] private GameObject maze;
    [SerializeField] private GameObject pacMan;
    private GameObject pacDotsParent;
    private GameObject[,] pacDotsArray;
    private Maze mazeScript;
    private MazeGenerator mazeGenerator;

    public bool RD;
    public bool Prim;
    public bool RB;

    void Start() {
        Physics.IgnoreLayerCollision(8, 10);
        pacMan=RespawnPacMan(new Vector3(1,0,1));
        currentPacManLives=maxPacManLives;
        virtualCamera1.Follow=pacMan.transform;
        InitialiseUI();
        StartNextStage();
        buildMazeBtn.onClick.AddListener(delegate () { this.StartNextStage(); });
        getGrenadeBtn.onClick.AddListener(delegate () { pacMan.GetComponent<Player>().GetItem("Grenade"); });
        getWallBreakerBtn.onClick.AddListener(delegate () { pacMan.GetComponent<Player>().GetItem("WallBreaker"); });
        getLaserBtn.onClick.AddListener(delegate () { pacMan.GetComponent<Player>().GetItem("Laser"); });
    }

    void Update() {

    }

    private void StartNextStage() {
        BuildMaze();
        GeneratePacDot();
    }

    private void InitialiseUI() {
        for (int i = 0; i<maxItemsNumber; i++) {
            GameObject button = Instantiate(itemObjectButtonPrefab, canvas.transform);
            button.transform.SetPositionAndRotation(new Vector3(100+150*i,50,0), new Quaternion());
            itemObjectButtonList.Add(button);
        }
        if (currentPacManLives==2) {
            livesIconThree.enabled=false;
        } else if (currentPacManLives==1) {
            livesIconTwo.enabled=false;
            livesIconThree.enabled=false;
        } else if (currentPacManLives==0) {
            livesIconOne.enabled=false;
            livesIconTwo.enabled=false;
            livesIconThree.enabled=false;
            GameOver();
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
        if (pacDotsParent!=null) {
            Destroy(pacDotsParent);
        }
        totalPacDotsInCurrentStage=0;
        pacDotsParent=Instantiate(pacDotsParentPrefab, new Vector3(0, 0, 0), new Quaternion());
        pacDotsArray=new GameObject[mazeWidth, mazeHeight];
        for(int i = 1; i<mazeWidth-1; i++) {
            for(int j = 1; j<mazeHeight-1; j++) {
                if (!MazeCubeIsBlocked(i, j)) {
                    GameObject pacDot = Instantiate(pacDotPrefab, new Vector3(i, 0, j), Quaternion.Euler(45, 0, 45));
                    pacDot.transform.parent=pacDotsParent.transform;
                    pacDot.GetComponent<PacDot>().SetXAndZPos(i, j);
                    pacDotsArray[i, j]=pacDot;
                    totalPacDotsInCurrentStage++;
                }
            }
        }
    }

    private GameObject RespawnPacMan(Vector3 point) {
        GameObject pacman = Instantiate(pacManPrefab, point, new Quaternion());
        pacman.GetComponent<Player>().maxItemsNumber=this.maxItemsNumber;
        return pacman;
    }

    public GameObject GetPlayer() {
        return pacMan;
    }

    public bool MazeCubeIsBlocked(int x, int z) {
        if(mazeScript.mazeObjects[x,z] == null) {
            return false;
        } else {
            return true;
        }
    }

    public void PacDotIsEaten(int x, int z) {
        Destroy(pacDotsArray[x, z]);
        pacDotsArray[x, z]=null;
    }

    public bool PointIsInsideMaze(int x, int z) {
        if(x>0 && x<mazeWidth-1 && z>0 &&z<mazeHeight-1) {
            return true;
        } else {
            return false;
        }
    }

    public bool HasPacDotInPoint(int x, int z) {
        if(pacDotsArray[x,z] !=null) {
            return true;
        } else {
            return false;
        }
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

    public void GhostHitPlayer() {
        StartCoroutine(PacManHittedByGhost(3f));
    }

    private IEnumerator PacManHittedByGhost(float second) {
        GameObject particle = pacMan.GetComponent<Player>().PlayDeathParticleSystem();
        pacMan.gameObject.SetActive(false);
        if (currentPacManLives ==3) {
            currentPacManLives--;
            livesIconThree.enabled=false;
        }else if(currentPacManLives ==2) {
            currentPacManLives--;
            livesIconTwo.enabled=false;
        } else if (currentPacManLives==1) {
            livesIconOne.enabled=false;
            GameOver();
            yield break;
        }
        virtualCamera1.GetComponent<Animator>().SetTrigger("PacManHitted");
        yield return new WaitForSeconds(second);
        pacMan.gameObject.SetActive(true);
        pacMan.transform.SetPositionAndRotation(new Vector3(1, 0, 1), Quaternion.Euler(0, 0, 0));
        pacMan.GetComponent<PlayerMovement>().startMovement=false;
        Destroy(particle);
    }

    private void GameOver() {
        Debug.Log("GameOver!");
    }
}
