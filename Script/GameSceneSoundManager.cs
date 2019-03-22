using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneSoundManager : MonoBehaviour {

    public AudioSource backgroundAudioSource;
    public List<AudioClip> bgmAudioList;
    public Text muteText;
    public AudioClip stageClearAudio;
    public AudioClip gameOverAudio;

    public AudioSource deathAudioSource;
    public AudioSource singleAudioSource;
    public AudioSource boostAudioSource;

    private int currentBGMIndex;

	void Start () {
        currentBGMIndex=-1;
	}
	
	// Update is called once per frame
	void Update () {
        if (backgroundAudioSource.isPlaying==false) {
            PlayRandomBGMClip();
        }
	}

    public void PlayDeathAudio() {
        deathAudioSource.Play();
    }

    public void PlaySingleAudio() {
        singleAudioSource.Play();
    }

    public void EnableBoostAudio() {
        boostAudioSource.enabled=true;
    }

    public void DisableBoostAudio() {
        boostAudioSource.enabled=false;
    }

    public void PlayGameOverAudio() {
        backgroundAudioSource.clip=gameOverAudio;
        backgroundAudioSource.Play();
        StartCoroutine(DisableBGMAfterSeconds(gameOverAudio.length));
    }

    public void PlayStageClearAudio() {
        backgroundAudioSource.clip=stageClearAudio;
        backgroundAudioSource.Play();
        StartCoroutine(DisableBGMAfterSeconds(stageClearAudio.length));
    }

    IEnumerator DisableBGMAfterSeconds(float second) {
        yield return new WaitForSeconds(second);
        backgroundAudioSource.enabled=false;
    }

    public void ChangeBgmMuteState() {
        if (backgroundAudioSource.mute==true) {
            backgroundAudioSource.mute=false;
            muteText.text="Mute";
        } else {
            backgroundAudioSource.mute=true;
            muteText.text="Unmute";
        }
    }

    private void PlayRandomBGMClip() {
        int bgmIndex = Random.Range(0, bgmAudioList.Count);
        while (bgmIndex==currentBGMIndex) {
            bgmIndex=Random.Range(0, bgmAudioList.Count);
        }
        backgroundAudioSource.clip=bgmAudioList[bgmIndex];
        backgroundAudioSource.Play();
    }

    public IEnumerator RestartBGMAfterSeconds(float second) {
        yield return new WaitForSeconds(second);
        backgroundAudioSource.enabled=true;
    }
}
