using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManMovement : MonoBehaviour {

    public float moveSpeed = 0.4f;
    public float step = 1.0f;

    Vector2 dest = Vector2.zero;
    private float colliderRadius;

    void Start() {
        dest = transform.position;
        colliderRadius = GetComponent<CircleCollider2D>().radius-0.1f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 p = Vector2.MoveTowards(transform.position, dest, moveSpeed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if ((Vector2)transform.position == dest) {
            if (Input.GetKey(KeyCode.UpArrow) && Valid(Vector2.up)) {
                dest = (Vector2)transform.position + Vector2.Scale(Vector2.up, new Vector2(step, step));
            }
            if (Input.GetKey(KeyCode.RightArrow) && Valid(Vector2.right)) {
                dest = (Vector2)transform.position + Vector2.Scale(Vector2.right, new Vector2(step, step));
            }
            if (Input.GetKey(KeyCode.DownArrow) && Valid(-Vector2.up)) {
                dest = (Vector2)transform.position - Vector2.Scale(Vector2.up, new Vector2(step, step));
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Valid(-Vector2.right)) {
                dest = (Vector2)transform.position - Vector2.Scale(Vector2.right, new Vector2(step, step));
            }
        }

        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool Valid(Vector2 dir) {
        RaycastHit2D hit1;
        RaycastHit2D hit2;
        RaycastHit2D hit3;
        Vector2 pos = transform.position;
        if (dir == Vector2.right) {
            hit1 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) + new Vector2(0, colliderRadius), pos + new Vector2(0, colliderRadius));
            hit2 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)), pos);
            hit3 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) - new Vector2(0, colliderRadius), pos - new Vector2(0, colliderRadius));
        } else if (dir == Vector2.left) {
            hit1 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) + new Vector2(0, colliderRadius), pos + new Vector2(0, colliderRadius));
            hit2 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)), pos);
            hit3 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) - new Vector2(0, colliderRadius), pos - new Vector2(0, colliderRadius));
        } else if (dir == Vector2.up) {
            hit1 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) + new Vector2(colliderRadius, 0), pos + new Vector2(colliderRadius, 0));
            hit2 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)), pos);
            hit3 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) - new Vector2(colliderRadius, 0), pos - new Vector2(colliderRadius, 0));
        } else {
            hit1 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) + new Vector2(colliderRadius, 0), pos + new Vector2(colliderRadius, 0));
            hit2 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)), pos);
            hit3 = Physics2D.Linecast(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) - new Vector2(colliderRadius, 0), pos - new Vector2(colliderRadius, 0));
        }

        Debug.DrawLine(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) + new Vector2(0, colliderRadius), pos + new Vector2(0, colliderRadius));
        Debug.DrawLine(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) + new Vector2(colliderRadius, 0), pos + new Vector2(colliderRadius, 0));
        Debug.DrawLine(pos + dir + Vector2.Scale(dir, new Vector2(0.1f, 0.1f)) - new Vector2(colliderRadius, 0), pos - new Vector2(colliderRadius, 0));

        Debug.Log(hit1.collider.name);
        Debug.Log("1:"+ (hit1.collider == GetComponent<Collider2D>()) + "2:" + (hit2.collider == GetComponent<Collider2D>()) + "3:" + (hit3.collider == GetComponent<Collider2D>()));

        //return (hit1.collider == GetComponent<Collider2D>() && hit2.collider == GetComponent<Collider2D>() && hit3.collider == GetComponent<Collider2D>());
        return (!hit1.collider.tag.Equals("Wall") && !hit3.collider.tag.Equals("Wall") && !hit3.collider.tag.Equals("Wall"));
    }
}
