using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    void Start() {
        //MyDelegate.SceneEvent += PrintScene;
    }

    void Update() {

    }

    public void LoadScene(string sceneName) {
        Globe.nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

}
