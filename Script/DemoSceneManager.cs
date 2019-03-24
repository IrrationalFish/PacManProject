using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoSceneManager : MonoBehaviour {

    public Text demoText;
    public Camera mainCamera;
    public Camera mazeCamera;

    public GameObject demoBlinky;
    public GameObject demoChaser;
    public GameObject demoThief;
    public GameObject demoAmbusher;

    private GameSceneManager gmScript;
    private ItemGenerator itemGenScript;
    private GhostGenerator ghostGenScript;
    private bool isThiefDemo = false;

    void Start() {
        mazeCamera.gameObject.SetActive(false);
        demoText.text="Demo scene";
        gmScript =gameObject.GetComponent<GameSceneManager>();
        itemGenScript=gameObject.GetComponent<ItemGenerator>();
        ghostGenScript=gameObject.GetComponent<GhostGenerator>();
    }

    private void Update() {
        if (isThiefDemo) {
            demoText.text=gmScript.pacDotsEatenByPlayer.ToString();
        }
    }

    public void BlinkyBtn() {
        gmScript.ClearLastStage();
        demoText.text="Blinky Test";
        gmScript.level=4;
        StartCoroutine(GenerateNoShostStage(demoBlinky, 7,5));
    }
    public void ChaserBtn() {
        gmScript.ClearLastStage();
        demoText.text="Chaser Test";
        gmScript.level=4;
        StartCoroutine(GenerateNoShostStage(demoChaser, 7, 5));
    }
    public void ThiefBtn() {
        isThiefDemo=true;
        gmScript.ClearLastStage();
        demoText.text="";
        gmScript.level=4;
        StartCoroutine(GenerateNoShostStage(demoThief, 7, 5));
    }
    public void AmbusherBtn() {
        gmScript.ClearLastStage();
        demoText.text="Ambusher Test";
        gmScript.level=4;
        StartCoroutine(GenerateNoShostStage(demoAmbusher, 7, 5));
    }

    public void WallBreakerBtn() {
        gmScript.ClearLastStage();
        demoText.text="Wall-Breaker Test";
        gmScript.level=8;
        StartCoroutine(GenerateBreakerStage());

    }
    public void GrenadeBtn() {
        gmScript.ClearLastStage();
        demoText.text="Grenade Test";
        gmScript.level=8;
        StartCoroutine(GenerateGrenadeStage());

    }
    public void LaserBtn() {
        gmScript.ClearLastStage();
        demoText.text="Laser Test";
        gmScript.level=8;
        StartCoroutine(GenerateLaserStage());

    }
    public void PelletBtn() {
        gmScript.ClearLastStage();
        demoText.text="Pellet Test";
        gmScript.level=8;
        StartCoroutine(GeneratePelletStage());
    }
    public void PortalBtn() {
        gmScript.ClearLastStage();
        demoText.text="Portal Test";
        gmScript.level=8;
        StartCoroutine(GeneratePortalStage());
    }

    public void ResetLevel() {
        Debug.Log("Reset maze building");
        gmScript.level=0;
        isThiefDemo=false;
        gmScript.GetExtraLife();
    }

    public void BuildMazeDemo() {
        string mazeType = "";
        gmScript.ClearLastStage();
        gmScript.level++;
        //StartCoroutine(gmScript.AfterStageClearMenuReturn(0f));
        gmScript.BuildMaze();
        if(gmScript.RD) { mazeType="Recursive Division"; }
        if (gmScript.Prim) { mazeType="Randomized Prim"; }
        if (gmScript.RB) { mazeType="Recursive Backracker"; }
        demoText.text="Level: "+gmScript.level+" "+mazeType;
    }

    public void SwitchCamera() {
        if (mainCamera.gameObject.activeSelf) {
            mainCamera.gameObject.SetActive(false);
            mazeCamera.gameObject.SetActive(true);
        } else {
            mainCamera.gameObject.SetActive(true);
            mazeCamera.gameObject.SetActive(false);
        }
    }

    private IEnumerator GenerateBreakerStage() {
        itemGenScript.wallBreakerUnlocked=true;
        yield return StartCoroutine(gmScript.AfterStageClearMenuReturn(0f));
        gmScript.ClearAllGhost();
        itemGenScript.wallBreakerUnlocked=false;
        ghostGenScript.GenerateStaticBlinky(11,7);
        ghostGenScript.GenerateStaticBlinky(11, 11);
    }
    private IEnumerator GenerateGrenadeStage() {
        itemGenScript.grenadeUnlocked=true;
        yield return StartCoroutine(gmScript.AfterStageClearMenuReturn(0f));
        gmScript.ClearAllGhost();
        itemGenScript.grenadeUnlocked=false;
        ghostGenScript.GenerateStaticBlinky(11, 7);
        ghostGenScript.GenerateStaticBlinky(11, 11);
    }
    private IEnumerator GenerateLaserStage() {
        itemGenScript.laserUnlocked=true;
        yield return StartCoroutine(gmScript.AfterStageClearMenuReturn(0f));
        gmScript.ClearAllGhost();
        itemGenScript.laserUnlocked=false;
        ghostGenScript.GenerateBlinky(11, 7);
        ghostGenScript.GenerateBlinky(11, 11);
    }
    private IEnumerator GeneratePelletStage() {
        itemGenScript.pelletUnlocked=true;
        yield return StartCoroutine(gmScript.AfterStageClearMenuReturn(0f));
        gmScript.ClearAllGhost();
        itemGenScript.pelletUnlocked=false;
    }
    private IEnumerator GeneratePortalStage() {
        itemGenScript.portalUnlocked=true;
        yield return StartCoroutine(gmScript.AfterStageClearMenuReturn(0f));
        gmScript.ClearAllGhost();
        itemGenScript.portalUnlocked=false;
    }

    private IEnumerator GenerateNoShostStage(GameObject demoGhost, int xPos, int zPos) {
        yield return StartCoroutine(gmScript.AfterStageClearMenuReturn(0f));
        gmScript.ClearAllGhost();
        gmScript.GetGhostList().Add(Instantiate(demoGhost, new Vector3(xPos, 0, zPos), new Quaternion()));
    }

}
