using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameSceneUIManager : MonoBehaviour {

    public GameObject stageClearMenu;
    public Button nextStageButton;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StageMenuEnter() {
        stageClearMenu.GetComponent<Animator>().SetTrigger("StageMenuEnter");
    }

    public void StageMenuReturn() {
        stageClearMenu.GetComponent<Animator>().SetTrigger("StageMenuReturn");
    }
}
