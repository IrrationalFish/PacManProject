using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager : MonoBehaviour {

    void Start() {
        //GetComponent<MazeGenPrim>().Generate();
        DontDestroyOnLoad(gameObject);
        MyDelegate.SceneEvent += PrintScene;
    }

    // Update is called once per frame
    void Update() {

    }

    public void LoadScene(string sceneName) {
        Globe.nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    public void PrintScene(string sceneName, int n) {
        Debug.Log(sceneName + "" + n);
    }
}
