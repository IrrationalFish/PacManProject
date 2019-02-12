using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeInstance : MonoBehaviour {

    public float movePower;
    public float moveSpeed;

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
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag =="Ghost") {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
