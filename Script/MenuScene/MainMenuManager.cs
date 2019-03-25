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

    public void OpenMusicLicenseWeb() {
        Application.OpenURL("https://www.jamendo.com/legal/creative-commons");
    }

    public void ExitGame() {
        Debug.Log("ExitGame");
        Application.Quit();
    }

}
