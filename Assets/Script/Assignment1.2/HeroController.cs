using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroController : MonoBehaviour {
	
	public LineRenderer lineRender_2;
	public int lengthOfLineRenderer = 2;
	public GameObject[] obstaclesArray;

	private Transform Obstacle;
	private Vector3 avoidance_Force;
	private Vector3 Veloctiy;
    private Vector3 Acceleration;
	private Vector3 Steering;
	private Vector3 Ahead;
	private Vector3 Ahead_Half;
    private float acceleration = 2.0f;
	private float max_Velocity = 10.0f;
	private float MAX_AVOID_FORCE = 10.0f;
	private float MAX_SEE_AHEAD = 7.0f;
	private float Obstacle_Radius = 1.0f;
	private float Obstacle_Timer = 0;
	private LineRenderer lineRenderer;
	private LineRenderer lineRenderer2;

	void Start () {
		Veloctiy = new Vector3 (Random.Range (-50.0f, 50.0f), 0.0f, Random.Range (-50.0f, 50.0f));

		lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);

		lineRenderer2 = lineRender_2.GetComponent<LineRenderer>();
		lineRenderer2.SetWidth(0.2F, 0.2F);
		lineRenderer2.SetVertexCount(lengthOfLineRenderer);
	}

	void Update () {
		FindObstacles ();
		AI_Run ();
		Render ();
	}

	void FindObstacles(){
		if( Obstacle_Timer == 0 ){
			obstaclesArray = GameObject.FindGameObjectsWithTag("Obstacle");

			Obstacle_Timer++;
		}
	}

	void AI_Run(){
		Acceleration = Veloctiy.normalized * acceleration;

		float dynamic_v = Veloctiy.magnitude / max_Velocity;
		Ahead = transform.position + Veloctiy.normalized * MAX_SEE_AHEAD * dynamic_v;
		Ahead_Half = transform.position + Veloctiy.normalized * MAX_SEE_AHEAD * dynamic_v * 0.5f;

		findMostThreateningObstacle ();
		Vector3 avoidance = new Vector3 (0.0f, 0.0f, 0.0f);
		if (Obstacle != null) {
			avoidance.x = Ahead.x - Obstacle.transform.position.x;
			avoidance.z = Ahead.z - Obstacle.transform.position.z;

			avoidance.Normalize();
			avoidance *= MAX_AVOID_FORCE;
		}
		else{
			avoidance = new Vector3(0.0f, 0.0f, 0.0f );
		}
		if (avoidance.x == 0.0f && avoidance.z == 0.0f) {
			Steering = new Vector3( 0.0f, 0.0f, 0.0f);
		}

		Steering += avoidance;
		if (Steering.magnitude > MAX_AVOID_FORCE ) {
			float round = MAX_AVOID_FORCE / Steering.magnitude;
			Steering *= round;
		}

		Acceleration += Steering;
		Veloctiy += Acceleration * Time.deltaTime;
		if (Veloctiy.magnitude > max_Velocity) {
			float round = max_Velocity / Veloctiy.magnitude;
			Veloctiy *= round;
		}
		
		transform.position += Veloctiy * Time.deltaTime;
	}

	void findMostThreateningObstacle(){
		if( Obstacle != null ){
			Obstacle.transform.renderer.material.color = Color.white;
		}
		Obstacle = null;

		for (int i = 0; i < obstaclesArray.Length; i++ ){
			GameObject temp_Obstacle = obstaclesArray[i];
			bool intersect = LineIntersectsCircle( temp_Obstacle );

			if( intersect && ( Obstacle == null || 
			    Vector3.Distance( transform.position, temp_Obstacle.transform.position ) < 
			    Vector3.Distance( transform.position, Obstacle.transform.position ))){
				Obstacle = temp_Obstacle.transform;
			}

		}

		if( Obstacle != null ){
			Obstacle.transform.renderer.material.color = Color.green;
		}
	}

	bool LineIntersectsCircle( GameObject i_obstacle ){
		Obstacle_Radius = i_obstacle.transform.GetComponent<ObstacleTrigger>().radius ;
		return (Vector3.Distance( i_obstacle.transform.position, Ahead ) <= Obstacle_Radius || Vector3.Distance( i_obstacle.transform.position, Ahead_Half ) <= Obstacle_Radius );
	}

	void Render(){
		lineRenderer.SetPosition (0, this.transform.position);
		//lineRenderer.SetPosition (1, this.transform.position + Veloctiy * 0.3f );
		lineRenderer.SetPosition (1, Ahead );
		
		lineRenderer2.SetPosition (0, transform.position);
		lineRenderer2.SetPosition (1,  transform.position + Steering.normalized * 3.0f );
	}
}
