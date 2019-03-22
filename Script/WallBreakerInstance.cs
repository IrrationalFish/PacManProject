using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreakerInstance : MonoBehaviour {

    public float rotateSpeed;
    GameSceneManager gmScript;

	void Start () {
        gmScript=GameObject.Find("GameManager").GetComponent<GameSceneManager>();
        gmScript.soundManager.EnableBreakerAudio();
	}

    private void FixedUpdate() {
        transform.Rotate(new Vector3(0, 0, 1)*rotateSpeed);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag =="Ghost") {
            Destroy(other.gameObject);
            Debug.Log("WallBreaker Hit Ghost Trigger");
            Destroy(this.gameObject);
            gmScript.GetPlayer().GetComponent<Player>().isUsingItem=false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("WallBreaker Collide!");
        if (other.gameObject.tag=="Wall") {
            if (!other.gameObject.GetComponent<Cube>().isBoundary) {
                Cube wall = other.gameObject.GetComponent<Cube>();
                gmScript.soundManager.PlayDeathAudio();
                gmScript.GmBreakWall(wall.x, wall.y);
                Destroy(this.gameObject);
                gmScript.GetPlayer().GetComponent<Player>().isUsingItem=false;
            } else {
                ItemGenerator gm = gmScript.gameObject.GetComponent<ItemGenerator>();
                gm.GenerateWallBreakerObject(Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z));
                gmScript.GetPlayer().GetComponent<Player>().isUsingItem=false;
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy() {
        gmScript.soundManager.DisableBreakerAudio();
    }
}
