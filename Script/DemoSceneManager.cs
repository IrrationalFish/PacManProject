using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoSceneManager : MonoBehaviour {

    public Text demoText;
    public Camera mainCamera;
    public Camera mazeCamera;

    public Button resetBtn;
    public Button buildMazeBtn;
    public Button cameraBtn;
    public Button rbBtn;

    private GameSceneManager gmScript;

    void Start() {
        mazeCamera.gameObject.SetActive(false);
        demoText.text="Demo scene";
        gmScript =gameObject.GetComponent<GameSceneManager>();
        resetBtn.onClick.AddListener(delegate () { ResetLevel(); });
        buildMazeBtn.onClick.AddListener(delegate () { BuildMazeDemo(); });
        cameraBtn.onClick.AddListener(delegate () { SwitchCamera(); });
        //resetBtn.onClick.AddListener(delegate () { ClearLastStage(); level++; StartCoroutine(AfterStageClearMenuReturn(0f)); ; });
    }

    private void ResetLevel() {
        Debug.Log("Reset maze building");
        gmScript.level=0;
    }

    private void BuildMazeDemo() {
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

    private void SwitchCamera() {
        if (mainCamera.gameObject.activeSelf) {
            mainCamera.gameObject.SetActive(false);
            mazeCamera.gameObject.SetActive(true);
        } else {
            mainCamera.gameObject.SetActive(true);
            mazeCamera.gameObject.SetActive(false);
        }
    }

}
