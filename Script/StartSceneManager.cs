using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour {

    public float animationTime = 5f;

	// Use this for initialization
	void Start () {
        StartCoroutine(AfterStart());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator AfterStart() {
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene("MainMenu");
    }
}
