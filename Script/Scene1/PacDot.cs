using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Enter");
        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
        if (collision.name == "PacMan") {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision) {
    //    Debug.Log("CollisionEnter");
    //    if (collision.gameObject.CompareTag("Wall")) {
    //        Destroy(gameObject);
    //    }
    //}
}
