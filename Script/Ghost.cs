using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour {

    public float moveSpeed;
    public Transform end;
    public Stack<Vector3> path = new Stack<Vector3>();
    public GameObject pathCube;
    //public List<Vector3> targetsList;
    public float delta = 0.001f;
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
        gmScript = GameObject.Find("GameManager").GetComponent<GameSceneManager>();
        //path = GetShortestPath(end.position);
        /*while (path.Count>0) {
            Instantiate(pathCube, path.Pop(), new Quaternion());
        }*/
        Debug.Log("Start Called");
    }

    /*protected void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("Move Up!");
            targetsList.Add(transform.position+new Vector3(0, 0, 1));
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Debug.Log("Move Down!");
            targetsList.Add(transform.position+new Vector3(0, 0, -1));
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Debug.Log("Move Left!");
            targetsList.Add(transform.position+new Vector3(-1, 0, 0));
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Debug.Log("Move Right!");
            targetsList.Add(transform.position+new Vector3(1, 0, 0));
        }
    }*/

    protected void FixedUpdate() {
        if (path.Count>0) {
            MoveToPosition(path.Peek());
        }
    }

    protected Stack<Vector3> GetShortestPath(Vector3 end) {
        int[] deltaX = { 0, 0, -1, 1 };
        int[] deltaZ = { -1, 1, 0, 0 };         //down up left right
        Stack<Vector3> tempPath = new Stack<Vector3>();
        //GameSceneManager gmScript = GameObject.Find("GameManager").GetComponent<GameSceneManager>();
        bool[,] visited = new bool[gmScript.mazeWidth, gmScript.mazeHeight];
        Queue<MazeNode> queue = new Queue<MazeNode>();
        MazeNode start = new MazeNode(this.transform.position, null);
        queue.Enqueue(start);
        visited[(int)start.pos.x, (int)start.pos.z]=true;
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
                        return tempPath;
                    }
                    queue.Enqueue(new MazeNode(new Vector3(xPos, 0, zPos), p));
                    visited[xPos, zPos]=true;
                }
            }
        }
        return null;
    }

    protected void MoveToPosition(Vector3 position) {
        transform.LookAt(position);
        bool xPositionReady = Mathf.Abs(this.transform.position.x-position.x)<delta;
        bool zPositionReady = Mathf.Abs(this.transform.position.z-position.z)<delta;
        if (xPositionReady&&zPositionReady) {          //到到指定位置
            this.transform.position=position;
            path.Pop();
            //transform.position=new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
            Debug.Log("Arrive!");
        } else {
            transform.Translate(Vector3.forward*moveSpeed);
            Debug.Log("Not Arrive!");
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag =="Test") {
            Destroy(other.gameObject);
        }
    }
}
