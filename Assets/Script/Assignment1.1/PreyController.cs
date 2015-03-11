using UnityEngine;
using System.Collections;

public class PreyController : MonoBehaviour {

	public GameObject Target;
	public LineRenderer lineRender_2;
	public enum AIType{
		Basic_Coordinates = 1,
		Basic_Speed       = 2,
		
	}
	public AIType ai_type;
	public float max_Velocity = 10.0f;
	public int lengthOfLineRenderer = 2;

	private Vector3 Veloctiy;
	private Vector3 Accerlation;
	private Vector3 Steering;

	void Start () {
		Veloctiy = new Vector3 (Random.Range (-10.0f, 10.0f), 0.0f, Random.Range (-10.0f, 10.0f));

		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);

		LineRenderer lineRenderer2 = lineRender_2.GetComponent<LineRenderer>();
		lineRenderer2.SetWidth(0.2F, 0.2F);
		lineRenderer2.SetVertexCount(lengthOfLineRenderer);
	}

	void Update () {
		AI_Run ();
		Render ();

		Debug.DrawLine (transform.position, this.transform.position + Veloctiy.normalized * 3.0f, Color.red);
		Debug.DrawLine (transform.position, this.transform.position - Steering.normalized * 3.0f, Color.blue);
	}
	
	void AI_Run(){
		switch (ai_type) {
			case AIType.Basic_Coordinates:{
				AI_Basic_Coordinates();
				break;
			}
			case AIType.Basic_Speed:{
				AI_Basic_Speed();
				break;
			}
		}
	}
	/* 
	For the AI_Basic_Coordinates methos, although it is simple, it works, expecially for the tile-based game. 
	In tile-based gaems the game domain is divided into discrtet tiles - squares, hexagons, etc. And the player's
	position is fixed to a discrete tile. In a continuous environment, position is represented by floating-point coordinates,
	which can represent any location 
	 */

	void AI_Basic_Coordinates(){
		if (transform.position.x > Target.transform.position.x) {
			transform.position += new Vector3( 0.1f, 0.0f, 0.0f ) * 0.5f;
		}
		else if( transform.position.x < Target.transform.position.x ){
			transform.position -= new Vector3( 0.1f, 0.0f, 0.0f ) * 0.5f;
		}
		else{}
		
		if (transform.position.z > Target.transform.position.z) {
			transform.position += new Vector3( 0.0f, 0.0f, 0.1f ) * 0.5f;
		}
		else if( transform.position.z < Target.transform.position.z ){
			transform.position -= new Vector3( 0.0f, 0.0f, 0.1f ) * 0.5f;
		}
		else{}
	}

	/*
	 * For the seek behavior, the addition of steering forces to the character 
	 * every frame makes it smoothly adjust its velocity, avoiding sudden route changes. 
	 * If the target moves, the character will gradually change its velocity vector, 
	 * trying to reach the target at its new location.
	 * The seek behavior involves two forces: desired velocity and steering
	 */
	void AI_Basic_Speed(){
		Vector3 desired_Velocity = - Target.transform.position + transform.position;
		desired_Velocity.Normalize ();
		desired_Velocity *= max_Velocity;

		Steering = desired_Velocity - Veloctiy;
		if (Steering.magnitude > max_Velocity) {
			float round = max_Velocity / Steering.magnitude;
			Steering *= round;
		}

		Veloctiy += Steering * Time.deltaTime;
		if (Veloctiy.magnitude > max_Velocity) {
			float round = max_Velocity / Veloctiy.magnitude;
			Veloctiy *= round;
		}

		transform.position += Veloctiy * Time.deltaTime;
	}

	void Render(){
		this.GetComponent<LineRenderer> ().SetPosition (0, this.transform.position);
		this.GetComponent<LineRenderer> ().SetPosition (1, this.transform.position + Steering.normalized * 3.0f );

		lineRender_2.GetComponent<LineRenderer> ().SetPosition (0, this.transform.position);
		lineRender_2.GetComponent<LineRenderer> ().SetPosition (1, this.transform.position + Veloctiy * 1.0f );
	}
}
