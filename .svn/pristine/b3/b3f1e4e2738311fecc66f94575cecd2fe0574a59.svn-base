using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	private Transform startPos, endPos;
	public Node startNode { get; set; }
	public Node goalNode { get; set; }
	
	public ArrayList pathArray;
	
	GameObject objStartCube, objEndCube;
	private float elapsedTime = 0.0f;

	public GameObject gridManager;
	public Transform Prefab_Grid;
	public Transform Prefab_OpenList; 
	public Transform Prefab_Begin;
	public Transform Prefab_End;
	public Transform Prefab_Path;
	public Transform Prefab_Obstacle;
	public float intervalTime = 1.0f;
	public int lengthOfLineRenderer = 50;
	public List<Transform> list = new List<Transform>();
	public List<Transform> open_list = new List<Transform>();

	private Camera camera;
	private GridManager g_manager;
	private LineRenderer lineRenderer;
	private Transform path;
	private Transform open_node;

	void Awake(){
		Vector3 initlizePosition_Begin = new Vector3( Random.Range( 12.0f, 20.0f ), 0.0f, Random.Range( 12.0f, 20.0f ) );
		Vector3 initlizePosition_End = new Vector3( Random.Range( 24.0f, 30.0f ), 0.0f, Random.Range( 24.0f, 30.0f ) );
		Instantiate (Prefab_Begin, initlizePosition_Begin, Quaternion.identity);
		Instantiate (Prefab_End, initlizePosition_End, Quaternion.identity);		
	}

	void Start () {
		g_manager = gridManager.transform.GetComponent<GridManager> ();
		objStartCube = GameObject.FindGameObjectWithTag("Start");
		objEndCube = GameObject.FindGameObjectWithTag("End");
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();

		lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);

		pathArray = new ArrayList();
		FindPath();
		Draw();
		DebugDrawGrid (Vector3.zero, 20, 20);
	}
	
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= intervalTime) {

			elapsedTime = 0.0f;
			FindPath();

		}
		AddObstacle ();
	}

	void AddObstacle(){
		if (Input.GetButtonDown("Fire1")) {
			Vector3 mousePos = Input.mousePosition;
			Vector3 mousePos_W = camera.ScreenToWorldPoint(mousePos);
			mousePos_W = new Vector3( mousePos_W.x, 0.0f, mousePos_W.z );
			Instantiate(Prefab_Obstacle, mousePos_W, transform.rotation);
				
			g_manager.obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
			g_manager.CalculateObstacles();
			FindPath();

			foreach(Transform path_node in list){
				Debug.Log("Destroy");
				Destroy(path_node.gameObject);
			}
			foreach(Transform open_node in open_list){
				Debug.Log("Destroy");
				Destroy(open_node.gameObject);
			}
			list.Clear();
			open_list.Clear();
			Draw();
		}
	}

	void FindPath() {
		startPos = objStartCube.transform;
		endPos = objEndCube.transform;
		
		startNode = new Node(GridManager.instance.GetGridCellCenter(
			GridManager.instance.GetGridIndex(startPos.position)));
		
		goalNode = new Node(GridManager.instance.GetGridCellCenter(
			GridManager.instance.GetGridIndex(endPos.position)));
		
		pathArray = AStar.FindPath(startNode, goalNode);
	}

	void DebugDrawGrid(Vector3 origin, int numRows, int numCols) {

		for (int i = 0; i < numRows + 1; i++) {
			for (int j = 0; j < numCols + 1; j++) {
				Vector3 startPos = origin + i * 1 * new Vector3(0.0f, 0.0f, 2.0f)+ j * 1 * new Vector3(2.0f, 0.0f, 0.0f) + new Vector3( 1.0f, 0.0f, 1.0f );
				Instantiate (Prefab_Grid, startPos, Quaternion.identity);
			}
		}
		
		// Draw the vertial grid lines
	}

	void Draw(){
		//Debug.Log ("Draw");
		if (pathArray == null)
			return;
		
		if (pathArray.Count > 0) {
			//return;
			int index = 1;
			foreach (Node node in pathArray) {
				if (index < pathArray.Count) {
					Node nextNode = (Node)pathArray[index];
					Vector3 path_position = node.position + new Vector3(0.0f,0.0f,0.0f);
					path = (Transform)Instantiate (Prefab_Path, path_position, Quaternion.identity);
					list.Add(path);
					index++;
				}
			}
		}

		if (AStar.openList.Length > 0) {
			//return;
			int index = 1;
			for( int i = 0; i < AStar.openList.Length; i ++ ){
				Vector3 path_position = AStar.openList.Get(i).position + new Vector3(0.0f,0.0f,0.0f);
				open_node = (Transform)Instantiate (Prefab_OpenList, path_position, Quaternion.identity);
				open_list.Add(open_node);
			}
		}
	}

	void OnDrawGizmos() {
		if (pathArray == null)
			return;
		
		if (pathArray.Count > 0) {
			//return;
			int index = 1;
			foreach (Node node in pathArray) {
				if (index < pathArray.Count) {
					Node nextNode = (Node)pathArray[index];
					Debug.DrawLine(node.position, nextNode.position,
					               Color.green);
					index++;
				}
			}
		}
	}

	void OnGUI () {
		GUI.Label(new Rect( 1000, 100, 100, 30), AStar.error );
	}
}