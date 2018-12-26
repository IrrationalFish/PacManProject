using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Globe {
    public static string nextSceneName;
}

public class AsyncLoadScene : MonoBehaviour {

    public Slider loadingSlider;
    public Text loadingText;

    private float loadingSpeed = 1;
    private float targetValue;
    private AsyncOperation operation;

    void Start() {
        loadingSlider.value = 0.0f;
        if (SceneManager.GetActiveScene().name == "LoadingScene") {      //启动协程
            StartCoroutine(AsyncLoading());
        }
    }

    void Update () {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f) {       //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }

        if (targetValue != loadingSlider.value) {
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);      //插值运算,在a，b间插c
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f) {
                loadingSlider.value = targetValue;
            }
        }

        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(loadingSlider.value * 100) == 100) {
            operation.allowSceneActivation = true;      //允许异步加载完毕后自动切换场景
            //MyDelegate.OnSceneEvent(Globe.nextSceneName, 5);
        }
	}

    IEnumerator AsyncLoading() {
        operation = SceneManager.LoadSceneAsync(Globe.nextSceneName);       //阻止当加载完成自动切换
        operation.allowSceneActivation = false;
        yield return operation;
    }
}
