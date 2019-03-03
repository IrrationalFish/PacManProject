using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour {

    public float moveSpeed;
    //public Transform end;
    public Stack<Vector3> path = new Stack<Vector3>();
    public float delta = 0.001f;

    //public GameObject pathCube;
    public GameObject ghostModel;
    public GameObject leftEye;
    public GameObject rightEye;

    public static GameSceneManager gmScript;

    private class MazeNode {
        public Vector3 pos;
        public MazeNode parentNode;

        public MazeNode(Vector3 posPar, MazeNode parentNodePar) {
            this.pos=posPar;
            this.parentNode=parentNodePar;
        }
    }

    protected void Start() {
        if(gmScript ==null) {
            gmScript=GameObject.Find("GameManager").GetComponent<GameSceneManager>();

        }
        InitialiseGhost();
    }

    protected void Update() {
        SetModelAndEye();
        if (path.Count<=0) {
            Vector3 nextEnd = GetNextEnd();
            path=GetShortestPath(this.transform.position,nextEnd);
        }
    }

    protected void FixedUpdate() {
        if (path.Count>0) {
            MoveToPosition(path.Peek());
        }
    }

    abstract protected void InitialiseGhost();
    abstract protected Vector3 GetNextEnd();

    protected Stack<Vector3> GetShortestPath(Vector3 start, Vector3 end) {
        int[] deltaX = { 0, 0, -1, 1 };
        int[] deltaZ = { -1, 1, 0, 0 };         //down up left right
        Stack<Vector3> tempPath = new Stack<Vector3>();
        if(end.Equals(start)) {
            tempPath.Push(new Vector3(end.x, 0, end.z));
            this.GetComponent<Animator>().SetBool("isMoving", false);
            Debug.Log("Change to notMoving");
            return tempPath;
        }
        bool[,] visited = new bool[gmScript.mazeWidth, gmScript.mazeHeight];
        Queue<MazeNode> queue = new Queue<MazeNode>();
        MazeNode startNode = new MazeNode(start, null);
        queue.Enqueue(startNode);
        visited[(int)startNode.pos.x, (int)startNode.pos.z]=true;
        while (queue.Count>0) {
            MazeNode p = queue.Dequeue();
            for (int i = 0; i<4; i++) {
                int xPos = (int)p.pos.x+deltaX[i];
                int zPos = (int)p.pos.z+deltaZ[i];
                if (xPos<0||zPos<0||xPos>gmScript.mazeWidth-1||zPos>gmScript.mazeHeight-1) {
                    continue;
                }
                if(!visited[xPos,zPos] && !gmScript.MazeCubeIsBlocked(xPos,zPos)){
                    if(xPos == end.x && zPos==end.z) {
                        tempPath.Push(new Vector3(end.x,0,end.z));
                        MazeNode pathNode = p;
                        while (pathNode.parentNode!=null) {
                            tempPath.Push(pathNode.pos);
                            pathNode=pathNode.parentNode;
                        }
                        this.GetComponent<Animator>().SetBool("isMoving", true);
                        return tempPath;
                    }
                    queue.Enqueue(new MazeNode(new Vector3(xPos, 0, zPos), p));
                    visited[xPos, zPos]=true;
                }
            }
        }
        this.GetComponent<Animator>().SetBool("isMoving", true);
        return tempPath;
    }

    protected void MoveToPosition(Vector3 position) {
        transform.LookAt(position);
        bool xPositionReady = Mathf.Abs(this.transform.position.x-position.x)<delta;
        bool zPositionReady = Mathf.Abs(this.transform.position.z-position.z)<delta;
        if (xPositionReady&&zPositionReady) {          //到到指定位置
            this.transform.position=position;
            path.Pop();
            //Debug.Log("Arrive!");
        } else {
            transform.Translate(Vector3.forward*moveSpeed);
            //Debug.Log("Not Arrive!");
        }
    }

    protected void OnTriggerEnter(Collider other) {
        if(other.tag =="Test") {
            Destroy(other.gameObject);
            Debug.Log("Meet Test");
        }else if(other.tag =="Player") {
            StartCoroutine(MeetPlayer(other.gameObject,3f));
            Debug.Log("Meet Player");
        }
    }

    IEnumerator MeetPlayer(GameObject player, float second) {
        GameObject particle = player.GetComponent<Player>().PlayDeathParticleSystem();
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(second);
        player.gameObject.SetActive(true);
        player.transform.SetPositionAndRotation(new Vector3(1, 0, 1), Quaternion.Euler(0, 0, 0));
        player.GetComponent<PlayerMovement>().startMovement=false;
        Destroy(particle);
    }

    protected void SetModelAndEye() {
        ghostModel.transform.rotation=Quaternion.Euler(25, 0, 0);

        if (transform.forward == new Vector3(0, 0, 1)) {     //up
            leftEye.transform.rotation=Quaternion.Euler(0, 0, 180);
            rightEye.transform.rotation=Quaternion.Euler(0, 0, 180);
        } else if(transform.forward==new Vector3(0, 0, -1)) {     //down
            leftEye.transform.rotation=Quaternion.Euler(0, 0, 0);
            rightEye.transform.rotation=Quaternion.Euler(0, 0, 0);
        } else if (transform.forward==new Vector3(1, 0, 0)) {   //right
            leftEye.transform.rotation=Quaternion.Euler(0, 0, 90);
            rightEye.transform.rotation=Quaternion.Euler(0, 0, 90);
        } else if (transform.forward==new Vector3(-1, 0, 0)) {  //left
            leftEye.transform.rotation=Quaternion.Euler(0, 0, -90);
            rightEye.transform.rotation=Quaternion.Euler(0, 0, -90);
        }
    }
}
