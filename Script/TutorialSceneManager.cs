using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialSceneManager : MonoBehaviour {

    public Animator playerSectionAnimator;
    public Animator itemSection1Animator;
    public Animator itemSection2Animator;
    public Animator ghostSectionAnimator;
    public int currentStage = 0;

    private void Start() {
        currentStage=0;
        playerSectionAnimator.SetTrigger("SectionEnter");
    }

    public void LoadScene(string sceneName) {
        Globe.nextSceneName=sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    public void NextButtonOnClick() {
        Debug.Log("NextButton!");
        if (currentStage==0) {
            playerSectionAnimator.SetTrigger("SectionExit");
            itemSection1Animator.SetTrigger("SectionEnter");
            currentStage=1;
        } else if (currentStage==1) {      //i1 exit, i2 enter, stage++
            itemSection1Animator.SetTrigger("SectionExit");
            itemSection2Animator.SetTrigger("SectionEnter");
            currentStage=2;
        }else if (currentStage==2) {
            itemSection2Animator.SetTrigger("SectionExit");
            ghostSectionAnimator.SetTrigger("SectionEnter");
            currentStage=3;
        }else if(currentStage==3) {
            ghostSectionAnimator.SetTrigger("SectionExit");
            playerSectionAnimator.SetTrigger("SectionEnter");
            currentStage=0;
        }
    }

    /*public void PrevButtonOnClick() {
        Debug.Log("PrevButton!");
        if (currentStage==1) {
            itemSection1Animator.SetTrigger("SectionExit");
            itemSection2Animator.SetTrigger("SectionEnter");
            currentStage=2;
        } else if (currentStage==2) {
            itemSection2Animator.SetTrigger("SectionExit");
            ghostSectionAnimator.SetTrigger("SectionEnter");
            currentStage=3;
        } else if (currentStage==3) {
            ghostSectionAnimator.SetTrigger("SectionExit");
            itemSection1Animator.SetTrigger("SectionEnter");
            currentStage=1;
        }
    }*/
}
