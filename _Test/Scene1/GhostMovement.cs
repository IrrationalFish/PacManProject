using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour {

    public Transform[] wayPoints;
    public float speed = 0.3f;

    private int currentWayPoint;

    void FixedUpdate() {
        if (transform.position != wayPoints[currentWayPoint].position) {
            Vector2 p = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        } else {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        }

        Vector2 dir = wayPoints[currentWayPoint].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("PacMan")) {
            Destroy(collision.gameObject);
        }
    }
}
