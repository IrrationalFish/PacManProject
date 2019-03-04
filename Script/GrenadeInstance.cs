using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeInstance : MonoBehaviour {

    public float movePower;
    public float moveSpeed;
    public float rotateSpeed;

    private bool isMoving = false;

    void Start () {
        if (Physics.Linecast(this.transform.position-new Vector3(0, 0.75f, 0), this.transform.position-new Vector3(0, 0.75f, 0)+this.transform.forward)) {
            this.GetComponent<Rigidbody>().AddForce((this.transform.forward+this.transform.up)*movePower);
        } else {
            Debug.Log("Grenade No Wall");
            this.transform.position=this.transform.position+new Vector3(0, -0.75f, 0);
            isMoving=true;
        }
    }
	
	void Update () {
        if(isMoving ==true) {
            this.GetComponent<Rigidbody>().velocity=this.transform.forward*moveSpeed;
            Debug.Log(this.transform.forward);
        }
    }

    private void FixedUpdate() {
        GetComponentsInChildren<Transform>()[1].Rotate(new Vector3(0, 0, 1)*rotateSpeed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Ghost") {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag =="Wall") {
            //GameSceneManager gm = GameObject.Find("GameManager").GetComponent<GameSceneManager>();
            //gm.GmBreakWall(collision.gameObject.GetComponent<Cube>().x, collision.gameObject.GetComponent<Cube>().y);
            Destroy(gameObject);
        }
    }
}
