using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUIManager : MonoBehaviour {

    public GameObject stageClearMenu;
    public GameObject gameOverMenu;

    void Start () {
        stageClearMenu.SetActive(true);
        gameOverMenu.SetActive(true);
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

    public void GameOverMenuEnter() {
        gameOverMenu.GetComponent<Animator>().SetTrigger("StageMenuEnter");
    }

    public void LoadScene(string sceneName) {
        Globe.nextSceneName=sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}
