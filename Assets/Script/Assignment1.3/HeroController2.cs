using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroController2 : MonoBehaviour {

	public GameObject AI_Manager;
	public LineRenderer lineRender_2;
	public int lengthOfLineRenderer = 2;
	public List<GameObject> Heros = new List<GameObject>();

	private GameObject[] enemy;
	private Vector3 Veloctiy;
	private Vector3 Acceleration;
	private Vector3 Steering;
	private float acceleration = 2.0f;
	private float max_Velocity = 10.0f;
	private LineRenderer lineRenderer;
	private LineRenderer lineRenderer2;

	private float weight_Alignment = 0.0f;
	private float weight_Cohesion = 0.0f;
	private float weight_Seperation = 0.0f;
	private float update_Timer = 0.0f;
	
	void Start () {
		enemy = GameObject.FindGameObjectsWithTag ("Enemy");

		//Veloctiy = new Vector3 (Random.Range (-50.0f, 0.0f), Random.Range (-50.0f, 50.0f), Random.Range (-50.0f, 0.0f));
		Veloctiy = new Vector3 (Random.Range (-50.0f, 0.0f), 0.0f, Random.Range (-50.0f, 0.0f));
		
		lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);

		lineRenderer2 = lineRender_2.GetComponent<LineRenderer>();
		lineRenderer2.SetWidth(0.2F, 0.2F);
		lineRenderer2.SetVertexCount(lengthOfLineRenderer);
	}
	
	void Update () {
		AI_Run ();
		Render ();

		if (!this.gameObject.renderer.isVisible) {
			//Debug.Log("Hehe");
			transform.position = new Vector3( transform.position.x * 0.9f, 0.0f, transform.position.z * 0.9f ); 
		}
	}

	Vector3 computeAlignment(){
		AI_Manager = GameObject.FindGameObjectWithTag ("AIManager");
		Heros = AI_Manager.GetComponent<AIManager3> ().Heros;
		Vector3 v = Veloctiy;

		int neighborCount = 0;
		Collider[] heros = Physics.OverlapSphere (transform.position, 10.0f);
		foreach (Collider hero in heros) {
			if( hero.gameObject.transform.tag == "Player" ){
				Vector3 other_velocity = hero.GetComponent<HeroController2>().Veloctiy;
				if( hero.gameObject != this.gameObject ){
					if( Vector3.Distance( this.transform.position, hero.transform.position ) < 20.0f ){
						
						v.x += other_velocity.x;
						v.y += other_velocity.y;
						v.z += other_velocity.z;
						neighborCount++;
					}
				}
			}
		}
		if (neighborCount == 0){
			return v;
		}
		v.x /= neighborCount;
		v.y /= neighborCount;
		v.z /= neighborCount;
		v.Normalize ();
		return v;
	}

	Vector3 computeCohesion(){
		AI_Manager = GameObject.FindGameObjectWithTag ("AIManager");
		Heros = AI_Manager.GetComponent<AIManager3> ().Heros;
		Vector3 v = Veloctiy;

		int neighborCount = 0;
		Collider[] heros = Physics.OverlapSphere (transform.position, 10.0f);
		foreach (Collider hero in heros) {
			if( hero.gameObject.transform.tag == "Player" ){
				Vector3 other_position = hero.transform.position;
				if( hero.gameObject != this.gameObject ){
					if( Vector3.Distance( this.transform.position, hero.transform.position ) < 20.0f ){
						v.x += other_position.x;
						v.y += other_position.y;
						v.z += other_position.z;
						neighborCount++;
					}
				}
			}
		}
		if (neighborCount == 0){
			return v;
		}
		v.x /= neighborCount;
		v.y /= neighborCount;
		v.z /= neighborCount;
		v = new Vector3 (v.x - transform.position.x, v.y - transform.position.y, v.z - transform.position.z);
		v.Normalize ();
		return v;
	}

	Vector3 computeSeparation(){
		AI_Manager = GameObject.FindGameObjectWithTag ("AIManager");
		Heros = AI_Manager.GetComponent<AIManager3> ().Heros;
		Vector3 v = Veloctiy;

		int neighborCount = 0;
		Collider[] heros = Physics.OverlapSphere (transform.position, 10.0f);
		foreach (Collider hero in heros) {
			if( hero.gameObject.transform.tag == "Player" ){
				Vector3 other_position = hero.transform.position;
				if( hero.gameObject != this.gameObject ){
					if( Vector3.Distance( this.transform.position, hero.transform.position ) < 20.0f ){
						v.x += other_position.x - transform.position.x;
						v.y += other_position.y - transform.position.y;
						v.z += other_position.z - transform.position.z;
						neighborCount++;
					}
				}
			}
		}

		if (neighborCount == 0){
			return v;
		}
		v.x /= neighborCount;
		v.y /= neighborCount;
		v.z /= neighborCount;
		v.x *= -1.0f;
		v.y *= -1.0f;
		v.z *= -1.0f;
		v.Normalize ();
		return v;
	}

	Vector3 FlockBehaivor(){
		//Debug.Log ("hehe________________");
		Vector3 alignment = computeAlignment();
		Vector3 cohesion = computeCohesion();
		Vector3 separation = computeSeparation();
		Vector3 v = Veloctiy;

		AI_Manager = GameObject.FindGameObjectWithTag ("AIManager");
		weight_Alignment = AI_Manager.GetComponent<AIManager3> ().weight_Alignment;
		weight_Cohesion = AI_Manager.GetComponent<AIManager3> ().weight_Cohesion;
		weight_Seperation = AI_Manager.GetComponent<AIManager3> ().weight_Seperation;

		v.x = alignment.x * weight_Alignment + cohesion.x * weight_Cohesion + separation.x * weight_Seperation;
		v.y = alignment.y * weight_Alignment + cohesion.y * weight_Cohesion + separation.y * weight_Seperation;
		v.z = alignment.z * weight_Alignment + cohesion.z * weight_Cohesion + separation.z * weight_Seperation;

		//Debug.Log (alignment.x * weight_Alignment + cohesion.x * weight_Cohesion + separation.x * weight_Seperation);

		return v;
	}


	
	void AI_Run(){
		Acceleration = Veloctiy.normalized * acceleration;

		for( int i = 0; i < enemy.Length; i++ ){
			if( Vector3.Distance( transform.position, enemy[i].transform.position ) < 20.0f ){
				Vector3 desired_Velocity = -enemy[i].transform.position + transform.position;
				desired_Velocity.Normalize ();
				desired_Velocity *= max_Velocity;
				
				Steering = desired_Velocity - Veloctiy;
				if (Steering.magnitude > max_Velocity) {
					float round = max_Velocity / Steering.magnitude;
					Steering *= round;
				}
				Acceleration += Steering;
			}
		}
		update_Timer += Time.deltaTime;
		if( update_Timer > 0.1f ){
			Steering = FlockBehaivor ();
			update_Timer = 0.0f;
		}
		Veloctiy += Acceleration * Time.deltaTime;
		Veloctiy += Steering;

		if (Veloctiy.magnitude > max_Velocity) {
			float round = max_Velocity / Veloctiy.magnitude;
			Veloctiy *= round;
		}
		transform.position += Veloctiy * Time.deltaTime;
	}
	
	void Render(){
		lineRenderer.SetPosition (0, this.transform.position);
		lineRenderer.SetPosition (1, this.transform.position + Veloctiy * 0.3f );

		lineRenderer2.SetPosition (0, transform.position);
		lineRenderer2.SetPosition (1,  transform.position + Steering.normalized * 3.0f );
	}
}
